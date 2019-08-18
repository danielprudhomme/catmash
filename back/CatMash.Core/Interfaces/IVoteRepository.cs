using CatMash.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Core.Interfaces
{
    public interface IVoteRepository : IBaseRepository<Vote>
    {
        Task<Vote> FindByOpponents(string cat1Id, string cat2Id);
    }
}
