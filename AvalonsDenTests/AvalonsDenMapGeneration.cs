/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenMapGeneration.cs
 * PURPOSE:     Basic Tests for Avalon
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using AvalonRuntime;
using MapGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;

namespace AvalonsDenTests
{
    /// <summary>
    ///     Basic Border Checks
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenMapGeneration
    {
        /// <summary>
        ///     The map (readonly). Value: new EditorMapEngine().
        /// </summary>
        private readonly EditorMapEngine _map = new();

        /// <summary>
        ///     Simple Check if Dummies are created correct
        /// </summary>
        [TestMethod]
        public void GenerateDummyBorder1X1()
        {
            var map = _map.Generate(1, 1);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border1X1);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 1x1 " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     Simple Check if Dummies are created correct
        /// </summary>
        [TestMethod]
        public void GenerateDummyBorder2X2()
        {
            var map = _map.Generate(2, 2);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border2X2);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 2x2 " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     Simple Check if Dummies are created correct
        /// </summary>
        [TestMethod]
        public void GenerateDummyBorder3X2()
        {
            var map = _map.Generate(3, 2);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border3X2);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 3x2 " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     Simple Check if Dummies are created correct
        /// </summary>
        [TestMethod]
        public void GenerateDummyBorder3X3()
        {
            var map = _map.Generate(3, 3);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border3X3);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 3x3 " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     Simple Check if Dummies are created correct
        /// </summary>
        [TestMethod]
        public void GenerateDummyBorder4X3()
        {
            var map = _map.Generate(4, 3);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X3);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 4x3 " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     Simple Check if Dummies are created correct
        /// </summary>
        [TestMethod]
        public void GenerateDummyBorder3X1()
        {
            var map = _map.Generate(3, 1);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border3X1);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 3x1 " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     Simple Check if Dummies are created correct
        /// </summary>
        [TestMethod]
        public void GenerateDummyBorder1X3()
        {
            var map = _map.Generate(1, 3);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border1X3);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 1x3 " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     Simple Check if Dummies are created correct
        /// </summary>
        [TestMethod]
        public void GenerateDummyBorder20X12()
        {
            var map = _map.Generate(20, 12);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border20X12);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 20X12 " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     The change map one tile Ne.
        /// </summary>
        [TestMethod]
        public void ChangeMapOneTileNe()
        {
            var map = _map.Generate(4, 4);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X4);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 4x4 " + Environment.NewLine + vector);

            //Load Dictionary
            var load = HelperMethods.GetData();
            //Get Tile List
            var oneTile = HelperMethods.GenerateOneTileDct(10);

            map = _map.ChangeMap(oneTile, map, load.MasterBordersDictionary, load.MasterTileDictionary);

            check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X4ChangedOneNe);

            vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            if (!check)
            {
                foreach (var border in map.Borders) Debug.WriteLine(border);

                Debug.WriteLine("Compared to:");
                foreach (var border in ResourcesMapGeneration.Border4X4ChangedOneNe) Debug.WriteLine(border);
            }

            Assert.IsTrue(check, "Test passed Dummy One Tile Change " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     The change map one tile Se.
        /// </summary>
        [TestMethod]
        public void ChangeMapOneTileSe()
        {
            var map = _map.Generate(4, 4);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X4);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 4x4 " + Environment.NewLine + vector);

            //Load Dictionary
            var load = HelperMethods.GetData();
            //Get Tile List
            var oneTile = HelperMethods.GenerateOneTileDct(12);

            map = _map.ChangeMap(oneTile, map, load.MasterBordersDictionary, load.MasterTileDictionary);

            check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X4ChangedOneSe);

            vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            if (!check)
            {
                foreach (var border in map.Borders) Debug.WriteLine(border);

                Debug.WriteLine("Compared to:");
                foreach (var border in ResourcesMapGeneration.Border4X4ChangedOneSe) Debug.WriteLine(border);
            }

