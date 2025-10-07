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
            builder.HasOne(c => c.StationName)
                  .WithMany()
                  .HasForeignKey(c => c.StationNameId)
                  .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
