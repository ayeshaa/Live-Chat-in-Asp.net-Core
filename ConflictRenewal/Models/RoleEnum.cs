using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConflictRenewal.Models
{
    public enum RoleEnum
    {
        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "User")]
        User,
    }
}
