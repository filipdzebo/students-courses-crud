using Microsoft.EntityFrameworkCore;
using Page_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<StatusStudenta> Status_Studenta { get; set; }

        public DbSet<Student> Studenti { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Kurs> Kursevi { get; set; }
        public DbSet<KursStudent> KursStudenti { get; set; }

        public virtual DbSet<StudentViewModel> Detalji { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KursStudent>()
                .HasKey(ks => new { ks.KursId, ks.StudentId });
            modelBuilder.Entity<Korisnik>()
                .Property(korisnik => korisnik.Role).HasDefaultValue("Korisnik");
        }
    }
}
