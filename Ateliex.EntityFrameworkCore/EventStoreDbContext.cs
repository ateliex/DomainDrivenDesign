using Microsoft.EntityFrameworkCore;

namespace Ateliex
{
    public class EventStoreDbContext : DbContext
    {
        public DbSet<EventStoreTable> EventStore { get; set; }

        public EventStoreDbContext()
        {

        }

        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder
            //    //.UseLazyLoadingProxies()
            //    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventStoreTable>()
                .HasKey(p => new { p.Name, p.Version });

            //modelBuilder.Entity<EventStoreTable>()
            //    .Property(p => p.Name)
            //    .ValueGeneratedNever();
        }
    }
}
