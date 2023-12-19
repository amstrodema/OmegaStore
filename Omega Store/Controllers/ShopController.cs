using Microsoft.AspNetCore.Mvc;
using Store.Business;
using Store.Data.Interface;
using Store.Model.ViewModel;

namespace Omega_Store.Controllers
{
    public class ShopController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreBusiness _storeBusiness;
        public ShopController(IUnitOfWork unitOfWork, StoreBusiness storeBusiness)
        {
            _unitOfWork = unitOfWork;
            _storeBusiness = storeBusiness;
        }
        [Route("Shop")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public async Task<IActionResult> Item(string t)
        {
            var res = await _storeBusiness.GetItem(t);
            return View(res);
        }
        public IActionResult Cart()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ItemFeatures([FromBody] Guid itemID)
        {
            var res = from feature in await _unitOfWork.Features.GetByItemID(itemID)
                      select new FeaturePicker()
                      {
                          ID = feature.ID,
                          Name = feature.Name,
                          Option = feature.Options
                      };
            return Ok(res);
        }
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
