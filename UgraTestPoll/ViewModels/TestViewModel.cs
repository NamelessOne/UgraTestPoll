using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.ViewModels
{
    public class TestViewModel : IValidatableObject
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public List<AskedQuestionViewModel> AskedQuestions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool validate = true;
            foreach(var question in AskedQuestions)
            {
                switch(question.Type)
                {
                    case AskedQuestionType.Checkbox:
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