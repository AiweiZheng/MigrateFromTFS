namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemnotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Message = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SystemNotifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SystemNotifications", new[] { "ApplicationUser_Id" });
            DropTable("dbo.SystemNotifications");
        }
    }
}
