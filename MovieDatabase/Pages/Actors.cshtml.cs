using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System.ComponentModel.DataAnnotations;
using DataAccessLibrary.Interfaces;

namespace MovieDatabase.Pages
{
	public class ActorsModel : PageModel
    {
        private readonly IMovieRepository _context;

        public IList<Actor> Actors { get; set; } = new List<Actor>();

        [BindProperty]
        public Movie? Movie { get; set; } = default!;

        [BindProperty, Required]
        public string ActorFirstName { get; set; }

        [BindProperty, Required]
        public string ActorLastName { get; set; }

        [BindProperty, Required]
        public DateTime Birthday { get; set; }

        
        public ActorsModel(IMovieRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
        {

            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.FirtOrDefaultAsync(id);
            Actors = await _context.GetActorsByMovie(Movie);

            return Page();

        }

        public async Task<Movie?> GetMovieById(int id, CancellationToken cancellationToken)
        {
            Movie? m = await _context.FindAsync(id, cancellationToken);
                // await _context.GetById(id, cancellationToken);
            return m;
        }
        
    }
}
