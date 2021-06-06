using System.Collections.Generic;

namespace GVDEditor.Entities
{
    /// <summary>
    ///     Dopravca vlaku (operátor)
    /// </summary>
    public sealed record Operator
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

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}