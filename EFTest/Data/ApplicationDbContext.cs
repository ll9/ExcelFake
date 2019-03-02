using EFTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Data
{
    class ApplicationDbContext: DbContext
    {

        public DbSet<SDDataTable> SDDataTables { get; set; }
        public DbSet<SDColumn> SDColumns { get; set; }
        public DbSet<SDComboboxColumn> SDComboboxColumns { get; set; }
        public DbSet<SDTextBoxColumn> SDTextBoxColumns { get; set; }
        public DbSet<SDProject> SDProjects { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=db.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SDDataTable>().Property(s => s.Id).HasDefaultValueSql("HEX(RANDOMBLOB(16))");
            modelBuilder.Entity<SDComboboxColumn>().Property(s => s.Id).HasDefaultValueSql("HEX(RANDOMBLOB(16))");
            modelBuilder.Entity<SDColumn>().Property(s => s.Id).HasDefaultValueSql("HEX(RANDOMBLOB(16))");
            modelBuilder.Entity<SDTextBoxColumn>().Property(s => s.Id).HasDefaultValueSql("HEX(RANDOMBLOB(16))");
            modelBuilder.Entity<SDProject>().Property(s => s.Id).HasDefaultValueSql("HEX(RANDOMBLOB(16))");
        }
    }
}
