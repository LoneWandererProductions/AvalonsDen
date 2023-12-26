/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenEventEngine.cs
 * PURPOSE:     Basic Tests for Avalon Even Engine
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The Avalon's Den game engine unit test class.
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenGameEngine
    {
        /// <summary>
        ///     Check handling of Game Engine
        /// </summary>
        [TestMethod]
        public void TurnEngine()
        {
            EngineGameTurns.Initiate(10, 0, 365);

            EngineGameTurns.CountActions(3);
            //3,0
            Assert.AreEqual(0, EngineGameTurns.CurrentCyle, "One: Correct Day count");
            Assert.AreEqual(3, EngineGameTurns.CycleModulo, "One: Correct Day cycle");
            Assert.AreEqual(0, EngineGameTurns.CurrentYear, "One: Year");

            EngineGameTurns.CountActions(7);

            //0,1
            Assert.AreEqual(1, EngineGameTurns.CurrentCyle, "Two: Correct Day count");
            Assert.AreEqual(0, EngineGameTurns.CycleModulo, "Two: Correct Day cycle");
            Assert.AreEqual(0, EngineGameTurns.CurrentYear, "Two: Year");

            EngineGameTurns.CountActions(12);

            //2,2
            Assert.AreEqual(2, EngineGameTurns.CurrentCyle, "Three: Correct Day count");
            Assert.AreEqual(2, EngineGameTurns.CycleModulo, "Three: Correct Day cycle");
            Assert.AreEqual(0, EngineGameTurns.CurrentYear, "Three: Year");

            EngineGameTurns.CountActions(22);

            //4,4
            Assert.AreEqual(4, EngineGameTurns.CurrentCyle, "Four: Correct Day count");
            Assert.AreEqual(4, EngineGameTurns.CycleModulo, "Four: Correct Day cycle");
            Assert.AreEqual(0, EngineGameTurns.CurrentYear, "Four: Year");

            EngineGameTurns.CountActions(362 * 10);
            Assert.AreEqual(1, EngineGameTurns.CurrentYear,
                "Last: Year " + EngineGameTurns.CurrentYear + " Days " + EngineGameTurns.CurrentCyle);
        }
    }
}