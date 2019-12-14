using System;

namespace MemoCards.Exceptions
{
    public class UserNotFoundException: Exception
    {
        public UserNotFoundException(): base("User not found.")
        {
            
        }

        public UserNotFoundException(string email) : base($"User with email '{email}' not found.")
        {

        }

    }
}