using System.Net;
using System.Web.Mvc;
using UgraTestPoll.Exceptions;
using UgraTestPoll.ViewModels;

namespace UgraTestPoll.Controllers
{
    /// <summary>
    /// Tests list page controller
    /// </summary>
    [Authorize]
    public class TestController : Controller
    {
        private TestHandler handler = new TestHandler();
        // GET: Test
        public ActionResult Index()
        {
            var tests = handler.GetActiveTests();
            return View(tests);
        }

        // GET: Test/Pass/5
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
            catch (WrongDBDataException) //Юзернейм есть, юзера в базе нет. Разлогиниваем его. 
            {
                RedirectToAction("Logout", "Account");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                handler.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
