namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNotification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "Type_Id", "dbo.Notifications");
            DropIndex("dbo.Notifications", new[] { "Type_Id" });
            AddColumn("dbo.Notifications", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "Git_Id", c => c.Int());
            CreateIndex("dbo.Notifications", "Git_Id");
            AddForeignKey("dbo.Notifications", "Git_Id", "dbo.Gigs", "Id");
            DropColumn("dbo.Notifications", "Type_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Type_Id", c => c.Int());
            DropForeignKey("dbo.Notifications", "Git_Id", "dbo.Gigs");
            DropIndex("dbo.Notifications", new[] { "Git_Id" });
            DropColumn("dbo.Notifications", "Git_Id");
            DropColumn("dbo.Notifications", "Type");
            CreateIndex("dbo.Notifications", "Type_Id");
            AddForeignKey("dbo.Notifications", "Type_Id", "dbo.Notifications", "Id");
        }
    }
}
