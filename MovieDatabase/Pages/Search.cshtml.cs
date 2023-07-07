using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [BindProperty, Required]
        public int Duration { get; set; }

        [BindProperty, Required]
        public DateTime ReleaseDate { get; set; }

        [BindProperty, Required]
        public int Rating { get; set; }

        public SearchModel(IMovieRepository db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            /*
            Movie m = Movies[0];
            await _db.AddAsync(m, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            */
            return RedirectToAction(nameof(IndexModel));

        }

        public async Task<IActionResult> OnPostAsync(string MovieName, CancellationToken cancellationToken)
        {
            Movie m = await _db.SearchMovieByTitle(MovieName, cancellationToken);
            Movies.Add(m);
            return Page();
        }

    }
}
