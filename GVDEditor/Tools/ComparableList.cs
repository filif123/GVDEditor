using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GVDEditor.Tools
{
    internal class ComparableList<T> : ReadOnlyCollection<T>
    {
        public ComparableList(IEnumerable<T> list) : base(list.ToArray())
        {
        }

        public override bool Equals(object other)
        {
            return other is IEnumerable<T> otherEnumerable && otherEnumerable.SequenceEqual(this);
        }

        public override int GetHashCode()
        {
            var hash = 43;
            unchecked
            {
                foreach (var item in this) hash = 19 * hash + item.GetHashCode();
            }

            return hash;
        }
    }
}