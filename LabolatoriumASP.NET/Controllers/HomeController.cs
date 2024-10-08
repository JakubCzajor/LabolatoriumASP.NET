using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LabolatoriumASP.NET.Models;

namespace LabolatoriumASP.NET.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Age(DateTime birth, DateTime future)
    {
        if (future < birth)
        {
            ViewBag.ErrorMessage = "future nie może być wcześniej niż birth";
            return View("CustomError");
        }
        
        ViewBag.Birth = birth;
        ViewBag.Future = future;
        
        var daysDiff = (future - birth).Days;
        ViewBag.Result = daysDiff / 365;
        
        return View();
    }
    
    public IActionResult Calculator(Operator? op, double? a, double? b)
    {
        // var op = Request.Query["op"];
        // var a = double.Parse(Request.Query["a"]);
        // var b = double.Parse(Request.Query["b"]);

        if (a is null || b is null)
        {
            ViewBag.ErrorMessage = "Niepoprawny format liczby w parametrze a lub b";
            return View("CustomError");
        }

        if (op is null)
        {
            ViewBag.ErrorMessage = "Nieznany operator!";
            return View("CustomError");
        }
        
        ViewBag.A = a;
        ViewBag.B = b;
        
        switch (op)
        {
            case Operator.Add:
                ViewBag.Result = a + b;
                ViewBag.Op = "+";
                break;
            case Operator.Sub:
                ViewBag.Result = a - b;
                ViewBag.Op = "-";
                break;
            case Operator.Mul:
                ViewBag.Result = a * b;
                ViewBag.Op = "*";
                break;
            case Operator.Div:
                ViewBag.Result = a / b;
                ViewBag.Op = "/";
                break;
        }
        
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public enum Operator
{
    Add, Sub, Mul, Div
}