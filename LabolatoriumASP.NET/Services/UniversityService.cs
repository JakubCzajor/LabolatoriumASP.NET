using LabolatoriumASP.NET.Models;
using LabolatoriumASP.NET.Services.IService;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LabolatoriumASP.NET.Services;

public class UniversityService : IUniversityService
{
    private readonly AppDbContext _context;

    public UniversityService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<University>> GetAll()
    {
        return await _context.Universities
            .Include(u => u.Country)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<UniversityRankingYear>> GetUniversityDetails(int universityId)
    {
        return await _context.UniversityRankingYears
            .Where(ury => ury.UniversityId == universityId)
            .Include(ury => ury.University)
            .Include(ury => ury.RankingCriteria)
            .ThenInclude(rc => rc.RankingSystem)
            .OrderByDescending(u => u.Year)
            .ThenBy(u => u.University.UniversityName)
            .ToListAsync();
    }

    public async Task<IEnumerable<RankingSystem>> GetRankingSystems()
    {
        return await _context.RankingSystems
            .ToListAsync();
    }

    public async Task<IEnumerable<int>> GetRankingYears(int universityId)
    {
        return await _context.UniversityRankingYears
            .Where(ury => ury.UniversityId == universityId && ury.Year.HasValue)
            .Select(ury => ury.Year.Value)
            .Distinct()
            .OrderBy(y => y)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<RankingCriterion>> GetCriteriaByRankingSystemId(int rankingSystemId)
    {
        return await _context.RankingCriteria
            .Where(c => c.RankingSystemId == rankingSystemId)
            .ToListAsync();
    }

    public async Task AddRankingNote(UniversityRankingYear note)
    {
        _context.UniversityRankingYears.Add(note);
        await _context.SaveChangesAsync();
    }
}