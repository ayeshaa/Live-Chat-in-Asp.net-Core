﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly ConflictRenewal.Data.ApplicationDbContext _context;

        public IndexModel(ConflictRenewal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Conflict> Conflict { get; set; }

        public async Task OnGetAsync()
        {
            Conflict = await _context.Conflict.Include(a => a.Journals).ToListAsync();
            foreach (var item in Conflict)
            {
                item.MostrecentjournalDate = item.Journals.Where(a => a.ConflictId == item.Id).OrderByDescending(a => a.JournalDate).Select(a => a.JournalDate).FirstOrDefault();
            }
            Conflict = Conflict.OrderByDescending(a => a.MostrecentjournalDate).ToList();
        }
    }
}
