﻿// <auto-generated />
using EFTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFTest.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("EFTest.Models.SDColumn", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("HEX(RANDOMBLOB(16))");

                    b.Property<string>("DataType");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("SDDataTableId");

                    b.Property<bool>("Synchronize");

                    b.HasKey("Id");

                    b.HasIndex("SDDataTableId");

                    b.ToTable("SDColumns");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SDColumn");

                    b.HasData(
                        new
                        {
                            Id = "8b4cdb44-23ec-476a-8bb8-c8f446b17104",
                            DataType = "System.String",
                            Name = "stringCol",
                            SDDataTableId = "3aad8202-defd-444e-a11a-564beaef779b",
                            Synchronize = true
                        },
                        new
                        {
                            Id = "ae3756ad-b272-451f-9dac-2b5514d6ff37",
                            DataType = "System.Int32",
                            Name = "intCol",
                            SDDataTableId = "3aad8202-defd-444e-a11a-564beaef779b",
                            Synchronize = true
                        },
                        new
                        {
                            Id = "aab94d61-e949-4b81-b836-7a86a1aea192",
                            DataType = "System.DateTime",
                            Name = "dateTimeCol",
                            SDDataTableId = "3aad8202-defd-444e-a11a-564beaef779b",
                            Synchronize = true
                        });
                });

            modelBuilder.Entity("EFTest.Models.SDDataTable", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("HEX(RANDOMBLOB(16))");

                    b.Property<string>("Name");

                    b.Property<bool>("Synchronize");

                    b.HasKey("Id");

                    b.ToTable("SDDataTables");

                    b.HasData(
                        new
                        {
                            Id = "3aad8202-defd-444e-a11a-564beaef779b",
                            Name = "testtable",
                            Synchronize = true
                        });
                });

            modelBuilder.Entity("EFTest.Models.SDProject", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("HEX(RANDOMBLOB(16))");

                    b.HasKey("Id");

                    b.ToTable("SDProjects");

                    b.HasData(
                        new
                        {
                            Id = "f6e62472-c9b2-4737-8a8c-52bfb27fbe1e"
                        });
                });

            modelBuilder.Entity("EFTest.Models.SDStatus", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("SDStatuses");
                });

            modelBuilder.Entity("EFTest.Models.SDComboboxColumn", b =>
                {
                    b.HasBaseType("EFTest.Models.SDColumn");

                    b.Property<string>("ComboboxValues");

                    b.HasDiscriminator().HasValue("SDComboboxColumn");
                });

            modelBuilder.Entity("EFTest.Models.SDTextBoxColumn", b =>
                {
                    b.HasBaseType("EFTest.Models.SDColumn");

                    b.HasDiscriminator().HasValue("SDTextBoxColumn");
                });

            modelBuilder.Entity("EFTest.Models.SDColumn", b =>
                {
                    b.HasOne("EFTest.Models.SDDataTable")
                        .WithMany("Columns")
                        .HasForeignKey("SDDataTableId");
                });
#pragma warning restore 612, 618
        }
    }
}
