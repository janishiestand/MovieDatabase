using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieDatabase.Pages
{
	public class AddActorModel : PageModel
    {
        private readonly IActorServicePage _context;

        [BindProperty]
        public ActorViewModel Actor { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int MovieId { get; set; }

        public AddActorModel(IActorServicePage context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MovieId = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            int? movId = MovieId;
            await _context.AddActorToMovie(Actor, MovieId, cancellationToken);
            return RedirectToPage("./Actors", new { id = movId });

        }
    }
}
