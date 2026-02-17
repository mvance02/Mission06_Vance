using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission06_Vance.Models;

namespace Mission06_Vance.Controllers;

public class HomeController : Controller
{
    // Injected database context — used for all database operations
    private readonly CollectionContext _context;

    public HomeController(CollectionContext context)
    {
        _context = context;
    }


    // -------------------------------------------------------------------------
    // HOME / ABOUT PAGES
    // -------------------------------------------------------------------------

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetToKnowJoel()
    {
        return View();
    }


    // -------------------------------------------------------------------------
    // LIST ALL MOVIES  —  /Home/MovieCollection
    // -------------------------------------------------------------------------

    // Fetches all movies from the database, sorted by title, and passes them to
    // the list view. Include() loads each movie's Category so names display correctly.
    public IActionResult MovieCollection()
    {
        var movies = _context.Collections
            .Include(m => m.Category)
            .OrderBy(m => m.Title)
            .ToList();

        return View(movies);
    }


    // -------------------------------------------------------------------------
    // ADD MOVIE  —  /Home/AddMovie
    // -------------------------------------------------------------------------

    [HttpGet]
    public IActionResult AddMovie()
    {
        // Populate the category dropdown before rendering the form
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");
        return View();
    }

    [HttpPost]
    public IActionResult AddMovie(Collection newMovie)
    {
        if (ModelState.IsValid)
        {
            _context.Collections.Add(newMovie);
            _context.SaveChanges();

            // Redirect to the full list after a successful add
            return RedirectToAction("MovieCollection");
        }

        // Validation failed — re-populate dropdown and return form with errors
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");
        return View(newMovie);
    }


    // -------------------------------------------------------------------------
    // EDIT MOVIE  —  /Home/EditMovie/{id}
    // -------------------------------------------------------------------------

    [HttpGet]
    public IActionResult EditMovie(int id)
    {
        // Find the movie to edit; return 404 if it doesn't exist
        var movie = _context.Collections.Find(id);
        if (movie == null)
        {
            return NotFound();
        }

        // Populate the category dropdown, pre-selecting the movie's current category
        ViewBag.Categories = new SelectList(
            _context.Categories, "CategoryID", "CategoryName", movie.CategoryID);
        return View(movie);
    }

    [HttpPost]
    public IActionResult EditMovie(Collection updatedMovie)
    {
        if (ModelState.IsValid)
        {
            _context.Collections.Update(updatedMovie);
            _context.SaveChanges();

            return RedirectToAction("MovieCollection");
        }

        // Validation failed — re-populate dropdown and return form with errors
        ViewBag.Categories = new SelectList(
            _context.Categories, "CategoryID", "CategoryName", updatedMovie.CategoryID);
        return View(updatedMovie);
    }


    // -------------------------------------------------------------------------
    // DELETE MOVIE  —  /Home/DeleteMovie/{id}
    // -------------------------------------------------------------------------

    [HttpGet]
    public IActionResult DeleteMovie(int id)
    {
        // Load the movie with its category so the confirmation page shows full details
        var movie = _context.Collections
            .Include(m => m.Category)
            .FirstOrDefault(m => m.CollectionID == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // ActionName keeps the route as "DeleteMovie" while giving the method a unique name
    [HttpPost, ActionName("DeleteMovie")]
    public IActionResult DeleteMovieConfirmed(int CollectionID)
    {
        var movie = _context.Collections.Find(CollectionID);
        if (movie != null)
        {
            _context.Collections.Remove(movie);
            _context.SaveChanges();
        }

        return RedirectToAction("MovieCollection");
    }
}
