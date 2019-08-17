using CatMash.Core.Entities;
using CatMash.Core.Interfaces;
using CatMash.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CatMash.Core.UnitTests
{
    public class CatServiceTest
    {
        [Fact]
        public async Task GetRankedList_ReturnsOrderedByRatingCatList()
        {
            IEnumerable<Cat> cats = new List<Cat>
            {
                new Cat { Id = "1", Rating = 10 },
                new Cat { Id = "2", Rating = 1 },
                new Cat { Id = "3", Rating = 5 },
            };
            var mockedRepo = new Mock<IBaseRepository<Cat>>();
            mockedRepo.Setup(p => p.GetAll()).Returns(Task.FromResult(cats));
            var mockedRatingService = new Mock<IRatingService>();

            var catService = new CatService(mockedRepo.Object, mockedRatingService.Object);

            var rankedlist = await catService.GetRankedList();
            Assert.Equal(3, rankedlist.Count());
            Assert.Equal("1", rankedlist.First().Id);
            Assert.Equal("3", rankedlist.ElementAt(1).Id);
            Assert.Equal("2", rankedlist.ElementAt(2).Id);
        }
    }
}
