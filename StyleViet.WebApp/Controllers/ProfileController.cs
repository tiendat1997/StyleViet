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
    public class ProfileController : Controller
    {
        private readonly ISalonService _salonService;
        public ProfileController(ISalonService salonService)
        {
            _salonService = salonService;
        }
        public IActionResult Index()
        {
            var context = Request.HttpContext;
            var id = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var salonModel = _salonService.GetProfile(id);
            return View(salonModel);
        }

        public IActionResult Update(SalonViewModel model)
        {
            if (model != null)
            {
                _salonService.UpdateProfile(model);
            }
            return RedirectToAction("Index");
        }
    }
}