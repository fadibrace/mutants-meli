using Microsoft.EntityFrameworkCore;

namespace MutantsAPI.Models
{
    public class ReadGenomeContext : DbContext
    {
        public ReadGenomeContext()
        {
        }

        public ReadGenomeContext(DbContextOptions<ReadGenomeContext> options)
            : base(options)
        {
        }

        public DbSet<Genome> Genomes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Genome>(entity => {
                entity.HasKey(e => e.DnaHash);
            });
        }
    }
}
