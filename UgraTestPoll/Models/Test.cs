using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.Models
{
    public class Test
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Question> Questions { get; set; } 
    }
}