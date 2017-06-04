using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistence
{
    public class VenueConfiguration : EntityTypeConfiguration<Venue>
    {
        public VenueConfiguration()
        {
            Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}