using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabolatoriumASP.NET.Models;

[Table("contacts")]
public class ContactEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(length: 20)]
    [MinLength(length: 2)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(length: 50)]
    [MinLength(length: 2)]
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public DateOnly BirthDate { get; set; }
    
    [Column("phone_number")]
    public string PhoneNumber { get; set; }
    
    public Category Category { get; set; }
    
    public DateTime Created { get; set; }
    
    public int OrganizationId {get; set;}
    public OrganizationEntity? Organization { get; set; }
}