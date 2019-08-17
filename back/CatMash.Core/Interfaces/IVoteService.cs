using CatMash.Core.Dto;
using CatMash.Core.Entities;
using System.Threading.Tasks;

namespace CatMash.Core.Interfaces
{
    public interface IVoteService
    {
        /// <summary>
        /// Returns the next vote to be displayed to the user (random weighted by occurrence)
        /// </summary>
        /// <returns>vote</returns>
        Task<Vote> GetNextVote();

        /// <summary>
        /// When a vote has been performed by the user, this function process to:
        /// - vote occurence++
        /// - update the rating of the opponent cats
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        Task HandleVote(VoteResult result);
    }
}
