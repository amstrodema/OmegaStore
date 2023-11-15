using App.Services;
using Microsoft.AspNetCore.Mvc;
using Store.Business;
using Store.Model;

namespace Omega_Store.Controllers
{
    public class ManagerController : Controller
    {
        private readonly UserBusiness _userBusiness;
        public ManagerController(UserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Setup()
        {
            var res = await _userBusiness.Setup();

            if (res.StatusCode != 200)
            {
                TempData["MessageError"] = res.Message;
            }
            else
            {
                TempData["MessageSuccess"] = res.Message;
            }
            return View();
        }
    }
}
