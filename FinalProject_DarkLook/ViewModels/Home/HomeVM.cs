using FinalProject_DarkLook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.ViewModels.Home
{
    public class HomeVM
    {
        public ICollection<Slider>  Sliders { get; set; }
        public ICollection<Future> Futures { get; set; }

        public ICollection<WatchCard> WatchCards { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<BrandLogo> BrandLogos { get; set; }
        public ICollection<WatchCard> Feature { get; set; }
        public ICollection<WatchCard> Arrival { get; set; }
        public ICollection<WatchCard> BestSeller { get; set; }
        public ICollection<WatchCard> DealWeek { get; set; }








    }
}
