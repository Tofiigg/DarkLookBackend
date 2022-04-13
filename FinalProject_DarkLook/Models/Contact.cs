using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [StringLength(400)]
        public string Location { get; set; }
        [StringLength(50)]

        public string Number { get; set; }
        [StringLength(400)]

        public string Careers { get; set; }
        [StringLength(100)]

        public string Email { get; set; }
        [StringLength(400)]

        public string SayHello { get; set; }
        [StringLength(50)]

        public string InfoEmail { get; set; }
        public bool IsDelete { get; set; }
    }
}
