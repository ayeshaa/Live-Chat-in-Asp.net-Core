using ConflictRenewal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public RoleEnum rollEnum { get; set; }

        public string Isadmin { get; set; }

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

            foreach (var item in Conflict.Journals)
            {
                if (item.createdBy != null)
                {
                    var user = _context.Users.Where(a => a.UserName == item.createdBy).FirstOrDefault();
                    var role = _context.UserRoles.Where(a => a.UserId == user.Id).FirstOrDefault();
                    var roletext = _context.Roles.Where(a => a.Id == role.RoleId).FirstOrDefault();
                    item.AdminRole = roletext.Name;
                }
            }
            var loginuser = _context.Users.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            var loginuserrole = _context.UserRoles.Where(a => a.UserId == loginuser.Id).FirstOrDefault();
            var loginuserroletext = _context.Roles.Where(a => a.Id == loginuserrole.RoleId).FirstOrDefault();
            foreach (var item in Conflict.Journals)
            {
                item.UserRole = loginuserroletext.Name;
                Isadmin = loginuserroletext.Name;
            }

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

        public async Task<IActionResult> OnPostEditAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            journal.JournalDate = DateTime.Now.ToUniversalTime();
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
            return RedirectToPage("/Conflicts/Details", new { id = ConId });
        }
        public async Task<IActionResult> OnGetDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            journal = await _context.Journal.FindAsync(id);

            if (journal != null)
            {
                _context.Journal.Remove(journal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Conflicts/Details", new { id = ConId });
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            journal.ConflictId = ConId;
            journal.createdBy = User.Identity.Name;
            journal.JournalDate = DateTime.Now.ToUniversalTime();
            journal.createdBy = User.Identity.Name;

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
