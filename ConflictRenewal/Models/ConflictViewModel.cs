using System.Collections.Generic;

namespace ConflictRenewal.Models
{
    public class ConflictViewModel
    {
        public IList<Conflict> Conflict { get; set; }

        public bool isAdmin { get; set; }
    }
}
