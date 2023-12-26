/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Engine/CellsClickedventArgs.cs
 * PURPOSE:     Helper Object to transmit Data
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Windows.Input;

namespace Renderer
{
    /// <inheritdoc />
    /// <summary>
    ///     The editor clicked event args class.
    /// </summary>
    public sealed class EditorClickedEventArgs : EventArgs
    {
        /// <summary>
        ///     Type of MouseClick
        /// </summary>
        public MouseButtonEventArgs ClickType { get; internal init; }

        /// <summary>
        ///     Coordinates of Clicked Tile
        /// </summary>
        public int ImagePoint { get; internal set; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     we just need it for the Editor no need to use the big guns
    /// </summary>
    public sealed class BoxClickedEventArgs : EventArgs
    {
        /// <summary>
        ///     The tile id.
        /// </summary>
        public int TileId { get; set; }
    }
}