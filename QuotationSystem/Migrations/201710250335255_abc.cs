namespace QuotationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Account = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        RegisterTime = c.DateTime(nullable: false),
                        LastLogTime = c.DateTime(nullable: false),
                        Department_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id, cascadeDelete: true)
                .Index(t => t.Department_Id);
            
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
                "dbo.QuotationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Metal = c.String(),
                        Shape = c.String(),
                        Model = c.String(),
                        Technology = c.String(),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommissionCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Memo = c.String(),
                        Class_Id = c.Int(),
                        Header_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductClasses", t => t.Class_Id)
                .ForeignKey("dbo.QuotationHeaders", t => t.Header_Id)
                .Index(t => t.Class_Id)
                .Index(t => t.Header_Id);
            
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
                        Commision = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Other = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesMemo = c.String(maxLength: 200),
                        PurchaseMemo = c.String(maxLength: 200),
                        Sales_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Sales_Id, cascadeDelete: true)
                .Index(t => t.Sales_Id);
            
            CreateTable(
                "dbo.UserLogHis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(),
                        Cookie = c.String(),
                        LoginTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuotationDetails", "Header_Id", "dbo.QuotationHeaders");
            DropForeignKey("dbo.QuotationHeaders", "Sales_Id", "dbo.Employees");
            DropForeignKey("dbo.QuotationHeaders", "Buyer_Id", "dbo.Employees");
            DropForeignKey("dbo.QuotationDetails", "Class_Id", "dbo.ProductClasses");
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropIndex("dbo.QuotationHeaders", new[] { "Sales_Id" });
            DropIndex("dbo.QuotationHeaders", new[] { "Buyer_Id" });
            DropIndex("dbo.QuotationDetails", new[] { "Header_Id" });
            DropIndex("dbo.QuotationDetails", new[] { "Class_Id" });
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            DropTable("dbo.UserLogHis");
            DropTable("dbo.QuotationHeaders");
            DropTable("dbo.QuotationDetails");
            DropTable("dbo.ProductClasses");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
