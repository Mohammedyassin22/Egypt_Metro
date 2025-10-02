using Domain.Modules;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistense.Data.Configuration
{
    public class StationCoordinatesConfiguration : IEntityTypeConfiguration<Station_Coordinates>
    {
        public void Configure(EntityTypeBuilder<Station_Coordinates> builder)
        {
            builder.HasKey(c => c.Station);

            builder.Property(c => c.Latitude)
                .IsRequired();

            builder.Property(c => c.Longitude)
                .IsRequired();
        }
    }
}
