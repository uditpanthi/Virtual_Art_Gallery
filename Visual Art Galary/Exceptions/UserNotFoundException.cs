﻿namespace Visual_Art_Galary.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public int UserIdNotFound { get; }

        public UserNotFoundException() : base("User not found.")
        {
        }

        public UserNotFoundException(int userId) : base($"User with ID {userId} not found.")
        {
            UserIdNotFound = userId;
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception ex) : base(message, ex)
        {
        }

        public UserNotFoundException(int userId, string message) : base(message)
        {
            UserIdNotFound = userId;
        }
    }
}
