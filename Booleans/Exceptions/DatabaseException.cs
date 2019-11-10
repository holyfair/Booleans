using System;

namespace Booleans.Exceptions
{
    internal class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}
