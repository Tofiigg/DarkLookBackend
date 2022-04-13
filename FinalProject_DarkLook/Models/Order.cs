using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int No { get; set; }
        public double TotalPrice { get; set; }
        public bool IsApprove { get; set; }
        public string ApproveNote { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }


      
    }
}
