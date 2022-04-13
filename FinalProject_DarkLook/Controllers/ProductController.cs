using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FinalProject_DarkLook.ViewModels.Watch;

namespace FinalProject_DarkLook.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(AppDbContext Context, UserManager<AppUser> userManager)
        {
            _context = Context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetDetail(int? Id)
        {
            if (Id == null) return RedirectToAction("Index","error");

            WatchCard watchCard = await _context.WatchCards.Include(x=>x.watchColours).Include(x=>x.watchSizes).FirstOrDefaultAsync(x => x.Id == Id);
            if (watchCard == null) return RedirectToAction("Index", "error");
            return Json(watchCard);
                
        }


        public async Task<IActionResult> AddBasket(int? Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (Id == null)
                return RedirectToAction("Index", "error");

            WatchCard watchCard = await _context.WatchCards.FirstOrDefaultAsync(x => x.Id == Id);
            if (watchCard == null)
                return RedirectToAction("Index", "error");


            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            BasketWatch existedbasketWatch = await _context.BasketWatches.
                FirstOrDefaultAsync(x => x.IsDeleted == false && x.AppUserId == appUser.Id && x.WatchId == watchCard.Id);

            if (existedbasketWatch != null)
            {
                existedbasketWatch.Count += 1;
            }
            else
            {

            BasketWatch basketWatch = new BasketWatch
            {
                AppUserId = appUser.Id,
                WatchId = watchCard.Id,
                Count = 1,
                CreatedTime = DateTime.Now
            };

                await _context.BasketWatches.AddAsync(basketWatch);

            }

            await _context.SaveChangesAsync();

            

            return RedirectToAction("Index", "shop");
        }


        public async Task<IActionResult> ShowBasket()
        {
            string strBasket = HttpContext.Request.Cookies["basket"];

            List<BasketVM> Watches = null;

            if (strBasket == null)
            {
                Watches = new List<BasketVM>();
            }
            else
            {
                Watches = JsonConvert.DeserializeObject<List<BasketVM>>(strBasket);
            }
            return Json(Watches);
        }

        public async Task<IActionResult> ViewCartBasket()
        {
            List<BasketWatch> basketwatches = new List<BasketWatch>();

            if (User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

                basketwatches = await _context.BasketWatches.Include(b => b.Watch)
                    .Where(b => b.IsDeleted == false).ToListAsync();
            }

            return View(basketwatches);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewCartBasket(BasketWatch basketWatch)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (basketWatch.AppUserId != appUser.Id)
            {
                return RedirectToAction("Index", "error");
            }

            List<BasketWatch> basketWatches = await _context.BasketWatches.Include(b => b.Watch)
                    .Where(b => b.IsDeleted == false).ToListAsync();

            BasketWatch dbBasketWatch = basketWatches
                .FirstOrDefault(b => b.Id == basketWatch.Id &&
                b.AppUserId == appUser.Id &&
                b.WatchId == basketWatch.WatchId &&
                b.IsDeleted == false);

            if (dbBasketWatch == null)
            {
                return RedirectToAction("Index", "error");
            }

            dbBasketWatch.Count = basketWatch.Count;

            await _context.SaveChangesAsync();

            return View(basketWatches);
        }

        public async Task<IActionResult> DeleteBasket(int? Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (Id == null) return RedirectToAction("Index", "error");

            List<BasketWatch> basketWatches = await _context.BasketWatches.Include(b => b.Watch)
                    .Where(b => b.IsDeleted == false).ToListAsync();

            BasketWatch basketWatch = basketWatches
                .FirstOrDefault(b => b.Id == Id && b.IsDeleted == false && b.AppUserId == appUser.Id);

            if (basketWatches == null) return RedirectToAction("Index", "error");

            basketWatch.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("ViewCartBasket");
        }


        public async Task<IActionResult> Search(string search)
        {

            IEnumerable<WatchCard> products = await _context.WatchCards
                .Include(p => p.watchSizes).ThenInclude(x=>x.Size)
                .Include(p => p.watchColours).ThenInclude(x=>x.Colour)
                .Where(p => p.Desc.ToLower().Contains(search.ToLower()) )
                .OrderByDescending(p => p.Id)
                .Take(10)
                .ToListAsync();

            return PartialView("_ProductSearchPartialcshtml", products);
        }


    }
}
