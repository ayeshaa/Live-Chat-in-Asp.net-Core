using ConflictRenewal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConflictRenewal.ViewModel
{
    public class ConversationViewModel
    {
        public Conversations convos { get; set; }

        public List<UserConversationVM> AddedUsers { get; set; }

        public bool isAdmin { get; set; }
    }
}
