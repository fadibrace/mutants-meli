using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Force.Crc32;

namespace MutantsAPI.Models
{
    [Table("Genomes")]
    public class Genome
    {
        public long Id { get; set; }

        [Required]
        public long DnaHash { get; set; }

        [Required]
        //[Column(TypeName = "text")]
        [Column(TypeName = "varchar(max)")]
        public string DnaSequence { get; set; }

        [Required]
        public bool IsMutant { get; set; }

        public Genome()
        {
        }

        public Genome(string[] dna, bool isMutant)
        {
            DnaSequence = string.Join(',', dna);
            DnaHash = Convert.ToInt64(Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(DnaSequence)));
            IsMutant = isMutant;
        }
    }
}
