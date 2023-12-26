/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenCampaignLoader.cs
 * PURPOSE:     Test and Load Resources, backup for basic Resources
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.IO;
using Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;
using Serializer;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The Avalons Den Campaign loader unit test
    ///     Backup for Master Lists!
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenCampaignLoader
    {
        /// <summary>
        ///     The path (read only). Value: Path.Combine(Directory.GetCurrentDirectory(), ResourcesGeneral.CampaignsFolder).
        /// </summary>
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), ResourcesGeneral.CampaignsFolder);

        /// <summary>
        ///     Load Master Tile Dictionary
        ///     Backup Solution As well.
        /// </summary>
        [TestMethod]
        public void LoadTileDct()
        {
            Serialize.SaveDctObjectToXml(ResourcesLoader.MasterTile,
                Path.Combine(_path, ResourcesGeneral.MasterTileDct));

            var dic =
                DeSerialize.LoadDictionaryFromXml<int, Tile>(Path.Combine(_path,
                    ResourcesGeneral.MasterTileDct));

            Assert.IsTrue(dic.Count == 28,
                "Successful Serialized and Deserialized MasterTile Dictionary, Count: " + dic.Count);
        }

        /// <summary>
        ///     Load Master Borders Dictionary
        ///     Backup Solution As well.
        /// </summary>
        [TestMethod]
        public void LoadTileBorders()
        {
            Serialize.SaveDctObjectToXml(ResourcesLoader.MasterBorder,
                Path.Combine(_path, ResourcesGeneral.MasterBorderDct));

            var dic =
                DeSerialize.LoadDictionaryFromXml<int, TileBorders>(Path.Combine(_path,
                    ResourcesGeneral.MasterBorderDct));

            Assert.IsTrue(dic.Count == 12,
                "Successful Serialized and Deserialized MasterTile Dictionary, Count: " + dic.Count);
        }

        /// <summary>
        ///     Just a bunch of tests to check if we get the right paths for Campaign
        /// </summary>
        [TestMethod]
        public void GeneratePathForCampaign()
        {
            var check = false;

            var path = LoadHelper.GetCampaignpath(ResourcesGeneral.CampaignName,
                ResourcesGeneral.MapName);

            var expectedPath = Path.Combine(_path,
                ResourcesGeneral.CampaignName,
                ResourcesGeneral.MapName);

            if (path == expectedPath) check = true;

            Assert.IsTrue(check,
                "Path Created: " + path + Environment.NewLine + "Path expected: " + expectedPath +
                Environment.NewLine);
        }
    }
}