using System;
using System.Collections.Generic;
using MutantsApi.Tests.Utils;
using MutantsAPI.Models;
using MutantsAPI.Repositories;
using Xunit;

namespace MutantsApi.Tests.RepositoryTests.StatsRepositoryTests
{
    public class RefreshGenomeStatsCacheTest
    {
        [Fact]
        public void RefreshGenomeStatsCache_LoadsCache_WhenInvokedAndCacheDoesNotExist()
        {
            var rOptions = TestDbContextOptionsFactory.GetTestReadDbOptions();
            var cOptions = TestDbContextOptionsFactory.GetTestCommandDbOptions();
            using var readContext = new ReadGenomeContext(rOptions);
            using var commandContext = new CommandGenomeContext(cOptions);
            var testGenomes = new List<Genome>() {
                    new Genome(new[] { "ACGT", "TGCA" }, false),
                    new Genome(new[] { "AAAA", "BBBB" }, true),
                };
            readContext.Genomes.AddRange(testGenomes);
            readContext.SaveChanges();
            var statsRepository = new StatsRepository(readContext, commandContext);

            statsRepository.RefreshGenomeStatsCache();
            var result = commandContext.StatsModels.Find(1);

            Assert.Equal(1, result.HumanCount);
            Assert.Equal(1, result.MutantCount);
            Assert.Equal(0.5, result.Ratio);
        }

        [Fact]
        public void RefreshGenomeStatsCache_UpdatesCache_WhenInvokedAndCacheAlreadyExists()
        {
            var rOptions = TestDbContextOptionsFactory.GetTestReadDbOptions();
            var cOptions = TestDbContextOptionsFactory.GetTestCommandDbOptions();
            using var readContext = new ReadGenomeContext(rOptions);
            using var commandContext = new CommandGenomeContext(cOptions);
            var testGenomes = new List<Genome>() {
                    new Genome(new[] { "ACGT", "TGCA" }, false),
                    new Genome(new[] { "AAAA", "BBBB" }, true),
                };
            readContext.Genomes.AddRange(testGenomes);
            readContext.SaveChanges();
            var testStats = new StatsModel() {
                Id = 1,
                HumanCount = 1,
                MutantCount = 0,
                Ratio = 0,
                Changed = DateTime.UtcNow.AddDays(-1)
            };
            commandContext.StatsModels.Add(testStats);
            commandContext.SaveChanges();
            var statsRepository = new StatsRepository(readContext, commandContext);

            statsRepository.RefreshGenomeStatsCache();
            var result = commandContext.StatsModels.Find(1);

            Assert.Equal(1, result.HumanCount);
            Assert.Equal(1, result.MutantCount);
            Assert.Equal(0.5, result.Ratio);
        }
    }
}
