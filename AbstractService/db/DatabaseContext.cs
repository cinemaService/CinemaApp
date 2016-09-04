using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AbstractService.db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Seance> Seances { get; set; }

        public DbSet<Spot> Spots { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Spots)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("ReservationId");
                    x.MapRightKey("SpotId");
                    x.ToTable("ReservationSpotMapping");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}