using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class WatchColour
    {
        public int Id { get; set; }
        public Colour Colour { get; set; }
        public WatchCard WatchCard { get; set; }
    }
}
