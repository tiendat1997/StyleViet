using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StyleViet.Service.Interface;
using StyleViet.Service.ViewModel;

namespace StyleViet.WebApp.Controllers
{    
    [Authorize(Roles = "Salon")]
    public class BusinessController : Controller
    {
        private readonly ISalonService _salonService;

        public BusinessController(ISalonService salonService)
        {
            _salonService = salonService;
        }
        
        public IActionResult Index()
        {
           
            return View();
        }

       
    }
}