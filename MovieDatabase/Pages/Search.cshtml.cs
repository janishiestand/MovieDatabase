using System.ComponentModel.DataAnnotations;
using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static DataAccessLibrary.Repositories.SearchPageService;

namespace MovieDatabase.Pages
{
	public class SearchModel : PageModel
    {
        private readonly ISearchPageService _db;

        public IReadOnlyList<MovieViewModel> Movies { get; set; }

        [BindProperty(SupportsGet = true), Required]
        public string MovieName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? ReleaseYear { get; set; }
        
        [BindProperty, Required]
        public int Duration { get; set; }

        [BindProperty, Required]
        public DateTime ReleaseDate { get; set; }

        [BindProperty, Required]
        public int Rating { get; set; }

        [BindProperty]
        public IReadOnlyList<ActorViewModel> Actors { get; set; }

        public SearchModel(ISearchPageService db)
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
            try
            {
                Movies = await _db.PerformSearchService(MovieName, ReleaseYear, addToDatabase, cancellationToken);

                if (addToDatabase)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return Page();
                }
            }
            catch (MovieNotFound)
            {
                TempData["ErrorMessage"] = "Movie not found.";
            }
            catch (MovieAlreadyInDatabase)
            {
                TempData["ErrorMessage"] = "This movie has already been added to the library.";
            }

            return RedirectToPage("./Index");
        }

    }
}
