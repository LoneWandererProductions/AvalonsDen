/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/HelperMethods.cs
 * PURPOSE:     Basic Shared Tests for Avalon
 * PROGRAMER:   Peter GeinitzWayfarer
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileHandler;
using Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The helper methods class.
    /// </summary>
    internal static class HelperMethods
    {
        /// <summary>
        ///     The compare borders dummy.
        /// </summary>
        /// <param name="borders">The borders.</param>
        /// <param name="compare">The compare.</param>
        /// <returns>The <see cref="bool" />.</returns>
        internal static bool CompareBordersDummy(IEnumerable<string> borders, IEnumerable<string> compare)
        {
            return borders.SequenceEqual(compare);
        }

        /// <summary>
        ///     Gets the Base data from another Folder
        /// </summary>
        /// <returns>Collection of the Data in need</returns>
        [TestMethod]
        public static LoaderContainer GetData()
        {
            var load = new LoaderContainer();

            var path = Path.Combine(DirectoryInformation.GetParentDirectory(3), ResourcesGeneral.Path);

            load.MasterBordersDictionary =
                WorkLoader.LoadTileBordersDct(Path.Combine(path, ResourcesGeneral.MasterBorderDct));
            load.MasterTileDictionary =
                WorkLoader.LoadTileDct(Path.Combine(path, ResourcesGeneral.MasterTileDct));

            Assert.IsNotNull(load.MasterTileDictionary, "Master Tile Dictionary was null");
            Assert.IsNotNull(load.MasterBordersDictionary, "Master Border was null");
            return load;
        }

        /// <summary>
        ///     The generate one tile dct.
        /// </summary>
        /// <param name="tileId">The tileId.</param>
        /// <returns>The <see cref="T:List{Coordinates}" />.</returns>
        internal static List<Coordinates> GenerateOneTileDct(int tileId)
        {
            var coordinate = new Coordinates
            {
                XRow = 1,
                YColumn = 1,
                ZLayer = 1,
                TileId = tileId
            };
            var one = new List<Coordinates> { coordinate };
            return one;
        }

        /// <summary>
        ///     Helper Method to Convert Border into a clean displayable String
        /// </summary>
        /// <param name="borders">Borders as Lust String</param>
        /// <returns>Borders as String</returns>
        public static string GenerateStringFromList(IEnumerable<string> borders)
        {
            return borders.Aggregate(string.Empty, (current, line) => current + line + Environment.NewLine);
        }

        /// <summary>
        ///     Helper Method to generate Custom TileDictionary
        /// </summary>
        /// <returns> Custom TileDictionary</returns>
        internal static Dictionary<int, Tile> InitiateMultiTileDct()
        {
            return new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMasterMt },
                { 1, ResourcesTransitions.TileNmt1 },
                { 2, ResourcesTransitions.TileNmt2 },
                { 3, ResourcesTransitions.TileEmt }
            };
        }

        /// <summary>
        ///     Helper Method to generate Custom TileDictionary
        /// </summary>
        /// <returns> Custom TileDictionary</returns>
        internal static Dictionary<int, Tile> InitiateTileDct()
        {
            return new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 1, ResourcesTransitions.TileN },
                { 2, ResourcesTransitions.TileNe },
                { 3, ResourcesTransitions.TileE },
                { 4, ResourcesTransitions.TileSe },
                { 5, ResourcesTransitions.TileW },
                { 6, ResourcesTransitions.TileSw },
                { 7, ResourcesTransitions.TileS },
                { 8, ResourcesTransitions.TileNw }
            };
        }

        /// <summary>
        ///     Helper Method to generate Custom TileDictionary
        /// </summary>
        /// <returns> Custom TileDictionary</returns>
        internal static Dictionary<int, Tile> InitiateTileDctSwCase()
        {
            return new Dictionary<int, Tile>
            {
                { 0, ResourcesTransitions.TileMaster },
                { 6, ResourcesTransitions.TileSw }
            };
        }
    }
}