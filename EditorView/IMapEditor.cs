/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        EditorView/IMapEditor.cs
 * PURPOSE:     Helper Window to create a new Map, Interface definition
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

namespace EditorView
{
    /// <summary>
    ///     The IMapEditor interface.
    /// </summary>
    internal interface IMapEditor
    {
        /// <summary>
        ///     Create the map.
        /// </summary>
        void CreateMap();
    }
}