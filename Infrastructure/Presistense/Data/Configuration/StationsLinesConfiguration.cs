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
    public class StationsLinesConfiguration : IEntityTypeConfiguration<Stations_Lines>
    {
        public void Configure(EntityTypeBuilder<Stations_Lines> builder)
        {
            builder.HasKey(sl => new { sl.Station, sl.LineId });

            builder.HasOne(sl => sl.Station)
                .WithMany(s => s.StationsLines)
                .HasForeignKey(sl => sl.Station);

            builder.HasOne(sl => sl.Line)
                .WithMany(l => l.StationsLines)
                .HasForeignKey(sl => sl.LineId);

            builder.Property(sl => sl.OrderInLine)
                .IsRequired();
        }
    }
        
    }
    
