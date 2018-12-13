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

namespace StyleViet.WebApp.Controllers
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

        public IActionResult Join()
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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}