/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenTransitions.cs
 * PURPOSE:     Basic Tests for the Transitions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Diagnostics;
using ExtendedSystemObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;
using TransitionEngine;

// ReSharper disable ArrangeBraces_foreach

namespace AvalonsDenTests
{
    /// <summary>
    ///     The Avalon's Den Transitions unit test class.
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenTransitions
    {
        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void PossibleTransitions()
        {
            TransitionEngineRegister.TileDct = HelperMethods.InitiateTileDct();

            var list = TransitionEngineRegister.GetAllTransitions(0);

            Assert.IsFalse(list.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(8, list.Count,
                "Test passed Collected all Transitions for Simple Example, Tile Count: " + list.Count);
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionDirectionsN()
        {
            TransitionEngineRegister.TileDct = new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 1, ResourcesTransitions.TileN }
            };

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(1, cache.Count, "Test passed the wrong corner Case N was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 0);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case N was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 1);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case N was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 2);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case N was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 7);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            //Tile Coordinate 4 must use Tile 1
            Assert.AreEqual(1, cache[4], "Test passed the wrong corner Case N was captured:");

            //out if Bounds Exception
            cache = test.GenerateCoordinates(listTranstitions, 9);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NE was captured:");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionDirectionsNe()
        {
            TransitionEngineRegister.TileDct = new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 2, ResourcesTransitions.TileNe }
            };

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(1, cache.Count, "Test passed the wrong corner Case NE was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 2);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NE was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 5);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NE was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NE was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 7);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            //Tile Coordinate 5 must use Tile 1
            Assert.AreEqual(2, cache[5], "Test passed the wrong corner Case NE was captured:");

            //out if Bounds Exception
            cache = test.GenerateCoordinates(listTranstitions, 9);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NE was captured:");

            //Special error catch
            cache = test.GenerateCoordinates(listTranstitions, 1);
            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NE, Special Test");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionDirectionsE()
        {
            TransitionEngineRegister.TileDct = new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 3, ResourcesTransitions.TileE }
            };

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(1, cache.Count, "Test passed the wrong corner Case E was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 2);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case E was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 5);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case E was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case E was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 3);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            //Tile Coordinate 4 must use Tile 4
            Assert.AreEqual(3, cache[4], "Test passed the wrong corner Case E was captured:");

            //out if Bounds Exception
            cache = test.GenerateCoordinates(listTranstitions, 9);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case E was captured:");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionDirectionsSe()
        {
            TransitionEngineRegister.TileDct = new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 4, ResourcesTransitions.TileSe }
            };

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(1, cache.Count, "Test passed the wrong corner Case SE was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 2);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SE was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 5);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SE was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SE was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 3);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            //Tile Coordinate 7 must use Tile 4
            Assert.AreEqual(4, cache[7], "Test passed the wrong corner Case SE was captured:");

            //out if Bounds Exception
            cache = test.GenerateCoordinates(listTranstitions, 9);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SE was captured:");

            //Special error catch
            cache = test.GenerateCoordinates(listTranstitions, 7);
            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SE, Special Test");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionDirectionsW()
        {
            TransitionEngineRegister.TileDct = new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 5, ResourcesTransitions.TileW }
            };

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(1, cache.Count, "Test passed the wrong corner Case W was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 6);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case W was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 7);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case W was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case W was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 3);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            //Tile Coordinate 6 must use Tile 5
            Assert.AreEqual(5, cache[6], "Test passed the wrong corner Case W was captured:");

            //out if Bounds Exception
            cache = test.GenerateCoordinates(listTranstitions, 9);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case W was captured:");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionDirectionsSw()
        {
            TransitionEngineRegister.TileDct = new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 6, ResourcesTransitions.TileSw }
            };

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(1, cache.Count, "Test passed the wrong corner Case SW was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 6);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SW was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 7);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SW was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SW was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 4);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            //Tile Coordinate 6 must use Tile 6
            Assert.AreEqual(6, cache[6], "Test passed the wrong corner Case SW was captured:");

            //out if Bounds Exception
            cache = test.GenerateCoordinates(listTranstitions, 9);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SW was captured:");

            //Special error catch
            cache = test.GenerateCoordinates(listTranstitions, 3);
            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case SW, Special Test");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionDirectionsS()
        {
            TransitionEngineRegister.TileDct = new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 7, ResourcesTransitions.TileS }
            };

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(1, cache.Count, "Test passed the wrong corner Case S was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 0);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case S was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 3);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case S was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 6);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case S was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 2);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            //Tile Coordinate 4 must use Tile 7
            Assert.AreEqual(7, cache[1], "Test passed the wrong corner Case S was captured:");

            //Special error catch
            cache = test.GenerateCoordinates(listTranstitions, 3);
            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case S, Special Test");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionDirectionsNw()
        {
            TransitionEngineRegister.TileDct = new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 8, ResourcesTransitions.TileNw }
            };

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Element was empty");

            Assert.AreEqual(1, cache.Count, "Test passed the wrong corner Case NW was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 0);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NW was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 3);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NW was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 6);

            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NW was captured:");

            cache = test.GenerateCoordinates(listTranstitions, 4);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            //Tile Coordinate 4 must use Tile 8
            Assert.AreEqual(8, cache[0], "Test passed the wrong corner Case NW was captured:");

            //Special error catch
            cache = test.GenerateCoordinates(listTranstitions, 2);
            Assert.IsTrue(cache.IsNullOrEmpty(), "Test passed the wrong corner Case NW, Special Test");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionGeneration()
        {
            TransitionEngineRegister.TileDct = HelperMethods.InitiateTileDct();

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(0);

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 4);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            Assert.AreEqual(8, cache.Count,
                "Test passed Generated the right amount of Tiles,  Master Id 4, expected 8: " + cache.Count);

            test = new AdjacentCoordinate(3, 3);
            cache = test.GenerateCoordinates(listTranstitions, 0);

            Assert.AreEqual(3, cache.Count,
                "Test passed Generated the right amount of Tiles,  Master Id 0, expected 3: " + cache.Count);

            test = new AdjacentCoordinate(3, 3);
            cache = test.GenerateCoordinates(listTranstitions, 2);

            Assert.AreEqual(3, cache.Count,
                "Test passed Generated the right amount of Tiles, Master Id 2, expected 3: " + cache.Count);

            test = new AdjacentCoordinate(3, 3);
            cache = test.GenerateCoordinates(listTranstitions, 6);

            Assert.AreEqual(3, cache.Count,
                "Test passed Generated the right amount of Tiles,  Master Id 6, expected 3: " + cache.Count);

            test = new AdjacentCoordinate(3, 3);
            cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.AreEqual(3, cache.Count,
                "Test passed Generated the right amount of Tiles,  Master Id 8, expected 3: " + cache.Count);
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionGenerationLiveData()
        {
            TransitionEngineRegister.TileDct = ResourcesLoader.MasterTile;

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(19);

            Assert.AreEqual(8, listTranstitions.Count, "Transitions expected: " + listTranstitions.Count);

            Debug.WriteLine("Right amount of Tiles");
            foreach (var tst in listTranstitions)
            {
                Debug.WriteLine(string.Concat("Tile Coordinate ", tst.Key, " Tile Id ", tst.Value));
            }

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.AreEqual(3, cache.Count,
                "Test passed Generated the right amount of Tiles,  Master Id 8, expected 3: " + cache.Count);

            //Check expected Tiles
            cache = test.GenerateCoordinates(listTranstitions, 4);

            Debug.WriteLine("Expected Tiles:");
            if (cache.Count == 0)
            {
                Debug.WriteLine("was empty");
            }

            foreach (var tst in cache)
            {
                Debug.WriteLine(string.Concat("Tile Coordinate ", tst.Key, " Tile Id ", tst.Value));
            }

            Assert.IsTrue(cache[1] == 20, "1 failed " + cache[1]);
            Assert.IsTrue(cache[2] == 24, "2 failed " + cache[2]);
            Assert.IsTrue(cache[5] == 22, "5 failed " + cache[5]);
            Assert.IsTrue(cache[8] == 25, "8 failed " + cache[8]);
            Assert.IsTrue(cache[7] == 21, "7 failed " + cache[7]);
            Assert.IsTrue(cache[6] == 26, "6 failed " + cache[6]);
            Assert.IsTrue(cache[3] == 23, "3 failed " + cache[3]);
            Assert.IsTrue(cache[0] == 27, "0 failed " + cache[0]);
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionGenerationLiveSingle()
        {
            TransitionEngineRegister.TileDct = ResourcesLoader.MasterTile;

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(19);

            Assert.IsFalse(listTranstitions.Count == 9, "Transitions expected: " + listTranstitions.Count);

            Debug.WriteLine("Right amount of Tiles");
            foreach (var tst in listTranstitions)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.AreEqual(3, cache.Count,
                "Test passed Generated the right amount of Tiles,  Master Id 8, expected 3: " + cache.Count);

            //Check expected Tiles
            cache = test.GenerateCoordinates(listTranstitions, 4);

            Debug.WriteLine("Expected Tiles:");
            if (cache.Count == 0)
            {
                Debug.WriteLine("was empty");
            }

            foreach (var tst in cache)
            {
                Debug.WriteLine(string.Concat("Tile Coordinate ", tst.Key, " Tile Id ", tst.Value));
            }

            Assert.IsTrue(cache[1] == 20, "1 failed " + cache[1]);
            Assert.IsTrue(cache[2] == 24, "2 failed " + cache[2]);
            Assert.IsTrue(cache[5] == 22, "5 failed " + cache[5]);
            Assert.IsTrue(cache[8] == 25, "8 failed " + cache[8]);
            Assert.IsTrue(cache[7] == 21, "7 failed " + cache[7]);
            Assert.IsTrue(cache[6] == 26, "6 failed " + cache[6]);
            Assert.IsTrue(cache[3] == 23, "3 failed " + cache[3]);
            Assert.IsTrue(cache[0] == 27, "0 failed " + cache[0]);

            var transgenerate = new TransitionGenerate(3, 3, ResourcesLoader.MasterTile);

            var item = new Coordinates(1, 1, 3, 19);

            try
            {
                transgenerate.AddTransition(item);
            }
            catch (TransitionException ex)
            {
                Assert.Fail("Assert: " + ex);
            }

            Debug.WriteLine(transgenerate.TransitionDictionary.Count);

            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count, "Got the wrong amount of Tiles");

            DebugMessages(transgenerate.TransitionDictionary);

            Assert.IsTrue(true, "Passes");
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        ///     0,1,2
        ///     0,x,x,x
        ///     1,x,x,x
        ///     2,x,x,x
        /// </summary>
        [TestMethod]
        public void TransitionGenerationLiveMultiple()
        {
            TransitionEngineRegister.TileDct = ResourcesLoader.MasterTile;

            var listTranstitions = TransitionEngineRegister.GetAllTransitions(19);

            Assert.IsFalse(listTranstitions.Count == 9, "Transitions expected: " + listTranstitions.Count);

            Debug.WriteLine("Right amount of Tiles");
            foreach (var tst in listTranstitions)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            var test = new AdjacentCoordinate(3, 3);
            var cache = test.GenerateCoordinates(listTranstitions, 8);

            Assert.AreEqual(3, cache.Count,
                "Test passed Generated the right amount of Tiles,  Master Id 8, expected 3: " + cache.Count);

            //Check expected Tiles
            cache = test.GenerateCoordinates(listTranstitions, 4);

            Debug.WriteLine("Expected Tiles:");
            if (cache.Count == 0)
            {
                Debug.WriteLine("was empty");
            }

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate " + tst.Key + " Tile Id " + tst.Value);
            }

            Assert.IsTrue(cache[1] == 20, "1 failed " + cache[1]);
            Assert.IsTrue(cache[2] == 24, "2 failed " + cache[2]);
            Assert.IsTrue(cache[5] == 22, "5 failed " + cache[5]);
            Assert.IsTrue(cache[8] == 25, "8 failed " + cache[8]);
            Assert.IsTrue(cache[7] == 21, "7 failed " + cache[7]);
            Assert.IsTrue(cache[6] == 26, "6 failed " + cache[6]);
            Assert.IsTrue(cache[3] == 23, "3 failed " + cache[3]);
            Assert.IsTrue(cache[0] == 27, "0 failed " + cache[0]);

            var transgenerate = new TransitionGenerate(3, 3, ResourcesLoader.MasterTile);

            var item = new Coordinates(1, 1, 3, 19);

            try
            {
                transgenerate.AddTransition(item);
            }
            catch (TransitionException ex)
            {
                Assert.Fail("Assert: " + ex);
            }

            Debug.WriteLine(transgenerate.TransitionDictionary.Count);

            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count, "Got the wrong amount of Tiles");

            DebugMessages(transgenerate.TransitionDictionary);

            Assert.IsTrue(true, "Passes");

            item = new Coordinates(2, 2, 3, 19);

            try
            {
                transgenerate.AddTransition(item);
            }
            catch (TransitionException ex)
            {
                Assert.Fail("Assert: " + ex);
            }

            Debug.WriteLine(transgenerate.TransitionDictionary.Count);

            //Edge Case 4 were Changed
            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count,
                "Got the wrong amount of Tiles: " + transgenerate.TransitionDictionary.Count);

            DebugMessages(transgenerate.TransitionDictionary);

            var tiles = transgenerate.TransitionDictionary;

            Assert.AreEqual(9, tiles.Count, "Got the wrong amount of Tiles: " + tiles.Count);

            DebugMessages(tiles);

            CheckTiles(tiles);
        }

        /// <summary>
        ///     Check a Tile
        /// </summary>
        /// <param name="tiles">Transition Tiles</param>
        private static void CheckTiles(IReadOnlyDictionary<int, List<int>> tiles)
        {
            var cache = tiles[0];
            Assert.AreEqual(1, cache.Count, "0 failed " + cache[0]);
            cache = tiles[1];
            Assert.AreEqual(1, cache.Count, "1 failed " + cache[0]);
            cache = tiles[2];
            Assert.AreEqual(1, cache.Count, "2 failed " + cache[0]);
            cache = tiles[3];
            Assert.AreEqual(1, cache.Count, "3 failed " + cache[0]);
            cache = tiles[4];
            Assert.AreEqual(1, cache.Count, "4 failed " + cache[0]);
            Debug.WriteLine("check: " + cache[0]);
            cache = tiles[5];
            Assert.AreEqual(2, cache.Count, "5 failed " + cache[0]);
            Debug.WriteLine("check: " + cache[0] + " " + cache[1]);
            cache = tiles[6];
            Assert.AreEqual(1, cache.Count, "6 failed " + cache[0]);
            cache = tiles[7];
            Assert.AreEqual(2, cache.Count, "7 failed " + cache[0]);
            Debug.WriteLine("check: " + cache[0] + " " + cache[1]);
            cache = tiles[8];
            Assert.AreEqual(1, cache.Count, "8 failed " + cache[0]);
            Debug.WriteLine("check: " + cache[0]);
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        ///     0,1,2
        ///     0,x,x,x
        ///     1,x,x,x
        ///     2,x,x,x
        /// </summary>
        [TestMethod]
        public void TransitionGenerationLiveDelete()
        {
            var transgenerate = new TransitionGenerate(3, 3, ResourcesLoader.MasterTile);

            var item = new Coordinates(1, 1, 3, 19);

            try
            {
                transgenerate.AddTransition(item);
            }
            catch (TransitionException ex)
            {
                Assert.Fail("Assert: " + ex);
            }

            Debug.WriteLine(transgenerate.TransitionDictionary.Count);

            item = new Coordinates(1, 2, 3, 19);

            try
            {
                transgenerate.AddTransition(item);
            }
            catch (TransitionException ex)
            {
                Assert.Fail("Assert: " + ex);
            }

            //Target Id is correct
            var cache = transgenerate.DeleteTransition(item);

            foreach (var tst in cache)
            {
                Debug.WriteLine("Tile Coordinate: " + tst.Key + " Tile Id " + tst.Value);
            }

            Assert.IsTrue(cache.Count == 6, "Error is here: " + cache.Count);
        }

        /// <summary>
        ///     0,1,2,          Here we checked 4 and 6
        ///     0,  x,x,x
        ///     1,  x,m,x
        ///     2, sw,x,x
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionCombineMiddleSw()
        {
            var masterTile = HelperMethods.InitiateTileDct();
            var coordinateMidle = new Coordinates(1, 1, 5, 0);
            var coordinateEdge = new Coordinates(0, 2, 5, 0);

            var transgenerate = new TransitionGenerate(3, 3, masterTile);
            Debug.WriteLine("Initiate");
            transgenerate.AddTransition(coordinateMidle);
            Debug.WriteLine("Add Transitions");
            var cacheOne = transgenerate.TransitionDictionary;

            Debug.WriteLine("One: Transitions Elements " + cacheOne.Count);

            transgenerate.AddTransition(coordinateEdge);
            var cacheTwo = transgenerate.TransitionDictionary;

            Debug.WriteLine("Two: Transitions Elements " + cacheTwo.Count);

            //  012     xxx
            //  345     0xx
            //  678     x0x
            Assert.IsTrue(cacheTwo.ContainsKey(0), "Error in 0");
            Debug.WriteLine("0 : " + cacheTwo[0].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(1), "Error in 1");
            Debug.WriteLine("1 : " + cacheTwo[1].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(2), "Error in 2");
            Debug.WriteLine("2 : " + cacheTwo[2].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(3), "Error in 3");
            Assert.AreEqual(2, cacheTwo[3].Count, "Error in 3");
            Debug.WriteLine("3 : " + cacheTwo[3].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(4), "Error in 4");
            Debug.WriteLine("4 : " + cacheTwo[4].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(5), "Error in 5");
            Debug.WriteLine("5 : " + cacheTwo[5].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(6), "Error in 6");
            Debug.WriteLine("6 : " + cacheTwo[6].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(7), "Error in 7");
            Debug.WriteLine("7 : " + cacheTwo[7].Count);
            Assert.AreEqual(2, cacheTwo[7].Count, "Error in 7");
            Assert.IsTrue(cacheTwo.ContainsKey(8), "Error in 8");
            Debug.WriteLine("8 : " + cacheTwo[8].Count);
            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count,
                "Error Second Step not enough changes registered " + transgenerate.TransitionDictionary.Count);
        }

        /// <summary>
        ///     0,1,2,          Here we checked 4 and 1
        ///     0,  x,n,x
        ///     1,  x,m,x
        ///     2,  x,x,x
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionCombineMiddleN()
        {
            var masterTile = HelperMethods.InitiateTileDct();
            var coordinateMidle = new Coordinates(1, 1, 5, 0);
            var coordinateEdge = new Coordinates(1, 0, 5, 0);

            var transgenerate = new TransitionGenerate(3, 3, masterTile);
            Debug.WriteLine("Initiate");
            transgenerate.AddTransition(coordinateMidle);
            Debug.WriteLine("Add Transitions");
            var cacheOne = transgenerate.TransitionDictionary;

            Debug.WriteLine("One: Transitions Elements " + cacheOne.Count);

            transgenerate.AddTransition(coordinateEdge);
            var cacheTwo = transgenerate.TransitionDictionary;

            Debug.WriteLine("Two: Transitions Elements " + cacheTwo.Count);

            //  012     xxx     x0x     212         Expected:0,2,3,5 and of course 4 get's one added
            //  345     x0x     xxx     212
            //  678     xxx     xxx     111

            Assert.IsTrue(cacheTwo.ContainsKey(0), "Error in 0");
            Assert.AreEqual(2, cacheTwo[0].Count, "Error in 0");
            Assert.IsTrue(cacheTwo.ContainsKey(1), "Error in 1");
            Debug.WriteLine("1 : " + cacheTwo[1].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(2), "Error in 2");
            Assert.AreEqual(2, cacheTwo[2].Count, "Error in 2");
            Assert.IsTrue(cacheTwo.ContainsKey(3), "Error in 3");
            Assert.AreEqual(2, cacheTwo[3].Count, "Error in 3");
            Assert.IsTrue(cacheTwo.ContainsKey(4), "Error in 4");
            Debug.WriteLine("4 : " + cacheTwo[4].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(5), "Error in 5");
            Assert.AreEqual(2, cacheTwo[5].Count, "Error in 5");
            Assert.IsTrue(cacheTwo.ContainsKey(6), "Error in 6");
            Debug.WriteLine("6 : " + cacheTwo[6].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(7), "Error in 7");
            Debug.WriteLine("7 : " + cacheTwo[7].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(8), "Error in 8");
            Debug.WriteLine("8 : " + cacheTwo[8].Count);

            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count,
                "Error Second Step not enough changes registered " + transgenerate.TransitionDictionary.Count);
        }

