using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using MutantsAPI.Controllers;
using MutantsAPI.Domains;
using MutantsAPI.Dtos;
using MutantsAPI.Models;
using Xunit;

namespace MutantsApi.Tests.ControllersTests
{
    public class StatsControllerTest
    {
        [Fact]
        public void GetStats_ReturnsStats_WhenCalled()
        {
            var mockStatsService = new Mock<IStatsService>();
            var testStats = new StatsModel() { HumanCount = 4, MutantCount = 2, Ratio = 0.5 };
            mockStatsService.Setup(x => x.GetStats()).Returns(testStats);
            var statsController = new StatsController(mockStatsService.Object);
            var testDna = new Dna() { DnaMatrix = new[] { "AAAA, CCCC, GGGG, TTTT" } };

            var result = statsController.GetGenomesStats();

            Assert.Equal(4, result.Value.HumanCount);
            Assert.Equal(2, result.Value.MutantCount);
            Assert.Equal(0.5, result.Value.Ratio);
        }
    }
}
