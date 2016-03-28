using System.ComponentModel.DataAnnotations;

namespace UgraTestPoll.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}