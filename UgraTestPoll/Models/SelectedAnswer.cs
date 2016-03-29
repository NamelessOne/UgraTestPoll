using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.Models
{
    /// <summary>
    /// Answer selected by user. 
    /// </summary>
    public abstract class SelectedAnswer
    {
        [Key]
        public int ID { get; set; }
        public int AnswerID { get; set; } //ID of answer which user selected.
                                          //If it is InputSelectedAnswer (answer without options), answer text must be obtined from Text fiel of SelectedAnswer, not SelectedAnswer.Answer
        public int UserID { get; set; }

        public virtual Answer Answer { get; set; } 
        public virtual User User { get; set; }
    }

    public class RadioSelectedAnswer : SelectedAnswer
    {
        //public bool Checked { get; set; }
    }
    public class CheckboxSelectedAnswer : SelectedAnswer
    {
        //public bool Checked { get; set; }
    }
    public class InputSelectedAnswer : SelectedAnswer
    {
        public string Text { get; set; }
    }
}