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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Abouts.OrderByDescending(x => x.Id).Where(g => g.IsDeleted == false).ToListAsync());
        }

      

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            //if (_context.News.Where(s => s.IsDeleted == false).Count() >= 15)
            //    return NotFound();

            if (ModelState.IsValid)
                return View(about);

            if (!about.File.CheckContentType("image"))
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View(about);
            }

            if (about.File.CheckLength(600))
            {
                ModelState.AddModelError("File", "Seklin Olcusu Maksimum 300 kb ola Biler");
                return View(about);
            }

            about.OriginalImageName = about.File.FileName;

            string filepath = Path.Combine(_env.WebRootPath, "images");

            about.Image = await about.File.SaveFileAsync(filepath);

            await _context.Abouts.AddAsync(about);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            About about = await _context.Abouts.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (about == null)
                return RedirectToAction("Index", "error");

            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, About about)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
            {
                return View(about);
            }

            About dbAbout = await _context.Abouts.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbAbout == null)
                return RedirectToAction("Index", "error");

            if (about.File != null)
            {
                if (!about.File.CheckContentType("image"))
                {
                    ModelState.AddModelError("File", "Duzgun File Secin");
                    return View(about);
                }

                if (about.File.CheckLength(500))
                {
                    ModelState.AddModelError("File", "Seklin Olcusu Maksimum 500 kb ola Biler");
                    return View(about);
                }

                string filepath = Path.Combine(_env.WebRootPath, "images");

                Helper.DeleteFile(filepath, dbAbout.Image);

                dbAbout.OriginalImageName = about.File.FileName;

                dbAbout.Image = await about.File.SaveFileAsync(filepath);
            }

            dbAbout.Title = about.Title;
            dbAbout.Desc = about.Desc;
            dbAbout.Html = about.Html;
            dbAbout.Bootstrap = about.Bootstrap;
            dbAbout.Css = about.Css;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            About about = await _context.Abouts.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (about == null)
                return RedirectToAction("Index", "error");

            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, About about)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            About DeletedAbout = await _context.Abouts.FirstOrDefaultAsync(s => s.Id == Id);

            if (about == null)
                return RedirectToAction("Index", "error");

            string path = Path.Combine(_env.WebRootPath, "images");

            Helper.DeleteFile(path, DeletedAbout.Image);

            DeletedAbout.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}
