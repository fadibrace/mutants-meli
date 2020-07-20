using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MutantsAPI.Dtos;
using Xunit;

namespace MutantsApi.Tests.UtilsTests
{
    public class DnaModelAttributeTests
    {
        [Fact]
        public void Dna_IsValid_ForSquareMatrixWithOnlyACGTCharacters()
        {
            var testDna = new Dna()
            {
                DnaMatrix = new[] {
                    "ACGT",
                    "ACGT",
                    "ACGT",
                    "ACGT"
                }
            };
            
            var context = new ValidationContext(testDna);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(testDna, context, results, true);

            Assert.True(isValid);
        }

        [Fact]
        public void Dna_IsNotValid_ForNotSquareMatrix()
        {
            var testDna = new Dna()
            {
                DnaMatrix = new[] {
                    "ACGTA",
                    "ACGT",
                    "ACGT",
                    "ACGT"
                }
            };

            var context = new ValidationContext(testDna);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(testDna, context, results, true);

            Assert.False(isValid);
        }

        [Fact]
        public void Dna_IsNotValid_ForNonACGTCharacters()
        {
            var testDna = new Dna()
            {
                DnaMatrix = new[] {
                    "ACGT",
                    "ACGT",
                    "ACGT",
                    "ACGX"
                }
            };

            var context = new ValidationContext(testDna);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(testDna, context, results, true);

            Assert.False(isValid);
        }
    }
}

