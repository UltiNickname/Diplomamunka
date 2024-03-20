﻿// <auto-generated />
using System;
using FoglalasAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoglalasAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240320105053_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FoglalasAPI.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CityId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("FoglalasAPI.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ReservationId"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("FinishedTime")
                        .HasColumnType("time without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Outdoor")
                        .HasColumnType("boolean");

                    b.Property<int>("RestaurantFK")
                        .HasColumnType("integer");

                    b.Property<bool>("SeperateRoom")
                        .HasColumnType("boolean");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.Property<int>("UserFK")
                        .HasColumnType("integer");

                    b.HasKey("ReservationId");

                    b.HasIndex("RestaurantFK");

                    b.HasIndex("UserFK");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("FoglalasAPI.Models.ReservedTables", b =>
                {
                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("ReservationId")
                        .HasColumnType("integer");

                    b.Property<int>("TableId")
                        .HasColumnType("integer");

                    b.HasIndex("ReservationId");

                    b.HasIndex("TableId");

                    b.ToTable("ReservedTables");
                });

            modelBuilder.Entity("FoglalasAPI.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RestaurantId"));

                    b.Property<bool>("AnimalFriendly")
                        .HasColumnType("boolean");

                    b.Property<int>("CityFK")
                        .HasColumnType("integer");

                    b.Property<bool>("ClosedOnMonday")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClosedOnSunday")
                        .HasColumnType("boolean");

                    b.Property<TimeOnly>("Closing")
                        .HasColumnType("time without time zone");

                    b.Property<bool>("FixedTables")
                        .HasColumnType("boolean");

                    b.Property<TimeOnly>("KitchenClosing")
                        .HasColumnType("time without time zone");

                    b.Property<bool>("Menu")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<TimeOnly>("Opening")
                        .HasColumnType("time without time zone");

                    b.Property<bool>("Outdoor")
                        .HasColumnType("boolean");

                    b.Property<bool>("SeperateRoom")
                        .HasColumnType("boolean");

                    b.Property<bool>("SzepKartyaAvailable")
                        .HasColumnType("boolean");

                    b.HasKey("RestaurantId");

                    b.HasIndex("CityFK");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("FoglalasAPI.Models.RestaurantTables", b =>
                {
                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<int>("TableId")
                        .HasColumnType("integer");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.ToTable("RestaurantTables");
                });

            modelBuilder.Entity("FoglalasAPI.Models.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TableId"));

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.HasKey("TableId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("FoglalasAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FoglalasAPI.Models.Reservation", b =>
                {
                    b.HasOne("FoglalasAPI.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoglalasAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoglalasAPI.Models.ReservedTables", b =>
                {
                    b.HasOne("FoglalasAPI.Models.Reservation", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoglalasAPI.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("FoglalasAPI.Models.Restaurant", b =>
                {
                    b.HasOne("FoglalasAPI.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("FoglalasAPI.Models.RestaurantTables", b =>
                {
                    b.HasOne("FoglalasAPI.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoglalasAPI.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("Table");
                });
#pragma warning restore 612, 618
        }
    }
}
