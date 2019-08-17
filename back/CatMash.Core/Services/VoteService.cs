using CatMash.Core.Dto;
using CatMash.Core.Entities;
using CatMash.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Core.Services
{
    public class VoteService : IVoteService
    {
        public Task<Vote> GetNextVote()
        {
            throw new NotImplementedException();
        }

        public Task HandleVote(VoteResult result)
        {
            throw new NotImplementedException();
        }
    }
}
