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
using ConflictRenewal.ViewModel;

namespace ConflictRenewal.Pages.Conflicts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
            Conflict = new Conflict();
            Conflict.ConflictDate = DateTime.Now;
            //SD = new AuditTrail();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Conflict Conflict { get; set; }

        public AuditTrail SD { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Conflict.EmailID = User.Identity.Name;
            Conflict.ConflictDate = DateTime.Now.ToUniversalTime();
            Conflict.MostrecentjournalDate = DateTime.Now.ToUniversalTime();

            //_context.Conflict.Add(Conflict);
            //await _context.SaveChangesAsync();

            AuditTrail SD = new AuditTrail(_context);
            SD.CreateRecord(Conflict);
            
            return RedirectToPage("./Index");
        }
    }
}