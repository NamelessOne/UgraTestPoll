using System.Net;
using System.Web.Mvc;
using UgraTestPoll.Exceptions;
using UgraTestPoll.ViewModels;

namespace UgraTestPoll.Controllers
{
    public class ResultController : Controller
    {
        private ResultHandler handler = new ResultHandler();
        // GET: Result
        public ActionResult Index()
        {
            return RedirectToAction("UsersList");
        }

        public ActionResult UsersList()
        {
            var users = handler.GetUsersList();
            return View(users);
        }

        public ActionResult UserTests(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tests = handler.GetTestsForUser(id.Value);
            return View(tests);
        }

        public ActionResult TestResults(int? id, int? userID)
        {
            if (id == null || userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
