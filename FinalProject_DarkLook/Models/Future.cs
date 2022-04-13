using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Future
    {
        public  int  Id { get; set; }
        [Required,StringLength(150)]
        public string Icon { get; set; }
        [Required, StringLength(150)]


        public string Title { get; set; }
        [Required, StringLength(150)]
        public string Desc { get; set; }

        public bool IsDeleted { get; set; }



    }
}
