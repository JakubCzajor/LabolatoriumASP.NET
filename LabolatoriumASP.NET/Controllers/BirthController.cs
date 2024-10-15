using LabolatoriumASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabolatoriumASP.NET.Controllers;

public class BirthController : Controller
{
 
    public IActionResult Form()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Result([FromForm] Birth model)
    {
        if (!model.IsValid())
        {
            return View("CustomError");
        }
        return View(model);
    }
}