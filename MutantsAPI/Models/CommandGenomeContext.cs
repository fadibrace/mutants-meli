using Microsoft.EntityFrameworkCore;

namespace MutantsAPI.Models
{
    public class CommandGenomeContext : DbContext
    {
        public CommandGenomeContext()
        {
        }

        public CommandGenomeContext(DbContextOptions<CommandGenomeContext> options)
            : base(options)
        {
        }

        public DbSet<Genome> Genomes { get; set; }
        public DbSet<StatsModel> StatsModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Genome>(entity => {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasKey(e => e.DnaHash);
            });
        }
    }
}
