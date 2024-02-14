using App.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Omega_Store.Models;
using Omega_Store.Services;
using Store.Business;
using Store.Model;
using Store.Model.ViewModel;

namespace Omega_Store.Controllers
{
    public class ManagerController : Controller
    {
        private readonly UserBusiness _userBusiness;
        private readonly IHttpContextAccessor _context;
        private readonly LoginValidator _loginValidator;
        private readonly CategoryBusiness _categoryBusiness;
        private readonly GroupBusiness _groupBusiness;
        private readonly StoreBusiness _storeBusiness;
        private readonly SlideBusiness _slideBusiness;
        private readonly OfferBusiness _offerBusiness;
        public ManagerController(UserBusiness userBusiness, IHttpContextAccessor context, LoginValidator loginValidator, CategoryBusiness categoryBusiness,
            GroupBusiness groupBusiness, StoreBusiness storeBusiness, SlideBusiness slideBusiness, OfferBusiness offerBusiness)
        {
            _userBusiness = userBusiness;
            _context = context;
            _loginValidator = loginValidator;
            _categoryBusiness = categoryBusiness;
            _groupBusiness = groupBusiness;
            _storeBusiness = storeBusiness;
            _slideBusiness = slideBusiness;
            _offerBusiness = offerBusiness;
        }
        private void SetFeedBack(int code, string message)
        {
            if (code != 200)
            {
                TempData["MessageError"] = message;
            }
            else
            {
                TempData["MessageSuccess"] = message;
            }
        }

