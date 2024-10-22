using LabolatoriumASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabolatoriumASP.NET.Controllers;

public class ContactController : Controller
{
    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1, new ContactModel()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@gmail.com",
                BirthDate = new DateOnly(2003, 08, 02),
                PhoneNumber = "123 456 789"
            }
        },
        {
            2, new ContactModel()
            {
                Id = 2,
                FirstName = "Ala",
                LastName = "Makota",
                Email = "ala.makota@gmail.com",
                BirthDate = new DateOnly(2008, 12, 05),
                PhoneNumber = "987 654 321"
            }
        },
        {
            3, new ContactModel()
            {
                Id = 3,
                FirstName = "Jacek",
                LastName = "Młody",
                Email = "jacek.mlody@gmail.com",
                BirthDate = new DateOnly(1997, 01, 24),
                PhoneNumber = "481 789 093"
            }
        },
    };

    private static int currentId = 3;
    
    // Lista kontaktów
    public IActionResult Index()
    {
        return View(_contacts);
    }

    // Formularz dodania kontaktu
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // Odebranie i zapisanie nowego kontaktu
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        model.Id = ++currentId;
        _contacts.Add(model.Id, model);
        
        return View("Index", _contacts);
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        
        return View("Index", _contacts);
    }
}