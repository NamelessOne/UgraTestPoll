using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("User Login")]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}