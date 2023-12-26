/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTestsCampaign/AvalonsDenCampaigns.cs
 * PURPOSE:     Test specific Methods in Campaigns
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using AvalonRuntime;
using CampaignDriver;
using Campaigns;
using FileHandler;
using Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;
using Serializer;

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     The Avalon's Den campaigns unit test class.
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenCampaigns
    {
        /// <summary>
        ///     The campaign name (const). Value: "MapChange".
        /// </summary>
        private const string CampaignName = "MapChange";

        /// <summary>
        ///     The campaign name save (const). Value: "SaveChange".
        /// </summary>
        private const string CampaignNameSave = "SaveChange";

        /// <summary>
        ///     The start map (const). Value: "Start".
        /// </summary>
        private const string StartMap = "Start";

        /// <summary>
        ///     The target map (const). Value: "End".
        /// </summary>
        private const string TargetMap = "End";

        /// <summary>
        ///     The standard folder
        /// </summary>
        private static readonly string StandardFolder = Path.Combine(Directory.GetCurrentDirectory(),
            ResourcesGeneral.CoreCampaign);

        /// <summary>
        ///     The standard folder + Campaign Folder
        /// </summary>
        private static readonly string RootCampaign = Path.Combine(StandardFolder, CampaignName);

        /// <summary>
        ///     The standard folder + Campaign Folder
        /// </summary>
        private static readonly string RootCampaignSave = Path.Combine(StandardFolder, CampaignNameSave);

        /// <summary>
        ///     Test MapChange and especial the ExecuteMapChange function
        ///     Could be improved and extended!
        /// </summary>
        [TestMethod]
        public void CampaignsProcessingExecuteMapChange()
        {
            //Clean up afterwards
            FileHandleDelete.DeleteCompleteFolder(RootCampaign);

            //Get Basic Files like MasterTile Dictionary and e.g. Borders
            var load = HelperMethods.GetData();
            FileHandleCreate.CreateFolder(RootCampaign);

            GenerateCoordinatesId(RootCampaign, StartMap);
            GenerateEventTypeExtension(RootCampaign, StartMap);

            //Create basic crap for the new Map Target
            GetMap(RootCampaign, TargetMap);

            //Create basic crap for the new Map Start
            var startMap = GetMap(RootCampaign, StartMap);

            var path = Path.Combine(RootCampaign, StartMap);

            var check = File.Exists(Path.ChangeExtension(path, ArtConst.EventTypeExtensionExt));

            Assert.IsTrue(check,
                "File not created (EventTypeExtension) as: " +
                Path.ChangeExtension(path, ArtConst.EventTypeExtensionExt));

            check = File.Exists(Path.ChangeExtension(path, ArtConst.MapExt));

            Assert.IsTrue(check,
                "File not created (Map) as: " + Path.ChangeExtension(path, ArtConst.MapExt));

            check = File.Exists(Path.ChangeExtension(path, ArtConst.CoordinatesIdExt));

            Assert.IsTrue(check,
                "File not created (CoordinatesIdExt) as: " + Path.ChangeExtension(path, ArtConst.CoordinatesIdExt));

            //Doing of the actual stuff, initiate Basics
            CampaignsRegister.SetRegister(null, startMap, null, CampaignName, load.MasterTileDictionary, 1,
                new PartyInventory());

            //First cycle
            CampaignsProcessing.ExecuteMapChange(0);

            Assert.AreEqual(CampaignsRegister.MapName, TargetMap,
                "Correct Map Name: " + CampaignsRegister.MapName);

            Assert.AreEqual(0, CampaignsRegister.CurrentPoint.XRow,
                "Correct X Coordinate: " + CampaignsRegister.CurrentPoint.XRow);

            Assert.AreEqual(0, CampaignsRegister.CurrentPoint.YColumn,
                "Correct Y Coordinate: " + CampaignsRegister.CurrentPoint.YColumn);

            //Second cycle
            CampaignsProcessing.ExecuteMapChange(0);

            Assert.AreEqual(CampaignsRegister.MapName, StartMap, "Correct Map Name: " + CampaignsRegister.MapName);

            Assert.AreEqual(0, CampaignsRegister.CurrentPoint.XRow,
                "Correct X Coordinate: " + CampaignsRegister.CurrentPoint.XRow);

            Assert.AreEqual(0, CampaignsRegister.CurrentPoint.YColumn,
                "Correct Y Coordinate: " + CampaignsRegister.CurrentPoint.YColumn);

            //Clean up afterwards
            FileHandleDelete.DeleteCompleteFolder(RootCampaign);
        }

        /// <summary>
        ///     TODO check for more details, and possible Errors
        ///     Rollback Test, Should not Assert and should throw no Exceptions
        /// </summary>
        [TestMethod]
        public void CampaignsProcessingExecuteMapChangeFail()
        {
            //Clean up afterwards
            FileHandleDelete.DeleteCompleteFolder(RootCampaign);

            //Get Basic Files like MasterTile Dictionary and e.g. Borders
            var load = HelperMethods.GetData();

            FileHandleCreate.CreateFolder(RootCampaign);

            GenerateCoordinatesId(RootCampaign, StartMap);
            GenerateEventTypeExtension(RootCampaign, StartMap);

            //Create basic crap for the new Map Start
            var startMap = GetMap(RootCampaign, StartMap);

            //Doing of the actual stuff, initiate Basics
            CampaignsRegister.SetRegister(null, startMap, null, CampaignName, load.MasterTileDictionary, 1,
                new PartyInventory());

            //Second cycle
            CampaignsProcessing.ExecuteMapChange(0);

            Assert.AreEqual(CampaignsRegister.MapName, StartMap, "Correct Map Name: " + CampaignsRegister.MapName);

            //Clean up afterwards
            FileHandleDelete.DeleteCompleteFolder(RootCampaign);
        }

        /// <summary>
        ///     Test campaign saving.
        ///     Structure of Saves.
        ///     Temp File:
        ///     Inventory Files
        ///     Map Folders
        ///     Changed Event Files
        /// </summary>
        [TestMethod]
        public void CampaignsSaving()
        {
            //Clean up afterwards
            FileHandleDelete.DeleteCompleteFolder(RootCampaignSave);

            //Get Basic Files like MasterTile Dictionary and e.g. Borders
            var load = HelperMethods.GetData();
            FileHandleCreate.CreateFolder(RootCampaignSave);

            GenerateCoordinatesId(RootCampaignSave, StartMap);
            GenerateEventTypeExtension(RootCampaignSave, StartMap);

            //Create basic crap for the new Map Start
            var startMap = GetMap(RootCampaignSave, StartMap);

            //Doing of the actual stuff, initiate Basics
            CampaignsRegister.SetRegister(null, startMap, null, CampaignNameSave, load.MasterTileDictionary, 19, null);

            CampaignsRegister.SetPoints(5);

            /*
             *Generate Save File
             */
            var save = CampaignsHelper.GenerateSave("mySave");

            Assert.AreEqual(save.MapName, StartMap, "Correct Map Name: " + CampaignsRegister.MapName);
            Assert.AreEqual(save.CampaignName, CampaignNameSave,
                "Correct Campaign Name: " + CampaignsRegister.CampaignName);
            Assert.AreEqual(save.ActualTime, CampaignsRegister.ActualTime,
                "Correct ActualTime: " + CampaignsRegister.ActualTime);
            Assert.AreEqual(save.ImageId, CampaignsRegister.ImageId, "Correct ImageId: " + CampaignsRegister.ImageId);
            Assert.AreEqual(save.PositionId, CampaignsRegister.TileId(),
                "Correct PositionId: " + CampaignsRegister.TileId());
            Assert.AreEqual("mySave", save.SaveName, "CorrectSaveName");

            var saveHandle = new SaveGameHandle();
            //get Handler
            CampaignsRegister.SetInventory(new PartyInventory());
            var cpn = HandlerOutputSingleton.Create();

            //Generate Save
            cpn.SetAutosave(save.CampaignName, save.MapName, CampaignsRegister.PartyInventory);
            saveHandle.CreateSaveFile(save);

            //Test Save Files and load them
            CampaignsProcessing.LoadSave(save);

            Assert.AreEqual(save.MapName, StartMap, "Correct Map Name: " + CampaignsRegister.MapName);
            Assert.AreEqual(save.CampaignName, CampaignNameSave,
                "Correct Campaign Name: " + CampaignsRegister.CampaignName);
            Assert.AreEqual(save.ActualTime, CampaignsRegister.ActualTime,
                "Correct ActualTime: " + CampaignsRegister.ActualTime);
            Assert.AreEqual(save.ImageId, CampaignsRegister.ImageId, "Correct ImageId: " + CampaignsRegister.ImageId);
            Assert.AreEqual(save.PositionId, CampaignsRegister.TileId(),
                "Correct PositionId: " + CampaignsRegister.TileId());
            Assert.AreEqual("mySave", save.SaveName, "CorrectSaveName");

            //Further Tests
            Assert.IsFalse(string.IsNullOrEmpty(CampaignsRegister.MapName), "MapName is not null");
            Assert.IsFalse(string.IsNullOrEmpty(CampaignsRegister.CampaignName), "MapName is not null");

            //Delete Save
            saveHandle.DeleteSaveFile(save.SaveName);

            //Clean up afterwards
            FileHandleDelete.DeleteCompleteFolder(RootCampaignSave);
        }

        /// <summary>
        ///     Generate CoordinatesIds
        /// </summary>
        /// <param name="campaign">Campaign Name</param>
        /// <param name="map">Map Name</param>
        private static void GenerateCoordinatesId(string campaign, string map)
        {
            //Add CoordinatesId Start
            var cid = new Dictionary<int, int> {{0, 0}};

            var path = Path.Combine(campaign, map);

            Serialize.SaveDctObjectToXml(cid, Path.ChangeExtension(path, ArtConst.CoordinatesIdExt));

            //Add CoordinatesId Target
            cid = new Dictionary<int, int> {{0, 0}};

            path = Path.Combine(campaign, TargetMap);

            Serialize.SaveDctObjectToXml(cid, Path.ChangeExtension(path, ArtConst.CoordinatesIdExt));
        }

        /// <summary>
        ///     Generate EventTypeExtension
        /// </summary>
        /// <param name="campaign">Campaign Name</param>
        /// <param name="map">Map Name</param>
        private static void GenerateEventTypeExtension(string campaign, string map)
        {
            //Add EventTypeExtension Start
            //EventType Extension, get MapName
            var ext = new Dictionary<int, EventTypeExtension>();

            var evt = new EventTypeExtension
            {
                Id = 0,
                Value = "End"
            };
            ext.Add(0, evt);

            evt = new EventTypeExtension
            {
                Id = 0,
                Value = "0"
            };
            ext.Add(1, evt);

            var path = Path.Combine(campaign, map);

            Serialize.SaveDctObjectToXml(ext,
                Path.ChangeExtension(path, ArtConst.EventTypeExtensionExt));

            //Add EventTypeExtension Start
            //EventType Extension, get MapName
            ext = new Dictionary<int, EventTypeExtension>();

            evt = new EventTypeExtension
            {
                Id = 0,
                Value = "Start"
            };
            ext.Add(0, evt);
            evt = new EventTypeExtension
            {
                Id = 0,
                Value = "0"
            };
            ext.Add(1, evt);

            path = Path.Combine(campaign, TargetMap);

            Serialize.SaveDctObjectToXml(ext,
                Path.ChangeExtension(path, ArtConst.EventTypeExtensionExt));
        }

        /// <summary>
        ///     Generate Map Object
        /// </summary>
        /// <param name="campaignPath">Path to Campaign</param>
        /// <param name="name">Name of the Map</param>
        /// <returns>And return the Object we need it once</returns>
        private static MapObject GetMap(string campaignPath, string name)
        {
            //Borders Target
            var map = new MapObject
            {
                MapName = name,
                Height = 1,
                Length = 2,
                Borders = new List<string> {"1|1|1|1|1|1", "1|0|0|1|1|1", "1|1|1|1|1|1"}
            };

            var lst = new List<SerializeableKeyValuePair.KeyValuePair<int, int>>();
            var kys = new SerializeableKeyValuePair.KeyValuePair<int, int>(0, 5);
            lst.Add(kys);
            kys = new SerializeableKeyValuePair.KeyValuePair<int, int>(1, 5);
            lst.Add(kys);
            kys = new SerializeableKeyValuePair.KeyValuePair<int, int>(1, 18);
            lst.Add(kys);

            map.MapList = lst;

            var path = Path.Combine(campaignPath, name);

            Serialize.SaveObjectToXml(map, Path.ChangeExtension(path, ArtConst.MapExt));

            return map;
        }
    }
}