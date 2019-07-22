using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Exceptions
{
    public sealed class AccountAlreadyExistException : DomainException
    {
        public AccountAlreadyExistException()
        {
        }

        public AccountAlreadyExistException(string message)
            : base(message)
        {
        }

        public AccountAlreadyExistException(
            string message,
            Exception innerException
        ) : base(message, innerException)
        {
        }
    }
}
