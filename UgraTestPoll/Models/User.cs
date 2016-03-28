using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgraTestPoll.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        public int ID { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual ICollection<SelectedAnswer> SelectedAnswers { get; set; }
    }
}