using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.ViewModels.Aboutt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
             
            return View(new AboutVM
            {

                OurTeams = await _context.OurTeams.Where(x=>x.IsDeleted==false).ToListAsync(),
                Skills=await _context.Skills.Where(x=>x.IsDeleted==false).ToListAsync(),
                Categories = await _context.Categories.Where(x => x.IsDeleted == false).ToListAsync(),
                About= await _context.Abouts.FirstOrDefaultAsync()


            });;
        }
    }
}
