using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StyleViet.Service.ViewModel;

namespace StyleViet.WebApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var vm = new ProfileViewModel
            {
                Claims = User.Claims,
                Name = User.Identity.Name
            };
            return View(vm);
        }
    }
}