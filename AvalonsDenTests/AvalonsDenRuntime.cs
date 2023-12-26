/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenRuntime.cs
 * PURPOSE:     Basic Tests for the Extended Collections
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Diagnostics;
using AvalonRuntime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The Avalon's Den runtime unit test class.
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenRuntime
    {
        /// <summary>
        ///     Example: length 4
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     3,  12,13,14,15
        ///     4,  16,17,18,19
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     3,  12,13,14,15
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     3,  12,13,14,15
        ///     4,  16,17,18,19
        ///     0,1,2,3,4,5
        ///     0,  0,1,2,3,4,5
        ///     1,  6,7,8,9,10,11
        ///     2,  12,13,14,15,16,17
        /// </summary>
        [TestMethod]
        public void TestCalculateId()
        {
            const int length = 4;
            const int lengthAlt = 6;

            var coordinate = new Coordinates(0, 0, 5, 0);
            var masterId = ArtShared.CalculateId(coordinate, length);
            Debug.WriteLine("Master Id:" + masterId);

            Assert.IsTrue(masterId == 0, "Test passed, Master Id = 0");

            coordinate = new Coordinates(3, 2, 5, 0);
            masterId = ArtShared.CalculateId(coordinate, length);
            Debug.WriteLine("Master Id:" + masterId);

            Assert.IsTrue(masterId == 11, "Test passed, Master Id = 11");

            coordinate = new Coordinates(1, 2, 5, 0);
            masterId = ArtShared.CalculateId(coordinate, length);
            Debug.WriteLine("Master Id:" + masterId);

            Assert.IsTrue(masterId == 9, "Test passed, Master Id = 9");

            coordinate = new Coordinates(2, 1, 5, 0);
            masterId = ArtShared.CalculateId(coordinate, length);
            Debug.WriteLine("Master Id:" + masterId);

            Assert.IsTrue(masterId == 6, "Test passed, Master Id = 6");

            coordinate = new Coordinates(1, 1, 5, 0);
            masterId = ArtShared.CalculateId(coordinate, length);
            Debug.WriteLine("Master Id:" + masterId);

            Assert.IsTrue(masterId == 5, "Test passed, Master Id = 5");

            coordinate = new Coordinates(3, 4, 5, 0);
            masterId = ArtShared.CalculateId(coordinate, length);
            Debug.WriteLine("Master Id:" + masterId);

            Assert.IsTrue(masterId == 19, "Test passed, Master Id = 19");

            coordinate = new Coordinates(5, 2, 5, 0);
            masterId = ArtShared.CalculateId(coordinate, lengthAlt);
            Debug.WriteLine("Master Id:" + masterId);

            Assert.IsTrue(masterId == 17, "Test passed, Master Id = 17");
        }

        /// <summary>
        ///     Example: length 4
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     3,  12,13,14,15
        ///     4,  16,17,18,19
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     3,  12,13,14,15
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     3,  12,13,14,15
        ///     4,  16,17,18,19
        ///     0,1,2,3,4,5
        ///     0,  0,1,2,3,4,5
        ///     1,  6,7,8,9,10,11
        ///     2,  12,13,14,15,16,17
        /// </summary>
        [TestMethod]
        public void TestCalculateCoordinate()
        {
            const int length = 4;
            const int lengthAlt = 6;

            var coordinate = new Coordinates(0, 0, 5, 0);
            var coordinateCalc = ArtShared.IdToCoordinate(0, length, coordinate.ZLayer);

            Assert.IsTrue(coordinate.Equals(coordinateCalc), "Test passed, Master Id = 0");

            coordinate = new Coordinates(3, 2, 5, 0);
            coordinateCalc = ArtShared.IdToCoordinate(11, length, coordinate.ZLayer);

            Assert.IsTrue(coordinate.Equals(coordinateCalc), "Test passed, Master Id = 11");

            coordinate = new Coordinates(1, 2, 5, 0);
            coordinateCalc = ArtShared.IdToCoordinate(9, length, coordinate.ZLayer);

            Assert.IsTrue(coordinate.Equals(coordinateCalc), "Test passed, Master Id = 9");

            coordinate = new Coordinates(2, 1, 5, 0);
            coordinateCalc = ArtShared.IdToCoordinate(6, length, coordinate.ZLayer);

            Assert.IsTrue(coordinate.Equals(coordinateCalc), "Test passed, Master Id = 6");

            coordinate = new Coordinates(1, 1, 5, 0);
            coordinateCalc = ArtShared.IdToCoordinate(5, length, coordinate.ZLayer);

            Assert.IsTrue(coordinate.Equals(coordinateCalc), "Test passed, Master Id = 5");

            coordinate = new Coordinates(3, 4, 5, 0);
            coordinateCalc = ArtShared.IdToCoordinate(19, length, coordinate.ZLayer);

            Assert.IsTrue(coordinate.Equals(coordinateCalc), "Test passed, Master Id = 19");

            coordinate = new Coordinates(5, 2, 5, 0);
            coordinateCalc = ArtShared.IdToCoordinate(17, lengthAlt, coordinate.ZLayer);
            Assert.IsTrue(coordinate.Equals(coordinateCalc), "Test passed, Master Id = 17");
        }
    }
}