/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenResource.cs
 * PURPOSE:     Basic Tests for Avalon Resources
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The Avalon's Den resource unit test class.
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenResource
    {
        /// <summary>
        ///     Check if our comparer works
        /// </summary>
        [TestMethod]
        public void CompareTwoCoordinates()
        {
            var check = ResourcesGeneral.CoordinateOne.Equals(ResourcesGeneral.CoordinateTwo);
            Assert.IsTrue(check, "Test passed Coordinates Compare Map " + check + Environment.NewLine);
        }

        /// <summary>
        ///     Check if our ContainsKey works
        /// </summary>
        [TestMethod]
        public void CompareCoordinatesDictionary()
        {
            var testContain = new Dictionary<Coordinates, int>
            {
                { ResourcesGeneral.CoordinateTwo, 0 },
                { ResourcesGeneral.CoordinateThree, 2 }
            };

            var check = testContain.ContainsKey(ResourcesGeneral.CoordinateOne);
            Assert.IsTrue(check, "Test passed Dictionary Contains Value " + check + Environment.NewLine);
        }
    }
}