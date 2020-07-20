using MutantsAPI.Domains;
using Xunit;

namespace MutantsApi.Tests.ServiceTests.GenomeServiceTests
{
    public class IsMutantTest
    {
        private readonly IGenomeService _genomeDomain;

        public IsMutantTest()
        {
            _genomeDomain = new GenomeService();
        }

        [Fact]
        public void IsMutant_ReturnsFalse_WhenThereAreNoMutantSecuences()
        {
            var testDna = new[] {
                "ACGTAC",
                "CGTACG",
                "TACGTA",
                "CGTACG",
                "TACGTA",
                "CGTACG"
            };

            var isMutant = _genomeDomain.IsMutant(testDna);

            Assert.False(isMutant, "DNA is not mutant but IsMutant says it is");
        }

        [Fact]
        public void IsMutant_ReturnsTrue_WhenThereAreMoreThanTwoMutantSecuences()
        {
            var testDna = new[] {
                "ATGCGA",
                "CAGTGC",
                "TTATGT",
                "AGAAGG",
                "CCCCTA",
                "TCACTG"
            };

            var isMutant = _genomeDomain.IsMutant(testDna);

            Assert.True(isMutant, "DNA is mutant but IsMutant says it isn't");
        }

        [Fact]
        public void IsMutant_ReturnsFalse_WhenThereIsOnlyOneMutantSecuence()
        {
            var testDna = new[] {
                "ACGTAC",
                "CAAAAG",
                "TACGTA",
                "CGTACG",
                "TACGTA",
                "CGTACG"
            };

            var isMutant = _genomeDomain.IsMutant(testDna);

            Assert.False(isMutant, "DNA is not mutant but IsMutant says it is");
        }

        [Fact]
        public void IsMutant_ReturnsTrue_ForDiagonalSequences()
        {
            var testDna = new[] {
                "ACGTAC",
                "CATACG",
                "TAAGTA",
                "CGGACG",
                "TGCGTA",
                "GGTACG"
            };

            var isMutant = _genomeDomain.IsMutant(testDna);

            Assert.True(isMutant, "DNA is mutant but IsMutant says it isn't");
        }

        [Fact]
        public void IsMutant_ReturnsTrue_ForEdgeSecuences()
        {
            var testDna = new[] {
                "ACGTAC",
                "CGTACA",
                "TACGTA",
                "CGTACA",
                "TACGTA",
                "CGCCCC"
            };

            var isMutant = _genomeDomain.IsMutant(testDna);

            Assert.True(isMutant, "DNA is mutant but IsMutant says it isn't");
        }

        [Fact]
        public void IsMutant_ReturnsTrue_ForOverlappingSequences()
        {
            var testDna = new[] {
                "ACATAC",
                "CGAACA",
                "TAAGTA",
                "CGAACA",
                "TAAGTA",
                "CGACCC"
            };

            var isMutant = _genomeDomain.IsMutant(testDna);

            Assert.True(isMutant, "DNA is mutant but IsMutant says it isn't");
        }
    }
}
