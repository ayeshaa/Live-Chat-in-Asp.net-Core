using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConflictRenewal.Models
{
    public class Conflict
    {
        public int Id { get; set; }

        [Display(Name = "Conflict Date")]
        [DataType(DataType.Date)]
        public DateTime ConflictDate { get; set; }

        [Display(Name = "1. Conflict Trigger")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 1 required.")]
        public string Question1 { get; set; }

        [Display(Name = "2. Emotional Experience")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 2 required.")]
        public string Question2 { get; set; }

        [Display(Name = "3. Conflict Statement")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 3 required.")]
        public string Question3 { get; set; }

        [Display(Name = "4. Desired Outcome")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 4 required.")]
        public string Question4 { get; set; }

        [Display(Name = "5. Solution Rehearsal")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 5 required.")]
        public string Question5 { get; set; }

        [Display(Name = "6. Permanent Solution")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Response to question 6 required.")]
        public string Question6 { get; set; }

        [Display(Name = "Most recent journal")]
        [DataType(DataType.Date)]
        public DateTime? MostrecentjournalDate { get; set; }

        [Display(Name = "Email Id")]
        public string EmailID { get; set; }

        [NotMapped]
        public string AdminRole { get; set; }

        [NotMapped]
        public bool ConflictStatus { get; set; }

        [NotMapped]
        public string CreatedBy { get; set; }

        public List<Journal> Journals { get; set; }
    }
}
