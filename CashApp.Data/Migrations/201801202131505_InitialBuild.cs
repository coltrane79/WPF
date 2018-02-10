namespace CashApp.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialBuild : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CashBalanceSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        FloatStart = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CashTotals_hundred = c.Int(nullable: false),
                        CashTotals_fifty = c.Int(nullable: false),
                        CashTotals_twenty = c.Int(nullable: false),
                        CashTotals_ten = c.Int(nullable: false),
                        CashTotals_five = c.Int(nullable: false),
                        CashTotals_two = c.Int(nullable: false),
                        CashTotals_one = c.Int(nullable: false),
                        CashTotals_quarter = c.Int(nullable: false),
                        CashTotals_dime = c.Int(nullable: false),
                        CashTotals_nickel = c.Int(nullable: false),
                        CashTotals_penny = c.Int(nullable: false),
                        CashDeposit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Debit_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Debit_Descritpion = c.String(),
                        MasterCard_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MasterCard_Descritpion = c.String(),
                        Visa_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Visa_Descritpion = c.String(),
                        Amex_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amex_Descritpion = c.String(),
                        Other_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Other_Descritpion = c.String(),
                        ChequeTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FloatEnd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Disbursement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Returns = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Variance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DailyZread_Id = c.Int(),
                        SalesPerson_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ZReads", t => t.DailyZread_Id)
                .ForeignKey("dbo.SalesPersons", t => t.SalesPerson_Id)
                .Index(t => t.DailyZread_Id)
                .Index(t => t.SalesPerson_Id);
            
            CreateTable(
                "dbo.ZReads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalesTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GiftCertificate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Coupon = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Void = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Net = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gross = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cash = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CashInDrawer = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalesPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CashBalanceSheets", "SalesPerson_Id", "dbo.SalesPersons");
            DropForeignKey("dbo.CashBalanceSheets", "DailyZread_Id", "dbo.ZReads");
            DropIndex("dbo.CashBalanceSheets", new[] { "SalesPerson_Id" });
            DropIndex("dbo.CashBalanceSheets", new[] { "DailyZread_Id" });
            DropTable("dbo.SalesPersons");
            DropTable("dbo.ZReads");
            DropTable("dbo.CashBalanceSheets");
        }
    }
}
