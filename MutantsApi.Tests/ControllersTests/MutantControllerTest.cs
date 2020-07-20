using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using MutantsAPI.Controllers;
using MutantsAPI.Domains;
using MutantsAPI.Dtos;
using Xunit;

namespace MutantsApi.Tests.ControllersTests
{
    public class MutantControllerTest
    {
        [Fact]
        public void PostMutant_ReturnsOk_ForMutant()
        {
            var mockGenomeService = new Mock<IGenomeService>();
            mockGenomeService.Setup(x => x.ProcessDna(It.IsAny<Dna>())).ReturnsAsync(true);
            var mutantController = new MutantController(mockGenomeService.Object);
            var testDna = new Dna() { DnaMatrix = new[] { "AAAA, CCCC, GGGG, TTTT" } };

            var result = mutantController.PostMutant(testDna).Result;
            var statusCodeResult = (IStatusCodeActionResult)result;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public void PostMutant_ReturnsForbidden_ForNotMutant()
        {
            var mockGenomeService = new Mock<IGenomeService>();
            mockGenomeService.Setup(x => x.ProcessDna(It.IsAny<Dna>())).ReturnsAsync(false);
            var mutantController = new MutantController(mockGenomeService.Object);
            var testDna = new Dna() { DnaMatrix = new[] { "AACC, GGTT, AACC, GGTT" } };

            var result = mutantController.PostMutant(testDna).Result;
            var statusCodeResult = (IStatusCodeActionResult)result;

            Assert.Equal(403, statusCodeResult.StatusCode);
        }
    }
}
