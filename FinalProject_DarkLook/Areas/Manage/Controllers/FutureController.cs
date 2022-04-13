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
{[Area("manage")]
    public class FutureController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FutureController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Futures.OrderByDescending(x => x.Id).Where(s => s.IsDeleted == false).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Future future)
        {
            if (_context.Futures.Where(s => s.IsDeleted == false).Count() >= 9)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
                return View(future);

            await _context.Futures.AddAsync(future);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Future future = await _context.Futures.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (future == null)
                return RedirectToAction("Index", "error");

            return View(future);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Future future)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
            {
                return View(future);
            }

            Future dbFuture = await _context.Futures.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbFuture == null)
                return RedirectToAction("Index", "error");


            dbFuture.Title = future.Title;
            dbFuture.Icon = future.Icon;
            dbFuture.Desc = future.Desc;


            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Future future = await _context.Futures.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (future == null)
                return RedirectToAction("Index", "error");

            return View(future);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, Future future)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Future DeletedFuture = await _context.Futures.FirstOrDefaultAsync(s => s.Id == Id);

            if (future == null)
                return RedirectToAction("Index", "error");



            DeletedFuture.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
