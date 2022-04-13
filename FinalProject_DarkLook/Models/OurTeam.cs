using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class OurTeam
    {
        public int Id { get; set; }
        [Required, StringLength(50)]

        public string Profession { get; set; }
        [Required,StringLength(150)]
        public string Image { get; set; }
        [Required, StringLength(50)]

        public string Name { get; set; }
        [Required, StringLength(500)]

        public string Desc { get; set; }
        [StringLength(150)]


        public string FaceBookUrl { get; set; }
        [StringLength(150)]

        public string TwitterUrl { get; set; }
        [StringLength(150)]

        public string PinterestUrl { get; set; }
        [StringLength(150)]

        public string BeUrl { get; set; }
        [StringLength(150)]

        public string Url { get; set; }

        public bool IsDeleted { get; set; }
        [StringLength(255)]
        public string OriginalImageName { get; set; }
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

    }
}
