using FinalProject_DarkLook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.ViewModels.Blogs
{
    public class BLogVM
    { 
        public ICollection<Category> Categories { get; set; }
        public ICollection<Blog> Blogs { get; set; }

    
    }
}
