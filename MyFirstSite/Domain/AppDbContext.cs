
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFirstSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstSite.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TextField> TextFields { get; set; } //Таблица с именем в БД
        public DbSet<ServiceItem> ServiceItems { get; set; } //Вторая таблица с именем в БД

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "4E89F435-FE3D-4131-91EF-4823FECDF34D",
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "DA8E4E71-BB7E-4AF3-8DED-8842DD17A32C",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "sapog@vaska.com",
                NormalizedEmail = "SAPOG@VASKA.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            }) ;
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "4E89F435-FE3D-4131-91EF-4823FECDF34D",
                UserId = "DA8E4E71-BB7E-4AF3-8DED-8842DD17A32C"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("CBBE7CA1-0B25-4440-AB9C-81BC4831FEF3"),
                CodeWord = "PageIndex",
                Title = "Главная"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("5C9653FD-15A7-4F46-BB28-C3310E4824FF"),
                CodeWord = "PageServices",
                Title = "Главная"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("550A4F12-5598-4A00-AF03-54AC6D530677"),
                CodeWord = "PageContaxt",
                Title = "Главная"
            });
        }

        




    }
}
