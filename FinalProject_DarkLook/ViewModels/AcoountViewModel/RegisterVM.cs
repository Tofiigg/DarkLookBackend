using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.ViewModels.AcoountViewModel
{
    public class RegisterVM
    {
        [Required,StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(50)]

        public string SurName { get; set; }
        [Required, StringLength(50)]

        public string UserName { get; set; }
        [EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
