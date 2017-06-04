namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyVenueInGig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "VenueId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Notifications", "OriginalVenue", c => c.Byte(nullable: false));
            CreateIndex("dbo.Gigs", "VenueId");
            AddForeignKey("dbo.Gigs", "VenueId", "dbo.Venues", "Id", cascadeDelete: true);
            DropColumn("dbo.Gigs", "Venue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gigs", "Venue", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.Gigs", "VenueId", "dbo.Venues");
            DropIndex("dbo.Gigs", new[] { "VenueId" });
            AlterColumn("dbo.Notifications", "OriginalVenue", c => c.String());
            DropColumn("dbo.Gigs", "VenueId");
        }
    }
}
