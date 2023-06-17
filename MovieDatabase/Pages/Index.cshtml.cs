using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Pages;

public class IndexModel : PageModel
{
    private readonly IMovieRepository _db;

    public IList<Movie> Movie { get; set; } = new List<Movie>();

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
        var Movies = _db.GetAllMoviesAsync(cancellationToken);
    }
}

