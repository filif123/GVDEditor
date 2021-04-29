using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Dopravca vlaku (operátor)
    /// </summary>
    public sealed class Operator
    {
        /// <summary>
        ///     Predvolený dopravca
        /// </summary>
        public static readonly Operator None = new(-1, "Žiadny");

        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="id">identifikátor dopravcu</param>
        /// <param name="name">názov dopravcu</param>
        public Operator(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        ///     Identifikátor dopravcu
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Názov dopravcu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     This
        /// </summary>
        public Operator This => this;

        /// <summary>
        ///     Vráti dopravcu zo zadaného listu podľa identifikátora dopravcu
        /// </summary>
        /// <param name="operators">list dopravcov</param>
        /// <param name="id">identifikátor dopravcu</param>
        /// <returns>dopravcu, ak ho nenájde, vráti <see langword="null" /></returns>
        public static Operator GetOperatorFromId(IEnumerable<Operator> operators, int id)
        {
            foreach (Operator dopravca in operators)
                if (dopravca.Id == id)
                    return dopravca;

            return null;
        }

        /// <summary>
        ///     Vráti dopravcu zo zadaného listu podľa názvu dopravcu
        /// </summary>
        /// <param name="operators">list dopravcov</param>
        /// <param name="name">názov dopravcu</param>
        /// <returns>dopravcu, ak ho nenájde, vráti <see langword="null" /></returns>
        public static Operator GetOperatorFromName(IEnumerable<Operator> operators, string name)
        {
            foreach (Operator dopravca in operators)
                if (dopravca.Name == name)
                    return dopravca;

            return null;
        }

        /// <summary>
        ///     Porovna dopravcov
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Operator dopravca && dopravca.Id == Id && dopravca.Name == Name;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Zistí, či sa prvky oboch listov dopravcov rovnajú
        /// </summary>
        /// <param name="o1">list 1</param>
        /// <param name="o2">list 2</param>
        /// <returns><see langword="true" /> ak sa obsahy obohch listov rovnajú, inak <see langword="false" /></returns>
        public static bool SequenceEquals(List<Operator> o1, List<Operator> o2)
        {
            if (o1.Count == o2.Count)
            {
                for (var i = 0; i < o1.Count; i++)
                    if (o1[i] != o2[i])
                        return false;

                return true;
            }

            return false;
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:GVDEditor.Entities.Operator" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Operator left, Operator right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:GVDEditor.Entities.Operator" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(Operator left, Operator right)
        {
            return !Equals(left, right);
        }
    }
}