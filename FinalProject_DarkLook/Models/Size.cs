using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Size
    {
        public  int Id { get; set; }
        public string Sizes { get; set; }
        public bool IsDeleted { get; set; }
        public List<WatchSize> WatchSizes { get; set; }

    }
}
