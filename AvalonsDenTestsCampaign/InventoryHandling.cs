/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTestsCampaign/InventoryHandling.cs
 * PURPOSE:     Basic Tests for Avalon Inventory Handling
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AvalonRuntime;
using DatabaseDriver;
using EventEngine;
using ExtendedSystemObjects;
using FileHandler;
using GameEngine;
using Loader;
using MapGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;

// ReSharper disable CyclomaticComplexity, can't be changed sadly

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     Check handling of Inventory
    ///     Be careful change the Interop.dll by hand
    /// </summary>
    [TestClass]
    public sealed class InventoryHandling
    {
        /// <summary>
        ///     The db name (const). Value: "First Blood".
        /// </summary>
        private const string DbName = "First Blood";

        /// <summary>
        ///     EventInput Interface from the EventEngine
        /// </summary>
        private static readonly EventInput Input = new();

        /// <summary>
        ///     EventOutput Interface from the EventEngine
        /// </summary>
        private static readonly EventOutput Output = new();

        /// <summary>
        ///     Save and Load the inventory.
        /// </summary>
        [TestMethod]
        public void LoadInventory()
        {
            //Generate Path
            var pathAutoSave = Path.Combine(ResourcesGeneral.CampaignsFolder, ResourcesGeneral.CampaignName,
                LoaderRessource.Temp, LoaderRessource.InventoryFile);
            var path = Path.Combine(ResourcesGeneral.CampaignsFolder, ResourcesGeneral.CampaignName,
                ResourcesGeneral.SaveExt);

            FileHandleDelete.DeleteFile(path);
            FileHandleDelete.DeleteFile(pathAutoSave);

            //generate a basic Inventory
            var inv = new PartyInventory
            {
                PartyOverview = new Party {Gold = 100},

                Carrying = new Dictionary<int, InventorySlot>
                {
                    {
                        0,
                        new InventorySlot
                        {
                            Position = 1,
                            Id = 0,
                            Amount = 2
                        }
                    }
                },
                Equipment = new Dictionary<int, Equipped>
                {
                    {
                        0,
                        new Equipped
                        {
                            Head = 3,
                            Amulet = 0,
                            Belt = 2
                        }
                    }
                }
            };

            //save to root and load
            EditorSave.SaveCampaign(path, null, inv);
            inv = WorkLoader.LoadInventory(ResourcesGeneral.CampaignName);
            Assert.IsNotNull(inv, "generated inv was empty");

            //CLean root, save to AutoSave and load
            FileHandleDelete.DeleteFile(path);
            WorkLoader.SaveInventory(inv, ResourcesGeneral.CampaignName);
            inv = WorkLoader.LoadSavedInventory(ResourcesGeneral.CampaignName);
            Assert.IsNotNull(inv, "generated inv in AutoSave was empty");

            //now check the inventory contents
            Assert.AreEqual(100, inv.PartyOverview.Gold, "Wrong data loaded");
            Assert.AreEqual(1, inv.Carrying[0].Position, "Wrong data loaded");
            Assert.AreEqual(3, inv.Equipment[0].Head, "Wrong data loaded");
        }

        /// <summary>
        ///     Test add Gold
        ///     Map Layout: 4 * 3
        ///     Name:       EngineTest
        ///     Campaign:   AvalonsDenTests
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     New Route:
        ///     0111
        ///     0101
        ///     0001
        /// </summary>
        [TestMethod]
        public void AddMoney()
        {
            FileHandleDelete.DeleteCompleteFolder(ResourcesGeneral.CPath);

            //Get all the Files in Place
            CreateStructure();

            //Get all pieces in place
            BasicInitiation(ResourcesEventEngine.EventTypeWithAddGold, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderOne, ResourcesEventEngine.EventIdOne);

            //Add gold and Item
            var eventR = Input.GetDisplay(0, 3);
            DisplayEventData(eventR);

            Assert.AreEqual(1, eventR.Typ, "Right Event Type");
            Assert.AreEqual(1, eventR.MyEventsTypes.Count,
                "Wrong amount of Events, Type: " + eventR.MyEventsTypes.First().Value.TypeOfEvent);

            //Get Money
            var cache = Output.GetId(eventR.MyEventsTypes[0].IdForFurtherEventInfo, ResourcesGeneral.CampaignName,
                ResourcesGeneral.MapName);

            Assert.AreEqual(100, cache, "Right Amount of Gold");
        }

        /// <summary>
        ///     Adds an Item to the Inventory
        /// </summary>
        [TestMethod]
        public void AddItem()
        {
            var partyInventory = new PartyInventory
            {
                Carrying = new Dictionary<int, InventorySlot>
                {
                    {
                        0,
                        new InventorySlot
                        {
                            Position = 0,
                            Id = 0,
                            Amount = 2
                        }
                    },
                    {
                        3,
                        new InventorySlot
                        {
                            Position = 3,
                            Id = 1,
                            Amount = 2
                        }
                    }
                }
            };

            var inventoryItem = new List<InventorySlot>
            {
                new()
                {
                    Id = 3,
                    Amount = 2
                }
            };

            var inv = InventoryHandler.AddToInventory(partyInventory, inventoryItem);

            Assert.AreEqual(3, inv.Count, "False amount of Items");

            Assert.AreEqual(0, inv[0].Position, "Wrong Position");
            Assert.AreEqual(3, inv[3].Position, "Wrong Position");
            Assert.AreEqual(1, inv[1].Position, "Wrong Position");
        }

        /// <summary>
        ///     Create the Master database. Will be used for our Test Campaign as well
        ///     we need the interop DLL if it does not work install the Nuget Package!
        ///     Is also the Basic Template for our base Database for the tests
        /// </summary>
        [TestMethod]
        public void MasterDatabase()
        {
            var core = Path.Combine(Directory.GetCurrentDirectory(), ResourcesGeneral.CoreCampaign,
                ResourcesGeneral.CampaignName);
            FileHandleCreate.CreateFolder(core);

            var path = Path.Combine(core, DbName);
            path = Path.ChangeExtension(path, "db");

            FileHandleDelete.DeleteFile(path);

            //first create new Database
            var input = HandlerInputSingleton.Create(core, DbName);
            var check = input.CreateMasterTable();

            Assert.IsTrue(check, "Db was not created");

            var output = HandlerOutputSingleton.CreateInstance(path);

            check = File.Exists(path);

            Assert.IsTrue(check, "Db was not created");

            var weapon = new Weapon
            {
                Id = 0,
                BaseName = "Rusty Sword",
                Description = "An old Sword that has seen better days",
                MaxStack = 1,
                ImageId = 0,
                Worth = 1,
                Rarity = 0,
                Weight = 4,
                //Specific Properties
                Damage = 2,
                DamageRange = 2,
                Range = GearWeapon.Range.Melee,
                Armor = 0,
                DamageType = GearWeapon.DamageType.Cut,
                Durability = 3,
                Position = InventoryEnum.EnumSlot.MainHand
            };

            input.AddItemToWeapon(weapon);

            var itemW = output.GetItemWeapon("0");
            check = SharedTools.Compare(itemW, weapon);
            Assert.IsTrue(check, "Right Weapon Values");

            var armor = new Armor
            {
                Id = 1,
                BaseName = "Leather Cap",
                Description = "Simple Cap",
                MaxStack = 1,
                ImageId = 1,
                Worth = 3,
                Rarity = 0,
                Weight = 1,
                //Specific Properties
                ArmorValue = 1,
                Durability = 10,
                ArmorClass = GearArmor.ArmorClass.Light,
                Position = InventoryEnum.EnumSlot.Head
            };

            input.AddItemToArmor(armor);

            var itemA = output.GetItemArmor("1");
            check = SharedTools.Compare(itemA, armor);
            Assert.IsTrue(check, "Right Armor Values");

            //Items: 1,3, 2,1
            armor = new Armor
            {
                Id = 2,
                BaseName = "Wool jacket",
                Description = "A long piece of Wool Cloth that covers most of the upper Body",
                MaxStack = 1,
                ImageId = 2,
                Worth = 5,
                Rarity = 0,
                Weight = 1,
                //Specific Properties
                ArmorValue = 2,
                Durability = 15,
                ArmorClass = GearArmor.ArmorClass.Light,
                Position = InventoryEnum.EnumSlot.Trousers
            };

            input.AddItemToArmor(armor);

            itemA = output.GetItemArmor("2");
            check = SharedTools.Compare(itemA, armor);
            Assert.IsTrue(check, "Right Armor Values");

            weapon = new Weapon
            {
                Id = 3,
                BaseName = "Short Bow",
                Description = "A short and simple Bow",
                MaxStack = 1,
                ImageId = 3,
                Worth = 15,
                Rarity = 0,
                Weight = 1,
                //Specific Properties
                Damage = 1,
                DamageRange = 3,
                Range = GearWeapon.Range.Ranged,
                Armor = 0,
                DamageType = GearWeapon.DamageType.Pierce,
                Durability = 10,
                Position = InventoryEnum.EnumSlot.MainHand
            };

            input.AddItemToWeapon(weapon);

            itemW = output.GetItemWeapon("3");
            check = SharedTools.Compare(itemW, weapon);
            Assert.IsTrue(check, "Right Weapon Values");

            weapon = new Weapon
            {
                Id = 4,
                BaseName = "Wooden Club",
                Description = "A short wooden Club",
                MaxStack = 1,
                ImageId = 4,
                Worth = 3,
                Rarity = 0,
                Weight = 1,
                //Specific Properties
                Damage = 1,
                DamageRange = 4,
                Range = GearWeapon.Range.Melee,
                Armor = 0,
                DamageType = GearWeapon.DamageType.Blunt,
                Durability = 20,
                Position = InventoryEnum.EnumSlot.MainHand
            };

            input.AddItemToWeapon(weapon);

            itemW = output.GetItemWeapon("4");
            check = SharedTools.Compare(itemW, weapon);
            Assert.IsTrue(check, "Right Weapon Values");

            weapon = new Weapon
            {
                Id = 5,
                BaseName = "Wood Axe",
                Description = "An Axe used for cutting wood",
                MaxStack = 1,
                ImageId = 5,
                Worth = 7,
                Rarity = 0,
                Weight = 1,
                //Specific Properties
                Damage = 2,
                DamageRange = 3,
                Range = GearWeapon.Range.Melee,
                Armor = 0,
                DamageType = GearWeapon.DamageType.Slash,
                Durability = 15,
                Position = InventoryEnum.EnumSlot.Head
            };

            input.AddItemToWeapon(weapon);

            itemW = output.GetItemWeapon("5");
            check = SharedTools.Compare(itemW, weapon);
            Assert.IsTrue(check, "Right Weapon Values");

            weapon = new Weapon
            {
                Id = 6,
                BaseName = "Small Buckler",
                Description = "An Axe used for cutting wood",
                MaxStack = 1,
                ImageId = 6,
                Worth = 10,
                Rarity = 0,
                Weight = 1,
                //Specific Properties
                Damage = 0,
                DamageRange = 0,
                Range = GearWeapon.Range.Melee,
                Armor = 3,
                DamageType = GearWeapon.DamageType.None,
                Durability = 10,
                Position = InventoryEnum.EnumSlot.OffHand
            };

            input.AddItemToWeapon(weapon);

            itemW = output.GetItemWeapon("6");
            check = SharedTools.Compare(itemW, weapon);
            Assert.IsTrue(check, "Right Weapon Values");

            armor = new Armor
            {
                Id = 7,
                BaseName = "Leather Boots",
                Description = "Sturdy Boots",
                MaxStack = 1,
                ImageId = 7,
                Worth = 20,
                Rarity = 0,
                Weight = 1,
                //Specific Properties
                ArmorValue = 1,
                Durability = 20,
                ArmorClass = GearArmor.ArmorClass.Light,
                Position = InventoryEnum.EnumSlot.Shoes
            };

            input.AddItemToArmor(armor);

            itemA = output.GetItemArmor("7");
            check = SharedTools.Compare(itemA, armor);
            Assert.IsTrue(check, "Right Armor Values");

            var misc = new Miscellaneous
            {
                Id = 8,
                BaseName = "Healing Potion",
                Description = "Healing Potion",
                MaxStack = 5,
                ImageId = 8,
                Worth = 50,
                Rarity = 0,
                Weight = 1,
                //Specific Properties
                Type = GearMisc.Type.Consumable,
                Position = InventoryEnum.EnumSlot.None
            };

            input.AddItemToMiscellaneous(misc);

            var itemM = output.GetItemMiscellaneous("8");
            check = SharedTools.Compare(itemM, misc);
            Assert.IsTrue(check, "Right Misc Values");

            var image = new Images
            {
                IdImage = 8,
                ImagePath = "Healing Potion.png"
            };

            input.AddItemToImage(image);

            var img = output.GetItemImage("8");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");

            image = new Images
            {
                IdImage = 7,
                ImagePath = "Leather Boots.png"
            };

            input.AddItemToImage(image);

            img = output.GetItemImage("7");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");

            image = new Images
            {
                IdImage = 6,
                ImagePath = "Small Buckler.png"
            };

            input.AddItemToImage(image);

            img = output.GetItemImage("6");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");

            image = new Images
            {
                IdImage = 5,
                ImagePath = "Wood Axe.png"
            };

            input.AddItemToImage(image);

            img = output.GetItemImage("5");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");

            image = new Images
            {
                IdImage = 4,
                ImagePath = "Wooden Club.png"
            };

            input.AddItemToImage(image);

            img = output.GetItemImage("4");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");

            image = new Images
            {
                IdImage = 3,
                ImagePath = "Short Bow.png"
            };

            input.AddItemToImage(image);

            img = output.GetItemImage("3");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");

            image = new Images
            {
                IdImage = 2,
                ImagePath = "Wool jacket.png"
            };

            input.AddItemToImage(image);

            img = output.GetItemImage("2");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");

            image = new Images
            {
                IdImage = 1,
                ImagePath = "Leather Cap.png"
            };

            input.AddItemToImage(image);

            img = output.GetItemImage("1");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");

            image = new Images
            {
                IdImage = 0,
                ImagePath = "Rusty Sword.png"
            };

            input.AddItemToImage(image);

            // Only once for Test purposes
            input.UpdateImage(image);

            img = output.GetItemImage("0");
            Assert.AreEqual(img.ImagePath, image.ImagePath, "Right Image");
        }

        /// <summary>
        ///     Create Basic Campaign Structure and Load it.
        /// </summary>
        [TestMethod]
        public void CreateCampaignStructure()
        {
            CreateStructure();

            var check = FileHandleSearch.CheckIfFolderContainsElement(ResourcesGeneral.CPath);

            Assert.IsTrue(check, "Folder contains no Files: " + ResourcesGeneral.CPath);
        }

        /// <summary>
        ///     Helper Method
        /// </summary>
        /// <returns>Returns Path</returns>
        private static void CreateStructure()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), ResourcesGeneral.CoreCampaign,
                ResourcesGeneral.CampaignName, ResourcesGeneral.MapName);
            Debug.WriteLine(path);

            var load = new LoaderContainer
            {
                MapObject = new MapObject(),
                EventCollection = new EventContainer
                {
                    CoordinatesId = ResourcesEventEngine.EventIdOne,
                    EventTypeExtensionDictionary = ResourcesEventEngine.EventTypeItem,
                    EventTypeDictionary = ResourcesEventEngine.EventTypeWithAddItem
                }
            };

            EditorSave.SaveMapAs(path, load);
        }

        /// <summary>
        ///     Setting up all basic pieces of a map
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="height">The height.</param>
        /// <param name="length">The length.</param>
        /// <param name="blocker">The blocker.</param>
        /// <param name="lst">The List.</param>
        /// <param name="eventId">The eventId.</param>
        private static void BasicInitiation(Dictionary<int, EventType> events, int height, int length, bool blocker,
            IEnumerable<int> lst, Dictionary<int, int> eventId)
        {
            //Just Preparations
            //Create Basic Map
            var mapE = new EditorMapEngine();
            var map = mapE.Generate(height, length);

            if (blocker)
            {
                //added Blockers

                var one =
                    lst.Select(
                        element =>
                            ArtShared.IdToCoordinate(element, length, ResourcesEventEngine.Layer,
                                ResourcesEventEngine.TileId)).ToList();

                //Generate Changes
                var load = HelperMethods.GetData();
                map = mapE.ChangeMap(one, map, load.MasterBordersDictionary, load.MasterTileDictionary);

                foreach (var element in map.Borders) Debug.WriteLine(element);
            }

            //Initiate Party
            Input.InitiateParty(0);

            //Generate new CoordinatesId and EventType
            var check = Input.InitiateMove(eventId, height, length, map.Borders,
                events);

            Assert.IsTrue(check, "Check if Move Engine was initiated correct failed");
        }

        /// <summary>
        ///     Just Display Data, for Review Purposes
        /// </summary>
        /// <param name="eventDisplay">Collected Event Data</param>
        private static void DisplayEventData(EventTypeDisplay eventDisplay)
        {
            if (eventDisplay == null) Assert.Fail("Object was empty");

            Debug.WriteLine("Type:" + eventDisplay.Typ);

            Debug.WriteLine("EventType");
            Debug.WriteLine(eventDisplay.Typ);
            Debug.WriteLine("Steps");
            if (!eventDisplay.PathTravel.IsNullOrEmpty()) Debug.WriteLine(eventDisplay.PathTravel.Count);

            Debug.WriteLine("Steps shown");
            if (!eventDisplay.PathDisplay.IsNullOrEmpty()) Debug.WriteLine(eventDisplay.PathDisplay.Count);

            Debug.WriteLine("Event Type internal");
            if (eventDisplay.MyEventsTypes == null) return;

            foreach (var element in eventDisplay.MyEventsTypes.Values)
            {
                Debug.WriteLine("EventDisplay Coordinate Id: " + element.CoordinatesId);
                Debug.WriteLine("EventDisplay Description: " + element.Description);
                Debug.WriteLine("EventDisplay Step On: " + element.IsStepOn);
                Debug.WriteLine("EventDisplay Label Event: " + element.LabelEvent);
            }
        }
    }
}