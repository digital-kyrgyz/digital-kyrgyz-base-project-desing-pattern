using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            PasswordHasher<AppUser> passHasher = new PasswordHasher<AppUser>();
            
            const string ADMIN_USER_ID = "22e40406-8a9d-2d82-912c-5d6a640ee696";
            const string ADMIN_ROLE_ID = "b421e928-0613-9ebd-a64c-f10b6a706e73";
            
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_USER_ID
            });
            
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ADMIN_USER_ID,
                Name = "user",
                NormalizedName = "USER"
            });

            var user1 = new AppUser
            {
                Id = ADMIN_ROLE_ID,
                UserName = "Melis",
                NormalizedUserName = "MELIS",
                Email = "melis.archabaev.kg@gmail.com",
                NormalizedEmail = "MELIS.ARCHABAEV.KG@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "",
                SecurityStamp = "I5MOLV6IDX2DRGZMNIQ6KEUQKW3QIG3A",
                ConcurrencyStamp = "c4736b7b-4dcf-be6b-8b03-e299b4836146"
            };
            
            var user2 = new AppUser
            {
                Id = ADMIN_USER_ID,
                UserName = "Ali",
                NormalizedUserName = "ALI",
                Email = "ali@gmail.com",
                NormalizedEmail = "Ali@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "",
                SecurityStamp = "I5MOLV6IDX2DRGZMNIQ6KEUQKW3QIG3A",
                ConcurrencyStamp = "c4736b7b-4dcf-be6b-8b03-e299b4836146"
            };
            
            user1.PasswordHash = passHasher.HashPassword(user1, "123");
            user2.PasswordHash = passHasher.HashPassword(user2, "123");
            
            builder.Entity<AppUser>().HasData(user1);
            builder.Entity<AppUser>().HasData(user2);
        }
    }
}