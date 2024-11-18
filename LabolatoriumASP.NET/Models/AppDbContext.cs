using Microsoft.EntityFrameworkCore;

namespace LabolatoriumASP.NET.Models;

public class AppDbContext :DbContext
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