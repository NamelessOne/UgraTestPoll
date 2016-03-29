using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using UgraTestPoll.DataAccessLevel;
using UgraTestPoll.ViewModels;
using System.Linq;
using UgraTestPoll.Models;

namespace UgraTestPoll.Controllers
{
    public class ResultController : Controller
    {
        private PollContext db = new PollContext();

        // GET: Result
        public ActionResult Index()
        {
            return RedirectToAction("UsersList");
        }

        public ActionResult UsersList()
        {
            var users = new List<UserViewModel>();
            foreach (var user in db.Users)
            {
                users.Add(new UserViewModel() { Login = user.Login, ID = user.ID });
            }
            return View(users);
        }

        public ActionResult UserTests(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tests = new List<TestsListElementViewModel>();
            var dbTests = db.Users.Find(id).SelectedAnswers.Select(x => x.Answer).Select(x => x.Question).Distinct().Select(x => x.Test).Distinct();
            foreach (var test in dbTests) //TODO сложный запрос. По userid получиться все пройденные им тесты
            {
                tests.Add(new TestsListElementViewModel() { Name = test.Name, ID = test.ID, UserID = id.Value });
            }
            return View(tests);
        }

        public ActionResult TestResults(int? id, int? userID)
        {
            if (id == null|| userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var passedTestViewModel = new PassedTestViewModel();
            var dbTest = db.Tests.Find(id);
            passedTestViewModel.TestName = db.Tests.Find(id).Name;
            passedTestViewModel.Questions = new List<PassedQuestionViewModel>();
            foreach (var question in dbTest.Questions)
            {
                var passedQuestionVM = new PassedQuestionViewModel();
                passedQuestionVM.Number = question.Number;
                passedQuestionVM.QuestionText = question.QuestionText;
                passedQuestionVM.SelectedAnswers = new List<string>();
                passedQuestionVM.CorrectAnswers = new List<string>();
                var correctAnswers = question.Answers.Where(x => x.Correct).Select(x => x.AnswerText);
                passedQuestionVM.CorrectAnswers.AddRange(correctAnswers);
                var passedAnswers = question.Answers.Select(x => x.SelectedAnswers).SelectMany(x => x).Where(x => x.UserID == userID.Value);
                foreach(var passedAnswer in passedAnswers)
                {
                    if(passedAnswer is InputSelectedAnswer)
                    {
                        passedQuestionVM.SelectedAnswers.Add((passedAnswer as InputSelectedAnswer).Text);
                    }
                    else
                    {
                        passedQuestionVM.SelectedAnswers.Add(passedAnswer.Answer.AnswerText);
                    }
                }
                passedTestViewModel.Questions.Add(passedQuestionVM);
            }
            return View(passedTestViewModel);
        }
    }
}
