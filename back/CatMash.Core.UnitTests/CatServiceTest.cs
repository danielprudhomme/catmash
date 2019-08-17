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
            var catRepo = new Mock<IBaseRepository<Cat>>();
            catRepo.Setup(p => p.GetAll()).Returns(Task.FromResult(cats));

            var voteRepo = new Mock<IBaseRepository<Vote>>();
            var uow = new Mock<IUnitOfWork>();
            var ratingService = new Mock<IRatingService>();
            var latelierService = new Mock<LAtelierService>();

            var catService = new CatService(catRepo.Object, voteRepo.Object, uow.Object, ratingService.Object, latelierService.Object);

            var rankedlist = await catService.GetRankedList();
            Assert.Equal(3, rankedlist.Count());
            Assert.Equal("1", rankedlist.First().Id);
            Assert.Equal("3", rankedlist.ElementAt(1).Id);
            Assert.Equal("2", rankedlist.ElementAt(2).Id);
        }
    }
}
