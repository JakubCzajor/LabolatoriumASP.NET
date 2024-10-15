namespace LabolatoriumASP.NET.Models;

public class Birth
{
    public string? FirstName { get; set; }
    public DateTime BirthDate { get; set; }

    public bool IsValid()
    {
        return FirstName != null && BirthDate < DateTime.Today && BirthDate != default;
    }

    public int CalculateAge()
    {
        return DateTime.Today.Year - BirthDate.Year;
    }
}