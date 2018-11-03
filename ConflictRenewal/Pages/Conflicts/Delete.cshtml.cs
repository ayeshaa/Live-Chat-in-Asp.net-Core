using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConflictRenewal.Data;
using ConflictRenewal.Models;
using Microsoft.AspNetCore.Authorization;

namespace ConflictRenewal.Pages.Conflicts
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public DeleteModel(ConflictRenewal.Data.ApplicationDbContext context)
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

            Conflict = await _context.Conflict
                                .Include(c => c.Journals)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (Conflict == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Conflict = await _context.Conflict.FindAsync(id);

            if (Conflict != null)
            {
                _context.Conflict.Remove(Conflict);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
