/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/TransitionEngine/AdjacentCoordinate.cs
 * PURPOSE:     Calculate direction and Id's for Transitions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using AvalonRuntime;

namespace TransitionEngine
{
    /// <summary>
    ///     The adjacent coordinate class.
    /// </summary>
    internal sealed class AdjacentCoordinate
    {
        /// <summary>
        ///     Height of the Map (readonly).
        /// </summary>
        private readonly int _height;

        /// <summary>
        ///     Length of the Map (readonly).
        /// </summary>
        private readonly int _length;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdjacentCoordinate" /> class.
        /// </summary>
        /// <param name="height">Height of the Map</param>
        /// <param name="length">Length of the Map</param>
        public AdjacentCoordinate(int height, int length)
        {
            _height = height;
            _length = length;
        }

        /// <summary>
        ///     Generates Surrounding Struct Points
        ///     -1,1  0,1   1,1   NW,N,NE   812     -1,-1   0,-1    1,-1
        ///     -1,0  0,0   1,0    S,0,E    7x3     -1,0    0,0     1,0
        ///     -1,-1 0,-1  1,-1   SW,W,SE  654     -1,1    0,1     1,1
        ///     Key = Direction, Value =Tile Id
        ///     012
        ///     345
        ///     678
        /// </summary>
        /// <param name="transitions"> Key = Direction, Value =Tile Id</param>
        /// <param name="masterId"> Id of Master Tile</param>
        /// <returns>Dictionary of Tiles, Key is MasterId, Value is Tile Id</returns>
        internal Dictionary<int, int> GenerateCoordinates(Dictionary<int, int> transitions, int masterId)
        {
            //id of location, Tile Id
            var cTiles = new Dictionary<int, int>();
            int calcId;

            if (!CheckIfPossible(masterId)) return null;

            //812       012
            //7x3       345
            //654       678 id is 4 for example
            foreach (var id in transitions)
                switch (id.Value)
                {
                    //N
                    //812       012
                    //7x3       345
                    //654       678
                    case 1:
                        if (!CheckN(masterId)) continue;

                        calcId = masterId - _length;
                        cTiles.Add(calcId, id.Key);
                        break;
                    //NE
                    //812       012
                    //7x3       345
                    //654       678
                    case 2:
                        if (!CheckNe(masterId)) continue;

                        calcId = masterId - _length + 1;
                        cTiles.Add(calcId, id.Key);
                        break;
                    //E
                    //812       012
                    //7x3       345
                    //654       678
                    case 3:
                        if (!CheckE(masterId)) continue;

                        calcId = masterId + 1;
                        cTiles.Add(calcId, id.Key);
                        break;
                    //SE
                    //812       012
                    //7x3       345
                    //654       678
                    case 4:
                        if (!CheckSe(masterId)) continue;

                        calcId = masterId + _length + 1;
                        cTiles.Add(calcId, id.Key);
                        break;
                    //W
                    //812       012
                    //7x3       345
                    //654       678
                    case 5:
                        if (!CheckW(masterId)) continue;

                        calcId = masterId + _length;
                        cTiles.Add(calcId, id.Key);
                        break;
                    //SW
                    //812       012
                    //7x3       345
                    //654       678
                    case 6:
                        if (!CheckSw(masterId)) continue;

                        calcId = masterId + _length - 1;
                        cTiles.Add(calcId, id.Key);
                        break;
                    //S
                    //812       012
                    //7x3       345
                    //654       678
                    case 7:
                        if (!CheckS(masterId)) continue;

                        calcId = masterId - 1;
                        cTiles.Add(calcId, id.Key);
                        break;
                    //NW
                    //812       012
                    //7x3       345
                    //654       678
                    case 8:
                        if (!CheckNw(masterId)) continue;

                        calcId = masterId - _length - 1;
                        cTiles.Add(calcId, id.Key);
                        break;

                    default:
                        continue;
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
        /// <returns>If needed</returns>
        private bool CheckIfPossible(int masterId)
        {
            if (masterId < 0) return false;

            return masterId < _height * _length;
        }

        /// <summary>
        ///     xNx
        ///     xxx
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <returns>If needed</returns>
        private bool CheckN(int masterId)
        {
            return masterId >= _length;
        }

        /// <summary>
        ///     xxNe
        ///     xxx
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <returns>If needed</returns>
        private bool CheckNe(int masterId)
        {
            if (masterId < _length) return false;
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, _length);

            return coordinate.XRow != _length - 1;
        }

        /// <summary>
        ///     xxx
        ///     xxE
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <returns>If needed</returns>
        private bool CheckE(int masterId)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, _length);
            return coordinate.XRow != _length - 1;
        }

        /// <summary>
        ///     xxx
        ///     xxx
        ///     xxSE
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <returns>If needed</returns>
        private bool CheckSe(int masterId)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, _length);
            if (coordinate.XRow == _length - 1) return false;

            return coordinate.YColumn != _height - 1;
        }

        /// <summary>
        ///     xxx
        ///     xxx
        ///     xWx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <returns>If needed</returns>
        private bool CheckW(int masterId)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, _length);
            return coordinate.YColumn != _height - 1;
        }

        /// <summary>
        ///     xxx
        ///     xxx
        ///     SWxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <returns>If needed</returns>
        private bool CheckSw(int masterId)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, _length);
            if (coordinate.YColumn == _height - 1) return false;

            return coordinate.XRow != 0;
        }

        /// <summary>
        ///     xxx
        ///     Sxx
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <returns>If needed</returns>
        private bool CheckS(int masterId)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, _length);
            return coordinate.XRow != 0;
        }

        /// <summary>
        ///     Nwxx
        ///     xxx
        ///     xxx
        /// </summary>
        /// <param name="masterId">Id of Master</param>
        /// <returns>If needed</returns>
        private bool CheckNw(int masterId)
        {
            //Convert to Coordinate
            var coordinate = ArtShared.IdToCoordinate(masterId, _length);
            if (masterId < _length) return false;

            return coordinate.XRow != 0;
        }
    }
}