using System;
using System.Web.Mvc;
using System.Web.Security;
using UgraTestPoll.ViewModels;

namespace UgraTestPoll.Controllers
{
    /// <summary>
    /// Account controller. Handle Login and registration requsests.
    /// </summary>
    public class AccountController : Controller
    {
        private AccountHandler handler = new AccountHandler();

        /// <summary>
        /// Index page. Redirect to Login page automatically.
        /// </summary>
        /// <returns>Redirect to Login page.</returns>
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

        /// <summary>
        /// Login user. Redirects back if login success or show error if login isnt success.
        /// </summary>
        /// <param name="user">User which trying to login.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Register new user. Redirects back if register success or show error if register isnt success. Automatically login user after successful registration.
        /// </summary>
        /// <param name="user">UserViewModel with login and password</param>
        /// <returns></returns>
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

        /// <summary>
        /// Logout current user. Redirects to main page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
