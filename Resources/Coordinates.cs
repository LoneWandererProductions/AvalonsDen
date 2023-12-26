/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/Coordinates.cs
 * PURPOSE:     Coordinates Object
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

//Disabled Resharper Warnings, do not make the suggested Changes!
// ReSharper disable NonReadonlyMemberInGetHashCode

using System;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     The coordinates class.
    /// </summary>
    public sealed class Coordinates : IEquatable<Coordinates>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Coordinates" /> class.
        /// </summary>
        public Coordinates()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Coordinates" /> class.
        /// </summary>
        /// <param name="xRow">The x row.</param>
        /// <param name="yColumn">The y column.</param>
        /// <param name="zLayer">The zLayer.</param>
        public Coordinates(int xRow, int yColumn, int zLayer)
        {
            XRow = xRow;
            YColumn = yColumn;
            ZLayer = zLayer;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Coordinates" /> class.
        /// </summary>
        /// <param name="xRow">The x row.</param>
        /// <param name="yColumn">The y column.</param>
        /// <param name="zLayer">The zLayer.</param>
        /// <param name="tileId">The tileId.</param>
        public Coordinates(int xRow, int yColumn, int zLayer, int tileId)
        {
            XRow = xRow;
            YColumn = yColumn;
            ZLayer = zLayer;
            TileId = tileId;
        }

        /// <summary>
        ///     Row as X
        /// </summary>
        public int XRow { get; set; }

        /// <summary>
        ///     Column as Y
        /// </summary>
        public int YColumn { get; set; }

        /// <summary>
        ///     Layer of Map
        /// </summary>
        public int ZLayer { get; set; }

        /// <summary>
        ///     TileId
        /// </summary>
        public int TileId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Compares If a Coordinate is equal to another Coordinate
        /// </summary>
        /// <param name="other">other Coordinate</param>
        /// <returns>True if equal, false if not</returns>
        public bool Equals(Coordinates other)
        {
            return XRow == other?.XRow && YColumn == other.YColumn && ZLayer == other.ZLayer;
        }

        /// <summary>
        ///     Provides the equal Command
        /// </summary>
        /// <param name="obj">Other Object</param>
        /// <returns>If Object is equal</returns>
        public override bool Equals(object obj)
        {
            return obj is Coordinates other && Equals(other);
        }

        /// <summary>
        ///     Generate Hash Code just for the Three Attributes, we don't need More
        /// </summary>
        /// <returns>HashCode of the Object</returns>
        public override int GetHashCode()
        {
            return XRow ^ YColumn ^ ZLayer;
        }
    }
}