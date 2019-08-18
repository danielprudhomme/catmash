using CatMash.Core.Entities;
using CatMash.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatMash.Core.Services
{
    public class CatService : ICatService
    {
        private readonly IBaseRepository<Cat> _catRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LAtelierService _latelierService;

        public CatService(IBaseRepository<Cat> catRepository,
            IVoteRepository voteRepository,
            IUnitOfWork unitOfWork,
            LAtelierService latelierService)
        {
            _catRepository = catRepository;
            _voteRepository = voteRepository;
            _unitOfWork = unitOfWork;
            _latelierService = latelierService;
        }

        /// <summary>
        /// Returns the list of cats ordered by rating descending
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Cat>> GetRankedList()
        {
            var cats = await _catRepository.GetAll();
            return cats.OrderByDescending(x => x.Rating);
        }

        /// <summary>
        /// Import cats from l'atelier
        /// For each new cat, create a vote between him and every other existing cat
        /// </summary>
        /// <returns></returns>
        public async Task Populate()
        {
            var dto = await _latelierService.ImportCats();

            if (dto == null || dto.Cats == null)
            {
                throw new Exception("Wrong http response when importing cats from of WS l'atelier");
            }

            var cats = (await _catRepository.GetAll()).ToList();
            foreach(var newCat in dto.Cats)
            {
                if (!cats.Any(x => x.Id == newCat.Id))
                {
                    var cat = new Cat
                    {
                        Id = newCat.Id,
                        Url = newCat.Url,
                        Rating = 1000
                    };
                    await _catRepository.Insert(cat);

                    foreach (var otherCat in cats)
                    {
                        var vote = new Vote
                        {
                            Id = Guid.NewGuid(),
                            Occurence = 0,
                            VoteCats = new List<VoteCat>()
                        };

                        vote.VoteCats.Add(new VoteCat
                        {
                            CatId = otherCat.Id,
                            VoteId = vote.Id,
                            Order = 0
                        });
                        vote.VoteCats.Add(new VoteCat
                        {
                            CatId = cat.Id,
                            VoteId = vote.Id,
                            Order = 1
                        });

                        await _voteRepository.Insert(vote);
                    }

                    cats.Add(cat);
                }
            }

            await _unitOfWork.Save();
        }
    }
}
