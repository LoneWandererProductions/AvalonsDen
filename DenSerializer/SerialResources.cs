/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DenSerializer/SerialResources.cs
 * PURPOSE:     Basic Resource File
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace DenSerializer
{
    /// <summary>
    ///     The serial resources class.
    /// </summary>
    internal static class SerialResources
    {
        /// <summary>
        ///     Error in path (const). Value: "No File Found: ".
        /// </summary>
        internal const string ErrorPath = "No File Found: ";

        /// <summary>
        ///     Error File is empty (const). Value: "File was empty: ".
        /// </summary>
        internal const string ErrorFileEmpty = "File was empty: ";

        /// <summary>
        ///     Error could not File was empty (const). Value: "Object was Empty: ".
        /// </summary>
        internal const string ErrorSerializerEmpty = "Object was Empty: ";

        /// <summary>
        ///     Error string in serializer XML.
        /// </summary>
        internal const string ErrorSerializerXml = "Could not Serialize, Error in XML: ";

        /// <summary>
        ///     Error string in serializer Stream.
        /// </summary>
        internal const string ErrorStream = "Could not Serialize, Error in Stream: ";
    }
}