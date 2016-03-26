using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.Models
{
    //public enum QuestionType
    //{
    //    Radio, Checkbox, Input
    //}
    public abstract class Question
    {
        [Key]
        public int ID { get; set; }
        public int TestId { get; set; }
        public string QuestionText { get; set; }
        public int Number { get; set; }
        //public QuestionType QuestionType { get; set; }
        public bool Active { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }

    public class RadioQuestion : Question
    {

    }

    public class CheckboxQuestion : Question
    {

    }

    public class InputQuestion : Question
    {

    }
}