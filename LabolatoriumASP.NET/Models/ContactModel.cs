using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LabolatoriumASP.NET.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Musisz podać imię!")]
    [MaxLength(length: 20, ErrorMessage = "Imię nie może być dłuższe niż 20 znaków!")]
    [MinLength(length: 2)]
    [Display(Name = "Imię")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Musisz podać nazwisko!")]
    [MaxLength(length: 50, ErrorMessage = "Nazwisko nie może być dłuższe niż 50 znaków!")]
    [MinLength(length: 2)]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }
    
    [EmailAddress]
    [Display(Name = "Adres Email")]
    public string Email { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Data urodzenia")]
    public DateOnly BirthDate { get; set; }
    
    [Phone]
    [RegularExpression(@"\d\d\d \d\d\d \d\d\d", ErrorMessage = "Wpisz numer wg wzoru: xxx xxx xxx")]
    [Display(Name = "Numer telefonu")]
    public string PhoneNumber { get; set; }
    
    [Display(Name = "Kategoria")]
    public Category Category { get; set; }
}