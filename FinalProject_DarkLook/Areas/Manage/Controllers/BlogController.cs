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

    public class blogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public blogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index(int page=1)
        {

            ViewBag.TotalPage = Math.Ceiling(_context.Blogs.Count() / 8m);
            ViewBag.Seltectedpage = page;
            return View(await _context.Blogs.OrderByDescending(x=>x.Id).Where(s => s.IsDeleted == false).Skip((page-1)*4).Take(4).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (_context.Blogs.Where(s => s.IsDeleted == false).Count() >= 20)
                return NotFound();

            if (ModelState.IsValid)
                return View(blog);

            if (!blog.File.CheckContentType("image"))
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View(blog);
            }

            if (blog.File.CheckLength(600))
            {
                ModelState.AddModelError("File", "Seklin Olcusu Maksimum 600 kb ola Biler");
                return View(blog);
            }

            blog.OriginalImageName = blog.File.FileName;

            string filepath = Path.Combine(_env.WebRootPath, "images", "blog");

            blog.Image = await blog.File.SaveFileAsync(filepath);

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Blog blogs = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (blogs == null)
                return RedirectToAction("Index", "error");
            return View(blogs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Blog blogs)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "error");
            }

            Blog dbblogsr = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbblogsr == null)
                return RedirectToAction("Index", "error");
            if (blogs.File != null)
            {
                if (!blogs.File.CheckContentType("image"))
                {
                    ModelState.AddModelError("File", "Duzgun File Secin");
                    return View(blogs);
                }

                if (blogs.File.CheckLength(600))
                {
                    ModelState.AddModelError("File", "Seklin Olcusu Maksimum 300 kb ola Biler");
                    return View(blogs);
                }

                string filepath = Path.Combine(_env.WebRootPath, "images", "blog");

                Helper.DeleteFile(filepath, dbblogsr.Image);

                dbblogsr.OriginalImageName = blogs.File.FileName;

                dbblogsr.Image = await blogs.File.SaveFileAsync(filepath);
            }

            dbblogsr.Title = blogs.Title;
            dbblogsr.Desc = blogs.Desc;
            dbblogsr.Info = blogs.Info;
            dbblogsr.Date = blogs.Date;
            dbblogsr.RedSide = blogs.RedSide;
            dbblogsr.AfterRed = blogs.AfterRed;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (blog == null)
                return RedirectToAction("Index", "error");

            return View(blog);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (blog == null)
                return RedirectToAction("Index", "error");

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, Blog blog)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Blog DeletedBlog = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == Id);

            if (blog == null)
                return RedirectToAction("Index", "error");

            string path = Path.Combine(_env.WebRootPath, "images", "blog");

            Helper.DeleteFile(path, DeletedBlog.Image);

            DeletedBlog.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
