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
using ConflictRenewal.ViewModel;

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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Conflict.EmailID = User.Identity.Name;
            Conflict.MostrecentjournalDate = DateTime.Now.ToUniversalTime();
            //_context.Attach(Conflict).State = EntityState.Modified;

            //SampleDataModel SD = new SampleDataModel();
            //return View(SD.GetData(id));

            try
            {
                //await _context.SaveChangesAsync();
                AuditTrail SD = new AuditTrail(_context);
                SD.UpdateRecord(Conflict);
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
