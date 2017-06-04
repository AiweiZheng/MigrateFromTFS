namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddVenue1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Venues",
                c => new
                {
                    Id = c.Byte(nullable: false),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Venues");
        }
    }
}
