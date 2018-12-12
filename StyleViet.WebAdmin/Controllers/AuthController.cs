using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StyleViet.Service.Enum;
using StyleViet.Service.Interface;
using StyleViet.Service.ViewModel;

namespace StyleViet.WebAdmin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string LoginStatus = _authService.AdminLogin(model);

                if (LoginStatus == "Success")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, RoleEnum.Admin.ToString())
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home");
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
        public IActionResult Register([Bind] AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                string status = _authService.RegisterAdmin(model);
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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}