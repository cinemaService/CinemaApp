namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTransactionId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "TransactionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "TransactionId", c => c.String());
        }
    }
}
