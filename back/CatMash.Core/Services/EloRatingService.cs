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
        public Ratings ComputeRatings(Ratings ratings, Result result)
        {
            var probabilityPlayer1 = 1.0 / (1.0 + Math.Pow(10, (ratings.Rating2 - ratings.Rating1) / 400.0));

            var actualScore = result == Result.Win ? 1 : 0;
            var newRating1 = ratings.Rating1 + 30 * (actualScore - probabilityPlayer1);
            var delta = (int)Math.Round(newRating1 - ratings.Rating1);

            return new Ratings
            {
                Rating1 = ratings.Rating1 + delta,
                Rating2 = ratings.Rating2 - delta
            };
        }
    }
}
