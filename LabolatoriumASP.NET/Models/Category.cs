using System.ComponentModel.DataAnnotations;

namespace LabolatoriumASP.NET.Models;

public enum Category
{
    [Display(Name = "Rodzina", Order = 1)]
    Family,
    [Display(Name = "Znajomi", Order = 3)]
    Friend,
    [Display(Name = "Kontakt zawodowy", Order = 2)]
    Business
}