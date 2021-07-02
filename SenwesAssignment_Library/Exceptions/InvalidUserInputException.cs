using System;

namespace SenwesAssignment_Library.Exceptions
{
    public class InvalidUserInputException : Exception
    {
        public InvalidUserInputException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
