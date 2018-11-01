using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConflictRenewal.Models
{
    public class Conflict
    {
        public int Id { get; set; }

        [Display(Name = "Conflict Date")]
        [DataType(DataType.Date)]
        public DateTime ConflictDate { get; set; }

        [Display(Name = "Conflict Trigger")]
        [DataType(DataType.MultilineText)]
        public string Question1 { get; set; }

        [Display(Name = "Emotional Experience")]
        [DataType(DataType.MultilineText)]
        public string Question2 { get; set; }

        [Display(Name = "Conflict Statement")]
        [DataType(DataType.MultilineText)]
        public string Question3 { get; set; }

        [Display(Name = "Desired Outcome")]
        [DataType(DataType.MultilineText)]
        public string Question4 { get; set; }

        [Display(Name = "Solution Rehearsal")]
        [DataType(DataType.MultilineText)]
        public string Question5 { get; set; }

        [Display(Name = "Permanent Solution")]
        [DataType(DataType.MultilineText)]
        public string Question6 { get; set; }

        [Display(Name = "Most recent journal")]
        [DataType(DataType.Date)]
        public DateTime MostrecentjournalDate { get; set; }

        public List<Journal> Journals { get; set; }
    }
}
