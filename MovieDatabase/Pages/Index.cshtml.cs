using DataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieDatabase.Pages;

public class IndexModel : PageModel
{
    private readonly MovieContext _movieContext;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, MovieContext movieContext)
    {
        _movieContext = movieContext;
        _logger = logger;
    }

    public void OnGet()
    {

    }
}

