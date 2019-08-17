using CatMash.Core.Dto;
using CatMash.Core.Enums;
using CatMash.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CatMash.Core.UnitTests
{
    public class EloRatingServiceTest
    {
        // P1 = (1.0 / (1.0 + pow(10, ((rating1 – rating2) / 400))));
        // P2 = (1.0 / (1.0 + pow(10, ((rating2 – rating1) / 400))));
        // rating = rating + K* (Actual Score – Expected score);
        [Theory]
        [InlineData(1200, 1000, Result.Win, 1207, 993)]
        [InlineData(1200, 1000, Result.Lose, 1177, 1023)]
        public void ComputeRatings_PerformEloRatingComputation(int rating1, int rating2, Result result, int expectedRating1, int expectedRating2)
        {
            var service = new EloRatingService();
            var currentRatings = new Ratings { Rating1 = rating1, Rating2 = rating2 };
            var newRatings = service.ComputeRatings(currentRatings, result);

            Assert.Equal(expectedRating1, newRatings.Rating1);
            Assert.Equal(expectedRating2, newRatings.Rating2);
        }

    }
}
