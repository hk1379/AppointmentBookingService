﻿// <auto-generated />
using System;
using AppointmentBooking.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppointmentBooking.Migrations
{
    [DbContext(typeof(AppointmentBookingDbContext))]
    partial class AppointmentBookingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppointmentBooking.Context.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EmailAddress")
                        .HasColumnOrder(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name")
                        .HasColumnOrder(1);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PhoneNumber")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("AppointmentBooking.Context.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CompanyName")
                        .HasColumnOrder(4);

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EmailAddress")
                        .HasColumnOrder(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name")
                        .HasColumnOrder(1);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PhoneNumber")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.ToTable("AppointmentCustomers");
                });

            modelBuilder.Entity("AppointmentBooking.Context.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description")
                        .HasColumnOrder(6);

                    b.Property<int>("Duration")
                        .HasColumnType("int")
                        .HasColumnName("Duration")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("FromDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("FromDateTime")
                        .HasColumnOrder(1);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Location")
                        .HasColumnOrder(5);

                    b.Property<bool?>("ReservationPaid")
                        .HasColumnType("bit")
                        .HasColumnName("ReservationPaid")
                        .HasColumnOrder(7);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Status")
                        .HasColumnOrder(4);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title")
                        .HasColumnOrder(3);

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("CompanyMeeting", b =>
                {
                    b.Property<int>("CompaniesId")
                        .HasColumnType("int");

                    b.Property<int>("MeetingsId")
                        .HasColumnType("int");

                    b.HasKey("CompaniesId", "MeetingsId");

                    b.HasIndex("MeetingsId");

                    b.ToTable("CompanyMeeting");
                });

            modelBuilder.Entity("CustomerMeeting", b =>
                {
                    b.Property<int>("CustomersId")
                        .HasColumnType("int");

                    b.Property<int>("MeetingsId")
                        .HasColumnType("int");

                    b.HasKey("CustomersId", "MeetingsId");

                    b.HasIndex("MeetingsId");

                    b.ToTable("CustomerMeeting");
                });

            modelBuilder.Entity("CompanyMeeting", b =>
                {
                    b.HasOne("AppointmentBooking.Context.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("CompaniesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppointmentBooking.Context.Models.Meeting", null)
                        .WithMany()
                        .HasForeignKey("MeetingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CustomerMeeting", b =>
                {
                    b.HasOne("AppointmentBooking.Context.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppointmentBooking.Context.Models.Meeting", null)
                        .WithMany()
                        .HasForeignKey("MeetingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
