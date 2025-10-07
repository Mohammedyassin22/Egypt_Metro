using Domain.Modules;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Presistense.Data.Configuration
{
    public class StationConfiguration : IEntityTypeConfiguration<Station_Name>
    {
        public void Configure(EntityTypeBuilder<Station_Name> builder)
        {
            builder.HasOne(s => s.Coordinates)
              .WithOne(c => c.Station)
              .HasForeignKey<Station_Coordinates>(c => c.StationId)
              .OnDelete(DeleteBehavior.Restrict); // ✅

            builder.HasMany(s => s.StationsLines)
                   .WithOne(sl => sl.Station)
                   .HasForeignKey(sl => sl.StationNameId)
                   .OnDelete(DeleteBehavior.Restrict); // ✅

            builder.Property(s => s.StationName)
                   .IsRequired()
                   .HasMaxLength(100);


        }
    }
}
