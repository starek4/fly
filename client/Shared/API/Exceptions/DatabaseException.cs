using System;
using System.Runtime.Serialization;

namespace Shared.API.Exceptions
{
    public class DatabaseException : Exception
    {
        private readonly StreamingContext context;
        public DatabaseException() : base() { }
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, System.Exception inner) : base(message, inner) { }
        protected DatabaseException(SerializationInfo info, StreamingContext context)
        {
            this.context = context;
        }
    }
}
