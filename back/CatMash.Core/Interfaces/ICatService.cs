using CatMash.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Core.Interfaces
{
    public interface ICatService
    {
        /// <summary>
        /// Populates Cats from l'atelier
        /// For each cat, create the combination Vote with all of the other existings cats
        /// </summary>
        /// <returns></returns>
        Task Populate();

        /// <summary>
        /// Returns the list of cats ordered by rating
        /// </summary>
        /// <returns>List of cats</returns>
        Task<IEnumerable<Cat>> GetRankedList();

        /// <summary>
        /// Updates the ELO rating of the opponent cats
        /// </summary>
        /// <param name="winner"></param>
        /// <param name="loser"></param>
        /// <returns></returns>
        Task UpdateRating(Cat winner, Cat loser);
    }
}
