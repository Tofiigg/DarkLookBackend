using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.Models;
using FinalProject_DarkLook.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Controllers
{
    public class BlogController : Controller
    {

        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            return View(new BLogVM
            {

               Blogs = await _context.Blogs.Where(x => x.IsDeleted == false).ToListAsync(),
               Categories=await _context.Categories.Where(x=>x.IsDeleted==false).ToListAsync()


        });
        }
        public IActionResult BlogDetails(int? Id)
        {
            if(Id==null)return RedirectToAction("index");
            Blog blog = _context.Blogs.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == Id);
            return View(blog);

        }

    }
}
