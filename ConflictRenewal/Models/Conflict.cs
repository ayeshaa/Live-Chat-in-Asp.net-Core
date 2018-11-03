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
        [Required(ErrorMessage = "Response to question 1 required.")]
        public string Question1 { get; set; }

        [Display(Name = "Emotional Experience")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 2 required.")]
        public string Question2 { get; set; }

        [Display(Name = "Conflict Statement")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 3 required.")]
        public string Question3 { get; set; }

        [Display(Name = "Desired Outcome")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 4 required.")]
        public string Question4 { get; set; }

        [Display(Name = "Solution Rehearsal")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 5 required.")]
        public string Question5 { get; set; }

        [Display(Name = "Permanent Solution")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 6 required.")]
        public string Question6 { get; set; }

        [Display(Name = "Most recent journal")]
        [DataType(DataType.Date)]
        public DateTime MostrecentjournalDate { get; set; }

        public List<Journal> Journals { get; set; }
    }
}
