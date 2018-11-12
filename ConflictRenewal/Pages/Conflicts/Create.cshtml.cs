using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConflictRenewal.Data;
using ConflictRenewal.Models;
using Microsoft.AspNetCore.Authorization;

namespace ConflictRenewal.Pages.Conflicts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public CreateModel(ConflictRenewal.Data.ApplicationDbContext context)
        {
            _context = context;
            Conflict = new Conflict();
            Conflict.ConflictDate = DateTime.Now;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Conflict Conflict { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Conflict.MostrecentjournalDate = DateTime.Now.ToUniversalTime();

            _context.Conflict.Add(Conflict);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}