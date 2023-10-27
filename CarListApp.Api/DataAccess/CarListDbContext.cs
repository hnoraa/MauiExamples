using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarListApp.Api.DataAccess
{
    public class CarListDbContext : IdentityDbContext
    {
        public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // create some seed data
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Make = "Ford",
                    Model = "Focus",
                    Vin = "1231231231"
                }
            );

            // create some roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "3A484B50-B144-4DC6-9D7E-57FD90C8FD1D",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "7BF4A632-C22F-4053-A1B5-B60AA823EB5D",
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            // create some users - hashed password
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "40ACB6F7-4406-4044-9E8C-EFE106A12588",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    UserName = "admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "Password"),
                    EmailConfirmed = true
                },
                new IdentityUser
                {
                    Id = "F280DF35-0E50-4290-B92A-069FCD9015E5",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    UserName = "user@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "Password"),
                    EmailConfirmed = true
                }
            );

            // assign users to roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    // admin
                    RoleId = "3A484B50-B144-4DC6-9D7E-57FD90C8FD1D",
                    UserId = "40ACB6F7-4406-4044-9E8C-EFE106A12588"
                },
                new IdentityUserRole<string>
                {
                    // user
                    RoleId = "7BF4A632-C22F-4053-A1B5-B60AA823EB5D",
                    UserId = "F280DF35-0E50-4290-B92A-069FCD9015E5"
                }
            );
        }
    }
}
