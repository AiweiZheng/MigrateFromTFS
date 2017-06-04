using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class LoginConfiguration : EntityTypeConfiguration<Login>
    {
        public LoginConfiguration()
        {
            Property(l => l.Username)
                .IsRequired();

            Property(l => l.SessionId)
                .IsRequired();

            Property(l => l.Date)
                .IsRequired();
        }
    }
}