using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class WatchCard
    {
        public int Id { get; set; }
        [Required,StringLength(200)]
        public string Image { get; set; }
        [StringLength(200)]

        public string HoverImage { get; set; }
        [Required, StringLength(150)]

        public string Desc { get; set; }
        [ StringLength(1000)]

        public string DescDetail { get; set; }
        [Required, StringLength(150)]

        public string Brand { get; set; }
        [ StringLength(150)]

        public string Code { get; set; }

        public  bool Stock { get; set; }
        public int Star { get; set; }

        [Required]
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
        public int Count { get; set; }
        public bool IsNewArrivals { get; set; }
        public bool IsBestSeller{ get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDealsOftheWeek{ get; set; }
        [NotMapped]
        public IFormFile HoverImageFile { get; set; }
        [NotMapped]
        public IFormFile MainImageFile { get; set; }
        public ICollection<BasketWatch> BasketWatches { get; set; }

        public List<WatchColour> watchColours { get; set; }
        public List<WatchSize> watchSizes { get; set; }
        public List<BrandLogo> BrandLogos { get; set; }




    }
}
