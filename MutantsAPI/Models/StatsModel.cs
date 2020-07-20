using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MutantsAPI.Models
{
    [Table("Stats")]
    public class StatsModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("count_human_dna")]
        public long HumanCount { get; set; }

        [JsonPropertyName("count_mutant_dna")]
        public long MutantCount { get; set; }

        public double Ratio { get; set; }

        [JsonIgnore]
        public DateTime Changed { get; set; }
    }

}
