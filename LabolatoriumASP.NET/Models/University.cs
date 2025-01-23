using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabolatoriumASP.NET.Models;

public partial class University
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    [Display(Name = "University")]
    public string? UniversityName { get; set; }

    public virtual Country? Country { get; set; }
}
