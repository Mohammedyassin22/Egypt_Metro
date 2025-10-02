using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Modules;

namespace Presistense.Data.Configuration
{
    public class LineConfiguration : IEntityTypeConfiguration<Line_Name>
    {
        public void Configure(EntityTypeBuilder<Line_Name> builder)
        {
            builder.HasKey(l => l.LineName);
            builder.HasMany(l => l.Faults)
              .WithOne(f => f.Line)              
              .HasForeignKey(f => f.LineId);     

        }
    }
}
