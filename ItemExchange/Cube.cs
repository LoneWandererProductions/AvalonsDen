/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/Cube.cs
 * PURPOSE:     Cube Object that holds the coordinates of the Cell
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace ItemExchange
{
    /// <summary>
    ///     The Cube class.
    /// </summary>
    public sealed class Cube
    {
        /// <summary>
        ///     Gets or sets the first x Coordinate.
        /// </summary>
        public int XOne { get; init; }

        /// <summary>
        ///     Gets or sets the first y Coordinate.
        /// </summary>
        public int YOne { get; init; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; init; }
    }
}