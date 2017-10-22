namespace QuotationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserLoginHisModel : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            DropTable("dbo.UserLogHis");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
