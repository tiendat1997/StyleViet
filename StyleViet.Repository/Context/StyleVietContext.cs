using Microsoft.EntityFrameworkCore;
using StyleViet.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Repository.Context
{
    public class StyleVietContext : DbContext
    {
        public StyleVietContext(DbContextOptions<StyleVietContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=DATTTSE62330;Database=StyleVietConn;User ID=sa;Password=tiendat");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccountRole>()
                .HasKey(a => new { a.AccountId, a.RoleId });

            builder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(a => a.AccountRoles)
                .HasForeignKey(f => f.AccountId);

            builder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(a => a.AccountRoles)
                .HasForeignKey(f => f.RoleId);

            builder.Entity<SalonService>()
                .HasKey(a => new { a.SalonId, a.ServiceId });

            builder.Entity<SalonService>()
                .HasOne(ss => ss.Salon)
                .WithMany(s => s.SalonService)
                .HasForeignKey(f => f.SalonId);

            builder.Entity<SalonService>()
                .HasOne(ss => ss.Service)
                .WithMany(s => s.SalonService)
                .HasForeignKey(f => f.ServiceId);

            base.OnModelCreating(builder);
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<SalonService> SalonService { get; set; }
        public DbSet<Gallery> Gallery { get; set; }        
    }
}
