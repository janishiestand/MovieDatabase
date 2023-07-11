using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieDatabase.Pages
{
	public class SearchModel : PageModel
    {
        private readonly IMovieRepository _db;

        public IList<Movie> Movies { get; set; } = new List<Movie>();

        [BindProperty(SupportsGet = true), Required]
        public string MovieName { get; set; }

        [BindProperty(SupportsGet = true), Required]
        public string Year { get; set; }

        [BindProperty, Required]
        public int Duration { get; set; }

        [BindProperty, Required]
        public DateTime ReleaseDate { get; set; }

        [BindProperty, Required]
        public int Rating { get; set; }

        [BindProperty]
        public List<Actor> Actors { get; set; }

        public SearchModel(IMovieRepository db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            return await PerformSearch(cancellationToken, addToDatabase: false);
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            return await PerformSearch(cancellationToken, addToDatabase: true);
        }

        private async Task<IActionResult> PerformSearch(CancellationToken cancellationToken, bool addToDatabase)
        {
            OMBdSearchResult query = await _db.SearchMovieByTitle(MovieName, Year, cancellationToken);
            if (query == null)
            {
                return await MovieNotFound(cancellationToken);
            }
            if (query.Response != "False")
            {
                Movie m = await _db.ConvertSearchResult(query, cancellationToken);
                Movies.Add(m);

                if (addToDatabase)
                {
                    await _db.AddAsync(m, cancellationToken);
                    await _db.SaveChangesAsync(cancellationToken);
                    Movies = await _db.GetAllMoviesAsync(cancellationToken);
                    return RedirectToPage("./Index");
                }
                return Page();
            }
            else
            {
                return await MovieNotFound(cancellationToken);  
            }
        }

        private async Task<IActionResult> MovieNotFound(CancellationToken cancellationToken)
        {
            TempData["ErrorMessage"] = "Movie not found.";
            return RedirectToPage("./Index");
        }

    }
}
