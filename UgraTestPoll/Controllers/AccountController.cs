﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using UgraTestPoll.DataAccessLevel;
using UgraTestPoll.Models;
using UgraTestPoll.ViewModels;

namespace UgraTestPoll.Controllers
{
    public class AccountController : Controller
    {
        private PollContext db = new PollContext();

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

            if (db.Users.Any(usr => usr.Login == user.Login && usr.Password == user.Password))
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
            if (db.Users.Any(u => u.Login.Equals(user.Login)))
            {
                ViewBag.Error = "User with same username already exists.";
                return View("Register");
            }
            var dbUser = new User();
            dbUser.Password = user.Password;
            dbUser.Login = user.Login;
            db.Users.Add(dbUser);
            try
            {
                db.SaveChanges();
            }
            catch (Exception) //TODO глобальный exception - плохо
            {
                ViewBag.Error = "Credentials invalid. Please try again.";
                return View("Register");
            }

            FormsAuthentication.RedirectFromLoginPage(user.Login, true);

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
