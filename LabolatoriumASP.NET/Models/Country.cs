using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabolatoriumASP.NET.Models;

public partial class Country
{
    public int Id { get; set; }

    [Display(Name = "Country")]
    public string? CountryName { get; set; }

    public virtual ICollection<University> Universities { get; set; } = new List<University>();
}
