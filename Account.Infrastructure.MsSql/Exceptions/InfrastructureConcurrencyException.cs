using System;

namespace Account.Infrastructure.MsSql.Exceptions
{
    public sealed class InfrastructureConcurrencyException : InfrastructureException
    {
        public InfrastructureConcurrencyException()
        {
        }

        public InfrastructureConcurrencyException(string message)
            : base(message)
        {
        }

        public InfrastructureConcurrencyException(
            string message,
            Exception innerException
        ) : base(message, innerException)
        {
        }
    }
}
