using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Business;
using Store.Data.Interface;
using Store.Model;
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
        public async Task<IActionResult> Index(string d)
        {
            if (!string.IsNullOrEmpty(d))
            {
                switch (d)
                {
                    case "watchlist":
                        {

                            return View("Favourites");
                        }
                    case "special-offer":
                        {
                            return View("SpecialOffer");
                        }
                    case "most-viewed":
                        {
                            return View("MostViewed");
                        }
                    case "tracking":
                        {
                            return View("Tracking");
                        }
                    case "most-purchased":
                        {
                            return View("MostPurchased");
                        }

                    default:
                        break;
                }
            }

            var res = await _storeBusiness.GetVMForShop();
            return View(res);
        }
        public IActionResult Category()
        {
            return View();
        }
        //public IActionResult Tracking()
        //{
        //    return View();
        //}
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
        public async Task<IActionResult> GetCart([FromBody] string orders)
        {
            try
            {
                var orderVM = JsonConvert.DeserializeObject<OrderVM[]>(orders);
                var res = await _storeBusiness.GetCart(orderVM);
                if (res.Count() < 1)
                {
                    return PartialView("_nocontent");
                }
                return PartialView("_cart", res);
            }
            catch (Exception)
            {
                return PartialView("_nocontent");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetFave([FromBody] string faves)
        {
            try
            {
                var favorites = JsonConvert.DeserializeObject<Guid[]>(faves);
                var res = await _storeBusiness.GetVMForFave(favorites);
                return PartialView("_favourite", res);
            }
            catch (Exception)
            {
                return PartialView("_nocontent");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ItemFeatures([FromBody] Guid itemID)
        {
            var res = from feature in await _unitOfWork.Features.GetByItemID(itemID)
                      select new FeaturePicker()
                      {
                          //ID = feature.ID,
                          Name = feature.Name,
                          Option = feature.Options
                      };
            return Ok(res);
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Currency([FromBody] string val)
        {
            GenericBusiness.ShoppingCurrencySymbol = val == "NGN" ? "₦" : "$";
            GenericBusiness.ShoppingCurrency = val;
            return Ok();
        }
    }
}
