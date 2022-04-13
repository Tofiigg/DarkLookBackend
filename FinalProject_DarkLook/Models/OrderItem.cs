using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public  int No { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public WatchCard WatchCard { get; set; }
    }
}
