﻿using FinalProject_DarkLook.Models;
using FinalProject_DarkLook.ViewModels.AcoountViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Areas.Manage.Controllers
{
    [Area("manage")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {


            ICollection<AppUser> appUsers = await _userManager.Users.ToListAsync();

            List<AppUserVM> appUserVMs = new List<AppUserVM>();

            foreach (AppUser appUser in appUsers)
            {
                AppUserVM appUserVM = new AppUserVM
                {
                    Id = appUser.Id,
                    Name = appUser.Name,
                    SurName = appUser.SurName,
                    Email = appUser.Email,
                    Role = (await _userManager.GetRolesAsync(appUser))[0],
                    UserName = appUser.UserName,
                    IsDeleted = appUser.IsDeleted
                };

                appUserVMs.Add(appUserVM);
            }

            return View(appUserVMs);
        }

        public async Task<IActionResult> ChangeStatus(string Id, bool status)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            appUser.IsDeleted = status;
            await _userManager.UpdateAsync(appUser);

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(string Id)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            AppUserVM appUserVM = new AppUserVM
            {
                Id = appUser.Id,
                Name = appUser.Name,
                SurName = appUser.SurName,
                Email = appUser.Email,
                Role = (await _userManager.GetRolesAsync(appUser))[0],
                UserName = appUser.UserName,
                IsDeleted = appUser.IsDeleted,
                Roles = new List<string> { "Admin", "Member" }
            };

            return View(appUserVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(string Id, string Roles)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            string oldRole = (await _userManager.GetRolesAsync(appUser))[0];

            await _userManager.RemoveFromRoleAsync(appUser, oldRole);

            await _userManager.AddToRoleAsync(appUser, Roles);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangePassword(string Id)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            AppUserVM appUserVM = new AppUserVM
            {
                Id = appUser.Id,
                Name = appUser.Name,
                SurName = appUser.SurName,
                Email = appUser.Email,
                Role = (await _userManager.GetRolesAsync(appUser))[0],
                UserName = appUser.UserName,
                IsDeleted = appUser.IsDeleted,
                Roles = new List<string> { "Admin", "Member" }
            };

            return View(appUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string Id, string Password)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            await _userManager.ResetPasswordAsync(appUser, token, Password);

            return RedirectToAction("Index");
        }
    }
}
