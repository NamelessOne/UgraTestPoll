using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.Models
{
    public abstract class SelectedAnswer
    {
        [Key]
        public int ID { get; set; }
        public int AnswerID { get; set; }
        public int UserID { get; set; }

        public virtual Answer Answer { get; set; } 
        public virtual User User { get; set; }
    }

    public class RadioSelectedAnswer : SelectedAnswer
    {
    }
    public class CheckboxSelectedAnswer : SelectedAnswer
    {
    }
    public class InputSelectedAnswer : SelectedAnswer
    {
        public string Text { get; set; }
    }
}