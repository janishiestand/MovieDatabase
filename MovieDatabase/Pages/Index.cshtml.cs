using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieDatabase.Pages;

public class IndexModel : PageModel
{
    private readonly IMovieRepository _db;
    
    public IList<Movie> Movies { get; set; } = new List<Movie>();

    [BindProperty, Required]
    public string MovieName { get; set; }

    [BindProperty, Required]
    public int Duration { get; set; }

    [BindProperty, Required]
    public DateTime ReleaseDate { get; set; }

    [BindProperty, Required]
    public int Rating { get; set; }

    [BindProperty]
    public List<Actor> Actors { get; set; }

    [BindProperty]
    public string SelectedFilter { get; set;  }

    public string SortBy { get; set; }

    public bool IsAscending { get; set; }


    public IndexModel(IMovieRepository db)
    {
        _db = db;
    }

    public async Task OnGetAsync(string sortBy, bool isAscending, string selectedFilter, string filterValue, CancellationToken cancellationToken)
    {
        SortBy = sortBy;
        IsAscending = isAscending;
        SelectedFilter = selectedFilter;

        IQueryable<Movie> moviesQuery = await _db.QueryAllMoviesAsync(cancellationToken);
        ViewData["SelectionOptions"] = SelectionOptions();
        if (!string.IsNullOrEmpty(SelectedFilter) && !string.IsNullOrEmpty(filterValue))
        {
            moviesQuery = await _db.ApplyFilter(moviesQuery, selectedFilter, filterValue);
        }

        moviesQuery = await _db.ApplySorting(moviesQuery, SortBy, IsAscending);

        Movies = await moviesQuery.ToListAsync(cancellationToken);
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            Movie movie = new(
                MovieName: MovieName,
                Duration: Duration,
                ReleaseDate: ReleaseDate,
                Rating: Rating,
                Actors: Actors
                );
            await _db.AddAsync(movie, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return RedirectToPage();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
    {
        Movie? toRemove = await _db.FindAsync(id, cancellationToken);
        if (toRemove == null)
        {
            return (NotFound());
        }
        _db.Delete(toRemove);
        await _db.SaveChangesAsync(cancellationToken);
        return RedirectToAction(nameof(IndexModel));
    }

    public List<SelectListItem> SelectionOptions()
    {
        List<SelectListItem> selectionOptions = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Movie Title", Value = "title" },
            new SelectListItem { Text = "Release Year", Value = "releaseDate" },
            new SelectListItem { Text = "Duration", Value = "duration" },
            new SelectListItem { Text = "Rating", Value = "rating" }
        };
        return selectionOptions;
    }

}

