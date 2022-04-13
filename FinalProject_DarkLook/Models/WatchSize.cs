using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class WatchSize
    {
        public int Id { get; set; }
        public Size Size { get; set; }
        public WatchCard WatchCard { get; set; }
    }
}
