using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.Models;
using FinalProject_DarkLook.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public async Task <IActionResult> Index()
        {

            return View(new HomeVM
            {
                Sliders = await _context.Sliders.Where(s => s.IsDeleted == false).ToListAsync(),
                Futures = await _context.Futures.Where(s => s.IsDeleted == false).ToListAsync(),
                WatchCards=await _context.WatchCards.Where(x=>x.IsDeleted==false).ToListAsync(),
                Feature = await _context.WatchCards.Where(p => p.IsFeatured).OrderByDescending(p => p.Id).Take(8).ToListAsync(),
                Arrival = await _context.WatchCards.Where(p => p.IsNewArrivals).OrderByDescending(p => p.Id).Take(8).ToListAsync(),
                BestSeller = await _context.WatchCards.Where(p => p.IsBestSeller).OrderByDescending(p => p.Id).Take(8).ToListAsync(),
                News = await _context.News.Where(x => x.IsDeleted == false).ToListAsync(),
                BrandLogos = await _context.BrandLogos.Where(x => x.IsDeleted == false).ToListAsync(),
                DealWeek=await _context.WatchCards.Where(p=>p.IsDealsOftheWeek).OrderByDescending(x=>x.Id).Take(8).ToListAsync()



            });
        }


    }
}
