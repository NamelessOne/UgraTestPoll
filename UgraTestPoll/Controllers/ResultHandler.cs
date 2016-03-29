using System.Collections.Generic;
using System.Linq;
using UgraTestPoll.DataAccessLevel;
using UgraTestPoll.Exceptions;
using UgraTestPoll.Models;
using UgraTestPoll.ViewModels;

namespace UgraTestPoll.Controllers
{
    public class ResultHandler
    {
        private PollContext db = new PollContext();

        public List<UserViewModel> GetUsersList()
        {
            var users = new List<UserViewModel>();
            foreach (var user in db.Users)
            {
                users.Add(new UserViewModel() { Login = user.Login, ID = user.ID });
            }
            return users;
        }

        public List<TestsListElementViewModel> GetTestsForUser(int id)
        {
            var tests = new List<TestsListElementViewModel>();
            var dbTests = db.Users.Find(id).SelectedAnswers.Select(x => x.Answer).Select(x => x.Question).Distinct().Select(x => x.Test).Distinct();
            foreach (var test in dbTests) //TODO сложный запрос. По userid получиться все пройденные им тесты
            {
                tests.Add(new TestsListElementViewModel() { Name = test.Name, ID = test.ID, UserID = id });
            }
            return tests;
        }

        public PassedTestViewModel GetResults(int testId, int userId)
        {
            if (db.Users.Find(userId) == null)
            {
                throw new WrongDBDataException("User with given id not found in database");
            }
            var passedTestViewModel = new PassedTestViewModel();
            var dbTest = db.Tests.Find(testId);
            passedTestViewModel.TestName = db.Tests.Find(testId).Name;
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
                var passedAnswers = question.Answers.Select(x => x.SelectedAnswers).SelectMany(x => x).Where(x => x.UserID == userId);
                foreach (var passedAnswer in passedAnswers)
                {
                    if (passedAnswer is InputSelectedAnswer)
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
            return passedTestViewModel;
        }
    }
}