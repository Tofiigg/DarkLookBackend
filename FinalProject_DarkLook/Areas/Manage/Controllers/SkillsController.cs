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
{
    [Area("manage")]
    public class SkillsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SkillsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Skills.OrderByDescending(x=>x.Id).Where(s => s.IsDeleted == false).ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skills skills)
        {
            if (_context.Skills.Where(s => s.IsDeleted == false).Count() >= 6)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
                return View(skills);

            await _context.Skills.AddAsync(skills);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Skills skills = await _context.Skills.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (skills == null)
                return RedirectToAction("Index", "error");

            return View(skills);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Skills skills)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
            {
                return View(skills);
            }

             Skills dbSkills = await _context.Skills.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (dbSkills == null)
                return RedirectToAction("Index", "error");


            dbSkills.Name = skills.Name;
            dbSkills.Percent = skills.Percent;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Skills skills = await _context.Skills.FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);

            if (skills == null)
                return RedirectToAction("Index", "error");

            return View(skills);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, Skills skills)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Skills DeletedSkills = await _context.Skills.FirstOrDefaultAsync(s => s.Id == Id);

            if (skills == null)
                return RedirectToAction("Index", "error");



            DeletedSkills.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
