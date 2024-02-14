using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Omega_Store.Services;
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
        private readonly LoginValidator _loginValidator;
        public ShopController(IUnitOfWork unitOfWork, StoreBusiness storeBusiness, LoginValidator loginValidator)
        {
            _unitOfWork = unitOfWork;
            _storeBusiness = storeBusiness;
            _loginValidator = loginValidator;
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
        public async Task<IActionResult> Category(string t)
        {
            try
            {
                if (string.IsNullOrEmpty(t))
                {
                    return RedirectToAction("Index");
                }
                var res = await _storeBusiness.GetFromCategory(t);
                return View(res);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        //public IActionResult Tracking()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Item(string t)
        {
            if (string.IsNullOrEmpty(t))
            {
                return RedirectToAction("Index");
            }
            var res = await _storeBusiness.GetItem(t);
            return View(res);
        }
        public IActionResult Cart()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            try
            {
                var userID = _loginValidator.GetUserID();
                //var review = JsonConvert.DeserializeObject<Review>(reviewJSON);
                //if (review == null)
                //{
                //    throw new Exception();
                //}
                return PartialView("_review", await _storeBusiness.AddReview(review, userID));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCart([FromBody] string orders)
        {
            try
            {
                var orderVM = JsonConvert.DeserializeObject<OrderVM[]>(orders);
                var res = await _storeBusiness.GetCart(orderVM);
                if (res.Orders.Count() < 1)
                {
                    return PartialView("_nocontent");
                }
                return PartialView("_cart", res.Orders);
            }
            catch (Exception)
            {
                return PartialView("_nocontent");
            }
        }
        public async Task<IActionResult> Confirmation()
        {
            try
            {
                if (TempData["CheckOutCart"].ToString() == "Confirmed")
                {
                    return View();
                }
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CheckOutCart(CheckOutVM checkOutVM)
        {
            TempData["CheckOutCart"] = "Confirmed";
            return RedirectToAction("Confirmation");
        }
        [HttpPost]
        public async Task<IActionResult> GetCheckOut([FromBody] string orders)
        {
            try
            {
                var orderVM = JsonConvert.DeserializeObject<OrderVM[]>(orders);
                var res = await _storeBusiness.GetCart(orderVM);
                if (res.Orders.Count() < 1)
                {
                    return PartialView("_nocontent");
                }
                _loginValidator.SetSession("cartHolder", orders);
                return PartialView("_checkout", res);
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