            Assert.IsTrue(check, "Test passed Dummy One Tile Change " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     The change map one tile Sw.
        /// </summary>
        [TestMethod]
        public void ChangeMapOneTileSw()
        {
            var map = _map.Generate(4, 4);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X4);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 4x4 " + Environment.NewLine + vector);

            //Load Dictionary
            var load = HelperMethods.GetData();
            //Get Tile List
            var oneTile = HelperMethods.GenerateOneTileDct(13);

            map = _map.ChangeMap(oneTile, map, load.MasterBordersDictionary, load.MasterTileDictionary);

            check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X4ChangedOneSw);

            vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            if (!check)
            {
                foreach (var border in map.Borders) Debug.WriteLine(border);

                Debug.WriteLine("Compared to:");
                foreach (var border in ResourcesMapGeneration.Border4X4ChangedOneSw) Debug.WriteLine(border);
            }

            Assert.IsTrue(check, "Test passed Dummy One Tile Change " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     The change map one tile Nw.
        /// </summary>
        [TestMethod]
        public void ChangeMapOneTileNw()
        {
            var map = _map.Generate(4, 4);

            var check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X4);
            var vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            Assert.IsTrue(check, "Test passed Dummy Map 4x4 " + Environment.NewLine + vector);

            //Load Dictionary
            var load = HelperMethods.GetData();
            //Get Tile List
            var oneTile = HelperMethods.GenerateOneTileDct(11);

            map = _map.ChangeMap(oneTile, map, load.MasterBordersDictionary, load.MasterTileDictionary);

            check = HelperMethods.CompareBordersDummy(map.Borders, ResourcesMapGeneration.Border4X4ChangedOneNw);

            vector = string.Empty;
            if (!check) vector = HelperMethods.GenerateStringFromList(map.Borders);

            if (!check)
            {
                foreach (var border in map.Borders) Debug.WriteLine(border);

                Debug.WriteLine("Compared to:");
                foreach (var border in ResourcesMapGeneration.Border4X4ChangedOneNw) Debug.WriteLine(border);
            }

            Assert.IsTrue(check, "Test passed Dummy One Tile Change " + Environment.NewLine + vector);
        }

        /// <summary>
        ///     Complex Borders
        ///     Map:
        ///     0,1,2,3
        ///     4,5,6,7
        ///     8,9,10,11
        /// </summary>
        [TestMethod]
        public void ComplexBorderChecks()
        {
            var map = _map.Generate(4, 4);
            //Load Dictionary
            var load = HelperMethods.GetData();
            //Get Tile List

            var one = ArtShared.IdToCoordinate(5, 4, 3, 13);
            var two = ArtShared.IdToCoordinate(6, 4, 3, 8);

            var oneTile = new List<Coordinates> {one, two};

            var oneM = HelperMethods.GenerateStringFromList(map.Borders);
            Debug.WriteLine(oneM);

            map = _map.ChangeMap(oneTile, map, load.MasterBordersDictionary, load.MasterTileDictionary);

            var twoM = HelperMethods.GenerateStringFromList(map.Borders);
            Debug.WriteLine(twoM);

            var equalString = "1|1|1|1|1|1|1|1|1|1|1|1" + Environment.NewLine +
                              "1|0|0|0|0|0|0|0|0|0|0|1" + Environment.NewLine +
                              "1|0|1|1|0|1|1|0|1|1|0|1" + Environment.NewLine +
                              "1|0|1|1|0|1|1|0|1|1|0|1" + Environment.NewLine +
                              "1|0|0|1|0|0|0|0|0|0|0|1" + Environment.NewLine +
                              "1|0|1|1|1|1|1|1|1|1|0|1" + Environment.NewLine +
                              "1|0|1|1|0|1|1|0|1|1|0|1" + Environment.NewLine +
                              "1|0|0|0|0|0|0|0|0|0|0|1" + Environment.NewLine +
                              "1|0|1|1|0|1|1|0|1|1|0|1" + Environment.NewLine +
                              "1|0|1|1|0|1|1|0|1|1|0|1" + Environment.NewLine +
                              "1|0|0|0|0|0|0|0|0|0|0|1" + Environment.NewLine +
                              "1|1|1|1|1|1|1|1|1|1|1|1" + Environment.NewLine;

            Assert.IsTrue(string.Equals(twoM, equalString, StringComparison.OrdinalIgnoreCase), "Test");
        }
    }
}