using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConflictRenewal.Models
{
    public class AuditTrailTable
    {
        public int ID { get; set; }
        public int KeyFieldID { get; set; }
        public System.DateTime DateTimeStamp { get; set; }
        public string DataModel { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
        public string Changes { get; set; }
        public int AuditActionTypeENUM { get; set; }
    }
}
