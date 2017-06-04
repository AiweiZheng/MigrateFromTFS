namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddVenue : DbMigration
    {
        public override void Up()
        {


            AlterColumn("dbo.Venues", "Name", c => c.String(nullable: false, maxLength: 255));
        }

        public override void Down()
        {

            AlterColumn("dbo.Venues", "Name", c => c.String());
        }
    }
}
