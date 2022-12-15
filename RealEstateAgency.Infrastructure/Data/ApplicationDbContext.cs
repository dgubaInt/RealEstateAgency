using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Core.Entities;

namespace RealEstateAgency.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AgentUser, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string admin_RoleId = "3d128fbb-2270-4b17-baf9-80a4eb7f9875";
            string admin_RoleConcurrencyStamp = "a4c871a1-73cf-428f-90f7-a273ccd4740c";
            string user_RoleId = "c368d1b0-d46a-4b51-b737-c016faa91e31";
            string user_RoleConcurrencyStamp = "f67a998b-7268-4e31-ae32-370aaaf10efc";

            string admin_UserId = "7b9556cf-4db8-42c4-86bc-2abebc218ce9";
            string admin_UserConcurrencyStamp = "4f743e54-0a01-46e1-a1bf-6f013f406211";

            builder.Entity<Role>().HasData(new Role
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = Guid.Parse(admin_RoleId),
                ConcurrencyStamp = admin_RoleConcurrencyStamp
            },
            new Role
            {
                Name = "user",
                NormalizedName = "USER",
                Id = Guid.Parse(user_RoleId),
                ConcurrencyStamp = user_RoleConcurrencyStamp
            });

            var adminUser = new AgentUser
            {
                Id = Guid.Parse(admin_UserId),
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = false,
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                ConcurrencyStamp = admin_UserConcurrencyStamp,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<AgentUser> passwordHasher = new PasswordHasher<AgentUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin_00");

            builder.Entity<AgentUser>().HasData(adminUser);

            builder.Entity<UserRole>().HasData(new UserRole
            {
                RoleId = Guid.Parse(admin_RoleId),
                UserId = Guid.Parse(admin_UserId)
            });

            var rentCategory = new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = "Rent",
                CreatedDate = DateTime.Now,
                ParentCategory = null,
                Position = 0
            };

            var buyCategory = new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = "Buy",
                CreatedDate = DateTime.Now,
                ParentCategory = null,
                Position = 0
            };

            builder.Entity<Category>().HasData(rentCategory, buyCategory);

            builder.Entity<Estate>()
                .HasMany(e => e.EstateOptions)
                .WithMany(o => o.Estates);

            builder.Entity<Estate>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Estates)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Estate>()
                .HasOne(e => e.AgentUser)
                .WithMany(c => c.Estates)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Estate>()
                .HasOne(e => e.Zone)
                .WithMany(c => c.Estates)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Estate>()
                .HasOne(e => e.EstateCondition)
                .WithMany(c => c.Estates)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Estate>()
                .HasOne(e => e.BuildingPlan)
                .WithMany(c => c.Estates)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Estate>()
                .HasOne(e => e.BuildingType)
                .WithMany(c => c.Estates)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Estate> Estates { get; set; }
        public DbSet<EstateOption> EstateOptions { get; set; }
        public DbSet<EstateCondition> EstateConditions { get; set; }
        public DbSet<BuildingPlan> BuildingPlans { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Zone> Zones { get; set; }
    }
}