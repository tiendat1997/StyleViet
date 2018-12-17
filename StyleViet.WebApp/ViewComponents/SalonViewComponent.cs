using Microsoft.AspNetCore.Mvc;
using StyleViet.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StyleViet.WebApp.ViewComponents
{
    public class SalonViewComponent : ViewComponent
    {
        private readonly ISalonService _salonService;

        public SalonViewComponent(ISalonService salonService)
        {
            _salonService = salonService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {            
            return View(await _salonService.GetAllSalon());
        }

    }
}
