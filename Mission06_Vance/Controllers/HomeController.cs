using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission06_Vance.Models;

namespace Mission06_Vance.Controllers;

public class HomeController : Controller
{
    private readonly CollectionContext _context;

    public HomeController(CollectionContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetToKnowJoel()
    {
        return View();
    }

    [HttpGet]
    public IActionResult MovieCollection()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");
        return View();
    }

    [HttpPost]
    public IActionResult MovieCollection(Collection response)
    {
        if (ModelState.IsValid)
        {
            _context.Collections.Add(response);
            _context.SaveChanges();
            return View("Confirmation", response);
        }
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");
        return View(response);
    }
}