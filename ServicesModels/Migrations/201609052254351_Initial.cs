namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
						Cover = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        SeanceId = c.Int(nullable: false),
                        TransactionId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seances", t => t.SeanceId, cascadeDelete: true)
                .Index(t => t.SeanceId);
            
            CreateTable(
                "dbo.Seances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        MovieId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Spots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.ReservationSpotMapping",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        SpotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.SpotId })
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .ForeignKey("dbo.Spots", t => t.SpotId)
                .Index(t => t.ReservationId)
                .Index(t => t.SpotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationSpotMapping", "SpotId", "dbo.Spots");
            DropForeignKey("dbo.ReservationSpotMapping", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Seances", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Spots", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Reservations", "SeanceId", "dbo.Seances");
            DropForeignKey("dbo.Seances", "MovieId", "dbo.Movies");
            DropIndex("dbo.ReservationSpotMapping", new[] { "SpotId" });
            DropIndex("dbo.ReservationSpotMapping", new[] { "ReservationId" });
            DropIndex("dbo.Spots", new[] { "RoomId" });
            DropIndex("dbo.Seances", new[] { "RoomId" });
            DropIndex("dbo.Seances", new[] { "MovieId" });
            DropIndex("dbo.Reservations", new[] { "SeanceId" });
            DropTable("dbo.ReservationSpotMapping");
            DropTable("dbo.Spots");
            DropTable("dbo.Rooms");
            DropTable("dbo.Seances");
            DropTable("dbo.Reservations");
            DropTable("dbo.Movies");
        }
    }
}
