using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MutantsAPI.Utils
{
    public class DnaModelAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            var inputValue = value as string[];
            var isValid = true;
            Regex regex = new Regex(@"^([ACGT]+)$");

            foreach(var dna in inputValue)
            {
                Match match = regex.Match(dna);
                if (dna.Length != inputValue.Length || !match.Success)
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }
    }
}
