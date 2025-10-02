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
    public class CongestionScheduleConfiguration : IEntityTypeConfiguration<CongestionSchedule>
    {
        public void Configure(EntityTypeBuilder<CongestionSchedule> builder)
        {
            builder.Property(c => c.LineId)
                .IsRequired();

            builder.Property(c => c.ObservationTime)
                .IsRequired();
        }
    }
}
