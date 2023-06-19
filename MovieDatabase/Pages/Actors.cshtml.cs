using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Pages
{
	public class ActorsModel : PageModel
    {
        private readonly MovieContext context;

        public IList<Actor> Actors { get; set; } = new List<Actor>();

        [BindProperty, Required]
        public string FirstName { get; set; }

        [BindProperty, Required]
        public string LastName { get; set; }

        [BindProperty, Required]
        public DateTime Birthday { get; set; }

        public ActorsModel(MovieContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {

        }
    }
}
