using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class About
    {
        public int Id {get;set;}
        [ StringLength(200)]

        public string ImageLeftSide { get; set; }

        [Required,StringLength(200)]

        public string Image { get; set; }
        [Required, StringLength(150)]

        public string Title { get; set; }
        [Required, StringLength(1000)]

        public string Desc { get; set; }
        [StringLength(150)]
        public string Html { get; set; }
        [StringLength(150)]

        public string Bootstrap { get; set; }
        [StringLength(150)]
        public string Css { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(255)]
        public string OriginalImageName { get; set; }
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }


}

