using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.ViewModels
{
    public class TestViewModel : IValidatableObject
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public List<AskedQuestionViewModel> AskedQuestions { get; set; }

        /// <summary>
        /// For successful validation each question must have:
        ///     - Input text and SelectedAnswerID (if input answer)
        ///     - SelectedAnswerID (if radio answer)
        ///     - MultipleSelectedAnswerIDs (if checkbox answer)
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool validate = true;
            //TODO AskedQuestion == null:/
            foreach (var question in AskedQuestions)
            {
                switch (question.Type)
                {
                    case AskedQuestionType.Checkbox:
                        if (question.MultipleSelectedAnswerIDs.Count < 1)
                            validate = false;
                        break;
                    case AskedQuestionType.Radio:
                        if (question.SelectedAnswerID == null)
                            validate = false;
                        break;
                    case AskedQuestionType.Input:
                        if (question.InputText == null)
                            validate = false;
                        break;
                }
            }
            if (!validate)
                yield return new ValidationResult("All questions must be answered.");
        }
    }
}