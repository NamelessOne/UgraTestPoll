using System.Collections.Generic;

namespace UgraTestPoll.ViewModels
{
    /// <summary>
    /// VM for display results with user selected and currect answers
    /// </summary>
    public class PassedQuestionViewModel
    {
        public int Number { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }

        public List<string> SelectedAnswers { get; set; }
        public List<string> CorrectAnswers { get; set; }
    }
}