using ServicesModels.db;

namespace Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ServicesModels.db.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Movies.AddOrUpdate(
                new Movie()
                {
                    Id = 1,
                    Title = "First try",
                    Description = "Always hurts",
					Cover = "default.jpg"
                });

            context.Rooms.AddOrUpdate(
                new Room()
                {
                    Id = 1,
                    Number = "First"
                });

            context.Spots.AddOrUpdate(
                new Spot()
                {
                    Id = 1,
                    Number = "1-A",
                    RoomId = 1
                },
                new Spot()
                {
                    Id = 2,
                    Number = "2-A",
                    RoomId = 1
                },
                new Spot()
                {
                    Id = 3,
                    Number = "3-A",
                    RoomId = 1
                },
                new Spot()
                {
                    Id = 4,
                    Number = "4-A",
                    RoomId = 1
                },
                new Spot()
                {
                    Id = 5,
                    Number = "5-A",
                    RoomId = 1
                },
                new Spot()
                {
                    Id = 6,
                    Number = "6-A",
                    RoomId = 1
                },
                new Spot()
                {
                    Id = 7,
                    Number = "1-B",
                    RoomId = 1
                },
                new Spot()
                {
                    Id = 8,
                    Number = "2-B",
                    RoomId = 1
                });

            context.Seances.AddOrUpdate(
                new Seance()
                {
                    Id = 1,
                    Date = DateTime.Today,
                    MovieId = 1,
                    RoomId = 1
                });
        }
    }
}
