using Microsoft.EntityFrameworkCore;
using QUALA.Models;
using System.Collections.Generic;

namespace QUALA.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<NSucursales> Sucursales { get; set; }
        public DbSet<NMonedas> Monedas { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NSucursales>()
                .HasOne(s => s.Moneda)
                .WithMany()
                .HasForeignKey(s => s.MonedaId);
        }
    }
}