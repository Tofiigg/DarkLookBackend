using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string  Text { get; set; }

        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        
    }
}
