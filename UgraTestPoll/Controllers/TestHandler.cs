using System.Collections.Generic;
using UgraTestPoll.DataAccessLevel;
using UgraTestPoll.ViewModels;
using System.Linq;
using System;
using UgraTestPoll.Models;
using UgraTestPoll.Exceptions;

namespace UgraTestPoll.Controllers
{
    internal class TestHandler
    {
        private PollContext db = new PollContext();

        public List<TestsListElementViewModel> GetActiveTests()
        {
            var tests = new List<TestsListElementViewModel>();
            foreach (var test in db.Tests.Where(t => t.Active))
            {
                tests.Add(new TestsListElementViewModel { Name = test.Name, ID = test.ID });
            }
            return tests;
        }

        /// <summary>
        /// 
        /// </summary>
        internal TestViewModel GetTestViewModel(int id)
        {
            var test = db.Tests.Find(id);
            if (test == null || !test.Active)
            {
                throw new WrongDBDataException("Test with given id not found");
            }
            var questions = test.Questions.Where(q => q.Active);
            if (questions == null || questions.Count() == 0) //Если в тесте нет вопросов - что-то не так с заполнением базы, выдаём 404
            {
                throw new WrongDBDataException("Test with given id has no questions");
            }
            var askedQuestions = new List<AskedQuestionViewModel>();
            foreach (var question in questions)
            {
                if (question.Answers.Count == 0)
                    throw new WrongDBDataException("One of questions has no answers");
                var askedQuestion = new AskedQuestionViewModel();
                askedQuestion.Number = question.Number;
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
                askedQuestion.Answers = selectedAnswers;
                askedQuestions.Add(askedQuestion);
            }
            var simpleContainer = new TestViewModel();
            simpleContainer.TestID = id;
            simpleContainer.AskedQuestions = askedQuestions;
            simpleContainer.TestName = test.Name;
            return simpleContainer;
        }

        internal void SaveTestResults(TestViewModel testViewModel, string username)
        {
            int currentUserId;
            try
            {
                currentUserId = db.Users.First(x => x.Login.Equals(username)).ID;
            }
            catch (Exception)
            {
                throw new WrongDBDataException("User with given username not found");
            }
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
                        inputAnswer.AnswerID = int.Parse(question.SelectedAnswerID);
                        selectedAnswers.Add(inputAnswer);
                        break;
                    case AskedQuestionType.Radio:
                        var radioAnswer = new RadioSelectedAnswer();
                        radioAnswer.UserID = currentUserId;
                        radioAnswer.AnswerID = int.Parse(question.SelectedAnswerID);
                        selectedAnswers.Add(radioAnswer);
                        break;
                }
            }
            var oldResults = db.SelectedAnswers.Where(x => x.UserID == currentUserId && x.Answer.Question.TestId == testViewModel.TestID);
            db.SelectedAnswers.RemoveRange(oldResults);
            db.SelectedAnswers.AddRange(selectedAnswers);
            db.SaveChanges();
        }

        internal void Dispose()
        {
            db.Dispose();
        }
    }
}