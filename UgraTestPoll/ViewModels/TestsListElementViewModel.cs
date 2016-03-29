using System.ComponentModel;

namespace UgraTestPoll.ViewModels
{
    public class TestsListElementViewModel
    {
        public int UserID { get; set; }
        public int ID { get; set; }
        [DisplayName("Test name")]
        public string Name { get; set; }
    }
}