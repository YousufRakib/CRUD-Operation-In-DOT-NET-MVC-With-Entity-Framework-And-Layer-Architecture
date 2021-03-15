using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TFPTest.Models;
using TFPTest.Models.ViewModel;
using TFPTest.TFPTest.BLL;

namespace TFPTest.Controllers
{
    

    public class HomeController : Controller
    {
        TFPTestDatabaseEntities _tFPTestDatabaseEntities = new TFPTestDatabaseEntities();
        UserManager _usermanager = new UserManager();

        [Authorize]
        public ActionResult Index()
        {
            List<UserAccount> userAccounts = _usermanager.userDataList();
            ViewModel data = new ViewModel();
            data.UserAccountList = userAccounts;
            return View(data);
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            List<CountryList> Countries = new List<CountryList>();
            MessageClass messageClass = new MessageClass();

            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                CountryList country = new CountryList();
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    country.CountryName = R.EnglishName;
                    Countries.Add(country);
                    CountryList.Add(R.EnglishName);
                }
            }

            ViewModel viewModel = new ViewModel();
            viewModel.countryLists = Countries;

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(ViewModel viewModel)
        {
            if (viewModel.userAccount.UserId == 0)
            {
                var UploadPath = Server.MapPath("~/Images/");
                long uId = _usermanager.ExistingUser(viewModel, UploadPath);
                if (uId != 0)
                {
                    var userData = _tFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == uId).FirstOrDefault();
                    
                    List<CountryList> Countries = new List<CountryList>();
                    List<string> CountryList = new List<string>();
                    CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                    foreach (CultureInfo CInfo in CInfoList)
                    {
                        CountryList country = new CountryList();
                        RegionInfo R = new RegionInfo(CInfo.LCID);
                        if (!(CountryList.Contains(R.EnglishName)))
                        {
                            country.CountryName = R.EnglishName;
                            Countries.Add(country);
                            CountryList.Add(R.EnglishName);
                        }
                    }

                    viewModel.userAccount = userData;
                    viewModel.countryLists = Countries;
                    ViewBag.MailMessage= "An email sent to your email address with confirmation link. Please check your inbox!";
                    return View(viewModel);
}
                else
                {
                    var userData = _tFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == uId).FirstOrDefault();

                    List<CountryList> Countries = new List<CountryList>();
                    List<string> CountryList = new List<string>();
                    CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                    foreach (CultureInfo CInfo in CInfoList)
                    {
                        CountryList country = new CountryList();
                        RegionInfo R = new RegionInfo(CInfo.LCID);
                        if (!(CountryList.Contains(R.EnglishName)))
                        {
                            country.CountryName = R.EnglishName;
                            Countries.Add(country);
                            CountryList.Add(R.EnglishName);
                        }
                    }

                    viewModel.userAccount = userData;
                    viewModel.countryLists = Countries;
                    ViewBag.LoginError = "This Email exist in the system";
                    return View(viewModel);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View(new LoginData());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginData loginData)
        {
            if (ModelState.IsValid)
            {
                int userId;
                if (new UserManager().Login(loginData, out userId))
                {
                    FormsAuthentication.RedirectFromLoginPage(userId.ToString(), loginData.RememberMe);
                    return RedirectToAction("Index");
                    
                }
                else
                {
                    ViewBag.LoginError = "Username or password was incorrect Or You didn't confirm email yet.";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ConfirmAccount(long userId)
        {
            if (new UserManager().LoginAfterConfirm(userId))
            {
                LoginData loginData = new LoginData();
                FormsAuthentication.RedirectFromLoginPage(userId.ToString(), loginData.RememberMe);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("CreateAccount");
            }
        }
        [HttpPost]
        public JsonResult RegisterConfirm(long userId)
        {
            UserAccount Data = _tFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == userId).FirstOrDefault();
            Data.IsActive = true;
            _tFPTestDatabaseEntities.SaveChanges();
            var msg = "Your Email Is Verified!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Delete(long id)
        {
            
            if (new UserManager().DeleteAccount(Convert.ToInt32(id)))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.DeleteError = "This Email exist in the system";
            }
            return View();
        }

        [Authorize]
        public ActionResult EditAccount(int id)
        {
            var userData = _tFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == id).FirstOrDefault();

            List<CountryList> Countries = new List<CountryList>();
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                CountryList country = new CountryList();
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    country.CountryName = R.EnglishName;
                    Countries.Add(country);
                    CountryList.Add(R.EnglishName);
                }
            }
           
            ViewModel viewModel = new ViewModel();
            viewModel.userAccount = userData;
            viewModel.countryLists = Countries;

            return View(viewModel);
        }
        [Authorize]
        public ActionResult ViewAccount(int id)
        {
            var userData = _tFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == id).FirstOrDefault();

            List<CountryList> Countries = new List<CountryList>();
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                CountryList country = new CountryList();
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    country.CountryName = R.EnglishName;
                    Countries.Add(country);
                    CountryList.Add(R.EnglishName);
                }
            }

            ViewModel viewModel = new ViewModel();
            viewModel.userAccount = userData;
            viewModel.countryLists = Countries;

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            bool a = User.Identity.IsAuthenticated;

            return RedirectToAction("Login");
        }

    }
}