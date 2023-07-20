using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieDatabase.Pages
{
	public class ActorUpdateModel : PageModel
    {
        private readonly IActorServicePage _context;

        [BindProperty]
        public ActorViewModel ActorToUpdate { get; set; }

        public ActorUpdateModel(IActorServicePage context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            ActorToUpdate = await _context.FindActor(id, cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _context.UpdateActor(ActorToUpdate, cancellationToken);

            int? movId = ActorToUpdate.movieID;
            return RedirectToPage("./Actors", new { id = movId });
        }

    }
}
