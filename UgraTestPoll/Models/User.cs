using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Login { get; set; }

        public virtual ICollection<SelectedAnswer> SelectedAnswers { get; set; }
    }
}