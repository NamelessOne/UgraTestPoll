using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}