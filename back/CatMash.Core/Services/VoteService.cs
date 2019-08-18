using CatMash.Core.Dto;
using CatMash.Core.Entities;
using CatMash.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Core.Services
{
    public class VoteService : IVoteService
    {
        private readonly IBaseRepository<Cat> _catRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRatingService _ratingService;

        public VoteService(
            IBaseRepository<Cat> catRepository,
            IVoteRepository voteRepository,
            IRatingService ratingService,
            IUnitOfWork unitOfWork)
        {
            _catRepository = catRepository;
            _voteRepository = voteRepository;
            _unitOfWork = unitOfWork;
            _ratingService = ratingService;
        }

        /// <summary>
        /// Returns a random weighted vote based on occurence :
        /// Votes are weighted so votes that have not occured often have more chances to be selected
        /// </summary>
        /// <returns></returns>
        public async Task<Vote> GetNextVote()
        {
            var votes = await _voteRepository.GetAll("VoteCats.Cat");
            var totalOccurence = votes.Sum(x => x.Occurence);

            var weightedVotes = new List<WeightedVote>();
            var previousWeightedSum = 0;
            foreach (var vote in votes)
            {
                var weightedVote = new WeightedVote
                {
                    Id = vote.Id,
                    Weight = totalOccurence == 0 ? 1 : (totalOccurence - vote.Occurence) / totalOccurence
                };

                weightedVote.WeightedSum = previousWeightedSum + weightedVote.Weight;
                weightedVotes.Add(weightedVote);
                previousWeightedSum = weightedVote.WeightedSum;
            }

            
            var rand = new Random().Next(0, previousWeightedSum);
            var randomVote = weightedVotes.OrderBy(x => x.WeightedSum).First(x => x.WeightedSum > rand);

            var selectedVote = votes.FirstOrDefault(x => x.Id == randomVote.Id);
            return selectedVote;
        }

        /// <summary>
        /// Finds the vote between the two cats and add 1 occurence,
        /// Then computes new rating for the cats based on result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task HandleVote(VoteResult result)
        {
            // Find the vote between the two cats, and add 1 occurence
            var vote = await _voteRepository.FindByOpponents(result.Cat1Id, result.Cat2Id);
            if (vote == null || vote.VoteCats == null)
            {
                throw new Exception("A vote should exist between every cat. Error ocurred during populating the table Cat.");
            }
            vote.Occurence++;
            await _voteRepository.Update(vote);

            // Compute the new rating of the two cats
            var cat1 = vote.VoteCats.Single(x => x.CatId == result.Cat1Id).Cat;
            var cat2 = vote.VoteCats.Single(x => x.CatId == result.Cat2Id).Cat;

            var currentRatings = new Ratings { Rating1 = cat1.Rating, Rating2 = cat2.Rating };
            var newRatings = _ratingService.ComputeRatings(currentRatings, result.Result);

            cat1.Rating = newRatings.Rating1;
            await _catRepository.Update(cat1);

            cat2.Rating = newRatings.Rating2;
            await _catRepository.Update(cat2);

            await _unitOfWork.Save();
        }
    }
}