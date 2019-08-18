using CatMash.Core.Entities;
using CatMash.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Infrastructure.Repositories
{
    public class VoteRepository : BaseRepository<Vote>, IVoteRepository
    {
        public VoteRepository(CatMashContext context) : base(context)
        {
        }

        public async Task<Vote> FindByOpponents(string cat1Id, string cat2Id)
        {
            var vote = _ctx.Set<Vote>().Include("VoteCats.Cat")
                .FirstOrDefault(v => v.VoteCats.Any(vc => vc.CatId == cat1Id) && v.VoteCats.Any(vc => vc.CatId == cat2Id));
            return vote;
        }
    }
}
