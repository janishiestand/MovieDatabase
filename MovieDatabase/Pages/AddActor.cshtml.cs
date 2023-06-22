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
	public class AddActorModel : PageModel
    {
        private readonly IActorRepository _context;

        public IList<Actor> Actors { get; set; } = new List<Actor>();

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty, Required]
        public string ActorFirstName { get; set; }

        [BindProperty, Required]
        public string ActorLastName { get; set; }

        [BindProperty, Required]
        public DateTime Birthday { get; set; }

        public AddActorModel(IActorRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
        {

            //int? movId = await _context.GetMovieIdByActorId(ActorUpdate.ActorId, cancellationToken);
            // return RedirectToPage("./Actors", new { id = movId });
            return Page();
        }
    }
}
