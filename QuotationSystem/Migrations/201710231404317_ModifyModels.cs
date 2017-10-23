namespace QuotationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyModels : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.QuotationHeaders", "Commision", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuotationDetails", "Header_Id", "dbo.QuotationHeaders");
            DropForeignKey("dbo.QuotationDetails", "Class_Id", "dbo.ProductClasses");
            DropIndex("dbo.QuotationDetails", new[] { "Header_Id" });
            DropIndex("dbo.QuotationDetails", new[] { "Class_Id" });
            DropColumn("dbo.QuotationHeaders", "Commision");
            DropTable("dbo.QuotationDetails");
        }
    }
}
