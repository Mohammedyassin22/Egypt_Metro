using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistense.Data.Configuration
{
    public class RushTimeConfigurations : IEntityTypeConfiguration<Rush_Times>
    {
        public void Configure(EntityTypeBuilder<Rush_Times> builder)
        {
            builder.HasOne(r => r.Station_Name)
               .WithMany()
               .HasForeignKey(r => r.Station_NameId)
               .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(r => r.Line)
                   .WithMany()
                   .HasForeignKey(r => r.LineId)
                   .OnDelete(DeleteBehavior.Restrict); // ✅
        }
    }
}
