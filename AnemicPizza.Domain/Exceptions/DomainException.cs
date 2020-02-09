using System;

namespace AnemicPizza.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
        public DomainException(Exception innerException) : base(innerException.Message, innerException) { }
    }
}
