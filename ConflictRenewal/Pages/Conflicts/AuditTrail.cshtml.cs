using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConflictRenewal.ViewModel;

namespace ConflictRenewal.Pages.Conflicts
{
    public class AuditTrailModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public AuditTrailModel(ConflictRenewal.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        //public JsonResult OnGet(int id)
        //{
        //    AuditTrail SD = new AuditTrail(_context);
        //    var AuditTrail = SD.GetAudit(id);
        //    return new JsonResult(AuditTrail);
        //}
        public JsonResult OnGetAsync(int id)
        {
            AuditTrail SD = new AuditTrail(_context);
            var AuditTrail = SD.GetAudit(id);
            return new JsonResult(AuditTrail);
        }
    }
}