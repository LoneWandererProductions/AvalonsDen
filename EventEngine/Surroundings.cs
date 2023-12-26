/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/Surroundings.cs
 * PURPOSE:     Helper Class to get the Surrounding areas on a Map, heavily inspired by the TransitionsEngine, why creating something new if the something else works.
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using AvalonRuntime;

// ReSharper disable InvertIf

namespace EventEngine
{
    /// <summary>
    ///     The surroundings class.
    /// </summary>
    internal static class Surroundings
    {
        /// <summary>
        ///     List of surrounding Coordinates
        /// </summary>
        /// <param name="masterId">The masterId.</param>
        /// <param name="height">The height.</param>
        /// <param name="length">The length.</param>
        /// <returns>The List of Tiles available<see cref="T:List{int}" />.</returns>
        public static List<int> GenerateCoordinates(int masterId, int height, int length)
        {
            //id of location, Tile Id
            var cTiles = new List<int>();
            int calcId;

            if (!CheckIfPossible(masterId, height, length)) return null;

            //N
            //812       012
            //7x3       345
            //654       678
            if (CheckN(masterId, length))
            {
                calcId = masterId - length;
                cTiles.Add(calcId);
            }

            //NE
            //812       012
            //7x3       345
            //654       678
            if (CheckNe(masterId, length))
            {
                calcId = masterId - length + 1;
                cTiles.Add(calcId);
            }

            //E
            //812       012
            //7x3       345
            //654       678
            if (CheckE(masterId, length))
            {
                calcId = masterId + 1;
                cTiles.Add(calcId);
            }

            //SE
            //812       012
            //7x3       345
            //654       678
            if (CheckSe(masterId, height, length))
            {
                calcId = masterId + length + 1;
                cTiles.Add(calcId);
            }

            //W
            //812       012
            //7x3       345
            //654       678
            if (CheckW(masterId, height, length))
            {
                calcId = masterId + length;
                cTiles.Add(calcId);
            }

            //SW
            //812       012
            //7x3       345
            //654       678
            if (CheckSw(masterId, height, length))
            {
                calcId = masterId + length - 1;
                cTiles.Add(calcId);
            }

            //S
            //812       012
            //7x3       345
            //654       678
            if (CheckS(masterId, length))
            {
                calcId = masterId - 1;
                cTiles.Add(calcId);
            }

            //NW
            //812       012
            //7x3       345
            //654       678
            if (CheckNw(masterId, length))
            {
                calcId = masterId - length - 1;
                cTiles.Add(calcId);
            }

            return cTiles;
        }

        /// <summary>
        ///     Checks if possible Id is correct
        ///     812       012
        ///     7x3       345
        ///     654       678 id is 4 for example
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="height">Height of the Map</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckIfPossible(int masterId, int height, int length)
        {
            if (masterId < 0) return false;

            return masterId < height * length;
        }

        /// <summary>
        ///     xNx
        ///     xxx
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckN(int masterId, int length)
        {
            return masterId >= length;
        }

        /// <summary>
        ///     xxNe
        ///     xxx
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckNe(int masterId, int length)
        {
            if (masterId < length) return false;
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, length);

            return coordinate.XRow != length - 1;
        }

        /// <summary>
        ///     xxx
        ///     xxE
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckE(int masterId, int length)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, length);
            return coordinate.XRow != length - 1;
        }

        /// <summary>
        ///     xxx
        ///     xxx
        ///     xxSE
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="height">Height of the Map</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckSe(int masterId, int height, int length)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, length);
            if (coordinate.XRow == length - 1) return false;

            return coordinate.YColumn != height - 1;
        }

        /// <summary>
        ///     xxx
        ///     xxx
        ///     xWx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="height">Height of the Map</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckW(int masterId, int height, int length)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, length);
            return coordinate.YColumn != height - 1;
        }

        /// <summary>
        ///     xxx
        ///     xxx
        ///     SWxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="height">Height of the Map</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckSw(int masterId, int height, int length)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, length);
            if (coordinate.YColumn == height - 1) return false;

            return coordinate.XRow != 0;
        }

        /// <summary>
        ///     xxx
        ///     Sxx
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckS(int masterId, int length)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, length);
            return coordinate.XRow != 0;
        }

        /// <summary>
        ///     Nwxx
        ///     xxx
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <param name="length">Length of the Map</param>
        /// <returns></returns>
        private static bool CheckNw(int masterId, int length)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, length);
            if (masterId < length) return false;

            return coordinate.XRow != 0;
        }
    }
}