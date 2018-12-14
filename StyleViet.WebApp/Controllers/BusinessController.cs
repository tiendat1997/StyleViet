using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StyleViet.WebApp.Controllers
{    
    [Authorize(Roles = "Salon")]
    public class BusinessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}