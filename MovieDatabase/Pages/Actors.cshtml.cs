using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DataAccessLibrary.Interfaces;

namespace MovieDatabase.Pages
{
	public class ActorsModel : PageModel
    {
        private readonly IActorServicePage _context;

        public IReadOnlyList<ActorViewModel> Actors { get; set; }

        [BindProperty, Required]
        public string ActorFirstName { get; set; }

        [BindProperty, Required]
        public string ActorLastName { get; set; }

        [BindProperty, Required]
        public DateTime Birthday { get; set; }

        [BindProperty]
        public int MovieId { get; set; }

        public ActorsModel(IActorServicePage context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            MovieId = id;
            Actors = await _context.ActorsByMovieID(id, cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
        {
            MovieId = id;
            if (id == null)
            {
                return NotFound();
            }
            Actors = await _context.ActorsByMovieID(id, cancellationToken);

            return Page();
        }
        
    }
}