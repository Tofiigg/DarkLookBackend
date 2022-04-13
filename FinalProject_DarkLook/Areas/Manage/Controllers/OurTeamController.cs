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
    public class OurTeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public OurTeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.OurTeams.OrderByDescending(x=>x.Id).Where(s => s.IsDeleted == false).ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OurTeam ourTeam)
        {
            if (_context.OurTeams.Where(s => s.IsDeleted == false).Count() >= 8)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
                return View(ourTeam);

            if (!ourTeam.File.CheckContentType("image"))
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View(ourTeam);
            }

            if (ourTeam.File.CheckLength(700))
            {
                ModelState.AddModelError("File", "Seklin Olcusu Maksimum 700 kb ola Biler");
                return View(ourTeam);
            }

            ourTeam.OriginalImageName = ourTeam.File.FileName;

            string filepath = Path.Combine(_env.WebRootPath, "images");

            ourTeam.Image = await ourTeam.File.SaveFileAsync(filepath);

            await _context.OurTeams.AddAsync(ourTeam);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            OurTeam ourTeam = await _context.OurTeams.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (ourTeam == null)
                return RedirectToAction("Index", "error");

            return View(ourTeam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, OurTeam ourTeam)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
            {
                return View(ourTeam);
            }

            OurTeam dbteam = await _context.OurTeams.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbteam == null)
                return RedirectToAction("Index", "error");

            if (ourTeam.File != null)
            {
                if (!ourTeam.File.CheckContentType("image"))
                {
                    ModelState.AddModelError("File", "Duzgun File Secin");
                    return View(ourTeam);
                }

                if (ourTeam.File.CheckLength(700))
                {
                    ModelState.AddModelError("File", "Seklin Olcusu Maksimum 700 kb ola Biler");
                    return View(ourTeam);
                }

                string filepath = Path.Combine(_env.WebRootPath, "images");

                Helper.DeleteFile(filepath, dbteam.Image);

                dbteam.OriginalImageName = ourTeam.File.FileName;

                dbteam.Image = await ourTeam.File.SaveFileAsync(filepath);
            }

            dbteam.Name = ourTeam.Name;
            dbteam.Profession = ourTeam.Profession;
            dbteam.Desc = ourTeam.Desc;
            dbteam.FaceBookUrl = ourTeam.FaceBookUrl;
            dbteam.TwitterUrl = ourTeam.TwitterUrl;
            dbteam.PinterestUrl = ourTeam.PinterestUrl;
            dbteam.Url = ourTeam.Url;
            dbteam.BeUrl = ourTeam.BeUrl;
          

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



     
        [HttpGet]
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            OurTeam ourTeam = await _context.OurTeams.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (ourTeam == null)
                return RedirectToAction("Index", "error");

            return View(ourTeam);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            OurTeam ourTeam = await _context.OurTeams.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (ourTeam == null)
                return RedirectToAction("Index", "error");

            return View(ourTeam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, OurTeam ourTeam)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            OurTeam DeletedTeam = await _context.OurTeams.FirstOrDefaultAsync(s => s.Id == Id);

            if (ourTeam == null)
                return RedirectToAction("Index", "error");



            DeletedTeam.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
