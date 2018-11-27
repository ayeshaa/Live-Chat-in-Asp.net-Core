using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConflictRenewal.Models
{
    public class Journal
    {
        public int Id { get; set; }

        [Display(Name = "Journal Date")]
        [DataType(DataType.Date)]
        public DateTime JournalDate { get; set; }

        [Display(Name = "Journal Entry")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Journal entry required.")]
        public string JournalContent { get; set; }

        public int ConflictId { get; set; }

        public string createdBy { get; set; }

        [NotMapped]
        public string AdminRole { get; set; }

        [NotMapped]
        public string UserRole { get; set; }

        public bool ConflictStatus { get; set; }

        public int StatusIdByRole { get; set; }

        public Conflict Conflict { get; set; }
    }
}