        [Route("Manager")]
        public async Task<IActionResult> Index()
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null )
            {
                return RedirectToAction("Login", "Manager");
            }
            if (user.IsAdmin || user.IsDev)
            {
                return View("Admin");
            }
            else if (user.IsOwner)
            {
                return View("Owner");
            }
            //if (_loginValidator.IsLoggedIn() && (user.ID == uID || user.Username == username))
            //{ 
            //    return RedirectToAction("Index", "Dashboard");
            //}
            //var userVM = await _userBusiness.GetUserVM(username, uID, user);
            return View();
        }
        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            var user = await _loginValidator.GetUserAuth();
            if (user != null)
            {
                return RedirectToAction("Index", "Manager");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginVM loginVM)
        {
            try
            {
                var res = await _userBusiness.Login(loginVM.UsernameOrEmail, loginVM.Password);
                _loginValidator.SetSession("user", JsonConvert.SerializeObject(res.Data));
                SetFeedBack(res.StatusCode, res.Message);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                FileService.WriteToFile("\n\n" + e, "ErrorLogs");
                return RedirectToAction("LogOut");
            }
           
        }
        public async Task<IActionResult> Setup()
        {
            var res = await _userBusiness.Setup();

            SetFeedBack(res.StatusCode, res.Message);

            return RedirectToAction("index", "home");
        }

        //[Route("Register")]
        //[Route("Auth/Register")]
        //public async Task<IActionResult> Register(string referral)
        //{
        //    if (await _loginValidator.IsLoggedInAuth())
        //    {
        //        return RedirectToAction("Index", "Dashboard");
        //    }
        //    return View("Register", referral);
        //}
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (await _loginValidator.IsLoggedInAuth())
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var res = await _userBusiness.Create(user);
            if (res.StatusCode != 200)
            {
                TempData["MessageError"] = res.Message;
                return RedirectToAction("Register");
            }
            TempData["MessageSuccess"] = res.Message;
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Verification(string quil)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else if (user.IsEmailVer)
            {
                TempData["MessageSuccess"] = "Email is verified!";
                return RedirectToAction("Index", "Dashboard");
            }

            if (quil == "send")
            {
                var res = await _userBusiness.ResetEmailVerification(user.ID);

                if (res.StatusCode != 200)
                {
                    TempData["MessageError"] = res.Message;
                }
                else
                {
                    TempData["MessageSuccess"] = "Email verification code has been sent to your email";
                }
                return RedirectToAction("Verification");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailVerification(string vercode)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else if (user.IsEmailVer)
            {
                TempData["MessageSuccess"] = "Email is verified!";
                return RedirectToAction("Profile", "Dashboard");
            }

            var res = await _userBusiness.EmailVerification(user.ID, vercode);

            if (res.StatusCode != 200)
            {
                TempData["MessageError"] = res.Message; 
                return RedirectToAction("Verification");
            }

            else
            {
                TempData["MessageSuccess"] = res.Message;
            }

            return RedirectToAction("Profile", "Dashboard");
        }
        
        public async Task<IActionResult> PasswordReset(string quil, string email)
        {
            var userID = await _loginValidator.GetUserIDAuth();
            string val = "";

            if (userID == default &&  string.IsNullOrEmpty(quil))
            {
                val = "mail";
            }
            else if (quil == "send" || userID != default)
            {
                var res = await _userBusiness.ResetPassword(userID, email);

                if (res.StatusCode != 200)
                {
                    TempData["MessageError"] = res.Message;
                    val = "mail";
                }
                else
                {
                    TempData["thisUser"] = res.Data;
                    ViewBag.Message = "Password reset code has been sent to your email";
                }

                
            }
            
            return View("PasswordReset", val);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string vercode, string password)
        {
            //var user = await _loginValidator.GetUserAuth();
            if (TempData["thisUser"] == null)
            {
                return RedirectToAction("PasswordReset");
            }
            try
            {
                var userID = Guid.Parse(TempData["thisUser"].ToString());

                var res = await _userBusiness.ChangePassword(userID, vercode, password);

                if (res.StatusCode == 209)
                {
                    TempData["MessageError"] = res.Message;
                    return RedirectToAction("PasswordReset", new { quil = "check" });
                }
                else if (res.StatusCode != 200)
                {
                    TempData["MessageError"] = res.Message;
                    return RedirectToAction("PasswordReset");
                }
                else
                {
                    TempData["MessageSuccess"] = res.Message;
                }

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception)
            {
                return RedirectToAction("PasswordReset");
            }
           
        }
        public async Task<IActionResult> Category(string s, string t)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(s== "add")
            {
                return View("categoryholder/add", await _categoryBusiness.GetVM());
            }else if(s == "modify" && !string.IsNullOrEmpty(t))
                return View("categoryholder/edit", await _categoryBusiness.GetVM(t));

            return View("categoryholder/category", await _categoryBusiness.GetVM());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(MainVM mainVM, IFormFile image)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Index", "Manager");
            }
            var res = await _categoryBusiness.Create(mainVM, user, image);

            SetFeedBack(res.StatusCode, res.Message);
            return RedirectToAction("category", new {s = "add"});
        }
        [HttpPost]
        public async Task<IActionResult> ModifyCategory(MainVM mainVM, IFormFile image)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Index", "Manager");
            }
            var res = await _categoryBusiness.Modify(mainVM, user, image);
            if (res.StatusCode != 200)
            {
                TempData["MessageError"] = res.Message; 
                return RedirectToAction("category", new { s = "modify", t= mainVM.Category.ID});
            }
            else
            {
                TempData["MessageSuccess"] = res.Message;
            }
            return RedirectToAction("category");
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup(MainVM mainVM)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Index", "Manager");
            }
            var res = await _groupBusiness.Create(mainVM, user);


            SetFeedBack(res.StatusCode, res.Message);
            return RedirectToAction("group", new { s = "add" });
        }
        [HttpPost]
        public async Task<IActionResult> ModifyGroup(MainVM mainVM)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Index", "Manager");
            }
            var res = await _groupBusiness.Modify(mainVM, user);
            if (res.StatusCode != 200)
            {
                TempData["MessageError"] = res.Message;
                return RedirectToAction("group", new { s = "modify", t = mainVM.Group.ID });
            }
            else
            {
                TempData["MessageSuccess"] = res.Message;
            }

            return RedirectToAction("group");
        }
        public async Task<IActionResult> Group(string s, string t)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (s == "add")
            {
                return View("groupholder/add", await _groupBusiness.GetVM());
            }
            else if (s == "modify" && !string.IsNullOrEmpty(t))
                return View("groupholder/edit", await _groupBusiness.GetVM(t));

            return View("groupholder/group", await _groupBusiness.GetVM());
        }
        public async Task<IActionResult> Stock(string s, string t)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (s == "add")
            {
                return View("stockholder/add", await _storeBusiness.GetVM());
            }
            else if (s == "modify" && !string.IsNullOrEmpty(t))
                return View("stockholder/edit", await _storeBusiness.GetVM(t));

            return View("stockholder/stock", await _storeBusiness.GetVM());
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(string itemVM, IFormFile img, IFormFile img1, IFormFile img2, IFormFile img3, IFormFile img4)
        {
            try
            {
                var user = await _loginValidator.GetUserAuth();
                var responses = JsonConvert.DeserializeObject<ItemVM>(itemVM);
                if (user == null)
                {
                    return new BadRequestResult();
                }
                if (responses == null)
                {
                    return new BadRequestResult();
                }
                var res = await _storeBusiness.Create(responses, user, img, img1, img2, img3, img4);
                if (res.StatusCode != 200)
                {
                    //TempData["MessageError"] = res.Message;
                    return new BadRequestResult();
                }
                else
                {
                    //TempData["MessageSuccess"] = res.Message;
                }

                return Ok();
            }
            catch (Exception e)
            {
                FileService.WriteToFile("\n\n" + e, "ErrorLogs");
                return new BadRequestResult();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ModifyItem(string itemVM, IFormFile img, IFormFile img1, IFormFile img2, IFormFile img3, IFormFile img4)
        {
            try
            {
                var user = await _loginValidator.GetUserAuth();
                var responses = JsonConvert.DeserializeObject<ItemVM>(itemVM);
                if (user == null)
                {
                    return new BadRequestResult();
                }
                if (responses == null)
                {
                    return new BadRequestResult();
                }
                var res = await _storeBusiness.Modify(responses, user, img, img1, img2, img3, img4);
                if (res.StatusCode != 200)
                {
                    //TempData["MessageError"] = res.Message;
                    return new BadRequestResult();
                }
                else
                {
                    //TempData["MessageSuccess"] = res.Message;
                }

                return Ok();
            }
            catch (Exception e)
            {
                FileService.WriteToFile("\n\n" + e, "ErrorLogs");
                return new BadRequestResult();
            }
        }
        public async Task<IActionResult> Order(string a, string t)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (a == "add")
            {

            }

            return View("orderholder/order");
        }
        public async Task<IActionResult> LogOut()
        {
            try
            {
                var user = await _loginValidator.GetUserAuth();
                var val = await _userBusiness.LogOut(user.ID, user.StoreID);
            }
            catch (Exception)
            {
            }
            _loginValidator.LogOut();

            TempData["MessageSuccess"] = "Logged Out Successfully";
            return RedirectToAction("login", "manager");
        }

        public async Task<IActionResult> Faq()
        {
            return View();
        }
        public async Task<IActionResult> Analytics()
        {
            return View();
        }
        public async Task<IActionResult> Customers()
        {
            return View();
        }
        public async Task<IActionResult> Reviews()
        {
            return View();
        }
        public async Task<IActionResult> Settings()
        {
            return View();
        }
        public async Task<IActionResult> Transactions()
        {
            return View();
        }
        public async Task<IActionResult> Offers(string s, Guid id)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else if (s == "add")
            {
                return View("offerholder/add");
            }
            else if (s == "delete" && id != default)
            {
                var res = await _offerBusiness.Delete(id);
                if (res.StatusCode != 200)
                {
                    TempData["MessageError"] = res.Message;
                }
                else
                {
                    TempData["MessageSuccess"] = res.Message;
                }
                return RedirectToAction("offers");
            }
            else if (s == "home" && id != default)
            {
                var res = await _offerBusiness.ToggleHome(id);
                if (res.StatusCode != 200)
                {
                    TempData["MessageError"] = res.Message;
                }
                else
                {
                    TempData["MessageSuccess"] = res.Message;
                }
                return RedirectToAction("offers");
            }
            var offers = await _offerBusiness.Get();
            return View("offerholder/index", offers);
        }

        public async Task<IActionResult> NewOffer(Offer offer, IFormFile image)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var res = await _offerBusiness.Create(offer, image, user.ID);
            if (res.StatusCode != 200)
            {
                TempData["MessageError"] = res.Message;
                return RedirectToAction("offers", new { s = "add" });
            }
            else
            {
                TempData["MessageSuccess"] = res.Message;
                return RedirectToAction("offers");
            }
        }
        public async Task<IActionResult> Mails()
        {
            return View("mailholder/index");
        }
        public async Task<IActionResult> Blog()
        {
            return View("blogholder/index");
        }
        public async Task<IActionResult> AdIntegrations()
        {
            return View("AdsIntegrationHolder/index");
        }
        public async Task<IActionResult> Slides(string s, Guid id)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (s == "add" )
            {
                return View("slideHolder/add");
            }
            if (s == "delete" && id != default )
            {
               var res = await _slideBusiness.Delete(id);
                if (res.StatusCode != 200)
                {
                    TempData["MessageError"] = res.Message;
                }
                else
                {
                    TempData["MessageSuccess"] = res.Message;
                }
                return RedirectToAction("slides");
            }
            var slides = await _slideBusiness.Get();
            return View("slideHolder/index", slides);
        }
        public async Task<IActionResult> NewSlide(Slide slide, IFormFile image)
        {
            var user = await _loginValidator.GetUserAuth();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var res = await _slideBusiness.Create(slide, image, user.ID);
            if (res.StatusCode != 200)
            {
                TempData["MessageError"] = res.Message;
                return RedirectToAction("slides", new { s = "add" });
            }
            else
            {
                TempData["MessageSuccess"] = res.Message;
                return RedirectToAction("slides");
            }
        }
    }
}
