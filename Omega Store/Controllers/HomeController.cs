using Microsoft.AspNetCore.Mvc;
using Omega_Store.Models;
using Store.Business;
using Store.Model.ViewModel;
using System.Diagnostics;

namespace Omega_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeneralBusiness _generalBusiness;

        public HomeController(ILogger<HomeController> logger, GeneralBusiness generalBusiness)
        {
            _logger = logger;
            _generalBusiness = generalBusiness;
        }

        public async Task<IActionResult> Index()
        {
           var res = await _generalBusiness.GetVMForHome();
            return View(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}