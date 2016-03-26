using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.Models
{
    public abstract class Answer
    {
        [Key]
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string AnswerText { get; set; }
        public bool Active { get; set; }
        public bool Correct { get; set; }

        public virtual Question Question { get; set; }
    }

    public class RadionAnswer : Answer
    {

    }

    public class CheckBoxAnswer : Answer
    {

    }

    public class InputAnswer : Answer
    {
        //TODO Correct always true, there is no other variants for input
        public new bool Correct
        {
            get
            {
                return true;
            }
            set
            {
            }
        }
    }
}