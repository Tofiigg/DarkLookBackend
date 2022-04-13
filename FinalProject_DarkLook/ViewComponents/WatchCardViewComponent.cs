using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.ViewComponents
{
    public class WatchCardViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public WatchCardViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<WatchCard> watchCards = await _context.WatchCards.Where(x => x.IsDeleted == false).ToListAsync();

            return View(await Task.FromResult(watchCards));
        }


    }
}
