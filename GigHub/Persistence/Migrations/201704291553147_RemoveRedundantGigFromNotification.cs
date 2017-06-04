namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRedundantGigFromNotification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "Git_Id", "dbo.Gigs");
            DropIndex("dbo.Notifications", new[] { "Git_Id" });
            DropColumn("dbo.Notifications", "Git_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Git_Id", c => c.Int());
            CreateIndex("dbo.Notifications", "Git_Id");
            AddForeignKey("dbo.Notifications", "Git_Id", "dbo.Gigs", "Id");
        }
    }
}
