
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkyPlanner.Entities;

namespace SkyPlanner.Configurations
{
    public class AirlineAirportConfiguration
    {
        public AirlineAirportConfiguration(EntityTypeBuilder<AirlineAirport> builder)
        {
            builder.HasKey(ft => new { ft.AirportId, ft.AirlineId });

            builder.HasOne(ft => ft.Airline)
                   .WithMany(a => a.AirlineAirports)
                   .HasForeignKey(ft => ft.AirlineId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ft => ft.Airport)
                   .WithMany(a => a.AirlineAirports)
                   .HasForeignKey(ft => ft.AirportId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
