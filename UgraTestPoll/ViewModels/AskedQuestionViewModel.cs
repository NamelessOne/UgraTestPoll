using System.Collections.Generic;

namespace UgraTestPoll.ViewModels
{
    public enum AskedQuestionType
    {
        Radio, Checkbox, Input
    }
    /// <summary>
    /// Question Vm for passing test. 
    /// К сожалению, я не понял, как вернуть из View в ActionResult абстрактный класс:/
    /// For successful validation must have:
    ///     - Input text and SelectedAnswerID (if input answer)
    ///     - SelectedAnswerID (if radio answer)
    ///     - MultipleSelectedAnswerIDs (if checkbox answer)
    /// </summary>
    public class AskedQuestionViewModel
    {
        public int Number { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }
        public AskedQuestionType Type { get; set; }

        public List<SelectedAnswerViewModel> Answers { get; set; }
        public string InputText { get; set; } //Text which contains answer, if question type = input
        private string _selectedAnswerId;
        public string SelectedAnswerID //Id of answer which user select, if question type = input or radio
        {
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
        public List<int> MultipleSelectedAnswerIDs //Id of answers which user select, if question type = checkbox (multiple answers)
        {
            get
            {
                List<int> result = new List<int>();
                if (Answers == null)
                    return result;
                foreach (var answer in Answers)
                {
                    if (answer.Checked)
                        result.Add(answer.AnswerID);
                }
                return result;
            }
        }
    }
}