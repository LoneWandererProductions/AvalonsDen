/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenStringParser.cs
 * PURPOSE:     Test our StringParser
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using EventEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The Avalon's den string parser unit test class.
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenStringParser
    {
        /// <summary>
        ///     Check if our basic functions do Work out
        ///     Test for EventEngine
        /// </summary>
        [TestMethod]
        public void ParseItems()
        {
            var cache = StringParser.ParseItems("(0    ,1)  (1,1 ) ");

            Assert.AreEqual(2, cache.Count, "Test failed no changes");

            Assert.AreEqual("0", cache[0].Id, "First: Right Id");
            Assert.AreEqual(1, cache[0].Amount, "First: Right Amount");

            Assert.AreEqual("1", cache[1].Id, "Second: Right Id");
            Assert.AreEqual(1, cache[1].Amount, "Second: Right Amount");
        }
    }
}