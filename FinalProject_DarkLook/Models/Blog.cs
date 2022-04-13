using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required,StringLength(150)]
        public string Title { get; set; }
        [Required, StringLength(1000)]

        public string Desc { get; set; }
    
        [Required, StringLength(200)]
        public string Image { get; set; }

        [Required, StringLength(1500)]
        public string Info { get; set; }
        [StringLength(550)]

        public string RedSide { get; set; }
        [ StringLength(550)]

        public string AfterRed { get; set; }
        public DateTime? Date { get; set; }
        public bool IsDeleted { get; set; }

        public BlogCategory BlogCategory { get; set; }

        [StringLength(255)]
        public string OriginalImageName { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
