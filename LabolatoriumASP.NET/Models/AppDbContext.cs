using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LabolatoriumASP.NET.Models;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    private string DbPath { get; set; }
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        string USER_ID = Guid.NewGuid().ToString();
        string ADMIN_ID = Guid.NewGuid().ToString();
        string USER_ROLE_ID = Guid.NewGuid().ToString();
        string ADMIN_ROLE_ID = Guid.NewGuid().ToString();

        modelBuilder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole()
                {
                    Id = USER_ROLE_ID,
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = USER_ROLE_ID
                },
                new IdentityRole()
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = ADMIN_ROLE_ID
                }
            );

        var user = new IdentityUser()
        {
            Id = USER_ID,
            Email = "adam@wsei.edu.pl",
            NormalizedEmail = "ADAM@WSEI.EDU.PL",
            UserName = "Adam",
            NormalizedUserName = "ADAM",
            EmailConfirmed = true
        };
        
        var admin = new IdentityUser()
        {
            Id = ADMIN_ID,
            Email = "admin@wsei.edu.pl",
            NormalizedEmail = "ADMIN@WSEI.EDU.PL",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            EmailConfirmed = true
        };
        
        PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = hasher.HashPassword(user, "1234!");
        admin.PasswordHash = hasher.HashPassword(admin, "4321!");
        
        modelBuilder.Entity<IdentityUser>().HasData(user);
        modelBuilder.Entity<IdentityUser>().HasData(admin);

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = USER_ID
                }
            );
        
        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("organizations")
            .HasData(
                new OrganizationEntity
                {
                    Id = 1,
                    Name = "WSEI",
                    NIP = "123456789",
                    REGION = "123456789"
                },
                new OrganizationEntity
                {
                    Id = 2,
                    Name = "PKP",
                    NIP = "987654321",
                    REGION = "987654321"
                });
        
        modelBuilder.Entity<OrganizationEntity>().OwnsOne(e => e.Address)
            .HasData(
                new{OrganizationEntityId = 1, City = "Kraków", Street = "św. Filipa 17"},
                new{OrganizationEntityId = 2, City = "Warszawa", Street = "Dworcowa 9"});
        
        modelBuilder.Entity<ContactEntity>()
            .HasOne<OrganizationEntity>(c => c.Organization)
            .WithMany(o => o.Contacts)
            .HasForeignKey(c => c.OrganizationId);
        
        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateOnly(1980, 1, 1),
                    PhoneNumber = "08888888888",
                    Email = "johndoe@gmail.com",
                    Category = Category.Business,
                    Created = DateTime.Now,
                    OrganizationId = 1
                },
                new ContactEntity()
                {
                    Id = 2,
                    FirstName = "Ala",
                    LastName = "Makota",
                    BirthDate = new DateOnly(2000, 3, 10),
                    PhoneNumber = "123456789",
                    Email = "alamakota@gmail.com",
                    Category = Category.Family,
                    Created = DateTime.Now,
                    OrganizationId = 2
                }
            );
    }
}