        /// <summary>
        ///     0,1,2,          Here we checked 4 and 6
        ///     0,  x,x,x
        ///     1,  x,m,x
        ///     2, sw,x,x
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionRemoveMiddleSw()
        {
            var masterTile = HelperMethods.InitiateTileDct();
            var coordinateMidle = new Coordinates(1, 1, 5, 0);
            var coordinateEdge = new Coordinates(0, 2, 5, 0);

            var transgenerate = new TransitionGenerate(3, 3, masterTile);
            Debug.WriteLine("Initiate");
            transgenerate.AddTransition(coordinateMidle);
            Debug.WriteLine("Add Transitions");
            var cacheOne = transgenerate.TransitionDictionary;

            Debug.WriteLine("One: Transitions Elements " + cacheOne.Count);

            transgenerate.AddTransition(coordinateEdge);
            var cacheTwo = transgenerate.TransitionDictionary;
            Debug.WriteLine("Two: Transitions Elements " + cacheTwo.Count);

            //  012     xxx
            //  345     0xx
            //  678     x0x

            Assert.IsTrue(cacheTwo.ContainsKey(0), "Error in 0");
            Debug.WriteLine("0 : " + cacheTwo[0].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(1), "Error in 1");
            Debug.WriteLine("1 : " + cacheTwo[1].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(2), "Error in 2");
            Debug.WriteLine("2 : " + cacheTwo[2].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(3), "Error in 3");
            Assert.AreEqual(2, cacheTwo[3].Count, "Error in 3");
            Assert.IsTrue(cacheTwo.ContainsKey(4), "Error in 4");
            Debug.WriteLine("4 : " + cacheTwo[4].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(5), "Error in 5");
            Debug.WriteLine("5 : " + cacheTwo[5].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(6), "Error in 6");
            Debug.WriteLine("6 : " + cacheTwo[6].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(7), "Error in 7");
            Assert.AreEqual(2, cacheTwo[7].Count, "Error in 7");
            Assert.IsTrue(cacheTwo.ContainsKey(8), "Error in 8");
            Debug.WriteLine("8 : " + cacheTwo[8].Count);

            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count,
                "Error Second Step not enough changes registered: " + transgenerate.TransitionDictionary.Count);

