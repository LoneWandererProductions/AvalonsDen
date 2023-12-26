/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenSaving.cs
 * PURPOSE:     Basic Tests for Save Operations
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.IO;
using AvalonRuntime;
using EventEngine;
using ExtendedSystemObjects;
using FileHandler;
using Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;
using Serializer;

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     The avalons den saving unit test class.
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenSaving
    {
        /// <summary>
        ///     "root"\SaveFiles
        /// </summary>
        private static readonly string Save = System.IO.Path.Combine(Directory.GetCurrentDirectory(),
            ResourcesGeneral.SavePath);

        /// <summary>
        ///     "root"\Content\Campaigns\
        /// </summary>
        private static readonly string Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(),
            ResourcesGeneral.CampaignsFolder);

        /// <summary>
        ///     The handle.
        /// </summary>
        private SaveGameHandle _handle;

        /// <summary>
        ///     Check for creating a Save File and if we move the Inventory File
        /// </summary>
        [TestMethod]
        public void SaveGame()
        {
            //Delete Target Folder
            var targetPath = System.IO.Path.Combine(
                Directory.GetCurrentDirectory(),
                ResourcesGeneral.SavePath,
                ResourcesGeneral.TestSave);
            FileHandleDelete.DeleteCompleteFolder(targetPath);

            //Actual test we are doing
            var save = new SaveInfos
            {
                CampaignName = ResourcesGeneral.CampaignNmeSave,
                MapName = ResourcesGeneral.MapName,
                SaveName = ResourcesGeneral.TestSave
            };

            var root = System.IO.Path.Combine(ResourcesGeneral.CampaignsFolder, ResourcesGeneral.CampaignNmeSave);
            var autosavePath = System.IO.Path.Combine(root, ResourcesGeneral.AutoSave);
            //Create an Inventory File
            WorkLoader.SaveInventory(new PartyInventory(), ResourcesGeneral.CampaignNmeSave);
            //just a test file in the AutoSave Path, the file is not correct put for the test it is enough
            Serialize.SaveObjectToXml(new SaveInfos(), System.IO.Path.Combine(autosavePath, ResourcesGeneral.TestSave));

            _handle = new SaveGameHandle();
            var check = _handle.CreateSaveFile(save);

            Assert.IsTrue(check, "Didn't create a Save File and the Inventory File");

            //check if something is in the Folder
            Assert.AreEqual(true, FileHandleSearch.CheckIfFolderContainsElement(targetPath),
                "File Copied: " + targetPath);

            //Delete Target Folder
            FileHandleDelete.DeleteCompleteFolder(targetPath);
            //delete Source Folder
            FileHandleDelete.DeleteCompleteFolder(autosavePath);
            //Delete Save File
            FileHandleDelete.DeleteFile(System.IO.Path.Combine(targetPath,
                System.IO.Path.ChangeExtension(targetPath, ResourcesGeneral.SaveExt)));
        }

        /// <summary>
        ///     Check for Loading a Save File
        ///     Todo Not a complete Test, just a basic one, could be improved with Event Handling
        ///     Structure of Saves.
        ///     Temp File:
        ///     Inventory Files
        ///     Map Folders
        ///     Changed Event Files
        /// </summary>
        [TestMethod]
        public void LoadGame()
        {
            //Actual test we are doing
            var save = new SaveInfos
            {
                CampaignName = ResourcesGeneral.CampaignNameLoad,
                MapName = ResourcesGeneral.MapName,
                SaveName = ResourcesGeneral.TestSave
            };

            var root = System.IO.Path.Combine(ResourcesGeneral.CampaignsFolder, ResourcesGeneral.CampaignNameLoad);

            //generates the Manifest and some Inventory Items
            EditorSave.SaveCampaign(System.IO.Path.Combine(root, ResourcesGeneral.CampaignManifest),
                new CampaignManifest(), new PartyInventory());

            //generate an AutoSave Folder
            var dct = new Dictionary<int, EventType>
            {
                {
                    1,
                    new EventType
                    {
                        CoordinatesId = 1,
                        TypeOfEvent = EventType.TypeOfEvents.Interaction
                    }
                }
            };

            var eve = new EventOutput();
            eve.SetAutosave(ResourcesGeneral.CampaignNameLoad, ResourcesGeneral.MapName, new PartyInventory());
            SaveHandleProcessing.Autosave(dct, ResourcesGeneral.CampaignNameLoad, ResourcesGeneral.MapName);

            _handle = new SaveGameHandle();
            var check = _handle.CreateSaveFile(save);

            Assert.IsTrue(check, "Didn't Create a Save File");

            var targetPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),
                ResourcesGeneral.SavePath,
                ResourcesGeneral.TestSave);

            //check if something is in the Folder
            Assert.AreEqual(true,
                FileHandleSearch.CheckIfFolderContainsElement(System.IO.Path.Combine(targetPath,
                    ResourcesGeneral.MapName)),
                "File Copied: " + targetPath);

            Assert.AreEqual(true,
                FileHandleSearch.CheckIfFolderContainsElement(System.IO.Path.Combine(targetPath, "Temp")),
                "File Copied: " + targetPath);

            //cleanup
            FileHandleDelete.DeleteCompleteFolder(targetPath);
        }

        /// <summary>
        ///     Check for deletion of a Save File
        ///     Mostly used LoaderSaveFile
        /// </summary>
        [TestMethod]
        public void DeleteSaveGame()
        {
            //Standard Path
            var expectedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),
                ResourcesGeneral.SavePath,
                ResourcesGeneral.TestSave,
                ResourcesGeneral.CampaignNameDel,
                ResourcesGeneral.MapName);

            //Actual test we are doing
            var save = new SaveInfos
            {
                CampaignName = ResourcesGeneral.CampaignNameDel,
                MapName = ResourcesGeneral.MapName,
                SaveName = ResourcesGeneral.TestSave
            };

            //Standard Path
            var autosavePath = System.IO.Path.Combine(Path,
                ResourcesGeneral.CampaignNameDel,
                ResourcesGeneral.AutoSave,
                ResourcesGeneral.MapName);

            //save Folder
            var target = LoadHelper.CreateSaveFolder(ResourcesGeneral.TestSave, save.CampaignName, save.MapName);

            // Check Save Folder
            Assert.AreEqual(target, expectedPath,
                "Save Folder was not created right: " + target + Environment.NewLine + " Created: " + Save +
                Environment.NewLine);

            //create dummy files
            SharedTools.CreateFiles(autosavePath, ResourcesGeneral.FileExtList);

            //cleanup
            _handle = new SaveGameHandle();
            _handle.DeleteSaveFile(ResourcesGeneral.TestSave);

            //check if the Save Folder is Empty
            Assert.AreEqual(false, FileHandleSearch.CheckIfFolderContainsElement(target),
                "File were not deleted:" + Environment.NewLine + " Target Path: " + target);

            Assert.IsTrue(Directory.Exists(Save), "Folder cleaned");
        }

        /// <summary>
        ///     Check for deletion of a Save File
        ///     Mostly used LoaderSaveFile
        /// </summary>
        [TestMethod]
        public void GetSaveGames()
        {
            //Preparations
            var expectedPath = System.IO.Path.Combine(Path,
                ResourcesGeneral.CampaignName,
                ResourcesGeneral.AutoSave,
                ResourcesGeneral.MapName);

            //create dummy files
            SharedTools.CreateFiles(expectedPath,
                ArtConst.FileExtList);

            //Actual test we are doing
            var save = new SaveInfos
            {
                CampaignName = ResourcesGeneral.CampaignName,
                MapName = ResourcesGeneral.MapName,
                SaveName = ResourcesGeneral.TestSave
            };

            var root = System.IO.Path.Combine(ResourcesGeneral.CampaignsFolder, ResourcesGeneral.CampaignName);
            var autosavePath = System.IO.Path.Combine(root, ResourcesGeneral.AutoSave);
            //Create an Inventory File
            WorkLoader.SaveInventory(new PartyInventory(), ResourcesGeneral.CampaignName);
            //just a test file in the AutoSave Path, the file is not correct put for the test it is enough
            Serialize.SaveObjectToXml(new SaveInfos(),
                System.IO.Path.Combine(autosavePath, ResourcesGeneral.CampaignName));

            _handle = new SaveGameHandle();
            var check = _handle.CreateSaveFile(save);

            Assert.IsTrue(check, "Didn't create a Save File");

            var list = _handle.GetSaveFiles();

            Assert.IsFalse(list.IsNullOrEmpty(), "File Number was incorrect " + list.Count + Environment.NewLine);

            Assert.AreEqual(list[0], ResourcesGeneral.TestSave, "File Name was incorrect " + list[0]);

            //cleanup
            _handle.DeleteSaveFile(ResourcesGeneral.TestSave);
        }
    }
}