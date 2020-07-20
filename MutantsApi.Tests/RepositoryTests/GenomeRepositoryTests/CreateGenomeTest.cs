using MutantsApi.Tests.Utils;
using MutantsAPI.Models;
using MutantsAPI.Repositories;
using Xunit;

namespace MutantsApi.Tests.GenomeRepositoryTests
{
    public class CreateGenomeRepositoryTest
    {
        [Fact]
        public void CreateGenome_DoesNotInsertGenome_WhenItAlreadyExistsInDb()
        {
            var rOptions = TestDbContextOptionsFactory.GetTestReadDbOptions();
            var cOptions = TestDbContextOptionsFactory.GetTestCommandDbOptions();

            using var readContext = new ReadGenomeContext(rOptions);
            using var commandContext = new CommandGenomeContext(cOptions);
            var testGenome = new Genome(new[] { "AC", "GT" }, false);
            commandContext.Genomes.Add(testGenome);
            commandContext.SaveChanges();
            var genomeRepository = new GenomeRepository(commandContext, readContext);

            var result = genomeRepository.CreateGenome(testGenome);

            Assert.False(result.Result, "'CreateGenome' method inserted a row, but it shouldn't have");
        }

        [Fact]
        public void CreateGenome_DoesInsertNewGenome_WhenItDoesntExistInDb()
        {
            var rOptions = TestDbContextOptionsFactory.GetTestReadDbOptions();
            var cOptions = TestDbContextOptionsFactory.GetTestCommandDbOptions();

            using var commandContext = new CommandGenomeContext(cOptions);
            using var readContext = new ReadGenomeContext(rOptions);
            var testGenome = new Genome(new[] { "AA", "GG" }, false);
            var genomeRepository = new GenomeRepository(commandContext, readContext);

            var result = genomeRepository.CreateGenome(testGenome);

            Assert.True(result.Result, "'CreateGenome' method did not insert a row, but it should have");
        }
    }
}
