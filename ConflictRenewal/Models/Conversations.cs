using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConflictRenewal.Models
{
    public class Conversations
    {
        [Key]
        public int Mid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public bool IsSeen { get; set; }
        public System.DateTime Date { get; set; }
        public string Image { get; set; }

        //public virtual AspNetUser AspNetUser { get; set; }
        //public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
