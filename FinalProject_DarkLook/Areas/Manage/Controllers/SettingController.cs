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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Setting.OrderByDescending(x => x.Id).Where(s => s.IsDeleted == false).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setting setting)
        {
            if (_context.Setting.Where(s => s.IsDeleted == false).Count() >= 1)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
                return View(setting);

            if (!setting.File.CheckContentType("image"))
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View(setting);
            }

            if (setting.File.CheckLength(700))
            {
                ModelState.AddModelError("File", "Seklin Olcusu Maksimum 700 kb ola Biler");
                return View(setting);
            }

            setting.OriginalImageName = setting.File.FileName;

            string filepath = Path.Combine(_env.WebRootPath, "images");

            setting.Logo = await setting.File.SaveFileAsync(filepath);

            await _context.Setting.AddAsync(setting);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Setting setting = await _context.Setting.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (setting == null)
                return RedirectToAction("Index", "error");

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Setting setting)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
            {
                return View(setting);
            }

            Setting dbSetting = await _context.Setting.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbSetting == null)
                return RedirectToAction("Index", "error");

            if (setting.File != null)
            {
                if (!setting.File.CheckContentType("image"))
                {
                    ModelState.AddModelError("File", "Duzgun File Secin");
                    return View(setting);
                }

                if (setting.File.CheckLength(700))
                {
                    ModelState.AddModelError("File", "Seklin Olcusu Maksimum 700 kb ola Biler");
                    return View(setting);
                }

                string filepath = Path.Combine(_env.WebRootPath, "images");

                Helper.DeleteFile(filepath, dbSetting.Logo);

                dbSetting.OriginalImageName = setting.File.FileName;

                dbSetting.Logo = await setting.File.SaveFileAsync(filepath);
            }

            dbSetting.Adress = setting.Adress;
            dbSetting.Email = setting.Email;
            dbSetting.Street = setting.Street;
            dbSetting.WebUrl = setting.WebUrl;
            dbSetting.WorkTimeDesc = setting.WorkTimeDesc;
            dbSetting.Twitter = setting.Twitter;
            dbSetting.LastUrl = setting.LastUrl;
            dbSetting.FacebookUrl = setting.FacebookUrl;
            dbSetting.GoogleUrl = setting.GoogleUrl;
            dbSetting.LinkUrl = setting.LinkUrl;
            dbSetting.Number = setting.Number;




            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Setting setting = await _context.Setting.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (setting == null)
                return RedirectToAction("Index", "error");

            return View(setting);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Setting setting = await _context.Setting.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (setting == null)
                return RedirectToAction("Index", "error");

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, Setting setting)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Setting DeletedSetting = await _context.Setting.FirstOrDefaultAsync(s => s.Id == Id);

            if (setting == null)
                return RedirectToAction("Index", "error");



            DeletedSetting.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}
