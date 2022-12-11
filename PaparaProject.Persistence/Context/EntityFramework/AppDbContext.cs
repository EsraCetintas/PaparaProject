using Microsoft.EntityFrameworkCore;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Persistence.Context.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Böylece tüm konfigürasyon dosyalarını reflection ile verdik.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ProjectPaparaDb;Trusted_Connection=True;");
        }

        public DbSet<Flat> Flats { get; set; }
        public DbSet<FlatType> FlatTypes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Dues> Dues { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
