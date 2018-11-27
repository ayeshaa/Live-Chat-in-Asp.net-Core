using System.ComponentModel.DataAnnotations;

namespace ConflictRenewal.Models
{
    public enum ConflictStatusEnum
    {
        [Display(Name = "Resolved")]
        Resolved = 1,
        [Display(Name = "Unresolved")]
        Unresolved = 2,
        [Display(Name = "Action Required")]
        ActionRequired = 3,
    }
}
