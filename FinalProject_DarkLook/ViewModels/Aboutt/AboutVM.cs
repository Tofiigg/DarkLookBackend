using FinalProject_DarkLook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.ViewModels.Aboutt
{
    public class AboutVM
    {
        public ICollection<Skills> Skills { get; set; }
        public About About { get; set; }
        public ICollection<OurTeam> OurTeams { get; set; }
        public ICollection<Category> Categories{ get; set; }



    }
}
