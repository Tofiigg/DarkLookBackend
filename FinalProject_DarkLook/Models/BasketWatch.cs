using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class BasketWatch
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int WatchId { get; set; }

        public int Count { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public AppUser AppUser { get; set; }
        public WatchCard Watch { get; set; } 
        


    }
}
