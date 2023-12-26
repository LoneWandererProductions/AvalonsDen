/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DenSerializer/DataItem.cs
 * PURPOSE:     Helper Object for serializing Dictionaries
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

//Disabled Resharper Warnings, do not make the suggested Changes!
// ReSharper disable MemberCanBeInternal
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace DenSerializer
{
    /// <summary>
    ///     Helper Object for serializing Dictionaries
    ///     Do not remove any Constructors or change Visibility!
    /// </summary>
    public sealed class DataItem
    {
        /// <summary>
        ///     Don't Remove
        ///     needed for Serialization of Dictionaries
        /// </summary>
        public DataItem()
        {
        }

        /// <summary>
        ///     Holds Key Value Pair, Key as usual in, Value a Serialized Object saved as String
        /// </summary>
        /// <param name="key">Key of Dictionary</param>
        /// <param name="value">String of Serialized Value</param>
        public DataItem(int key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        ///     Don't Remove
        ///     needed for Serialization of Dictionaries
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        ///     Don't Remove
        ///     needed for Serialization of Dictionaries
        /// </summary>
        public string Value { get; set; }
    }
}