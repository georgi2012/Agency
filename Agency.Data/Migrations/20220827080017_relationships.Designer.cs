﻿// <auto-generated />
using System;
using Agency.Data.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Agency.Data.Migrations
{
    [DbContext(typeof(AgencyDBContext))]
    [Migration("20220827080017_relationships")]
    partial class relationships
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Agency.Data.Models.Contracts.Journey", b =>
                {
                    b.Property<Guid>("JourneyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.Property<string>("StartLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleID")
                        .HasColumnType("int");

                    b.HasKey("JourneyID");

                    b.HasIndex("VehicleID");

                    b.ToTable("Journeys");
                });

            modelBuilder.Entity("Agency.Data.Models.Contracts.Ticket", b =>
                {
                    b.Property<Guid>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AdministrativeCosts")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("JourneyID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TicketID");

                    b.HasIndex("JourneyID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Agency.Data.Models.Vehicles.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleID"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassangerCapacity")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerKilometer")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("VehicleID");

                    b.ToTable("Vehicles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Vehicle");
                });

            modelBuilder.Entity("Agency.Data.Models.Vehicles.Models.Airplane", b =>
                {
                    b.HasBaseType("Agency.Data.Models.Vehicles.Models.Vehicle");

                    b.Property<bool>("HasFreeFood")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Airplane");
                });

            modelBuilder.Entity("Agency.Data.Models.Vehicles.Models.Boat", b =>
                {
                    b.HasBaseType("Agency.Data.Models.Vehicles.Models.Vehicle");

                    b.Property<bool>("OffersWaterSports")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Boat");
                });

            modelBuilder.Entity("Agency.Data.Models.Vehicles.Models.Bus", b =>
                {
                    b.HasBaseType("Agency.Data.Models.Vehicles.Models.Vehicle");

                    b.HasDiscriminator().HasValue("Bus");
                });

            modelBuilder.Entity("Agency.Data.Models.Vehicles.Models.Train", b =>
                {
                    b.HasBaseType("Agency.Data.Models.Vehicles.Models.Vehicle");

                    b.Property<int>("Carts")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Train");
                });

            modelBuilder.Entity("Agency.Data.Models.Contracts.Journey", b =>
                {
                    b.HasOne("Agency.Data.Models.Vehicles.Models.Vehicle", "Vehicle")
                        .WithMany("Journeys")
                        .HasForeignKey("VehicleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Agency.Data.Models.Contracts.Ticket", b =>
                {
                    b.HasOne("Agency.Data.Models.Contracts.Journey", "Journey")
                        .WithMany("Tickets")
                        .HasForeignKey("JourneyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Journey");
                });

            modelBuilder.Entity("Agency.Data.Models.Contracts.Journey", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Agency.Data.Models.Vehicles.Models.Vehicle", b =>
                {
                    b.Navigation("Journeys");
                });
#pragma warning restore 612, 618
        }
    }
}
