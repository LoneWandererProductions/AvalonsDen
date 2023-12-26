/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/TileBorders.cs
 * PURPOSE:     Borders Description
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable ClassNeverInstantiated.Global, not necessary

namespace Resources
{
    /// <summary>
    ///     Object that describes the Borders of a Cell
    ///     Actual it is static for since I described all possible Borders
    ///     Users might add new types later
    ///     Add as a Build In
    ///     NW,N,NE,
    ///     W,X,E
    ///     SW,S,SE
    ///     TODO Implement Pathfinder Diagonal Movement Points in the Editor, TODO add it as hard coded Resource
    /// </summary>
    public sealed class TileBorders
    {
        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BlockAble { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BorderNorth { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BorderEast { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BorderSouth { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BorderWest { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BorderNorthWest { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BorderNorthEast { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BorderSouthEast { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether passable or not
        /// </summary>
        public bool BorderSouthWest { get; init; }
    }
}