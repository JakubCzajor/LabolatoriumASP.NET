namespace LabolatoriumASP.NET.Models;

public class UniversityDetailsViewModel
{
    public int UniversityId { get; set; }
    public string UniversityName { get; set; }
    public string? SelectedRankingSystem { get; set; }
    public int? SelectedYear { get; set; }
    public string? SearchTerm { get; set; }
    public IEnumerable<string> RankingSystems { get; set; }
    public IEnumerable<int> Years { get; set; }
    public IEnumerable<UniversityRankingYear> FilteredRankings { get; set; }
}