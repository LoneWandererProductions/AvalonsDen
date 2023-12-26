/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/Serializer.cs
 * PURPOSE:     Test for AvalonsDenSerializer
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using FileHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;
using Serializer;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The Avalon's Den serializer unit test class.
    /// </summary>
    [TestClass]
    public sealed class Serializer
    {
        /// <summary>
        ///     Check if our basic functions do Work out
        /// </summary>
        [TestMethod]
        public void LoadObjectFromXml()
        {
            var maps = new MapObject
            {
                MapList = new List<SerializeableKeyValuePair.KeyValuePair<int, int>>
                {
                    new(1, 1),
                    new(2, 2)
                }
            };

            var path = Path.Combine(Directory.GetCurrentDirectory(), nameof(LoadObjectFromXml));

            Serialize.SaveObjectToXml(maps, path);

            var test = DeSerialize.LoadObjectFromXml<MapObject>(path);

            Assert.AreEqual(2, test.MapList.Count, "Wrong amount of elements");

            var check = ComparePair(test.MapList);
            Assert.IsTrue(check, "Test failed no changes");

            FileHandleDelete.DeleteFile(path);
        }

        /// <summary>
        ///     Load the Dictionary from XML.
        /// </summary>
        [TestMethod]
        public void LoadDctFromXml()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), nameof(LoadDctFromXml));

            Serialize.SaveDctObjectToXml(ResourcesLoader.MasterTile,
                Path.Combine(path, ResourcesGeneral.MasterTileDct));

            var target = Path.Combine(Directory.GetCurrentDirectory(), nameof(LoadDctFromXml),
                ResourcesGeneral.MasterTileDct);

            var dic = DeSerialize.LoadDictionaryFromXml<int, Tile>(target);

            Assert.IsTrue(dic.Count == 28,
                "Successful Serialized and Deserialized MasterTile Dictionary, Count: " + dic.Count);

            FileHandleDelete.DeleteFile(Directory.GetCurrentDirectory());
        }

        /// <summary>
        ///     Check if our XmlTools works
        /// </summary>
        [TestMethod]
        public void XmlToolTests()
        {
            var map = new MapObject
            {
                MapName = "test"
            };

            var path = Path.Combine(Directory.GetCurrentDirectory(), ResourcesGeneral.CampaignsFolder, "tst.cdf");

            Serialize.SaveObjectToXml(map, path);

            var cache = XmlTools.GetFirstAttributeFromXml(path, "MapName");

            //Display
            Assert.AreEqual("test", cache, "Correct Display");

            FileHandleDelete.DeleteFile(path);
        }

        /// <summary>
        ///     Helper Method
        /// </summary>
        /// <param name="mapDictionary">Map Dictionary</param>
        /// <returns>Check if they are equal</returns>
        private static bool ComparePair(IReadOnlyList<SerializeableKeyValuePair.KeyValuePair<int, int>> mapDictionary)
        {
            var one = mapDictionary[0];
            var two = mapDictionary[1];
            if (one.Key != 1 && one.Value != 1) return false;

            return two.Key == 2 || two.Value == 2;
        }
    }
}