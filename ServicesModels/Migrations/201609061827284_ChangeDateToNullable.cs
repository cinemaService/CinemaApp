namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "TransactionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "TransactionDate", c => c.DateTime(nullable: false));
        }
    }
}
