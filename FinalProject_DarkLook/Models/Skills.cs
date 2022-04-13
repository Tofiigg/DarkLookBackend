using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Skills
    {
        public int Id { get; set; }
        [Required,StringLength(150)]

        public string Name { get; set; }

        public int Percent { get; set; }
        public bool IsDeleted { get; set; }
    }
}
