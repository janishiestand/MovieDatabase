using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieDatabase.Pages
{
	public class UpdateModel : PageModel
    {
        private readonly IMovieRepository _context;

        [BindProperty]
        public Movie MovieUpdate { get; set; }

        public UpdateModel(IMovieRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovieUpdate = await _context.FirstOrDefaultAsync(id, cancellationToken);
            var MovieToUpdate = await _context.FindAsync(id, cancellationToken);

            if (await TryUpdateModelAsync<Movie>(
                MovieToUpdate, "MovieUpdate",
                c => c.MovieName, c => c.Duration, c => c.ReleaseDate, c => c.Rating))
            {
                await _context.SaveChangesAsync(cancellationToken);
                return RedirectToPage("./Index");
            }
            return Page();
        }

    }
}
