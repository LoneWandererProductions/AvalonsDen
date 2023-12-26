/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/SerializeableKeyValuePair.cs
 * PURPOSE:     Makes a Key Value Pair Serialize-able
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * SOURCES:     https://blogs.msdn.microsoft.com/seshadripv/2005/11/02/serializing-an-object-of-the-keyvaluepair-generic-class/
 */

using System;
using System.Xml.Serialization;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global, used for serialization, the set will be used
// ReSharper disable MemberCanBePrivate.Global, used for serialization, the set will be used

namespace Resources
{
    /// <summary>
    ///     Might be slower in Dictionaries, must be checked
    /// </summary>
    public static class SerializeableKeyValuePair
    {
        /// <summary>
        ///     The key value pair struct.
        /// </summary>
        /// <typeparam name="TK">The Key</typeparam>
        /// <typeparam name="TV">The Value</typeparam>
        [Serializable]
        [XmlType(TypeName = nameof(SerializeableKeyValuePair))]
        public struct KeyValuePair<TK, TV>
        {
            /// <summary>
            ///     Initializes a new instance of the class
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="value">The value.</param>
            public KeyValuePair(TK key, TV value) : this()
            {
                Key = key;
                Value = value;
            }

            /// <summary>
            ///     Gets or sets the key.
            /// </summary>
            public TK Key { get; set; }

            /// <summary>
            ///     Gets or sets the value.
            /// </summary>
            public TV Value { get; set; }
        }
    }
}