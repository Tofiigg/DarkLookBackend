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
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ContactController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacts.OrderByDescending(x => x.Id).Where(s => s.IsDelete == false).ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (_context.OurTeams.Where(s => s.IsDeleted == false).Count() >= 2)
                return RedirectToAction("Index", "error");

            if (ModelState.IsValid)
                return View(contact);

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Contact contact = await _context.Contacts.FirstOrDefaultAsync(s => s.Id == Id && s.IsDelete == false);

            if (contact == null)
                return RedirectToAction("Index", "error");

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Contact contact)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            Contact dbContact = await _context.Contacts.FirstOrDefaultAsync(s => s.Id == Id && s.IsDelete == false);

            if (dbContact == null)
                return RedirectToAction("Index", "error");


            dbContact.Location = contact.Location;
            dbContact.Number = contact.Number;
            dbContact.Email = contact.Email;
            dbContact.Careers = contact.Careers;
            dbContact.SayHello = contact.SayHello;
            dbContact.InfoEmail = contact.InfoEmail;


            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Contact Contact = await _context.Contacts.FirstOrDefaultAsync(s => s.Id == Id && s.IsDelete == false);

            if (Contact == null)
                return RedirectToAction("Index", "error");

            return View(Contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, Contact contact)
        {
            if (Id == null)
                return RedirectToAction("Index", "error");

            Contact DeletedContact= await _context.Contacts.FirstOrDefaultAsync(s => s.Id == Id);

            if (contact == null)
                return RedirectToAction("Index", "error");



            DeletedContact.IsDelete = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }





    }
}
