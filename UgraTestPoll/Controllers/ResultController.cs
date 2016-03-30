using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using UgraTestPoll.Exceptions;
using UgraTestPoll.ViewModels;
using UgraTestPoll.Controllers.Handlers;

namespace UgraTestPoll.Controllers
{
    /// <summary>
    /// Controller for Test result pages (users list, tests list and testanswers pages).
    /// </summary>
    public class ResultController : Controller
    {
        private ResultHandler handler = new ResultHandler();

        /// <summary>
        /// Index page. Automatically redirects to UsersList.
        /// </summary>
        public ActionResult Index()
        {
            return RedirectToAction("UsersList");
        }

        /// <summary>
        /// Users list page.
        /// </summary>
        public ActionResult UsersList()
        {
            var users = handler.GetUsersList();
            return View(users);
        }

        /// <summary>
        /// Test passed by user with given id
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns></returns>
        public ActionResult UserTests(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!handler.IsUserExists(id.Value))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            List<TestsListElementViewModel> tests;
            tests = handler.GetTestsForUser(id.Value);
            return View(tests);
        }
        /// <summary>
        /// Results of user with given id for test with given id.
        /// </summary>
        /// <param name="id">test id</param>
        /// <param name="userID">user id</param>
        /// <returns></returns>
        public ActionResult TestResults(int? id, int? userID)
        {
            if (id == null || userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!handler.IsUserExists(userID.Value))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            PassedTestViewModel passedTestViewModel = null;
            try
            {
                passedTestViewModel = handler.GetResults(id.Value, userID.Value);
            }
            catch (WrongDBDataException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(passedTestViewModel);
        }
    }
}
