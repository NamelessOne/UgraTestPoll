using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UgraTestPoll.ViewModels
{
    public enum AskedQuestionType
    {
        Radio, Checkbox, Input
    }
    /// <summary>
    /// Question Vm for passing test. 
    /// К сожалению, я не понял, как вернуть из View в ActionResult абстрактный класс:/ А так его надо бы разбить на 1 абстрактный, и три наследника.
    /// </summary>
    public class AskedQuestionViewModel : IValidatableObject
    {
        public int Number { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }
        public AskedQuestionType Type { get; set; }

        public List<SelectedAnswerViewModel> Answers { get; set; }
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

        /// <summary>
        /// For successful validation each question must have:
        ///     - Input text (if input answer)
        ///     - SelectedAnswerID (if radio answer)
        ///     - At least one answer checked (if checkbox answer)
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool validate = true;
            //TODO AskedQuestion == null:/
            switch (Type)
            {
                case AskedQuestionType.Checkbox:
                    if (Answers == null || Answers.Where(x => x.Checked).Count() < 1)
                        validate = false;
                    break;
                case AskedQuestionType.Radio:
                    if (SelectedAnswerID == null)
                        validate = false;
                    break;
                case AskedQuestionType.Input:
                    if (Answers==null || Answers.Count == 0 || Answers.First().Text == null)
                        validate = false;
                    break;
            }
            if (!validate)
                yield return new ValidationResult("All questions must be answered.");
        }
    }
}