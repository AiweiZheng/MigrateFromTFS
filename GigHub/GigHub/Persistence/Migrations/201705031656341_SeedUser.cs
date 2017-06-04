namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd5ddfc5a-4fd5-4c3d-8c97-db30a75838c8', N'AccountManager')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0779235e-2ea4-41f5-821d-6aabf531d521', N'Artist')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'fc67efce-88a8-4e5c-99fe-6f01b20c4690', N'Guest')

INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'108d3ef9-f121-48e1-8518-8be0b5e932c1', N'admin@gmail.com', 0, N'AOovJbIp71nI/HRzeOklTN/mOFnsN1ZTFGupkgQDPAhPVeYu/K1yFPvh2fzLrlg2LA==', N'22b0ab37-9eb8-4492-a616-daef338c4b67', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com', N'admin')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'3f7aa941-8389-489e-8c40-dad2d403e515', N'guest@gmail.com', 0, N'ADVyrQNeEaxJuSZpbu0vRr/tPQlamWwUqOr8GH24VAZTDX5sy7Q9LteebLByIYaBGw==', N'3a36d7b0-2069-43a0-8dc9-0e50218369a1', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com', N'guest')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'fc0822c8-c858-4baa-a17c-fb34ed8f2bcc', N'artist@gmail.com', 0, N'APGr02Do6x0ZtFyXqBv8E5vl7NrK+uTfeRyRy7gYkDSaX/dofIIjuSZRZeTgkel8uA==', N'b2894dc5-864f-4b78-b2aa-5fe2cdd3c9d0', NULL, 0, 0, NULL, 1, 0, N'artist@gmail.com', N'artist')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fc0822c8-c858-4baa-a17c-fb34ed8f2bcc', N'0779235e-2ea4-41f5-821d-6aabf531d521')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'108d3ef9-f121-48e1-8518-8be0b5e932c1', N'd5ddfc5a-4fd5-4c3d-8c97-db30a75838c8')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3f7aa941-8389-489e-8c40-dad2d403e515', N'fc67efce-88a8-4e5c-99fe-6f01b20c4690')



");
        }

        public override void Down()
        {
        }
    }
}
