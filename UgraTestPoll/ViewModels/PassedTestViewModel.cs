using System.Collections.Generic;

namespace UgraTestPoll.ViewModels
{
    public class PassedTestViewModel
    {
        public string TestName { get; set; }
        public List<PassedQuestionViewModel> Questions { get; set; }
    }
}