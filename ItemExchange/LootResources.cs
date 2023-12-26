/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/LootResources.cs
 * PURPOSE:     Basic String Resources
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;

// ReSharper disable StaticMemberInitializerReferesToMemberBelow

namespace ItemExchange
{
    /// <summary>
    ///     The loot resources class.
    /// </summary>
    internal static class LootResources
    {
        /// <summary>
        ///     The Name extension for specific Tile Names
        /// </summary>
        internal const string NameExtension = "Tile";

        /// <summary>
        ///     The inventory limiter
        /// </summary>
        internal const int InventoryLimiter = 10;

        /// <summary>
        ///     The selection image
        /// </summary>
        internal static readonly string SelectionImage =
            Path.Combine(Directory.GetCurrentDirectory(), "Resource", "selection.png");

        /// <summary>
        ///     The selection image
        /// </summary>
        internal static readonly string BlankImage =
            Path.Combine(Directory.GetCurrentDirectory(), "Resource", "blank.png");

        /// <summary>
        ///     The selection image
        /// </summary>
        internal static readonly string ErrorImage =
            Path.Combine(Directory.GetCurrentDirectory(), "Resource", "error.png");

        /// <summary>
        ///     The backround image
        /// </summary>
        internal static readonly string BackroundImage =
            Path.Combine(Directory.GetCurrentDirectory(), "Resource", "base.png");

        /// <summary>
        ///     Gets or sets the image path.
        /// </summary>
        /// <value>
        ///     The image path.
        /// </value>
        internal static string ImagePath { get; set; }

        /// <summary>
        ///     Gets or sets the cell.
        /// </summary>
        internal static int Cell { get; set; } = 75;

        /// <summary>
        ///     Gets or sets the divider.
        /// </summary>
        internal static int Splitter { get; set; } = 10;

        /// <summary>
        ///     Gets or sets the Row.
        /// </summary>
        internal static int Row { get; set; } = 3;
    }
}