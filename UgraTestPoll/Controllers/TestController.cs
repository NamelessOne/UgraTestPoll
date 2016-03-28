using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UgraTestPoll.DataAccessLevel;
using UgraTestPoll.Models;
using UgraTestPoll.ViewModels;

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
            var askedQuestions = new List<AskedQuestionViewModel>();
            foreach (var question in questions)
            {
                //TODO generate SelectedAnswers
                var askedQuestion = new AskedQuestionViewModel();
                askedQuestion.Number = question.Number;
                askedQuestion.Active = question.Active;
                askedQuestion.QuestionText = question.QuestionText;
                var selectedAnswers = new List<SelectedAnswerViewModel>();
                if (question is InputQuestion)
                {
                    askedQuestion.Type = AskedQuestionType.Input;
                    askedQuestion.SelectedAnswerID = question.Answers.First().ID.ToString();
                    var selectedAnswer = new SelectedAnswerViewModel();
                    selectedAnswer.AnswerID = question.Answers.First().ID;
                    selectedAnswer.Text = question.Answers.First().AnswerText;
                    selectedAnswers.Add(selectedAnswer);
                }
                else if (question is RadioQuestion)
                {
                    askedQuestion.Type = AskedQuestionType.Radio;
                    foreach (var answer in question.Answers)
                    {
                        var selectedAnswer = new SelectedAnswerViewModel();
                        selectedAnswer.Text = answer.AnswerText;
                        selectedAnswer.AnswerID = answer.ID;
                        selectedAnswers.Add(selectedAnswer);
                    }
                }
                else if (question is CheckboxQuestion)
                {
                    askedQuestion.Type = AskedQuestionType.Checkbox;
                    foreach (var answer in question.Answers)
                    {
                        var selectedAnswer = new SelectedAnswerViewModel();
                        selectedAnswer.Text = answer.AnswerText;
                        selectedAnswer.AnswerID = answer.ID;
                        selectedAnswers.Add(selectedAnswer);
                    }
                }
                //askedQuestion.Answers = question.Answers;
                askedQuestion.SelectedAnswers = selectedAnswers;
                askedQuestions.Add(askedQuestion);
            }
            var simpleContainer = new TestViewModel();
            simpleContainer.TestID = id.GetValueOrDefault();
            simpleContainer.AskedQuestions = askedQuestions;
            return View(simpleContainer);
        }

        [HttpPost]
        public ActionResult Try(TestViewModel testViewModel)
        {
            //TODO удаление старых данных
            if(!ModelState.IsValid)
            {
                    ViewBag.Error = "Form is not valid; please review and try again.";
                    return View("Try", testViewModel);
            }
            var currentUserId = db.Users.FirstOrDefault(x => x.Login.Equals(User.Identity.Name)).ID;
            var selectedAnswers = new List<SelectedAnswer>();            
            foreach (var question in testViewModel.AskedQuestions)
            {
                switch (question.Type)
                {
                    case AskedQuestionType.Checkbox:
                        foreach (var id in question.MultipleSelectedAnswerIDs)
                        {
                            var checkBoxAnswer = new CheckboxSelectedAnswer();
                            checkBoxAnswer.AnswerID = id;
                            checkBoxAnswer.UserID = currentUserId;
                            selectedAnswers.Add(checkBoxAnswer);
                        }
                        break;
                    case AskedQuestionType.Input:
                        var inputAnswer = new InputSelectedAnswer();
                        inputAnswer.UserID = currentUserId;
                        inputAnswer.Text = question.InputText;
                        inputAnswer.AnswerID = int.Parse(question.SelectedAnswerID); //TODO
                        selectedAnswers.Add(inputAnswer);
                        break;
                    case AskedQuestionType.Radio:
                        var radioAnswer = new RadioSelectedAnswer();
                        radioAnswer.UserID = currentUserId;
                        //if(question.SelectedAnswerID==null)
                        //{
                        //    ModelState.AddModelError("Radio button", "Check at least one of variants");
                        //    return RedirectToAction("Try", simpleContainer.TestID);
                        //}
                        radioAnswer.AnswerID = int.Parse(question.SelectedAnswerID); //TODO
                        selectedAnswers.Add(radioAnswer);
                        break;
                }
            }
            var oldResults = db.SelectedAnswers.Where(x => x.UserID == currentUserId && x.Answer.Question.TestId == testViewModel.TestID);
            db.SelectedAnswers.RemoveRange(oldResults);
            db.SelectedAnswers.AddRange(selectedAnswers);
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
