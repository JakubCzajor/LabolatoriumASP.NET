using System.ComponentModel.DataAnnotations;

namespace LabolatoriumASP.NET.Models;

public class AddRankingNoteViewModel
{
    [Required]
    public int UniversityId { get; set; }

    [Required]
    [Range(2017, 9999, ErrorMessage = "Year must be above 2016.")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Ranking system is required.")]
    public int RankingSystemId { get; set; }

    [Required(ErrorMessage = "Criteria is required.")]
    public int CriteriaId { get; set; }

    [Required]
    [Range(0, 1000, ErrorMessage = "Score must be between 0 and 1000.")]
    public int Score { get; set; }

    public IEnumerable<RankingSystem> RankingSystems { get; set; } = new List<RankingSystem>();
}
