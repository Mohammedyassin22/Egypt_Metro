using Domain.Modules;
using Domain.Modules.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presistense.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistense.Identity
{
    using Domain; // لو الكلاسات AppUsers و Address موجودة في Domain
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class UsersDbContext : IdentityDbContext<AppUsers>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemplyRefernce).Assembly);

            modelBuilder.Entity<Address>().ToTable("Addresses");

            modelBuilder.Entity<AppUsers>()
                 .HasOne(u => u.Address)
                 .WithOne()
                 .HasForeignKey<Address>(a => a.AppUserId)
                 .IsRequired();

        }
        public DbSet<Address> Addresses { get; set; }
    }

}
