﻿using ConflictRenewal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ConflictRenewal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConflictRenewal.Pages.Conflicts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public IndexModel(ConflictRenewal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsAdminConflict { get; set; }

        public RoleEnum rollEnum { get; set; }

        public ConflictStatusEnum ConflictStatusEnum { get; set; }

        public ConflictViewModel Conflictview { get; set; }

        ConflictViewModel conflict = new ConflictViewModel();

        public async Task OnGetAsync()
        {
            var user = _context.Users.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            var role = _context.UserRoles.Where(a => a.UserId == user.Id).FirstOrDefault();
            var roletext = _context.Roles.Where(a => a.Id == role.RoleId).FirstOrDefault();
            if (roletext.Name == RoleEnum.Admin.ToString())
            {
                conflict.Conflict = await _context.Conflict.Include(a => a.Journals).ToListAsync();
            }
            else
            {
                conflict.Conflict = await _context.Conflict.Where(a => a.EmailID == User.Identity.Name).Include(a => a.Journals).ToListAsync();
            }
            foreach (var item in conflict.Conflict)
            {
                item.MostrecentjournalDate = item.Journals.Where(a => a.ConflictId == item.Id).OrderByDescending(a => a.JournalDate).Select(a => (DateTime?)a.JournalDate).FirstOrDefault();
                item.ConflictStatus = item.Journals.Where(a => a.JournalDate == item.MostrecentjournalDate).Select(a => a.StatusIdByRole).FirstOrDefault();
                //item.CreatedBy = item.Journals.Where(a => a.JournalDate == item.MostrecentjournalDate).Select(a => a.createdBy).FirstOrDefault();
                //if (item.CreatedBy != User.Identity.Name)
                //{
                //    IsAdminConflict = true;
                //}
                //else
                //{
                //    IsAdminConflict = false;
                //}
                item.AdminRole = roletext.Name;
                if (item.AdminRole == RoleEnum.Admin.ToString())
                {
                    conflict.isAdmin = true;
                }
                else
                {
                    conflict.isAdmin = false;
                }
            }
            conflict.Conflict = conflict.Conflict.OrderByDescending(a => a.MostrecentjournalDate).ToList();

            Conflictview = conflict;
        }
        //public JsonResult OnGetFilter(BooksFilterBy filterBy)
        //{
        //    return new JsonResult(_filterService.GetFilterDropDownValues(filterBy));
        //}
        public JsonResult Audit(int id)
        {
            AuditTrail SD = new AuditTrail(_context);
            var AuditTrail = SD.GetAudit(id);
            return new JsonResult(AuditTrail);
        }
    }
}
