using System.Data.Entity.Migrations;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void Setup()
        {
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();
            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser { UserName = "user1", Name = "user1", Email = "_", PasswordHash = "_" });
            context.Users.Add(new ApplicationUser { UserName = "user2", Name = "user2", Email = "_", PasswordHash = "_" });
            context.SaveChanges();
        }
    }
}
