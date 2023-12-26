/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorMap/IMapEditor.cs
 * PURPOSE:     Helper Window to create a new Map, Interface definition
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

namespace EditorMap
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