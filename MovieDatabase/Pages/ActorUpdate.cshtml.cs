﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace MovieDatabase.Pages
{
	public class ActorUpdateModel : PageModel
    {
        private readonly IActorRepository _context;

        [BindProperty]
        public Actor ActorUpdate { get; set; } = default!;

        public ActorUpdateModel(IActorRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
        {
            ActorUpdate = await _context.FindAsync(id, cancellationToken);
            Actor ActorToUpdate = await _context.FindAsync(id, cancellationToken);

            if (await TryUpdateModelAsync<Actor>(
                ActorToUpdate, "ActorUpdate",
                c => c.ActorFirstName, c => c.ActorLastName, c => c.Birthday))
            {
                await _context.SaveChangesAsync(cancellationToken);


                /*
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Movieid FROM Actors WHERE ActorId =" + ActorUpdate.ActorId;
                cmd.Connection = con;
                */

                // int? movId = await _context.GetMovieIDByActorID(ActorUpdate.ActorId, ActorUpdate);

                // return RedirectToPage("./Actors", new { id = movId });
                return RedirectToPage("./Actors");
            }

            return Page();
        }

    }
}