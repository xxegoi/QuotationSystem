namespace QuotationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProductClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuotationHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        QDate = c.DateTime(nullable: false),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fob = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SeaCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommissionType = c.Int(nullable: false),
                        Other = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesMemo = c.String(maxLength: 200),
                        PurchaseMemo = c.String(maxLength: 200),
                        Buyer_Id = c.Int(nullable: false),
                        Sales_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Buyer_Id)
                .Index(t => t.Sales_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuotationHeaders", "Sales_Id", "dbo.Employees");
            DropForeignKey("dbo.QuotationHeaders", "Buyer_Id", "dbo.Employees");
            DropIndex("dbo.QuotationHeaders", new[] { "Sales_Id" });
            DropIndex("dbo.QuotationHeaders", new[] { "Buyer_Id" });
            DropTable("dbo.QuotationHeaders");
            DropTable("dbo.ProductClasses");
        }
    }
}
