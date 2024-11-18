using LabolatoriumASP.NET.Models;
using LabolatoriumASP.NET.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LabolatoriumASP.NET.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // Lista kontaktów
    public IActionResult Index()
    {
        return View(_contactService.GetAll());
    }

    // Formularz dodania kontaktu
    [HttpGet]
    public IActionResult Add()
    {
        var model = new ContactModel();
        model.Organizations=_contactService.GetOrganizations().Select(o => new SelectListItem()
        {
            Value = o.Id.ToString(),
            Text = o.Name,
            Selected = o.Id == 1    
        }).ToList();
        
        return View(model);
    }

    // Odebranie i zapisanie nowego kontaktu
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        _contactService.Add(model);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View(_contactService.GetById(id));
    }

    [HttpPost]
    public IActionResult Edit(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _contactService.Update(model);
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        _contactService.Delete(id);
        
        return RedirectToAction("Index");
    }
}