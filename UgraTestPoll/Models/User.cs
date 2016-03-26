using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual ICollection<SelectedAnswer> SelectedAnswers { get; set; }
    }
}