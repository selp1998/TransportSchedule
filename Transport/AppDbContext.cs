using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Models;

namespace Transport
{
    class AppDbContext : DbContext
    {
        public AppDbContext()
           : base("DbConnection")
        { }
        public virtual DbSet<Autobus> Buses { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Trolleybus> Trolleys { get; set; }
        public virtual DbSet<AutobusStation> AutobusStations { get; set; }
        public virtual DbSet<Arrive> Arrives { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { }
    }
}
