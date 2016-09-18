using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using ReservationServiceModels;
namespace ServicesModels.db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Seance> Seances { get; set; }

        public DbSet<Spot> Spots { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

		public DatabaseContext() : base(Config.DbName)
		{
			var name = Config.DbName;
        }

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