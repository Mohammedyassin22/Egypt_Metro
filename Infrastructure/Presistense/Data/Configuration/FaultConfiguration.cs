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
    public class FaultConfiguration : IEntityTypeConfiguration<Faults>
    {
        public void Configure(EntityTypeBuilder<Faults> builder)
        {
            builder.Property(f => f.EndTime)
             .IsRequired(false);

            builder.HasOne(f => f.Line)
                   .WithMany(l => l.Faults)
                   .HasForeignKey(f => f.LineId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
