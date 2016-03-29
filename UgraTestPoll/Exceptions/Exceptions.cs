using System;

namespace UgraTestPoll.Exceptions
{
    class WrongDBDataException : Exception
    {
        public WrongDBDataException(string message) : base(message)
        {
        }
    }
    class UserRegistrationException : Exception
    {
        public UserRegistrationException(string message) : base(message)
        {
        }
    }
}