            //Remove
            transgenerate.DeleteTransition(coordinateEdge);
            if (transgenerate.TransitionDictionary.Count != 8)
            {
                Assert.Fail("Error not enough Tiles: " + transgenerate.TransitionDictionary.Count + " Expected 8");
            }

            Debug.WriteLine("Tiles remaining: " + transgenerate.TransitionDictionary.Count + " Expected 8");
        }

        /// <summary>
        ///     0,1,2,3
        ///     0,  x,x,x,x         0,1,2,3
        ///     1,  x,x,m,x         4,5,6,7
        ///     2,  x,m,x,x         8,9,10,11
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionMiddleSwBigger()
        {
            var masterTile = HelperMethods.InitiateTileDctSwCase();
            var coordinateEdge = new Coordinates(2, 1, 5, 0);

            var transgenerate = new TransitionGenerate(3, 4, masterTile);
            Debug.WriteLine("Initiate");
            transgenerate.AddTransition(coordinateEdge);
            Debug.WriteLine("Add Transitions");
            var tiles = TransitionEngineRegister.GetAllTransitions(0);
            Assert.AreEqual(1, tiles.Count,
                "Test passed Generated the right amount of Tiles:" + tiles.Count + " Expected 1");
            Debug.WriteLine("Transitions " + tiles.Count);

            var cacheOne = transgenerate.TransitionDictionary;

            Debug.WriteLine("One: Transitions Elements " + cacheOne.Count);

            Assert.AreEqual(2, transgenerate.TransitionDictionary.Count,
                "Test passed Generated the right amount of Tiles:" + transgenerate.TransitionDictionary.Count +
                " Expected 2");
        }

        /// <summary>
        ///     0,1,2,          Here we checked 4 and 1
        ///     0,  x,n,x
        ///     1,  x,m,x
        ///     2,  x,x,x
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionRemoveMiddleN()
        {
            var masterTile = HelperMethods.InitiateTileDct();
            var coordinateMidle = new Coordinates(1, 1, 5, 0);
            var coordinateEdge = new Coordinates(1, 0, 5, 0);

            var transgenerate = new TransitionGenerate(3, 3, masterTile);
            Debug.WriteLine("Initiate");
            transgenerate.AddTransition(coordinateMidle);
            Debug.WriteLine("Add Transitions");
            var cacheOne = transgenerate.TransitionDictionary;

            Debug.WriteLine("One: Transitions Elements " + cacheOne.Count);

            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count,
                "Error First Step not enough changes registered " + transgenerate.TransitionDictionary.Count);

            transgenerate.AddTransition(coordinateEdge);
            var cacheTwo = transgenerate.TransitionDictionary;

            Debug.WriteLine("Two: Transitions Elements " + cacheTwo.Count);

            //  012     xxx     x0x     212         Expected: 0,2,3,5 and of course 4 get's one added
            //  345     x0x     xxx     212
            //  678     xxx     xxx     111

            Assert.IsTrue(cacheTwo.ContainsKey(0), "Error in 0");
            Assert.AreEqual(2, cacheTwo[0].Count, "Error in 0");
            Assert.IsTrue(cacheTwo.ContainsKey(1), "Error in 1");
            Debug.WriteLine("1 : " + cacheTwo[1].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(2), "Error in 2");
            Assert.AreEqual(2, cacheTwo[2].Count, "Error in 2");
            Assert.IsTrue(cacheTwo.ContainsKey(3), "Error in 3");
            if (cacheTwo[3].Count != 2)
            {
                Assert.Fail("Error in 3");
            }

            Assert.IsTrue(cacheTwo.ContainsKey(4), "Error in 4");
            Debug.WriteLine("4 : " + cacheTwo[4].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(5), "Error in 5");
            Assert.AreEqual(2, cacheTwo[5].Count, "Error in 5");
            Assert.IsTrue(cacheTwo.ContainsKey(6), "Error in 6");
            Debug.WriteLine("6 : " + cacheTwo[6].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(7), "Error in 7");
            Debug.WriteLine("7 : " + cacheTwo[7].Count);
            Assert.IsTrue(cacheTwo.ContainsKey(8), "Error in 8");
            Debug.WriteLine("8 : " + cacheTwo[8].Count);

            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count,
                "Error Second Step not enough changes registered: " + transgenerate.TransitionDictionary.Count);

            //Remove
            transgenerate.DeleteTransition(coordinateEdge);

            Assert.AreEqual(8, transgenerate.TransitionDictionary.Count,
                "Error not enough Tiles: " + transgenerate.TransitionDictionary.Count);
        }

        /// <summary>
        ///     Just Multi Terrain Tiles no other Tiles Involved
        ///     Check if Loading works
        ///     0,1,2,3
        ///     0,  x,x,x,x         0,1,2,3
        ///     1,  m,m,m,x         4,5,6,7
        ///     2,  x,m,x,x         8,9,10,11
        /// </summary>
        [TestMethod]
        public void TransitionLoadMultiTerrain()
        {
            var masterTile = HelperMethods.InitiateMultiTileDct();

            //  Map:        Transitions:     Directions     Directions
            //  0,1,2,3     x,x,x,x          812            x,x,x,x
            //  4,5,6,7     0,1,2,x          7x3            0,3,3,x
            //  8,9,10,11   x,4,x,x          654            x,6,x,x
            var transgenerate = new TransitionGenerate(3, 4, masterTile);

            var coordinate = new Coordinates(0, 1, 5, 0);

            transgenerate.AddTransition(coordinate);

            Assert.AreEqual(4, transgenerate.TransitionDictionary.Count,
                "Test passed Generated the right amount of Tiles: " + transgenerate.TransitionDictionary.Count);
            var dct = transgenerate.TransitionDictionary;

            transgenerate = new TransitionGenerate(3, 4, HelperMethods.InitiateTileDct(), dct);

            dct = transgenerate.TransitionDictionary;

            Assert.IsTrue(dct.ContainsKey(4), "No Values 4");
            Assert.AreEqual(0, dct[4][0], "Wrong Values 4 " + dct[4][0]);
            Assert.IsTrue(dct.ContainsKey(5), "No Values 5");
            Assert.AreEqual(1, dct[5][0], "Wrong Values 5 " + dct[5][0]);
            Assert.IsTrue(dct.ContainsKey(6), "No Values 6");
            Assert.AreEqual(2, dct[6][0], "Wrong Values 6 " + dct[6][0]);
            Assert.IsTrue(dct.ContainsKey(9), "No Values 9");
            Assert.AreEqual(3, dct[9][0], "Wrong Values 9 " + dct[9][0]);
        }

        /// <summary>
        ///     Just Multi Terrain Tiles no other Tiles Involved
        ///     0,1,2,3
        ///     0,  x,x,x,x         0,1,2,3
        ///     1,  m,m,m,x         4,5,6,7
        ///     2,  x,m,x,x         8,9,10,11
        /// </summary>
        [TestMethod]
        public void TransitionMultiTerrain()
        {
            var masterTile = HelperMethods.InitiateMultiTileDct();

            //  Map:        Transitions:     Directions     Directions
            //  0,1,2,3     x,x,x,x          812            x,x,x,x
            //  4,5,6,7     0,1,2,x          7x3            0,3,3,x
            //  8,9,10,11   x,4,x,x          654            x,6,x,x
            var transgenerate = new TransitionGenerate(3, 4, masterTile);

            var cache = TransitionProcessing.GetAllMultiTransitions(0);

            Debug.WriteLine("Coordinate Done");

            Assert.IsFalse(cache.IsNullOrEmpty(), "Result was Empty");
            Debug.WriteLine("TransitionEngine Done");
            Debug.WriteLine("Tile Count " + cache.Count);

            Assert.AreEqual(4, cache.Count,
                "Test passed Generated the right amount of Tiles: " + cache.Count);

            var coordinate = new Coordinates(0, 1, 5, 0);

            transgenerate.AddTransition(coordinate);

            Assert.AreEqual(4, transgenerate.TransitionDictionary.Count,
                "Test passed Generated the right amount of Tiles: " + transgenerate.TransitionDictionary.Count);
            var dct = transgenerate.TransitionDictionary;

            DebugMessages(transgenerate.TransitionDictionary);

            foreach (var tst in transgenerate.TransitionDictionary)
            {
                Debug.WriteLine(string.Concat("Tile Coordinate ", tst.Key, " Tile Id ", tst.Value[0],
                    " Tile Id Count ", tst.Value.Count));
            }

            Assert.IsTrue(dct.ContainsKey(4), "No Values 4");
            Assert.AreEqual(0, dct[4][0], "Wrong Values 4" + dct[4][0]);
            Assert.IsTrue(dct.ContainsKey(5), "No Values 5");
            Assert.AreEqual(1, dct[5][0], "Wrong Values 5 " + dct[5][0]);
            Assert.IsTrue(dct.ContainsKey(6), "No Values 6");
            Assert.AreEqual(2, dct[6][0], "Wrong Values 6" + dct[6][0]);
            Assert.IsTrue(dct.ContainsKey(9), "No Values 9");
            Assert.AreEqual(3, dct[9][0], "Wrong Values 9" + dct[9][0]);
        }

        /// <summary>
        ///     Just Multi Terrain Tiles no other Tiles Involved
        ///     0,1,2,3
        ///     0,  x,x,x,x         0,1,2,3
        ///     1,  m,m,m,x         4,5,6,7
        ///     2,  x,m,x,x         8,9,10,11
        /// </summary>
        [TestMethod]
        public void TransitionDeleteMultiTerrain()
        {
            //Initiate
            var masterTile = TransitionEngineRegister.TileDct = HelperMethods.InitiateMultiTileDct();

            var cache = TransitionProcessing.GetAllMultiTransitions(0);

            if (cache.IsNullOrEmpty())
            {
                Assert.Fail("Result was Empty");
            }

            Assert.IsTrue(cache.Count == 4,
                "Test passed Generated the right amount of Tiles: " + cache.Count);

            //First Generate MultiTile and Delete

            //  Map:        Transitions:     Directions     Directions
            //  0,1,2,3     0,1,2,x          812            0,3,3,x
            //  4,5,6,7     x,5,x,x          7x3            x,6,x,x
            //  8,9,10,11   x,x,x,x          654            x,x,x,x
            var transgenerate = new TransitionGenerate(3, 4, masterTile);

            var coordinate = new Coordinates(0, 0, 5, 0);

            transgenerate.AddTransition(coordinate);

            Assert.AreEqual(4, transgenerate.TransitionDictionary.Count,
                "Test passed Generated the right amount of Tiles: " + transgenerate.TransitionDictionary.Count);
            var dct = transgenerate.TransitionDictionary;

            Assert.IsTrue(dct.ContainsKey(0), "No Values 0");
            Assert.AreEqual(0, dct[0][0], "Wrong Values 0 " + dct[0][0]);
            Assert.IsTrue(dct.ContainsKey(1), "No Values 1");
            Assert.AreEqual(1, dct[1][0], "Wrong Values 1 " + dct[1][0]);
            Assert.IsTrue(dct.ContainsKey(2), "No Values 2");
            Assert.AreEqual(2, dct[2][0], "Wrong Values 2 " + dct[2][0]);
            Assert.IsTrue(dct.ContainsKey(5), "No Values 5");
            Assert.AreEqual(3, dct[5][0], "Wrong Values 5 " + dct[5][0]);
            var coordinateDelete = new Coordinates(1, 1, 5, 0);
            var del = transgenerate.DeleteTransition(coordinateDelete);

            Debug.WriteLine("Tile Delete");
            foreach (var tst in del)
            {
                Debug.WriteLine("Tile Coordinate: " + tst.Key + " Tile Id " + tst.Value);
            }

            Assert.AreEqual(0, transgenerate.TransitionDictionary.Count,
                "Test passed Delete Tiles: " + transgenerate.TransitionDictionary.Count);

            // Second New Tile and Delete

            coordinate = new Coordinates(0, 1, 5, 0);

            transgenerate.AddTransition(coordinate);

            Assert.AreEqual(4, transgenerate.TransitionDictionary.Count,
                "Test passed Generated the right amount of Tiles: " + transgenerate.TransitionDictionary.Count);

            dct = transgenerate.TransitionDictionary;

            foreach (var tst in transgenerate.TransitionDictionary)
            {
                Debug.WriteLine(string.Concat("Tile Coordinate ", tst.Key, " Tile Id ", tst.Value[0],
                    " Tile Id Count ", tst.Value.Count));
            }

            Assert.IsTrue(dct.ContainsKey(4), "No Values 4");
            Assert.AreEqual(0, dct[4][0], "Wrong Values 4" + dct[4][0]);
            Assert.IsTrue(dct.ContainsKey(5), "No Values 5");
            Assert.AreEqual(1, dct[5][0], "Wrong Values 5" + dct[5][0]);
            Assert.IsTrue(dct.ContainsKey(6), "No Values 6");
            Assert.AreEqual(2, dct[6][0], "Wrong Values 4" + dct[6][0]);
            Assert.IsTrue(dct.ContainsKey(9), "No Values 9");
            Assert.AreEqual(3, dct[9][0], "Wrong Values 9" + dct[9][0]);

            coordinateDelete = new Coordinates(1, 2, 5, 0);
            transgenerate.DeleteTransition(coordinateDelete);

            Assert.IsTrue(transgenerate.TransitionDictionary.Count == 0,
                "Test passed Delete Tiles: " + transgenerate.TransitionDictionary.Count);

            //Try to delete an Empty Multi Tile

            coordinate = new Coordinates(0, 0, 5, 0);

            transgenerate.AddTransition(coordinate);

            Assert.AreEqual(4, transgenerate.TransitionDictionary.Count,
                "Test passed Generated the right amount of Tiles: " + transgenerate.TransitionDictionary.Count);
            dct = transgenerate.TransitionDictionary;

            Assert.IsTrue(dct.ContainsKey(0), "No Values 0");
            Assert.AreEqual(0, dct[0][0], "Wrong Values 0" + dct[0][0]);
            Assert.IsTrue(dct.ContainsKey(1), "No Values 1");
            Assert.AreEqual(1, dct[1][0], "Wrong Values 1" + dct[1][0]);
            Assert.IsTrue(dct.ContainsKey(2), "No Values 2");
            Assert.AreEqual(2, dct[2][0], "Wrong Values 2" + dct[2][0]);
            Assert.IsTrue(dct.ContainsKey(5), "No Values 5");
            Assert.AreEqual(3, dct[5][0], "Wrong Values 5" + dct[5][0]);

            coordinateDelete = new Coordinates(1, 0, 5, 0);
            transgenerate.DeleteTransition(coordinateDelete);

            Assert.IsTrue(transgenerate.TransitionDictionary.Count == 0,
                "Test passed not deleted Tiles: " + transgenerate.TransitionDictionary.Count);
        }

        /// <summary>
        ///     Just Multi Terrain Tiles no other Tiles Involved
        ///     0,1,2,3
        ///     0,  x,x,x,x         0,1,2,3
        ///     1,  m,m,m,x         4,5,6,7
        ///     2,  x,m,x,x         8,9,10,11
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(TransitionException), "Could not Place Multi Terrain Tile")]
        public void TransitionMultiTerrainException()
        {
            var masterTile = HelperMethods.InitiateMultiTileDct();

            //  Map:        Transitions:     Directions     Directions
            //  0,1,2,3     x,x,x,x          812            x,x,x,x
            //  4,5,6,7     0,1,2,x          7x3            0,3,3,x
            //  8,9,10,11   x,4,x,x          654            x,6,x,x
            var transgenerate = new TransitionGenerate(1, 1, masterTile);

            var coordinate = new Coordinates(0, 0, 5, 0);
            transgenerate.AddTransition(coordinate);
        }

        /// <summary>
        ///     Just Multi Terrain Tiles no other Tiles Involved
        ///     Don't overwrite Tiles!
        ///     0,1,2,3
        ///     0,  x,x,x,x         0,1,2,3
        ///     1,  m,m,m,x         4,5,6,7
        ///     2,  x,m,x,x         8,9,10,11
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(TransitionException), "Multi Terrain Tile already in Place")]
        public void TransitionMultiTerrainDonotOverwrite()
        {
            // Initiate
            var masterTile = HelperMethods.InitiateMultiTileDct();

            //  Map:        Transitions:     Directions     Directions
            //  0,1,2,3     x,x,x,x          812            x,x,x,x
            //  4,5,6,7     0,1,2,x          7x3            0,3,3,x
            //  8,9,10,11   x,4,x,x          654            x,6,x,x
            var transgenerate = new TransitionGenerate(3, 4, masterTile);

            var cache = TransitionProcessing.GetAllMultiTransitions(0);

            Debug.WriteLine("Coordinate Done");

            Assert.IsFalse(cache.IsNullOrEmpty(), "Result was Empty");

            Debug.WriteLine("TransitionEngine Done");
            Debug.WriteLine("Tile Count " + cache.Count);

            Assert.AreEqual(4, cache.Count,
                "Test passed Generated the right amount of Tiles: " + cache.Count);

            var coordinate = new Coordinates(0, 1, 5, 0);

            transgenerate.AddTransition(coordinate);

            Assert.AreEqual(4, transgenerate.TransitionDictionary.Count,
                "Test passed Generated the right amount of Tiles: " + transgenerate.TransitionDictionary.Count);
            var dct = transgenerate.TransitionDictionary;

            DebugMessages(transgenerate.TransitionDictionary);

            Assert.IsTrue(dct.ContainsKey(4), "No Values 4");
            Assert.AreEqual(0, dct[4][0], "Wrong Values 4" + dct[4][0]);
            Assert.IsTrue(dct.ContainsKey(5), "No Values 5");
            Assert.AreEqual(1, dct[5][0], "Wrong Values 5" + dct[5][0]);
            Assert.IsTrue(dct.ContainsKey(6), "No Values 6");
            Assert.AreEqual(2, dct[6][0], "Wrong Values 6" + dct[6][0]);
            Assert.IsTrue(dct.ContainsKey(9), "No Values 9");
            Assert.AreEqual(3, dct[9][0], "Wrong Values 9" + dct[9][0]);

            //Now we try to add another Transition and it should fail since they overlap!

            coordinate = new Coordinates(0, 0, 5, 0);

            transgenerate.AddTransition(coordinate);

            Assert.AreEqual(4, transgenerate.TransitionDictionary.Count,
                "Test passed right amount of Tiles: " + transgenerate.TransitionDictionary.Count);
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionGenerationDeleteWithWrongIdLayer()
        {
            var transgenerate = new TransitionGenerate(3, 3, ResourcesLoader.MasterTile);

            var item = new Coordinates(1, 1, 3, 19);

            try
            {
                transgenerate.AddTransition(item);
            }
            catch (TransitionException ex)
            {
                Assert.Fail("Assert: " + ex);
            }

            Debug.WriteLine(transgenerate.TransitionDictionary.Count);

            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count, "Got the wrong amount of Tiles");

            DebugMessages(transgenerate.TransitionDictionary);

            Assert.IsTrue(true, "Passes");

            //Delete
            item = new Coordinates(1, 1, 0, 0);

            transgenerate.DeleteTransition(item);

            Assert.IsTrue(transgenerate.TransitionDictionary.IsNullOrEmpty(),
                "Got the wrong amount of Tiles: " + transgenerate.TransitionDictionary.Count);
        }

        /// <summary>
        ///     012
        ///     345
        ///     678
        ///     NW,N,NE
        ///     S,0,E
        ///     SW,W,SE
        ///     812
        ///     7x3
        ///     654
        /// </summary>
        [TestMethod]
        public void TransitionGenerationDeleteStressTest()
        {
            var transgenerate = new TransitionGenerate(3, 3, ResourcesLoader.MasterTile);

            var item = new Coordinates(1, 1, 3, 19);

            try
            {
                transgenerate.AddTransition(item);
            }
            catch (TransitionException ex)
            {
                Assert.Fail("Assert: " + ex);
            }

            Assert.AreEqual(9, transgenerate.TransitionDictionary.Count,
                "Got the wrong amount of Tiles:" + transgenerate.TransitionDictionary.Count);

            Assert.IsTrue(true, "Passes");

            //Delete

            item = new Coordinates(1, 1, 0, 0);
            transgenerate.DeleteTransition(item);
            item = new Coordinates(0, 0, 0, 0);
            transgenerate.DeleteTransition(item);
            item = new Coordinates(0, 1, 0, 0);
            transgenerate.DeleteTransition(item);
            item = new Coordinates(1, 0, 0, 0);
            transgenerate.DeleteTransition(item);
            item = new Coordinates(1, 2, 0, 0);
            transgenerate.DeleteTransition(item);
            item = new Coordinates(0, 2, 0, 0);
            transgenerate.DeleteTransition(item);
            item = new Coordinates(2, 2, 0, 0);
            transgenerate.DeleteTransition(item);

            Assert.IsTrue(true, "Passes");
        }

        /// <summary>
        ///     The transitions master test.
        ///     0,1,2
        ///     0,  t,t,t         0,1,2
        ///     1,  t,m,t         3,4,5
        ///     2,  t,m,t         6,7,8
        ///     3,  t,t,t         11,12,13
        /// </summary>
        [TestMethod]
        public void TransitionsMaster()
        {
            var transgenerate = new TransitionGenerate(4, 3, ResourcesLoader.MasterTile);
            var item = new Coordinates(1, 1, 3, 19);
            transgenerate.AddTransition(item);
            item = new Coordinates(1, 2, 3, 19);

            transgenerate.AddTransition(item);

            //4*3
            Assert.AreEqual(12, transgenerate.TransitionDictionary.Count,
                "Got the wrong amount of Tiles:" + transgenerate.TransitionDictionary.Count);

            var dct = transgenerate.TransitionDictionary;
            Assert.AreEqual(1, dct[4].Count, "Wrong Values one " + dct[4].Count);
            DebugMessages(dct[4], 4);
            Assert.AreEqual(1, dct[7].Count, "Wrong Values two " + dct[7].Count);
            DebugMessages(dct[7], 7);
            Assert.AreEqual(19, dct[4][0], "Wrong Id one: " + dct[4][0]);
            DebugMessages(dct[4], 4);
            Assert.AreEqual(19, dct[7][0], "Wrong Id one: " + dct[4][0]);
            DebugMessages(dct[7], 7);
        }

        /// <summary>
        ///     The transitions master test.
        ///     Really Small Map
        /// </summary>
        [TestMethod]
        public void TransitionsMasterSmall()
        {
            var transgenerate = new TransitionGenerate(2, 1, ResourcesLoader.MasterTile);
            var item = new Coordinates(0, 0, 3, 19);
            transgenerate.AddTransition(item);
            item = new Coordinates(0, 1, 3, 19);

            transgenerate.AddTransition(item);

            //2*1
            Assert.AreEqual(2, transgenerate.TransitionDictionary.Count,
                "Got the wrong amount of Tiles:" + transgenerate.TransitionDictionary.Count);

            var dct = transgenerate.TransitionDictionary;
            Assert.AreEqual(1, dct[0].Count, "Wrong Values one " + dct[0].Count);
            DebugMessages(dct[0], 4);
            Assert.AreEqual(1, dct[1].Count, "Wrong Values two " + dct[1].Count);
            DebugMessages(dct[1], 7);
            Assert.AreEqual(19, dct[0][0], "Wrong Id two: " + dct[0][0]);
            DebugMessages(dct[0], 4);
            Assert.AreEqual(19, dct[1][0], "Wrong Id two: " + dct[1][0]);
            DebugMessages(dct[1], 7);
        }

        /// <summary>
        ///     Just basic Debug Messages
        /// </summary>
        /// <param name="tiles">Transition Dictionary</param>
        private static void DebugMessages(Dictionary<int, List<int>> tiles)
        {
            foreach (var tst in tiles)
            {
                Debug.WriteLine("Count: " + tst.Value.Count);

                foreach (var element in tst.Value)
                {
                    Debug.WriteLine("Count Elements: " + tst.Value.Count);
                    Debug.WriteLine(string.Concat("Tile Coordinate: ", tst.Key, " Tile Id ", element));
                }
            }
        }

        /// <summary>
        ///     Just basic Debug Messages
        /// </summary>
        /// <param name="list">specific Transition List</param>
        /// <param name="id">Id of Master</param>
        private static void DebugMessages(IReadOnlyCollection<int> list, int id)
        {
            Debug.WriteLine("Id: " + id);
            Debug.WriteLine("Count: " + list.Count);
            foreach (var tst in list)
            {
                Debug.Write("Tile Coordinate: " + tst + " ");
            }
        }
    }
}