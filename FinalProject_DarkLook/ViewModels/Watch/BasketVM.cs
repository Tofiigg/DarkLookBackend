using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.ViewModels.Watch
{
    public class BasketVM
    {
        public  int Id { get; set; }
        public string Title { get; set; }
        public string MainImage { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }

    }
}
