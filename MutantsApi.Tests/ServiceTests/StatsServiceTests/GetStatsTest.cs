using Moq;
using MutantsAPI.Domains;
using MutantsAPI.Models;
using MutantsAPI.Repositories;
using Xunit;

namespace MutantsApi.Tests.StatsServiceTests
{
    public class GetStatsTest
    {
        [Fact]
        public void GetStats_ReturnsStats_WhenInvoked()
        {
            var mockStatsRepo = new Mock<IStatsRepository>();
            var testStat = new StatsModel() { HumanCount = 4, MutantCount = 2, Ratio = 0.5 };
            mockStatsRepo.Setup(x => x.GetGenomeStats()).Returns(testStat);
            var statsService = new StatsService(mockStatsRepo.Object);
            
            var result = statsService.GetStats();

            Assert.Equal(result, testStat);
        }
    }
}
