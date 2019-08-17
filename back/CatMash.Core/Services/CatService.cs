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
        private readonly IRatingService _ratingService;

        public CatService(IBaseRepository<Cat> catRepository, IRatingService ratingService)
        {
            _catRepository = catRepository;
            _ratingService = ratingService;
        }

        public async Task<IEnumerable<Cat>> GetRankedList()
        {
            var cats = await _catRepository.GetAll();
            return cats.OrderByDescending(x => x.Rating);
        }

        public Task Populate()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRating(Cat winner, Cat loser)
        {
            throw new NotImplementedException();
        }
    }
}
