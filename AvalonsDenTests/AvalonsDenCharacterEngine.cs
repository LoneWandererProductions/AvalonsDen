/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenCharacterEngine.cs
 * PURPOSE:     Basic Tests for Avalon Character Engine
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Diagnostics;
using System.IO;
using AvalonRuntime;
using CharacterEngine;
using FileHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializer;

namespace AvalonsDenTests
{
    /// <summary>
    ///     Basic Event Tests not complete yet
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenCharacterEngine
    {
        /// <summary>
        ///     The char one (const). Value: "one".
        /// </summary>
        private const string CharOne = "one";

        /// <summary>
        ///     The character path (const). Value: "Character".
        /// </summary>
        private const string CharacterPath = "Character";

        /// <summary>
        ///     The character file (const). Value: "tst.cdf".
        /// </summary>
        private const string CharacterFile = "tst.cdf";

        /// <summary>
        ///     Returns Standard Path of Character
        ///     Path: \"root"\
        /// </summary>
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), ResourcesGeneral.CampaignsFolder);

        /// <summary>
        ///     Test Reading from XML
        /// </summary>
        [TestMethod]
        public void ReadIdFromXml()
        {
            CharacterProcessing.SaveCharacterBaseStats(ResourcesGeneral.Stats, Path.Combine(_path, CharacterFile));

            var level = XmlTools.GetFirstAttributeFromXml(Path.Combine(_path, CharacterFile), "Level");

            //Display
            Assert.AreEqual("1", level, "Correct Display");
        }

        /// <summary>
        ///     Load the character files.
        /// </summary>
        [TestMethod]
        public void LoadCharacterFiles()
        {
            var path = Path.Combine(_path, ResourcesGeneral.CampaignName);
            var biographyChanged = Path.Combine(path, ResourcesGeneral.AutoSave,
                Path.ChangeExtension(CharacterFile, ArtConst.CharacterBiographyExt));

            var biographyUnchanged = Path.Combine(path, CharacterPath,
                Path.ChangeExtension(CharOne, ArtConst.CharacterBiographyExt));

            var statsUnchanged = Path.Combine(_path, ResourcesGeneral.CampaignName, CharacterPath,
                Path.ChangeExtension(CharacterFile, ArtConst.CharacterStatsExt));

            Debug.WriteLine(biographyChanged);
            CharacterProcessing.SaveCharacterBiography(ResourcesGeneral.Biography, biographyChanged);
            Debug.WriteLine(biographyUnchanged);
            CharacterProcessing.SaveCharacterBiography(ResourcesGeneral.BiographyTwo, biographyUnchanged);
            Debug.WriteLine(statsUnchanged);
            CharacterProcessing.SaveCharacterBaseStats(ResourcesGeneral.Stats, statsUnchanged);

            var check = FileHandleSearch.FileExists(biographyChanged);

            //Display
            Assert.IsTrue(check, "Biography not created");

            var cache = CharacterProcessing.CollectMasterData(ResourcesGeneral.CampaignName);

            Assert.AreEqual(2, cache.Count, "Biography not Collected");
            Assert.AreEqual(1, cache[1].Id, "Wrong id");
            Assert.IsTrue(cache[1].Npc, "Wrong Npc");
            Assert.AreEqual(Path.Combine(_path, @"CampaignNameTest\Character\tst.acs"), cache[1].CharacterStatsPath,
                "Stats were not correct");
            Assert.AreEqual(2, cache[2].Id, "Wrong id");
            Assert.IsNull(cache[2].CharacterStatsPath, "Stats were not correct");

            FileHandleDelete.DeleteCompleteFolder(_path);
        }
    }
}