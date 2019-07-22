using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Exceptions
{
    public sealed class AccountNotExistException : DomainException
    {
        public AccountNotExistException()
        {
        }

        public AccountNotExistException(string message)
            : base(message)
        {
        }

        public AccountNotExistException(
            string message,
            Exception innerException
        ) : base(message, innerException)
        {
        }
    }
}
