﻿// <auto-generated />
using EFTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFTest.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190207194358_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Columns");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SDColumn");
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
                });

            modelBuilder.Entity("EFTest.Models.SDProject", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("HEX(RANDOMBLOB(16))");

                    b.HasKey("Id");

                    b.ToTable("Projects");
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
