using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConflictRenewal.Data;
using ConflictRenewal.Models;

namespace ConflictRenewal.Pages.Conflicts
{
    public class CreateModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public CreateModel(ConflictRenewal.Data.ApplicationDbContext context)
        {
            _context = context;
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

            _context.Conflict.Add(Conflict);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}