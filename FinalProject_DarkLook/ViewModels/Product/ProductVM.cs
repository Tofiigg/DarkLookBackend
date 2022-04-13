using FinalProject_DarkLook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.ViewModels.Product
{
    public class ProductVM
    {

        public ICollection<WatchCard> WatchCards { get; set; }
        public WatchCard Watchard { get; set; }
        public ICollection<Colour> Colours { get; set; }
        public ICollection<Size> Sizes { get; set; }
        public ICollection<BrandLogo> BrandLogos { get; set; }
        public List<WatchColour> watchColours { get; set; }
        public List<WatchSize> watchSizes { get; set; }

        public List<string> Sizess { get; set; }
        public List<Category> Categories { get; set;}
        public List<WatchCard> Featured { get; set; }



    }
}
