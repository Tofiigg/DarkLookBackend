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
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BrandController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.BrandLogos.OrderByDescending(x => x.Id).Where(s => s.IsDeleted == false).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandLogo brandLogo)
        {
            if (_context.BrandLogos.Where(s => s.IsDeleted == false).Count() >= 8)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
                return View(brandLogo);

            if (!brandLogo.File.CheckContentType("image"))
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View(brandLogo);
            }

            if (brandLogo.File.CheckLength(700))
            {
                ModelState.AddModelError("File", "Seklin Olcusu Maksimum 700 kb ola Biler");
                return View(brandLogo);
            }

            brandLogo.OriginalImageName = brandLogo.File.FileName;

            string filepath = Path.Combine(_env.WebRootPath, "images" ,"brand");

            brandLogo.Image = await brandLogo.File.SaveFileAsync(filepath);

            await _context.BrandLogos.AddAsync(brandLogo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            BrandLogo brandLogo = await _context.BrandLogos.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (brandLogo == null)
                return RedirectToAction("Index", "error");

            return View(brandLogo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, BrandLogo brandLogo)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
            {
                return View(brandLogo);
            }

            BrandLogo dbBrand = await _context.BrandLogos.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbBrand == null)
                return RedirectToAction("Index", "error");

            if (brandLogo.File != null)
            {
                if (!brandLogo.File.CheckContentType("image"))
                {
                    ModelState.AddModelError("File", "Duzgun File Secin");
                    return View(brandLogo);
                }

                if (brandLogo.File.CheckLength(700))
                {
                    ModelState.AddModelError("File", "Seklin Olcusu Maksimum 700 kb ola Biler");
                    return View(brandLogo);
                }

                string filepath = Path.Combine(_env.WebRootPath, "images","brand");

                Helper.DeleteFile(filepath, dbBrand.Image);

                dbBrand.OriginalImageName = brandLogo.File.FileName;

                dbBrand.Image = await brandLogo.File.SaveFileAsync(filepath);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            BrandLogo brandLogo = await _context.BrandLogos.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (brandLogo == null)
                return RedirectToAction("Index", "error");

            return View(brandLogo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, BrandLogo brandLogo)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            BrandLogo DeletedBrand = await _context.BrandLogos.FirstOrDefaultAsync(s => s.Id == Id);

            if (brandLogo == null)
                return RedirectToAction("Index", "error");



            DeletedBrand.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
