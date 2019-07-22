using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLibrary.SeedOfWork
{
    public abstract class Enumeration : IComparable
    {
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }

        public int CompareTo(object other) =>
            Id.CompareTo(((Enumeration)other).Id);

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly
            );

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T FromValue<T>(int value) 
            where T : Enumeration =>
            GetAll<T>().FirstOrDefault(item => item.Id == value);

        public static bool IsDefined<T>(int value)
            where T : Enumeration =>
            FromValue<T>(value) != null;

        public static T FromDisplayName<T>(string displayName) 
            where T : Enumeration =>
            GetAll<T>().FirstOrDefault(item => 
                string.Equals(
                    item.Name, 
                    displayName, 
                    StringComparison.CurrentCultureIgnoreCase));

        public static bool IsDefined<T>(string displayName) 
            where T : Enumeration => FromDisplayName<T>(displayName) != null;

        public static int AbsoluteDifference(
            Enumeration firstValue, 
            Enumeration secondValue) =>
            Math.Abs(firstValue.Id - secondValue.Id);

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        protected bool Equals(Enumeration other) => 
            string.Equals(Name, other.Name) && Id == other.Id;

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null
                            ? Name.GetHashCode() 
                            : 0) * 397) ^ Id;
            }
        }

        public override string ToString() => Name;
    }
}
