using System.Collections.Generic;

namespace UgraTestPoll.ViewModels
{
    public enum AskedQuestionType
    {
        Radio, Checkbox, Input
    }
    public class AskedQuestionViewModel
    {
        public int Number { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }
        public AskedQuestionType Type {get; set;}

        public List<SelectedAnswerViewModel> SelectedAnswers { get; set; }
        public string InputText { get; set; }
        private string _selectedAnswerId;
        public string SelectedAnswerID {
            get
            {
                return _selectedAnswerId;
            }
            set
            {
                if (value != null)
                    _selectedAnswerId = value;
            }
        }
        public List<int> MultipleSelectedAnswerIDs
        {
            get
            {
                List<int> result = new List<int>();
                foreach(var answer in SelectedAnswers)
                {
                    if (answer.Checked)
                        result.Add(answer.AnswerID);
                }
                return result;
            }
        }
    }
}