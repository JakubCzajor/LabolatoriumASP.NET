using LabolatoriumASP.NET.Models;
using LabolatoriumASP.NET.Services;
using LabolatoriumASP.NET.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LabolatoriumASP.NET.Controllers;

public class UniversityController : Controller
{
    private readonly IUniversityService _universityService;
    
    public UniversityController(IUniversityService universityService)
    {
        _universityService = universityService;
    }
    
    public async Task<IActionResult> Index(string? search, int page = 1, int size = 20)
    {
        var universities = await _universityService.GetAll();
        
        if (!string.IsNullOrEmpty(search))
        {
            universities = universities
                .Where(u => u.UniversityName.Contains(search, StringComparison.OrdinalIgnoreCase)
                            || u.Country.CountryName.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        var totalUniversities = universities.Count();
        var paginatedUniversities = universities
            .OrderBy(u => u.UniversityName)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

        ViewBag.SearchTerm = search;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalUniversities / (double)size);
        ViewBag.PageSize = size;

        return View(paginatedUniversities);
    }
    
    public async Task<IActionResult> Details(int id, string? rankingSystem, int? year, string? search, int page = 1, int size = 10)
    {
        var rankingSystems = await _universityService.GetRankingSystems();
        var years = await _universityService.GetRankingYears(id);
    
        var rankingSystemsNames = rankingSystems
            .Select(rs => rs.SystemName)
            .ToList();
        
        var universityRankings = await _universityService.GetUniversityDetails(id);
        
        if (!string.IsNullOrEmpty(rankingSystem))
        {
            universityRankings = universityRankings
                .Where(r => r.RankingCriteria?.RankingSystem?.SystemName == rankingSystem);
        }

        if (year.HasValue)
        {
            universityRankings = universityRankings
                .Where(r => r.Year == year.Value);
        }
        
        var totalUniversityRankings = universityRankings.Count();
        
        var paginatedUniversities = universityRankings
            .Skip((page - 1) * size)
            .Take(size);

        var viewModel = new UniversityDetailsViewModel
        {
            UniversityId = id,
            UniversityName = universityRankings.FirstOrDefault()?.University.UniversityName,
            RankingSystems = rankingSystemsNames,
            Years = years,
            SelectedRankingSystem = rankingSystem,
            SelectedYear = year,
            SearchTerm = search,
            FilteredRankings = paginatedUniversities.ToList()
        };
        
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalUniversityRankings / (double)size);
        ViewBag.PageSize = size;

        return View(viewModel);
    }
    
    public async Task<IActionResult> CreateRankingNote(int universityId)
    {
        var rankingSystems = await _universityService.GetRankingSystems();
        var model = new AddRankingNoteViewModel
        {
            UniversityId = universityId,
            RankingSystems = rankingSystems,
            Year = DateTime.Now.Year
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRankingNote(AddRankingNoteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.RankingSystems = await _universityService.GetRankingSystems();
            return View(model);
        }
        
        await _universityService.AddRankingNote(new UniversityRankingYear()
        {
            UniversityId = model.UniversityId,
            Year = model.Year,
            Score = model.Score,
            RankingCriteriaId = model.CriteriaId
        });

        return RedirectToAction("Details", "University", new { id = model.UniversityId });
    }
    
    public async Task<IActionResult> GetCriteria(int rankingSystemId)
    {
        if (rankingSystemId <= 0)
            return BadRequest("Invalid ranking system ID.");

        var criteria = await _universityService.GetCriteriaByRankingSystemId(rankingSystemId);

        if (!criteria.Any())
            return NotFound("No criteria found for the selected ranking system.");

        return Json(criteria.Select(c => new
        {
            id = c.Id,
            criteriaName = c.CriteriaName
        }));
    }

}