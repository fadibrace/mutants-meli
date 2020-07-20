using Moq;
using MutantsAPI.Domains;
using MutantsAPI.Models;
using MutantsAPI.Repositories;
using Xunit;

namespace MutantsApi.Tests.ServiceTests.GenomeServiceTests
{
    public class CreateGenomeTest
    {
        [Fact]
        public void CreateGenome_CallsGenomeRepo_WhenInvoked()
        {
            var mockGenomeRepo = new Mock<IGenomeRepository>();
            var genomeService = new GenomeService(mockGenomeRepo.Object);
            var testGenome = new Genome(new[] { "AC", "GT" }, false);

            var result = genomeService.CreateGenome(testGenome);

            mockGenomeRepo.Verify(x => x.CreateGenome(It.IsAny<Genome>()), Times.Once);
        }

        [Fact]
        public void CreateGenome_ReturnsGenome_WhenCreationIsSuccessful()
        {
            var mockGenomeRepo = new Mock<IGenomeRepository>();
            mockGenomeRepo.Setup(x => x.CreateGenome(It.IsAny<Genome>())).ReturnsAsync(true);
            var genomeService = new GenomeService(mockGenomeRepo.Object);
            var testGenome = new Genome(new[] { "AC", "GT" }, false);

            var result = genomeService.CreateGenome(testGenome);

            Assert.Equal(testGenome.DnaHash, result.Result.DnaHash);
        }

        [Fact]
        public void CreateGenome_ReturnsNull_WhenCreationIsNotSuccessful()
        {
            var mockGenomeRepo = new Mock<IGenomeRepository>();
            mockGenomeRepo.Setup(x => x.CreateGenome(It.IsAny<Genome>())).ReturnsAsync(false);
            var genomeService = new GenomeService(mockGenomeRepo.Object);
            var testGenome = new Genome(new[] { "AC", "GT" }, false);

            var result = genomeService.CreateGenome(testGenome);

            Assert.Null(result.Result);
        }
    }
}
