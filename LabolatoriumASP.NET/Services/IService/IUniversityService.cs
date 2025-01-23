using LabolatoriumASP.NET.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LabolatoriumASP.NET.Services.IService;

public interface IUniversityService
{
    Task<List<University>> GetAll();
    Task<IEnumerable<UniversityRankingYear>> GetUniversityDetails(int universityId);
    Task<IEnumerable<RankingSystem>> GetRankingSystems();
    Task<IEnumerable<int>> GetRankingYears(int universityId);
    Task<IEnumerable<RankingCriterion>> GetCriteriaByRankingSystemId(int rankingSystemId);
    Task AddRankingNote(UniversityRankingYear note);
}