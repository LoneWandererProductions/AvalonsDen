/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorItems/EditorItemsRegister.cs
 * PURPOSE:     Register of Item Editor, Factory Pattern, at least planed
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace EditorItems
{
    /// <summary>
    ///     My Register where I store most shared Variables
    /// </summary>
    internal static class EditorItemsRegister
    {
        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        internal static string Path { get; set; }
    }
}