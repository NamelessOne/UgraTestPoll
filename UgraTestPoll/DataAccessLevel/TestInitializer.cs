using System.Collections.Generic;
using UgraTestPoll.Models;

namespace UgraTestPoll.DataAccessLevel
{
    /// <summary>
    /// Crate the sample data for database
    /// </summary>
    public class TestInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PollContext>
    {
        protected override void Seed(PollContext context)
        {
            var tests = new List<Test>
            {
            new Test{Name="Test 1", Active=true},
            new Test{Name="Test 2", Active=true},
            };
            tests.ForEach(s => context.Tests.Add(s));
            context.SaveChanges();

            var users = new List<User>
            {
            new User{Login="User 1", Password="Password 1"},
            new User{Login="User 2", Password="Password 2"},
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var questions = new List<Question>
            {
            new RadioQuestion{QuestionText="Radio question", Number=1, Active=true, TestId=tests[0].ID},
            new CheckboxQuestion{QuestionText="CheckBox Question", Number=2, Active=true, TestId=tests[0].ID},
            new InputQuestion{QuestionText="Input Question", Number=3, Active=true, TestId=tests[0].ID},
            };
            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();

            var answers = new List<Answer>
            {
            new RadionAnswer{AnswerText="Radio Answer 1", Active=true, Correct=true, QuestionID=questions[0].ID},
            new RadionAnswer{AnswerText="Radio Answer 2", Active=true, Correct=false, QuestionID=questions[0].ID},
            new CheckBoxAnswer{AnswerText="Checkbox Answer 1", Active=true, Correct=true, QuestionID=questions[1].ID},
            new CheckBoxAnswer{AnswerText="Checkbox Answer 2", Active=true, Correct=true, QuestionID=questions[1].ID},
            new InputAnswer{AnswerText="Input Answer 1", Active=true, Correct=true, QuestionID=questions[2].ID},
            };           
            answers.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();
        }
    }
}