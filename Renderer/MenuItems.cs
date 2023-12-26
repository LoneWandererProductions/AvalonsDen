/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Renderer/MenuItems.cs
 * PURPOSE:     Simple Helper Struct
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Renderer
{
    /// <summary>
    ///     The menu items class.
    /// </summary>
    public sealed class MenuItems
    {
        /// <summary>
        ///     Positive or negative Orientation
        /// </summary>
        public int Position { internal get; set; }

        /// <summary>
        ///     Gets or sets the Image Uri.
        /// </summary>
        public string ImagePath { internal get; set; }

        /// <summary>
        ///     Gets or sets the Tool tip.
        /// </summary>
        public string Tooltip { internal get; set; }
    }

    /// <summary>
    ///     The Cursor class. Contains basic Image Informations of the Menu
    /// </summary>
    public sealed class MCursor
    {
        /// <summary>
        ///     Gets or sets the Background image.
        /// </summary>
        public string Background { internal get; set; }

        /// <summary>
        ///     Position 0, image of Idle Icon
        /// </summary>
        public string Idle { internal get; set; }
    }
}