using System;

namespace FlyClientApi.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }
    }
}
