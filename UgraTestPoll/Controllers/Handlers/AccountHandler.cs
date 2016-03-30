using System;
using System.Linq;
using UgraTestPoll.DataAccessLevel;
using UgraTestPoll.Exceptions;
using UgraTestPoll.Models;
using UgraTestPoll.ViewModels;

namespace UgraTestPoll.Controllers.Handlers
{
    /// <summary>
    /// Handle account controller requests.
    /// </summary>
    internal class AccountHandler
    {
        private PollContext db = new PollContext(); //database context

        /// <returns>true if user exists in db, otherwice false</returns>
        internal bool Login(UserViewModel user)
        {
            return db.Users.Any(usr => usr.Login == user.Login && usr.Password == user.Password);
        }
        /// <summary>
        /// Add new user to database.
        /// </summary>
        /// <param name="user">User which nedds to be registered</param>
        /// <exception cref="UserRegistrationException">User with the same username already exists</exception>
        /// <exception cref="WrongDBDataException">Cant add user to database (possible bad login/password strings)</exception>
        /// <returns></returns>
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