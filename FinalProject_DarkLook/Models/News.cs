using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class News
    {
        public int Id { get; set; }
        
        public int Day { get; set; }
        [Required,StringLength(10)]
        public string Month { get; set; }
        [Required, StringLength(100)]


        public string Image { get; set; }
        [Required, StringLength(200)]

        public string Title { get; set; }
        [Required, StringLength(1000)]

        public string Desc { get; set; }
        [Required, StringLength(1000)]

        public string Info { get; set; }
        [StringLength(1000)]

        public string  RedInfo { get; set; }
        [StringLength(1010)]

        public string SingleDesc { get; set; }
        public bool IsDeleted { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        [StringLength(255)]
        public string OriginalImageName { get; set; }

    }
}
