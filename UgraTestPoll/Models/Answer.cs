using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.Models
{
    /// <summary>
    /// Answer for test question. Each question must have at least one answer.
    /// </summary>
    public abstract class Answer
    {
        [Key]
        public int ID { get; set; }
        public int QuestionID { get; set; }
        [Required]
        public string AnswerText { get; set; }
        public bool Active { get; set; }
        public bool Correct { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<SelectedAnswer> SelectedAnswers { get; set; } //User selected answers, which refers to this answer
    }

    public class RadionAnswer : Answer
    {

    }

    public class CheckBoxAnswer : Answer
    {

    }

    public class InputAnswer : Answer
    {

    }
}