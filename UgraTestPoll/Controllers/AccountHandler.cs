using System;
using System.Linq;
using UgraTestPoll.DataAccessLevel;
using UgraTestPoll.Exceptions;
using UgraTestPoll.Models;
using UgraTestPoll.ViewModels;

namespace UgraTestPoll.Controllers
{
    internal class AccountHandler
    {
        private PollContext db = new PollContext();

        internal bool Login(UserViewModel user)
        {
            return db.Users.Any(usr => usr.Login == user.Login && usr.Password == user.Password);
        }

        internal void Register(UserViewModel user)
        {
            if (db.Users.Any(u => u.Login.Equals(user.Login)))
            {
                throw new UserRegistrationException("User with same username already exists.");
            }
            var dbUser = new User();
            dbUser.Password = user.Password;
            dbUser.Login = user.Login;
            db.Users.Add(dbUser);
            try
            {
                db.SaveChanges();
            }
            catch (Exception) //TODO глобальный exception - плохо
            {
                throw new WrongDBDataException("Credentials invalid. Please try again.");
            }
        }
    }
}