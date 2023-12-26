﻿/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     CommonLibraryTests
 * FILE:        CommonLibraryTests/Utilities.cs
 * PURPOSE:     Test for utility Class
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using ExtendedSystemObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonLibraryTests
{
    /// <summary>
    ///     Basic tests for the utility Class
    /// </summary>
    [TestClass]
    public class Utilities
    {
        /// <summary>
        ///     Checks if we get the correct Indexes
        /// </summary>
        [TestMethod]
        public void GetAvailableIndex()
        {
            var lst = new List<int>
            {
                0,
                2,
                3,
                4,
                6
            };

            var index = Utility.GetFirstAvailableIndex(lst);

            Assert.AreEqual(1, index, string.Concat("Index was false", index));

            var keys = Utility.GetAvailableIndex(lst, 3);

            Assert.AreEqual(1, keys[0], string.Concat("Index was false", keys[0]));
            Assert.AreEqual(5, keys[1], string.Concat("Index was false", keys[1]));
            Assert.AreEqual(7, keys[2], string.Concat("Index was false", keys[2]));
        }

        /// <summary>
        ///     Check the Previouses and  the next element.
        /// </summary>
        [TestMethod]
        public void PreviousNextElement()
        {
            var lst = new List<int> {0, 3, 6};

            var index = Utility.GetNextElement(3, lst);
            Assert.AreEqual(6, index, string.Concat("Index was false", index));
            index = Utility.GetNextElement(6, lst);
            Assert.AreEqual(0, index, string.Concat("Index was false", index));
            index = Utility.GetPreviousElement(3, lst);
            Assert.AreEqual(0, index, string.Concat("Index was false", index));
            index = Utility.GetPreviousElement(0, lst);
            Assert.AreEqual(6, index, string.Concat("Index was false", index));
            index = Utility.GetPreviousElement(1, lst);
            Assert.AreEqual(6, index, string.Concat("Index was false", index));
            index = Utility.GetNextElement(1, lst);
            Assert.AreEqual(0, index, string.Concat("Index was false", index));
        }
    }
}
