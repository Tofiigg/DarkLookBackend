using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Colour
    {
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<WatchColour> WatchColours { get; set; }
    }
}
