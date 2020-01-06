using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArtFactory.Common
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public override string ToString() => Name;

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object obj)
        {
            var other = obj as Enumeration;

            if (other == null)
                return false;

            var typeMatches = GetType().Equals(other.GetType());
            var valueMatches = Id.Equals(other.Id);

            return typeMatches && valueMatches;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(x => x.GetValue(null)).Cast<T>();
        }

        public static T FromId<T>(int id) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(x => x.Id == id);

            return matchingItem ?? throw new InvalidOperationException($"{id} is not a valid id in {typeof(T)}");
        }

        public static T FromName<T>(string name) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(x => x.Name == name);

            return matchingItem ?? throw new InvalidOperationException($"'{name}' is not a valid name in {typeof(T)}");
        }
    }
}
