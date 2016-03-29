using System;
using System.Web.Mvc;
using System.Web.Security;
using UgraTestPoll.ViewModels;

namespace UgraTestPoll.Controllers
{
    public class AccountController : Controller
    {
        private AccountHandler handler = new AccountHandler();

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Form is not valid; please review and try again.";
                return View("Login");
            }

            if (handler.Login(user))
                FormsAuthentication.RedirectFromLoginPage(user.Login, true);

            ViewBag.Error = "Credentials invalid. Please try again.";
            return View("Login");
        }

        [HttpPost]
        public ActionResult Register(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Form is not valid; please review and try again.";
                return View("Register");
            }
            try
            {
                handler.Register(user);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View("Register");
            }
            FormsAuthentication.RedirectFromLoginPage(user.Login, true);
            //Предыдущая строчка равносильная return, вроде:/
            ViewBag.Error = "Credentials invalid. Please try again.";
            return View("Register");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
