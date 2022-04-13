using FinalProject_DarkLook.Models;
using FinalProject_DarkLook.ViewModels.AcoountViewModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_DarkLook.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        public readonly RoleManager<IdentityRole> _rolemanager;
        public AccountController(UserManager<AppUser> manager, SignInManager<AppUser> signInManager, IWebHostEnvironment env, RoleManager<IdentityRole> rolemanager)
        {
            _manager = manager;
            _signInManager = signInManager;
            _env = env;
            _rolemanager = rolemanager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);

            }

            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,
                SurName = registerVM.SurName,
                Email = registerVM.Email,
                UserName = registerVM.UserName
            };

            IdentityResult identityResult = await _manager.CreateAsync(appUser, registerVM.Password);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
                return View(registerVM);
            }

            await _signInManager.SignInAsync(appUser, false);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
                return View(registerVM);
            }

            await _signInManager.SignInAsync(appUser, false);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DarkLook", "darklook.t@gmail.com"));
            message.To.Add(new MailboxAddress(appUser.Name, appUser.Email));
            message.Subject = "Email Tesdiqleyin";

            string emailbody = string.Empty;

            using (StreamReader stream = new StreamReader(Path.Combine(_env.WebRootPath, "template", "confirmemail.html")))
            {
                emailbody = stream.ReadToEnd();
            }

            string emailconfirmtokent = await _manager.GenerateEmailConfirmationTokenAsync(appUser);

            string url = Url.Action("confirmemail", "account", new { Id = appUser.Id, token = emailconfirmtokent }, Request.Scheme);

            emailbody = emailbody.Replace("{{fullName}}", $"{appUser.Name} {appUser.SurName}").Replace("{{url}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("darklook.t@gmail.com", "tofig1010");
            smtp.Send(message);
            smtp.Disconnect(true);


            return RedirectToAction("Login", "account");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)

                return RedirectToAction("Index", "error");

            AppUser appUser = await _manager.FindByEmailAsync(loginVM.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("", "Email Və Ya şifrə Yanlışdir");
                return View(loginVM);
            }

            if (appUser.EmailConfirmed == false)
            {
                return RedirectToAction("login", "account");
            }

            Microsoft.AspNetCore.Identity.SignInResult signinResult = await _signInManager
                .PasswordSignInAsync(appUser, loginVM.Password, true, true);

            if (signinResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Email Bloklanıb");
                return View(loginVM);
            }

            if (!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Email Və Ya şifrə Yanlışdir");
                return View(loginVM);
            }


            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction("Index", "error");
            }

            AppUser appUser = await _manager.FindByIdAsync(Id);

            if (appUser == null)
            {
                return RedirectToAction("Index", "error");
            }

            IdentityResult identityResult = await _manager.ConfirmEmailAsync(appUser, token);
            if (!identityResult.Succeeded)
            {
                return RedirectToAction("Index", "error");
            }

            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction("Index", "error");
            AppUser appUser = await _manager.FindByEmailAsync(email);

            if (appUser == null)
                return RedirectToAction("Index", "error");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DarkLook", "darklook.t@gmail.com"));
            message.To.Add(new MailboxAddress(appUser.Name, appUser.Email));
            message.Subject = "Reset Password";

            string emailbody = string.Empty;

            using (StreamReader stream = new StreamReader(Path.Combine(_env.WebRootPath, "template", "forgotpassword.html")))
            {
                emailbody = stream.ReadToEnd();
            }

            string forgetpasswordtoken = await _manager.GeneratePasswordResetTokenAsync(appUser);

            string url = Url.Action("changepassword", "account", new { Id = appUser.Id, token = forgetpasswordtoken }, Request.Scheme);

            emailbody = emailbody.Replace("{{fullName}}", $"{appUser.Name} {appUser.SurName}").Replace("{{url}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("darklook.t@gmail.com", "tofig1010");
            smtp.Send(message);
            smtp.Disconnect(true);

            return View();
        }



        public async Task<IActionResult> ChangePassword(string Id, string token)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction("Index", "error");
            }

            AppUser appUser = await _manager.FindByIdAsync(Id);

            if (appUser == null)
            {
                return RedirectToAction("Index", "error");
            }

            ResetPasswordVM resetPasswordVM = new ResetPasswordVM
            {
                Id = Id,
                Token = token
            };

            return View(resetPasswordVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(resetPasswordVM.Id) || string.IsNullOrWhiteSpace(resetPasswordVM.Token))
            {
                return RedirectToAction("Index", "error");
            }

            AppUser appUser = await _manager.FindByIdAsync(resetPasswordVM.Id);

            if (appUser == null)
            {
                return RedirectToAction("Index", "error");
            }

            IdentityResult identityResult = await _manager.ResetPasswordAsync(appUser, resetPasswordVM.Token, resetPasswordVM.Password);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(resetPasswordVM);
            }

            return RedirectToAction("Login");
        }

        #region Add Role
        public async Task<IActionResult> AddRole()
        {
            if (!await _rolemanager.RoleExistsAsync("Admin"))
            {
                await _rolemanager.CreateAsync(new IdentityRole { Name = "Admin" });
            }
            if (!await _rolemanager.RoleExistsAsync("Member"))
            {
                await _rolemanager.CreateAsync(new IdentityRole { Name = "Member" });
            }
            if (!await _rolemanager.RoleExistsAsync("User"))
            {
                await _rolemanager.CreateAsync(new IdentityRole { Name = "User" });
            }

            return Content("Role Yarandi");
        }
        #endregion

    }
}
