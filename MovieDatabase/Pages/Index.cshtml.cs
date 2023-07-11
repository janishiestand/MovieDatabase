using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

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

    public string SortBy { get; set; }
    public bool IsAscending { get; set; }


    public IndexModel(IMovieRepository db)
    {
        _db = db;
    }

    public async Task OnGetAsync(string sortBy, bool isAscending, CancellationToken cancellationToken)
    {
        // Movies = await _db.GetAllMoviesAsync(cancellationToken);
        SortBy = sortBy;
        IsAscending = isAscending;
        IQueryable<Movie> moviesQuery = await _db.QueryAllMoviesAsync(cancellationToken);

        switch (SortBy)
        {
            case "title":
                moviesQuery = (IsAscending) ? moviesQuery.OrderBy(m => m.MovieName) : moviesQuery.OrderByDescending(m => m.MovieName);
                break;
            case "duration":
                moviesQuery = (IsAscending) ? moviesQuery.OrderBy(m => m.Duration) : moviesQuery.OrderByDescending(m => m.Duration);
                break;
            case "releaseDate":
                moviesQuery = (IsAscending) ? moviesQuery.OrderBy(m => m.ReleaseDate) : moviesQuery.OrderByDescending(m => m.ReleaseDate);
                break;
            case "rating":
                moviesQuery = (IsAscending) ? moviesQuery.OrderBy(m => m.Rating) : moviesQuery.OrderByDescending(m => m.Rating);
                break;
            default:
                moviesQuery = moviesQuery.OrderBy(m => m.id);
                break;
        }

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


}

