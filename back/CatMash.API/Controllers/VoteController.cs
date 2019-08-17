using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatMash.API.ViewModels;
using CatMash.Core.Dto;
using CatMash.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatMash.API.Controllers
{
    [Route("api/vote")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVoteService _voteService;
        private readonly ICatService _catService;

        public VoteController(IMapper mapper, IVoteService voteService, ICatService catService)
        {
            _mapper = mapper;
            _voteService = voteService;
            _catService = catService;
        }

        [HttpGet]
        public async Task<VoteViewModel> GetNextVote()
        {
            await _catService.Populate();
            return await GetNextVoteViewModel();
        }

        [HttpPost]
        public async Task<VoteViewModel> Post([FromBody] VoteResult result)
        {
            await _voteService.HandleVote(result);
            return await GetNextVoteViewModel();
        }

        private async Task<VoteViewModel> GetNextVoteViewModel()
        {
            var vote = await _voteService.GetNextVote();
            var cats = vote.VoteCats.OrderBy(x => x.Order).Select(x => x.Cat);

            if (cats.Count() != 2)
            {
                throw new Exception("VoteCats should always have 2 elements.");
            }

            var vm = new VoteViewModel
            {
                Cat1 = _mapper.Map<CatViewModel>(cats.First()),
                Cat2 = _mapper.Map<CatViewModel>(cats.Last()),
            };

            return vm;
        }
    }
}
