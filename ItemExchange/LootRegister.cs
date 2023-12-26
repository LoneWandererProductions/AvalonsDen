/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/LootRegister.cs
 * PURPOSE:     Register of ItemExchange
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

namespace ItemExchange
{
    /// <summary>
    ///     Basic Register
    /// </summary>
    internal static class LootRegister
    {
        /// <summary>
        ///     Gets or sets the tile selected.
        /// </summary>
        /// <value>
        ///     The tile selected.
        /// </value>
        public static string TileSelected { get; internal set; }

        /// <summary>
        ///     Gets or sets the loot.
        /// </summary>
        /// <value>
        ///     The loot.
        /// </value>
        public static Dictionary<int, LootingItemView> Loot { get; internal set; }

        /// <summary>
        ///     The Id of the selected item, if none than value is -1
        /// </summary>
        internal static int ItemSelected { get; set; }

        /// <summary>
        ///     Gets or sets the image paths.
        /// </summary>
        /// <value>
        ///     The image paths.
        /// </value>
        internal static Dictionary<int, string> ImagePaths { get; set; }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        internal static void Clear()
        {
            ItemSelected = -1;
            TileSelected = string.Empty;
        }
    }
}