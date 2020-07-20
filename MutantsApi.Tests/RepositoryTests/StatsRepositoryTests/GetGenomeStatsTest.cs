using System.Collections.Generic;
using MutantsApi.Tests.Utils;
using MutantsAPI.Models;
using MutantsAPI.Repositories;
using Xunit;

namespace MutantsApi.Tests.RepositoryTests.StatsRepositoryTests
{
    public class GetGenomeStatsTest
    {
        [Fact]
        public void GetStats_ReturnsCorrectStatsModel_WhenInvokedWithExistingData()
        {
            var rOptions = TestDbContextOptionsFactory.GetTestReadDbOptions();
            var cOptions = TestDbContextOptionsFactory.GetTestCommandDbOptions();
            using var readContext = new ReadGenomeContext(rOptions);
            using var commandContext = new CommandGenomeContext(cOptions);
            var testGenomes = new List<Genome>() {
                    new Genome(new[] { "ACGT", "TGCA" }, false),
                    new Genome(new[] { "AAAA", "GGGG" }, true),
                    new Genome(new[] { "ACAC", "GTGT" }, false),
                };
            readContext.Genomes.AddRange(testGenomes);
            readContext.SaveChanges();
            var statsRepository = new StatsRepository(readContext, commandContext);

            var result = statsRepository.GetGenomeStats();

            Assert.Equal(2, result.HumanCount);
            Assert.Equal(1, result.MutantCount);
            Assert.Equal((double)1 / 3, result.Ratio);

        }

        [Fact]
        public void GetStats_ReturnsCorrectStatsModel_WhenInvokedWithNoData()
        {
            var rOptions = TestDbContextOptionsFactory.GetTestReadDbOptions();
            var cOptions = TestDbContextOptionsFactory.GetTestCommandDbOptions();
            using var readContext = new ReadGenomeContext(rOptions);
            using var commandContext = new CommandGenomeContext(cOptions);
            var statsRepository = new StatsRepository(readContext, commandContext);

            var result = statsRepository.GetGenomeStats();

            Assert.Equal(0, result.HumanCount);
            Assert.Equal(0, result.MutantCount);
            Assert.Equal(0, result.Ratio);
        }


        [Fact]
        public void GetStats_ReturnsCorrectStatsModel_WhenInvokedWithOnlyOneHumanRow()
        {
            var rOptions = TestDbContextOptionsFactory.GetTestReadDbOptions();
            var cOptions = TestDbContextOptionsFactory.GetTestCommandDbOptions();
            using var readContext = new ReadGenomeContext(rOptions);
            using var commandContext = new CommandGenomeContext(cOptions);
            var testGenomes = new List<Genome>() {
                    new Genome(new[] { "ACGT", "TGCA" }, false),
                };
            readContext.Genomes.AddRange(testGenomes);
            readContext.SaveChanges();
            var statsRepository = new StatsRepository(readContext, commandContext);

            var result = statsRepository.GetGenomeStats();

            Assert.Equal(1, result.HumanCount);
            Assert.Equal(0, result.MutantCount);
            Assert.Equal(0, result.Ratio);

        }

        [Fact]
        public void GetStats_ReturnsCorrectStatsModel_WhenInvokedWithOnlyOneMutantRow()
        {
            var rOptions = TestDbContextOptionsFactory.GetTestReadDbOptions();
            var cOptions = TestDbContextOptionsFactory.GetTestCommandDbOptions();
            using var readContext = new ReadGenomeContext(rOptions);
            using var commandContext = new CommandGenomeContext(cOptions);
            var testGenomes = new List<Genome>() {
                    new Genome(new[] { "ACGT", "TGCA" }, true),
                };
            readContext.Genomes.AddRange(testGenomes);
            readContext.SaveChanges();
            var statsRepository = new StatsRepository(readContext, commandContext);

            var result = statsRepository.GetGenomeStats();

            Assert.Equal(0, result.HumanCount);
            Assert.Equal(1, result.MutantCount);
            Assert.Equal(1, result.Ratio);
        }
    }
}
