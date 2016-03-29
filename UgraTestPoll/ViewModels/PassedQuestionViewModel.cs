using System.Collections.Generic;

namespace UgraTestPoll.ViewModels
{
    public class PassedQuestionViewModel
    {
        public int Number { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }

        public List<string> SelectedAnswers { get; set; }
    }
}