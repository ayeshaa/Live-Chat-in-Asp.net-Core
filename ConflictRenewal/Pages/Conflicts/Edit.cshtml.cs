using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConflictRenewal.Data;
using ConflictRenewal.Models;
using Microsoft.AspNetCore.Authorization;

namespace ConflictRenewal.Pages.Conflicts
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public EditModel(ConflictRenewal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Conflict Conflict { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Conflict = await _context.Conflict.FirstOrDefaultAsync(m => m.Id == id);

            if (Conflict == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Conflict).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConflictExists(Conflict.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ConflictExists(int id)
        {
            return _context.Conflict.Any(e => e.Id == id);
        }
    }
}
