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


    public IndexModel(IMovieRepository db)
    {
        _db = db;
    }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (await _db.CountAsync(cancellationToken) == 0)
            {
                string file = System.IO.File.ReadAllText("sampledata.json");
                Movies = JsonSerializer.Deserialize<List<Movie>>(file);
                await _db.AddRangeAsync(Movies, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
            }
            Movies = await _db.GetAllMoviesAsync(cancellationToken);
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
            RedirectToPage("/");
        } 
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            Movie movie = new(
                MovieName: MovieName,
                Duration = Duration,
                ReleaseDate = ReleaseDate,
                Rating = Rating
                );
            await _db.AddAsync(movie, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return RedirectToPage();
        }
        return Page();
    }
}

