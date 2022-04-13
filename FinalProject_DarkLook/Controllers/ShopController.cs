using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.Models;
using FinalProject_DarkLook.ViewModels;
using FinalProject_DarkLook.ViewModels.Home;
using FinalProject_DarkLook.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int page=1)
            {

            ViewBag.TotalPage = Math.Ceiling(_context.WatchCards.Count() / 8m);
            ViewBag.Seltectedpage = page;

            return View(new ProductVM
            {
                WatchCards = await _context.WatchCards.Include(x => x.watchColours).Include(x => x.watchSizes).Where(x => x.IsDeleted == false).Skip((page-1)*8).Take(9).ToListAsync(),
                BrandLogos = await _context.BrandLogos.Where(x => x.IsDeleted == false).ToListAsync(),
                Categories = await _context.Categories.Where(x => x.IsDeleted == false).ToListAsync(),
                Sizes=await _context.Sizes.Where(x=>x.IsDeleted==false).ToListAsync(),
                Colours=await _context.Colours.Where(x=>x.IsDeleted==false).ToListAsync()


            }); ;;;
            }

        public async Task<IActionResult> WatchDetail (int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            WatchCard watchCard = await _context.WatchCards.Where(x => x.IsDeleted == false).
                Include(x => x.watchColours).ThenInclude(x => x.Colour).Include(x => x.watchSizes).
                ThenInclude(x => x.Size).FirstOrDefaultAsync(x => x.Id == Id);

    if(watchCard==null) return RedirectToAction("Index", "error");





            ProductVM productVM = new ProductVM
            {
                Watchard = watchCard,
                Categories=await _context.Categories.Where(x=>x.IsDeleted==false).ToListAsync(),
                BrandLogos=await _context.BrandLogos.Where(x=>x.IsDeleted==false).ToListAsync(),
                Featured= await _context.WatchCards.Where(p => p.IsFeatured).OrderByDescending(p => p.Id).Take(8).ToListAsync(),
            };
            return View(productVM);


        }
    }
}
