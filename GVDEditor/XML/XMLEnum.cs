using System;

namespace GVDEditor.XML
{
    /// <summary>
    ///     Predstavuje triedu, ktorá spracuváva Enumerácie serializovateľné do XML
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class XMLEnum<T> where T : Enum
    {
        /// <summary>
        ///     Konvertuje string na enumeraciu
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T StringToEnum(string s)
        {
            if (string.IsNullOrEmpty(s)) return default;
            return (T) Enum.Parse(typeof(T), s);
        }
        
        /// <summary>
        ///     Konvertuje enumeraciu na string
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string EnumToString(T e)
        {
            return Enum.GetName(typeof(T), e);
        }
    }
}