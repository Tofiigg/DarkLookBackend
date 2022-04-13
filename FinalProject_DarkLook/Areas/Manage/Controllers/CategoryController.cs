using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Areas.Manage.Controllers
{ [Area("manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.OrderByDescending(x => x.Id).Where(s => s.IsDeleted == false).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (_context.Categories.Where(s => s.IsDeleted == false).Count() >= 9)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
                return View(category);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Category category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (category == null)
                return RedirectToAction("Index", "error");

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Category category)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbCategory == null)
                return RedirectToAction("Index", "error");


            dbCategory.Name = category.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Category category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (category == null)
                return RedirectToAction("Index", "error");

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, Category category)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Category DeletedCategory = await _context.Categories.FirstOrDefaultAsync(s => s.Id == Id);

            if (category == null)
                return RedirectToAction("Index", "error");



            DeletedCategory.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
