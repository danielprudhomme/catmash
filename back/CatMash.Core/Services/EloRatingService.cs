using CatMash.Core.Dto;
using CatMash.Core.Enums;
using CatMash.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatMash.Core.Services
{
    public class EloRatingService : IRatingService
    {
        private readonly int k = 30;

        public Ratings ComputeRatings(Ratings ratings, Result result)
        {
            throw new NotImplementedException();
        }
    }
}
