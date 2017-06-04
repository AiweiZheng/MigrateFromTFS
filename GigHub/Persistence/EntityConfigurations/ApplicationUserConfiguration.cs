using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.Description)
                .HasMaxLength(1000);

            Property(u => u.Activated)
                .IsRequired();

            HasMany(u => u.Followers)
                 .WithRequired(f => f.Followee)
                 .WillCascadeOnDelete(false);

            HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

        }
    }
}