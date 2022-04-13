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
    
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public NewsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            ViewBag.TotalPage = Math.Ceiling(_context.News.Count() / 8m);
            ViewBag.Seltectedpage = page;
            return View(await _context.News.OrderByDescending(x=>x.Id).Where(x=>x.IsDeleted==false).Skip((page-1)*6).Take(6).ToListAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news)
        {
            //if (_context.News.Where(s => s.IsDeleted == false).Count() >= 15)
            //    return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
                return View(news);

            if (!news.File.CheckContentType("image"))
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View(news);
            }

            if (news.File.CheckLength(600))
            {
                ModelState.AddModelError("File", "Seklin Olcusu Maksimum 300 kb ola Biler");
                return View(news);
            }

            news.OriginalImageName = news.File.FileName;

            string filepath = Path.Combine(_env.WebRootPath, "images", "blog");

            news.Image = await news.File.SaveFileAsync(filepath);

            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            News news = await _context.News.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (news == null)
                return RedirectToAction("Index", "error");

            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            News news = await _context.News.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (news == null)
                return RedirectToAction("Index", "error");

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, News news)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
            {
                return View(news);
            }

            News dbNewsr = await _context.News.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbNewsr == null)
                return RedirectToAction("Index", "error");

            if (news.File != null)
            {
                if (!news.File.CheckContentType("image"))
                {
                    ModelState.AddModelError("File", "Duzgun File Secin");
                    return View(news);
                }

                if (news.File.CheckLength(300))
                {
                    ModelState.AddModelError("File", "Seklin Olcusu Maksimum 300 kb ola Biler");
                    return View(news);
                }

                string filepath = Path.Combine(_env.WebRootPath, "images", "blog");

                Helper.DeleteFile(filepath, dbNewsr.Image);

                dbNewsr.OriginalImageName = news.File.FileName;

                dbNewsr.Image = await news.File.SaveFileAsync(filepath);
            }

            dbNewsr.Title = news.Title;
            dbNewsr.Desc = news.Desc;
            dbNewsr.Info = news.Info;
            dbNewsr.Month = news.Month;
            dbNewsr.RedInfo = news.RedInfo;
            dbNewsr.SingleDesc = news.SingleDesc;
            dbNewsr.Day = news.Day;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            News news = await _context.News.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (news == null)
                return RedirectToAction("Index", "error");

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, News news)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            News DeletedNews = await _context.News.FirstOrDefaultAsync(s => s.Id == Id);

            if (news == null)
                return RedirectToAction("Index", "error");

            string path = Path.Combine(_env.WebRootPath, "images", "blog");

            Helper.DeleteFile(path, DeletedNews.Image);

            DeletedNews.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
