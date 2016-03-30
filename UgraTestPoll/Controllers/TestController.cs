using System.Net;
using System.Web.Mvc;
using UgraTestPoll.Exceptions;
using UgraTestPoll.ViewModels;
using UgraTestPoll.Controllers.Handlers;

namespace UgraTestPoll.Controllers
{
    /// <summary>
    /// Tests list page controller
    /// </summary>
    [Authorize]
    public class TestController : Controller
    {
        private TestHandler handler = new TestHandler();
        /// <summary>
        /// Show all possible tests
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var tests = handler.GetActiveTests();
            return View(tests);
        }

        /// <summary>
        /// Show test by id
        /// </summary>
        /// <param name="id">test id</param>
        /// <returns>404 if test not found, or test havent questions, or one of questions have no answers</returns>
        public ActionResult Pass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestViewModel testModel = null;
            try
            {
                testModel = handler.GetTestViewModel(id.Value);
            }
            catch (WrongDBDataException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(testModel);
        }
        /// <summary>
        /// Validate and save test results if validation complete. Redirect to tests list if successful.
        /// </summary>
        /// <param name="testViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Pass(TestViewModel testViewModel)
        {
            //TODO удаление старых данных
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Form is not valid; please review and try again.";
                return View("Pass", testViewModel);
            }
            try
            {
                handler.SaveTestResults(testViewModel, User.Identity.Name);
            }
            catch (UserNotFoundException) //User with given username not exists in db. Logout.
            {
                return RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("Index");
        }
    }
}
