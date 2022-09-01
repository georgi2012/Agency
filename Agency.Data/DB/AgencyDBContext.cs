using System;
using System.Collections.Generic;
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Agency.Data.DBSeed;
using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.Data.DB
{
    public partial class AgencyDBContext : DbContext
    {
        public AgencyDBContext()
        {
        }

        public AgencyDBContext(DbContextOptions<AgencyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Journey> Journeys { get; set; }
        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Airplane> Airplanes { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<Boat> Boats { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\AgencyDB;Database=AgencyDB;Trusted_Connection=True;");
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            OnModelCreatingPartial(modelBuilder);
            //seed
            //modelBuilder.Seeder();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
