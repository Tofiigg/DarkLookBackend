using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(255)]
        public string OriginalImageName { get; set; }
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
