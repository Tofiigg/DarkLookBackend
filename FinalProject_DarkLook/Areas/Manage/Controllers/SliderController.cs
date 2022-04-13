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
{ [Area("manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.Where(s => s.IsDeleted == false).ToListAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (_context.Sliders.Where(s => s.IsDeleted == false).Count() >= 5)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
                return View(slider);

            if (!slider.File.CheckContentType("image"))
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View(slider);
            }

            if (slider.File.CheckLength(300))
            {
                ModelState.AddModelError("File", "Seklin Olcusu Maksimum 300 kb ola Biler");
                return View(slider);
            }

            slider.OriginalImageName = slider.File.FileName;

            string filepath = Path.Combine(_env.WebRootPath, "images");

            slider.Image = await slider.File.SaveFileAsync(filepath);

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (slider == null)
                return RedirectToAction("Index", "error");

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, Slider slider)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Slider Deletedslider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == Id);

            if (slider == null)
                return RedirectToAction("Index", "error");

            string path = Path.Combine(_env.WebRootPath, "image", "bg-images");

            Helper.DeleteFile(path, Deletedslider.Image);

            Deletedslider.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
