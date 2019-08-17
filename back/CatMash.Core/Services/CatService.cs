using CatMash.Core.Entities;
using CatMash.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public Task<IEnumerable<Cat>> GetRankedList()
        {
            throw new NotImplementedException();
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
