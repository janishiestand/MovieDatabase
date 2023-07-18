using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLibrary.Models;

namespace MovieDatabase.Pages;

public class IndexModel : PageModel
{
    private readonly IIndexPageService _db;
    
    public IReadOnlyList<IndexPageViewModel> Movies { get; set; }
    
    [BindProperty, Required]
    public string MovieName { get; set; }

    [BindProperty, Required]
    public int Duration { get; set; }

    [BindProperty, Required]
    public DateTime ReleaseDate { get; set; }

    [BindProperty, Required]
    public int Rating { get; set; }

    [BindProperty]
    public IReadOnlyList<IndexActorViewModel> Actors { get; set; }

    [BindProperty]
    public string SelectedFilter { get; set;  }

    public string SortBy { get; set; }

    public bool IsAscending { get; set; }


    public IndexModel(IIndexPageService db)
    {
        _db = db;
    }

    public async Task OnGetAsync(string sortBy, bool isAscending, string selectedFilter, string filterValue, CancellationToken cancellationToken)
    {
        SortBy = sortBy;
        IsAscending = isAscending;
        SelectedFilter = selectedFilter;
        ViewData["SelectionOptions"] = SelectionOptions();
        IReadOnlyList<IndexPageViewModel> moviesQuery = await _db.GetIndexPageViewModelsAsync(SortBy, IsAscending, SelectedFilter, filterValue, cancellationToken);
        Movies = moviesQuery;
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            await _db.AddMovie(MovieName, Duration, ReleaseDate, Rating, Actors.ToList(), cancellationToken);
            return RedirectToPage();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
    {
        await _db.DeleteMovie(id, cancellationToken);
        return RedirectToAction(nameof(IndexModel));
    }

    public List<SelectListItem> SelectionOptions()
    {
        List<SelectListItem> selectionOptions = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Movie Title", Value = "title" },
            new SelectListItem { Text = "Release Year", Value = "releaseDate" },
            new SelectListItem { Text = "Duration", Value = "duration" },
            new SelectListItem { Text = "Rating", Value = "rating" }
        };
        return selectionOptions;
    }

}

