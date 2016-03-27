using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UgraTestPoll.DataAccessLevel;
using UgraTestPoll.Models;
using Microsoft.AspNet.Identity;

namespace UgraTestPoll.Controllers
{
    /// <summary>
    /// Tests list page controller
    /// </summary>
    [Authorize]
    public class TestController : Controller
    {
        private PollContext db = new PollContext();

        // GET: Test
        public ActionResult Index()
        {
            return View(db.Tests.ToList());
        }

        // GET: Test/Try/5
        public ActionResult Try(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            var questions = test.Questions;
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        [HttpPost]
        public ActionResult SaveAnswers(FormCollection form)
        {
            //TODO получаем значения из формы
            var currentUserId = db.Users.FirstOrDefault(x => x.Login.Equals(User.Identity.Name)).ID;
            foreach (var key in form.Keys)
            {
                System.Diagnostics.Debug.WriteLine("key = " + key + " value = " + form.GetValue(key.ToString()).AttemptedValue);
                if (key.ToString().StartsWith("radio"))
                {
                    var radioSelectedAnswer = new RadioSelectedAnswer();
                    radioSelectedAnswer.UserID = currentUserId;
                    radioSelectedAnswer.AnswerID = int.Parse(form.Get(key.ToString()));
                    db.SelectedAnswers.Add(radioSelectedAnswer);
                }
                else if (key.ToString().StartsWith("input"))
                {
                    var inputSelectedAnswer = new InputSelectedAnswer();
                    inputSelectedAnswer.Text = form.Get(key.ToString());
                    inputSelectedAnswer.AnswerID = int.Parse(key.ToString().Replace("input", ""));
                    inputSelectedAnswer.UserID = currentUserId;
                    db.SelectedAnswers.Add(inputSelectedAnswer);
                }
                else if (key.ToString().StartsWith("checkbox"))
                {
                    if(bool.Parse(form.Get(key.ToString()).Split(',')[0]))
                    {
                        var checkBoxSelectedAnswer = new CheckboxSelectedAnswer();
                        checkBoxSelectedAnswer.UserID = currentUserId;
                        checkBoxSelectedAnswer.AnswerID = int.Parse(key.ToString().Replace("checkbox", ""));
                        db.SelectedAnswers.Add(checkBoxSelectedAnswer);
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
