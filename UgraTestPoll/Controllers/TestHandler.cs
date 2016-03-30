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

        /// <returns>Active tests</returns>
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
        /// Gets testVM with questions and answers.
        /// </summary>
        /// <exception cref="WrongDBDataException">Wrong db relations structure for given test id</exception>
        /// <returns>testVM with questions and answers.</returns>
        internal TestViewModel GetTestViewModel(int id)
        {
            var test = db.Tests.Find(id);
            if (test == null || !test.Active)
            {
                throw new WrongDBDataException("Test with given id not found");
            }
            var questions = test.Questions.Where(q => q.Active);
            if (questions == null || questions.Count() == 0) //If text not contains quistions - something wrong with db, return 404
            {
                throw new WrongDBDataException("Test with given id has no questions");
            }
            var askedQuestions = new List<AskedQuestionViewModel>();
            foreach (var question in questions) //for each question get all possible answers
            {
                if (question.Answers.Count < 1)
                    throw new WrongDBDataException("One of questions has no answers");
                var askedQuestion = new AskedQuestionViewModel();
                askedQuestion.Number = question.Number;
                askedQuestion.QuestionText = question.QuestionText;
                var selectedAnswers = new List<SelectedAnswerViewModel>();
                if (question is InputQuestion)
                {
                    askedQuestion.Type = AskedQuestionType.Input;
                    var selectedAnswer = new SelectedAnswerViewModel();
                    selectedAnswer.AnswerID = question.Answers.First().ID;
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

        /// <summary>
        /// Save test results to database.
        /// </summary>
        /// <param name="username">User which passed the test</param>
        /// <param name="testViewModel">TestVM from test form which must be saved</param>
        /// <exception cref="UserNotFoundException">"User with given username not found</exception>
        internal void SaveTestResults(TestViewModel testViewModel, string username)
        {
            int currentUserId;
            try
            {
                currentUserId = db.Users.First(x => x.Login.Equals(username)).ID;
            }
            catch (Exception)
            {
                throw new UserNotFoundException("User with given username not found");
            }
            var selectedAnswers = new List<SelectedAnswer>();
            foreach (var question in testViewModel.AskedQuestions)
            {
                switch (question.Type)
                {
                    case AskedQuestionType.Checkbox:
                        foreach (var id in question.Answers.Where(x => x.Checked).Select(x => x.AnswerID))
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
                        inputAnswer.Text = question.Answers.First().Text;
                        inputAnswer.AnswerID = question.Answers.First().AnswerID;
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
            //Remove old results of user for this test from database
            var oldResults = db.SelectedAnswers.Where(x => x.UserID == currentUserId && x.Answer.Question.TestId == testViewModel.TestID);
            db.SelectedAnswers.RemoveRange(oldResults);
            //Add ne results to db
            db.SelectedAnswers.AddRange(selectedAnswers);
            db.SaveChanges();
        }
    }
}