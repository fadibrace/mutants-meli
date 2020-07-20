using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MutantsAPI.Utils;

namespace MutantsAPI.Dtos
{
    public class Dna
    {
        [Required]
        [DnaModel(ErrorMessage = "Invalid DNA")]
        [JsonPropertyName("dna")]
        public string[] DnaMatrix { set; get; }

    }
}
