﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StyleViet.Service.Constant;
using StyleViet.Service.Enum;
using StyleViet.Service.Interface;
using StyleViet.Service.Model;
using StyleViet.Service.ViewModel;

namespace StyleViet.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IProfileService _profileService;

        public AuthController(IAuthService authService, IProfileService profileService)
        {
            _authService = authService;
            _profileService = profileService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Join()
        {
            return View();
        }       
        public async Task<IActionResult> Login()
        {
            var result = await HttpContext.AuthenticateAsync(TemporaryAuthenticationDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }
            var vm = new LoginViewModel();

            return View(vm);
        }
        [Route("login/{provider}")]
        public IActionResult Login(string provider, string returnUrl = null)
        {
            var profileUrl = !string.IsNullOrWhiteSpace(returnUrl) ? $"{Url.Action("Profile")}?returnUrl={returnUrl}" : Url.Action("Profile");

            return Challenge(new AuthenticationProperties { RedirectUri = profileUrl }, provider);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _authService.AccountLogin(model);

                if (result.IsSuccess)
                {
                    var claims = new List<Claim>{new Claim(ClaimTypes.Name, model.Username)};
                    string action = "Index";
                    string controller = "Home";
                    if (result.RoleId == (int)RoleEnum.Member)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, RoleEnum.Member.ToString()));
                    }           
                    else
                    {
                        action = "Index";
                        controller = "Business";
                        claims.Add(new Claim(ClaimTypes.Role, RoleEnum.Salon.ToString()));
                    }
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction(action, controller);
                }
                else
                {
                    TempData["LoginStatus"] = "Login Failed.Please enter correct credentials";
                    return View();
                }
            }
            else
                return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind] MemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                string status = _authService.RegisterMember(model);               
                if (status.Equals("Success"))
                {
                    ModelState.Clear();
                    TempData["RegisterStatus"] = "Registration Successful!";
                }
                else
                {
                    TempData["RegisterStatus"] = "Registration Failed ! Please try again";
                }
                return View();
            }
            return View();
        }

        [Route("Join/Register")]
        public IActionResult SalonRegister()
        {
            return View("SalonRegister");
        }

        [HttpPost]
        [Route("Join/Register")]
        public IActionResult SalonRegister(SalonViewModel model)
        {
            if (ModelState.IsValid)
            {
                string status = _authService.RegisterSalon(model);
                if (status.Equals("Success"))
                {
                    ModelState.Clear();
                    TempData["RegisterStatus"] = "Registration Successful!";
                }
                else
                {
                    TempData["RegisterStatus"] = "Registration Failed ! Please try again";
                }
                return View();
            }
            return View();            
        }

        public async Task<IActionResult> Profile(string returnUrl = null)
        {
            var result = await HttpContext.AuthenticateAsync(TemporaryAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            var username = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _profileService.RetrieveAsync(username);
            if (profile != null)
            {
                return await SignInUserAsync(profile, returnUrl);
            }

            var vm = new ProfileViewModel
            {
                UserName = username,
                Name = result.Principal.Identity.Name,
                Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
                Address = result.Principal.FindFirst(ClaimTypes.StreetAddress)?.Value,
                ReturnUrl = returnUrl
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var profile = new Profile
                {                   
                    UserName = model.UserName,
                    Name = model.Name,
                    Email = model.Email,
                    Address = model.Address
                };
                await _profileService.CreateAsync(profile);
                return await SignInUserAsync(profile, model.ReturnUrl);
            }

            return View(model);
        }
        private async Task<IActionResult> SignInUserAsync(Profile profile, string returnUrl)
        {
            await HttpContext.SignOutAsync(TemporaryAuthenticationDefaults.AuthenticationScheme);
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, profile.UserName),
                new Claim(ClaimTypes.Name, profile.UserName),
                new Claim(ClaimTypes.Email, profile.Email)
            };

            if (!string.IsNullOrWhiteSpace(profile.Address))
            {
                claims.Add(new Claim(ClaimTypes.StreetAddress, profile.Address));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Redirect(string.IsNullOrWhiteSpace(returnUrl) ? "/Home/Index" : returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

    }
}