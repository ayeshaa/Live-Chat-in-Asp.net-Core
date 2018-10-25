using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConflictRenewal.Data;
using ConflictRenewal.Models;

namespace ConflictRenewal.Pages.Conflicts
{
    public class DetailsModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public DetailsModel(ConflictRenewal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
