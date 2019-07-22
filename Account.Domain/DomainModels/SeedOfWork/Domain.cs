using System.Diagnostics.CodeAnalysis;

namespace Account.Domain.DomainModels.SeedOfWork
{
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    [SuppressMessage("ReSharper", "MergeConditionalExpression")]
    public abstract class Domain
    {
        private int? _requestedHashCode;
        public virtual string AccountNumber { get; protected set; }
        public bool IsTransient() =>
            AccountNumber == null;

        #region eq

        public override bool Equals(object obj)
        {
            // ReSharper disable once MergeSequentialChecks
            if (obj == default || !(obj is Domain))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Domain)obj;

            if (item.IsTransient() || IsTransient())
                return false;

            return item.AccountNumber == AccountNumber;
        }

        public override int GetHashCode()
        {
            if (IsTransient())
                // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
                return base.GetHashCode();

            if (!_requestedHashCode.HasValue)
                _requestedHashCode = AccountNumber.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }

        public static bool operator ==(Domain left, Domain right) =>
            Equals(left, null)
                ? Equals(right, null)
                : left.Equals(right);

        public static bool operator !=(Domain left, Domain right) =>
            !(left == right);

        #endregion
    }
}
