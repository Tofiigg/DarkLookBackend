using FinalProject_DarkLook.DAL;
using FinalProject_DarkLook.Models;
using FinalProject_DarkLook.ViewModels.AcoountViewModel;
using FinalProject_DarkLook.ViewModels.Watch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Services
{
    public class LayoutService
    {
    private readonly IHttpContextAccessor _httpContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;

    public LayoutService(IHttpContextAccessor httpContext, UserManager<AppUser> userManager, AppDbContext context)
    {
        _httpContext = httpContext;
        _userManager = userManager;
        _context = context;
    }

        public Setting GetSetting()
        {
            return _context.Setting.FirstOrDefault();
        }
        public async Task<AppUserVM> GetUser()
        {
            AppUserVM appUserVM = null;
            if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity.Name);

                appUserVM = new AppUserVM
                {
                    Name = appUser.Name,
                    SurName = appUser.SurName
                };
            }

            return appUserVM;
        }


        public async Task<List<BasketWatch>> GetBasket()
        {



            List<BasketWatch> basketWatches = new List<BasketWatch>();


            if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
               AppUser appUser = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity.Name);

               basketWatches= await _context.BasketWatches.Include(x=>x.Watch).Where(x => x.IsDeleted == false && x.AppUserId == appUser.Id).ToListAsync();
            }



            //string strBasket = _httpContext.HttpContext.Request.Cookies["basket"];

            //List<BasketVM> Watches = null;

            //if (strBasket == null)
            //{
            //    Watches = new List<BasketVM>();
            //}
            //else
            //{
            //    Watches = JsonConvert.DeserializeObject<List<BasketVM>>(strBasket);
            //}
            return basketWatches;
        }


    }
}
