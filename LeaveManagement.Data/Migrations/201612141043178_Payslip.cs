namespace LeaveManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payslip : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.PaySlip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        SavedPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            
        }
        
        public override void Down()
        {
           
            DropForeignKey("dbo.PaySlip", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PaySlip", new[] { "UserId" });
            DropTable("dbo.PaySlip");
        }
    }
}
