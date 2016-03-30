using System.Collections.Generic;

namespace UgraTestPoll.ViewModels
{
    public class TestViewModel 
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public List<AskedQuestionViewModel> AskedQuestions { get; set; }
    }
}