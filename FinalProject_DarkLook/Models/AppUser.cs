using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class AppUser:IdentityUser
    {
        [Required, StringLength(100)]

        public string Name { get; set; }

        [Required, StringLength(100)]

        public string SurName { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<BasketWatch> BasketWatches { get; set; }
        public ICollection<BildingAdress> BildingAdresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
