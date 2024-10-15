using LabolatoriumASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabolatoriumASP.NET.Controllers;

public class CalculatorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Form()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Result([FromForm] Calculator model)
    {
        if (!model.IsValid())
        {
            return View("CustomError");
        }
        return View(model);
    }
}

public enum Operators
{
    Add, Sub, Mul, Div
}