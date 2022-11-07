using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace RealEstateAgency.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            string admin_RoleId = "3d128fbb-2270-4b17-baf9-80a4eb7f9875";
            string admin_RoleConcurrencyStamp = "a4c871a1-73cf-428f-90f7-a273ccd4740c";
            string user_RoleId = "c368d1b0-d46a-4b51-b737-c016faa91e31";
            string user_RoleConcurrencyStamp = "f67a998b-7268-4e31-ae32-370aaaf10efc";

            string admin_UserId = "7b9556cf-4db8-42c4-86bc-2abebc218ce9";
            string admin_UserConcurrencyStamp = "4f743e54-0a01-46e1-a1bf-6f013f406211";

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = admin_RoleId,
                ConcurrencyStamp = admin_RoleConcurrencyStamp
            },
            new IdentityRole
            {
                Name = "user",
                NormalizedName = "USER",
                Id = user_RoleId,
                ConcurrencyStamp = user_RoleConcurrencyStamp
            });

            var adminUser = new IdentityUser
            {
                Id = admin_UserId,
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = false,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                ConcurrencyStamp = admin_UserConcurrencyStamp
            };

            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin_00");

            builder.Entity<IdentityUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = admin_RoleId,
                UserId = admin_UserId
            });

            base.OnModelCreating(builder);
        }
    }
}