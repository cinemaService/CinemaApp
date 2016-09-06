namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        ReservationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ReservationId", "dbo.Reservations");
            DropIndex("dbo.Transactions", new[] { "ReservationId" });
            DropTable("dbo.Transactions");
        }
    }
}
