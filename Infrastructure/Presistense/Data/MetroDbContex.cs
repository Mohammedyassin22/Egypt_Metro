using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Modules;
using Microsoft.EntityFrameworkCore;

namespace Presistense.Data
{
    public class MetroDbContex:DbContext
    {
        public MetroDbContex(DbContextOptions<MetroDbContex> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemplyRefernce).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Ticket_Prices> Ticket_Prices { get; set; }
        public DbSet<Station_Name> Station_Names { get; set; }
        public DbSet<Station_Coordinates> Station_Coordinates { get; set; }
        public DbSet<Line_Name> Line_Names { get; set; }
        public DbSet<Stations_Lines> Stations_Lines { get; set; }
        public DbSet<Chatbot> chatbots { get; set; }
        public DbSet<Faults> Faults { get; set; }
        public DbSet<Rush_Times> Rush_Times { get; set;}


    }
}
