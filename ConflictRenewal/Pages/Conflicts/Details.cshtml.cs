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
    public class DetailsModel : PageModel
    {
        public static int ConId { get; set; }

        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public DetailsModel(ConflictRenewal.Data.ApplicationDbContext context)
        {
            _context = context;
            journal = new Journal();
            journal.JournalDate = DateTime.Now;

        }

        public Conflict Conflict { get; set; }

        [BindProperty]
        public Journal journal { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ConId = Convert.ToInt32(id);
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

        public async Task<IActionResult> OnGetEditAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Conflict = await _context.Conflict
                               .Include(c => c.Journals)
                               .AsNoTracking()
                               .FirstOrDefaultAsync(m => m.Id == ConId);

            journal = await _context.Journal
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.Id == id);

            if (journal == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(journal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConflictExists(journal.Id))
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            journal.ConflictId = ConId;

            _context.Journal.Add(journal);
            await _context.SaveChangesAsync();

            // return RedirectToPage("./Index");
            return RedirectToPage("/Conflicts/Details", new { id = ConId });
        }

        private bool ConflictExists(int id)
        {
            return _context.Journal.Any(e => e.Id == id);
        }
    }
}
