using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.Extensions;
using FinalProject_DarkLook.Helpers;
using FinalProject_DarkLook.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Areas.Manage.Controllers
{
    [Area("manage")]
    public class WatchCardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public WatchCardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            ViewBag.TotalPage = Math.Ceiling(_context.WatchCards.Count() / 8m);
            ViewBag.Seltectedpage = page;

            return View(await _context.WatchCards.OrderByDescending(x=>x.Id).Where(g => g.IsDeleted == false).Skip((page-1)*8).Take(8).ToListAsync());


        }

        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
                return View("Error404");

            WatchCard watchCard = await _context.WatchCards.FirstOrDefaultAsync(g => g.Id == Id && g.IsDeleted == false);

            if (watchCard == null)
                return View("Error404");

            return View(watchCard);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WatchCard watchCard)
        {


            if (ModelState.IsValid)
                return View(watchCard);

       

         

            if (watchCard.MainImageFile == null)
            {
                ModelState.AddModelError("MainImageFile", "Main Shekil Mutleq Secilmelidi");
                return View(watchCard);

            }

            if (watchCard.HoverImageFile == null)
            {
                ModelState.AddModelError("HoverImageFile", "Hover Shekil Mutleq Secilmelidi");
                return View(watchCard);
            }

            if (!watchCard.MainImageFile.CheckContentType("image"))
            {
                ModelState.AddModelError("MainImageFile", "MainImageFile tipini Duzgun Secin");
                return View(watchCard);
            }

            if (watchCard.MainImageFile.CheckLength(500))
            {
                ModelState.AddModelError("MainImageFile", "MainImageFile Uzunlugu Maksimum 500 kb Ola Biler");
                return View(watchCard);
            }

            string filePath = Path.Combine(_env.WebRootPath, "images", "product");

            watchCard.Image = await watchCard.MainImageFile.SaveFileAsync(filePath);

            if (!watchCard.HoverImageFile.CheckContentType("image"))
            {
                ModelState.AddModelError("HoverImageFile", "HoverImageFile tipini Duzgun Secin");
                return View(watchCard);
            }

            if (watchCard.HoverImageFile.CheckLength(500))
            {
                ModelState.AddModelError("HoverImageFile", "HoverImageFile Uzunlugu Maksimum 200 kb Ola Biler");
                return View(watchCard);
            }

            watchCard.HoverImage = await watchCard.HoverImageFile.SaveFileAsync(filePath);

           

            await _context.WatchCards.AddAsync(watchCard);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            WatchCard watchCard = await _context.WatchCards.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (watchCard == null)
                return RedirectToAction("Index", "error");

            return View(watchCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, WatchCard watchCard)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            WatchCard DeletedWatch= await _context.WatchCards.FirstOrDefaultAsync(s => s.Id == Id);

            if (watchCard == null)
                return RedirectToAction("Index", "error");



            DeletedWatch.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {

            if (Id == null)
                return RedirectToAction("Index", "error");

            WatchCard watch = await _context.WatchCards.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (watch == null)
                return RedirectToAction("Index", "error");

            return View(watch);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, WatchCard watch)
        {
           
            if (Id == null)
                return RedirectToAction("Index", "error");

           WatchCard dbWatch = await _context.WatchCards.FirstOrDefaultAsync(p => p.Id == Id);

            if (dbWatch == null)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
                return View(dbWatch);

           
           
            string filePath = Path.Combine(_env.WebRootPath, "images", "product");

            if (watch.MainImageFile != null)
            {

                if (!watch.MainImageFile.CheckContentType("image"))
                {
                    ModelState.AddModelError("MainImageFile", "MainImageFile tipini Duzgun Secin");
                    return View(dbWatch);
                }

                if (watch.MainImageFile.CheckLength(500))
                {
                    ModelState.AddModelError("MainImageFile", "MainImageFile Uzunlugu Maksimum 500 kb Ola Biler");
                    return View(dbWatch);
                }

                Helper.DeleteFile(filePath, dbWatch.Image);

                dbWatch.Image = await watch.MainImageFile.SaveFileAsync(filePath);

            }

            if (watch.HoverImageFile != null)
            {
                if (!watch.HoverImageFile.CheckContentType("image"))
                {
                    ModelState.AddModelError("HoverImageFile", "HoverImageFile tipini Duzgun Secin");
                    return View(watch);
                }

                if (watch.HoverImageFile.CheckLength(600))
                {
                    ModelState.AddModelError("HoverImageFile", "HoverImageFile Uzunlugu Maksimum 600 kb Ola Biler");
                    return View(watch);
                }

                Helper.DeleteFile(filePath, dbWatch.HoverImage);

                dbWatch.HoverImage = await watch.HoverImageFile.SaveFileAsync(filePath);
            }


            dbWatch.Desc = watch.Desc;
            dbWatch.Price = watch.Price;
            dbWatch.IsBestSeller = watch.IsBestSeller;
            dbWatch.IsDealsOftheWeek = watch.IsDealsOftheWeek;
            dbWatch.IsFeatured = watch.IsFeatured;
            dbWatch.IsNewArrivals = watch.IsNewArrivals;
            dbWatch.Code = watch.Code;
            dbWatch.Stock = watch.Stock;
            dbWatch.Star = watch.Star;
            dbWatch.DescDetail = watch.DescDetail;
            dbWatch.Brand = watch.Brand;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
