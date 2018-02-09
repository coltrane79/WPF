namespace CashApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ZReads", "ZReadDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SalesPersons", "firstName", c => c.String(nullable: false));
            AlterColumn("dbo.SalesPersons", "lastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesPersons", "lastName", c => c.String());
            AlterColumn("dbo.SalesPersons", "firstName", c => c.String());
            DropColumn("dbo.ZReads", "ZReadDate");
        }
    }
}
