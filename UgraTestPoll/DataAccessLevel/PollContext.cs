using UgraTestPoll.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace UgraTestPoll.DataAccessLevel
{
    public class PollContext : DbContext
    {

        public PollContext() : base("TestPollConnection")
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<SelectedAnswer> SelectedAnswers { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}