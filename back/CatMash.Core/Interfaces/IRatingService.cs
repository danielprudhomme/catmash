using CatMash.Core.Dto;
using CatMash.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatMash.Core.Interfaces
{
    public interface IRatingService
    {
        /// <summary>
        /// Computes new ratings of player1 (rating1) and player2 (rating2) based on result
        /// </summary>
        /// <param name="ratings"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        Ratings ComputeRatings(Ratings ratings, Result result);
    }
}
