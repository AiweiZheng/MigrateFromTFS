namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddActivatedToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Activated", c => c.Boolean(nullable: false, defaultValue: true));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Activated");
        }
    }
}
