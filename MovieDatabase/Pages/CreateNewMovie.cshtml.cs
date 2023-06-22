using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieDatabase.Pages
{
	public class CreateNewMovieModel : PageModel
    {
        private readonly IMovieRepository _context;

        [BindProperty, Required]
        public string MovieName { get; set; }

        [BindProperty, Required]
        public int Duration { get; set; }

        [BindProperty, Required]
        public DateTime ReleaseDate { get; set; }

        [BindProperty, Required]
        public int Rating { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
    }
}
