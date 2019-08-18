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

        public Task<Vote> GetNextVote()
        {
            throw new NotImplementedException();
        }

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