/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/MapGenerator/MapGeneratorResources.cs
 * PURPOSE:     String Resources
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace MapGenerator
{
    /// <summary>
    ///     Contains Strings and Magic Number
    /// </summary>
    internal static class MapGeneratorResources
    {
        /// <summary>
        ///     The wall (const). Value: 1. Not passable
        /// </summary>
        internal const int Wall = 1;

        /// <summary>
        ///     The path (const). Value: 0.Is passable
        /// </summary>
        internal const int Path = 0;

        /// <summary>
        ///     The cell height (const). Value: 3. Height of the Border Cell
        /// </summary>
        internal const int CellHeight = 3;

        /// <summary>
        ///     The separator (const). Value: "|".
        /// </summary>
        internal const string Separator = "|";
    }
}