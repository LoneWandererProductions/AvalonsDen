/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/WorkLoader.cs
 * PURPOSE:     Load all Types of Objects for the Campaign and Editor
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AvalonRuntime;
using DenSerializer;
using ExtendedSystemObjects;
using FileHandler;
using Resources;
using Serializer;

namespace Loader
{
    /// <summary>
    ///     Does all the work at startup
    ///     This class searches for all Files needed
    ///     Than loads it into the right Objects
    ///     Editor and Campaign
    /// </summary>
    public static class WorkLoader
    {
        /// <summary>
        ///     Basic Constructor
        ///     Loads Standard MasterBorders Dictionary and  MasterTile Dictionary just in case they will be used as a fall back in
        ///     case they are not
        ///     defined
        /// </summary>
        /// <returns>LoadUpCollection</returns>
        public static LoaderContainer LoadMaster()
        {
            var tile = OpenTileDct();

            if (tile.IsNullOrEmpty()) return null;

            var border = OpenBorderDct();

            if (border.IsNullOrEmpty()) return null;

            return new LoaderContainer
            {
                MasterTileDictionary = tile,
                MasterBordersDictionary = border
            };
        }

        /// <summary>
        ///     Loads the files needed for a Campaign
        ///     Loads:
        ///     Map
        ///     Event Objects
        ///     Transition File
        ///     Master Tile
        ///     Border
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="startMap">Name of the StartMap</param>
        /// <returns>LoadUpCollection</returns>
        public static LoaderContainer LoadCampaign(string campaignName, string startMap)
        {
            return new()
            {
                CampaignName = campaignName,
                MasterTileDictionary =
                    OpenStandardTileDctMap(LoaderRessource.MasterTileDct, campaignName),
                MapObject = LoadStartMap(campaignName, startMap),
                EventCollection = LoadEventCollection(campaignName, startMap),
                MasterBordersDictionary = OpenBorderDct(),
                TransitionDictionary = LoadTransitionMap(campaignName, startMap)
            };
        }

        /// <summary>
        ///     Overload: Returns Map and EventMap in specified Path appendix
        ///     Loaded From MapChange
        ///     Don't forget to Load the Extra Layers of the Map
        ///     Does not need to load the Inventory
        ///     Campaign Only
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>LoadUpCollection</returns>
        public static LoaderContainer LoadCollectionMap(string campaignName, string mapName)
        {
            mapName = Path.GetFileNameWithoutExtension(mapName);

            return new LoaderContainer
            {
                CampaignName = campaignName,
                MasterTileDictionary =
                    OpenStandardTileDctMap(LoaderRessource.MasterTileDct, campaignName),
                MapObject = LoadStartMap(campaignName, mapName),
                EventCollection = LoadSpecifiedEvent(campaignName, mapName),
                TransitionDictionary = LoadTransitionMap(campaignName, mapName)
            };
        }

        /// <summary>
        ///     Helper Method to load EventMasterCollection and Map
        ///     Loads:
        ///     Map
        ///     Transitions
        ///     Event Objects
        ///     Editor only
        /// </summary>
        /// <param name="path">PathName</param>
        /// <returns>EventMasterCollection, Map, Transitions</returns>
        public static LoaderContainer LoadCollectionMap(string path)
        {
            path = PathInformation.GetPathWithoutExtension(path);

            return new LoaderContainer
            {
                MapObject =
                    DeSerialize.LoadObjectFromXml<MapObject>(Path.ChangeExtension(path, ArtConst.MapExt)),
                TransitionDictionary =
                    DenDeSerialize.XmlDeSerializerTransitionDictionaryDictionary(Path.ChangeExtension(path,
                        ArtConst.TransitionFileExt)),

                //load all event Parts
                EventCollection = new EventContainer
                {
                    EventTypeDictionary =
                        DeSerialize.LoadDictionaryFromXml<int, EventType>(Path.ChangeExtension(path,
                            ArtConst.EventTypeExt)),
                    EventTypeExtensionDictionary =
                        DeSerialize.LoadDictionaryFromXml<int, EventTypeExtension>(Path.ChangeExtension(path,
                            ArtConst.EventTypeExtensionExt)),
                    CoordinatesId =
                        DeSerialize.LoadDictionaryFromXml<int, int>(Path.ChangeExtension(path,
                            ArtConst.CoordinatesIdExt))
                }
            };
        }

        /// <summary>
        ///     Helper Method to load EventMasterCollection
        ///     contains:  EventCoordinatesList, EventTypeList, EventAssetList
        ///     Editor Only
        /// </summary>
        /// <param name="path">PathName</param>
        /// <returns>Opens EventMasterCollection</returns>
        public static EventContainer LoadEventCollection(string path)
        {
            path = PathInformation.GetPathWithoutExtension(path);

            return new EventContainer
            {
                EventTypeDictionary =
                    DeSerialize.LoadDictionaryFromXml<int, EventType>(Path.ChangeExtension(path,
                        ArtConst.EventTypeExt)),
                EventTypeExtensionDictionary =
                    DeSerialize.LoadDictionaryFromXml<int, EventTypeExtension>(Path.ChangeExtension(path,
                        ArtConst.EventTypeExtensionExt)),
                CoordinatesId =
                    DeSerialize.LoadDictionaryFromXml<int, int>(Path.ChangeExtension(path, ArtConst.CoordinatesIdExt))
            };
        }

        /// <summary>
        ///     Loads a CampaignManifest
        ///     Editor, Chapter
        /// </summary>
        /// <param name="path">path to the object</param>
        /// <returns>Campaign Object</returns>
        public static CampaignManifest LoadCampaignManifest(string path)
        {
            return DeSerialize.LoadObjectFromXml<CampaignManifest>(path);
        }

        /// <summary>
        ///     Loads TileBorders Dictionary
        /// </summary>
        /// <param name="path">path to the object</param>
        /// <returns>TileBordersDictionary Object</returns>
        /// ä
        [return: MaybeNull]
        public static Dictionary<int, TileBorders> LoadTileBordersDct(string path)
        {
            return DeSerialize.LoadDictionaryFromXml<int, TileBorders>(path);
        }

        /// <summary>
        ///     Loads TileDictionary
        /// </summary>
        /// <param name="path">path to the object</param>
        /// <returns>TileDictionary Object</returns>
        [return: MaybeNull]
        public static Dictionary<int, Tile> LoadTileDct(string path)
        {
            return DeSerialize.LoadDictionaryFromXml<int, Tile>(path);
        }

        /// <summary>
        ///     Load EventTypeExtension Dictionary
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns></returns>
        public static Dictionary<int, EventTypeExtension> LoadEventTypeExt(string campaignName, string mapName)
        {
            var path = LoadHelper.GetCampaignpath(campaignName, mapName);
            return
                DeSerialize.LoadDictionaryFromXml<int, EventTypeExtension>(Path.ChangeExtension(path,
                    ArtConst.EventTypeExtensionExt));
        }

        /// <summary>
        ///     Changed Inventory
        ///     Loads the saved inventory.
        ///     \Campaign\Core\"CampaignName"\Temp\Inventory.aiy
        ///     Changed Inventory
        /// </summary>
        /// <param name="campaignName">Name of the campaign.</param>
        /// <returns>Load saved Inventory</returns>
        public static PartyInventory LoadSavedInventory(string campaignName)
        {
            var path = Path.Combine(ArtConst.CampaignsFolder, campaignName, LoaderRessource.Temp);

            return new PartyInventory
            {
                Carrying = DeSerialize.LoadDictionaryFromXml<int, InventorySlot>(Path.Combine(path,
                    LoaderRessource.InventoryFile)),
                Equipment = DeSerialize.LoadDictionaryFromXml<int, Equipped>(Path.Combine(path,
                    LoaderRessource.EquipmentFile)),
                PartyOverview = DeSerialize.LoadObjectFromXml<Party>(Path.Combine(path, LoaderRessource.PartyFile))
            };
        }

        /// <summary>
        ///     Master Inventory
        ///     Loads the inventory from the Campaign Start, we won't change anything.
        ///     \Campaign\Core\"CampaignName"\Inventory.aiy
        /// </summary>
        /// <param name="campaignName">Name of the campaign.</param>
        /// <returns>Changed Inventory</returns>
        public static PartyInventory LoadInventory(string campaignName)
        {
            var path = Path.Combine(ArtConst.CampaignsFolder, campaignName);

            return new PartyInventory
            {
                Carrying = DeSerialize.LoadDictionaryFromXml<int, InventorySlot>(Path.Combine(path,
                    LoaderRessource.InventoryFile)),
                Equipment = DeSerialize.LoadDictionaryFromXml<int, Equipped>(Path.Combine(path,
                    LoaderRessource.EquipmentFile)),
                PartyOverview = DeSerialize.LoadObjectFromXml<Party>(Path.Combine(path, LoaderRessource.PartyFile))
            };
        }

        /// <summary>
        ///     Saves the inventory.
        ///     Goes into the Temp Folder
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        /// <param name="campaignName">Name of the campaign.</param>
        public static void SaveInventory(PartyInventory inventory, string campaignName)
        {
            var path = Path.Combine(ArtConst.CampaignsFolder, campaignName, LoaderRessource.Temp);

            Serialize.SaveObjectToXml(inventory.PartyOverview, Path.Combine(path, LoaderRessource.PartyFile));
            Serialize.SaveDctObjectToXml(inventory.Carrying, Path.Combine(path, LoaderRessource.InventoryFile));
            Serialize.SaveDctObjectToXml(inventory.Equipment, Path.Combine(path, LoaderRessource.EquipmentFile));
        }

        /// <summary>
        ///     Get MasterTile Dictionary
        /// </summary>
        /// <returns>MasterTile Dictionary</returns>
        private static Dictionary<int, Tile> OpenTileDct()
        {
            return LoadTileDctPath(LoaderRessource.MasterTileDct);
        }

        /// <summary>
        ///     Loads the Border Dictionary
        /// </summary>
        /// <returns>Border Dictionary</returns>
        private static Dictionary<int, TileBorders> OpenBorderDct()
        {
            var path = Path.Combine(LoaderRessource.MasterBorderDct);
            return DeSerialize.LoadDictionaryFromXml<int, TileBorders>(path);
        }

        /// <summary>
        ///     We Open the MasterTile Dictionary, we may have to use a custom one though
        /// </summary>
        /// <param name="tiledct">Name of the MasterTile Dictionary </param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <returns>Standard MasterTile Dictionary or Custom</returns>
        private static Dictionary<int, Tile> OpenStandardTileDctMap(string tiledct, string campaignName)
        {
            var path = Path.Combine(ArtConst.CampaignsFolder, campaignName, LoaderRessource.CustomTileDct);

            return tiledct == null
                ? LoadTileDctPath(path)
                //if an alternative Tile Dictionary exists use this instead
                : LoadTileDctPath(LoaderRessource.MasterTileDct);
        }

        /// <summary>
        ///     Get the Start Map
        /// </summary>
        /// <param name="campaignName">Name of Campaign</param>
        /// <param name="mapName">Name of Start Map</param>
        /// <returns>The map Object</returns>
        private static MapObject LoadStartMap(string campaignName, string mapName)
        {
            var path = LoadHelper.GetCampaignpath(campaignName, mapName);
            return DeSerialize.LoadObjectFromXml<MapObject>(Path.ChangeExtension(path, ArtConst.MapExt));
        }

        /// <summary>
        ///     Load all Data needed for Events
        /// </summary>
        /// <param name="campaignName">Name of Campaign</param>
        /// <param name="mapName">Name of Map</param>
        /// <returns>Collected Event Objects</returns>
        private static EventContainer LoadEventCollection(string campaignName, string mapName)
        {
            var path = LoadHelper.GetCampaignpath(campaignName, mapName);

            return new EventContainer
            {
                EventTypeDictionary =
                    DeSerialize.LoadDictionaryFromXml<int, EventType>(Path.ChangeExtension(path,
                        ArtConst.EventTypeExt)),
                EventTypeExtensionDictionary =
                    DeSerialize.LoadDictionaryFromXml<int, EventTypeExtension>(Path.ChangeExtension(path,
                        ArtConst.EventTypeExtensionExt)),
                CoordinatesId =
                    DeSerialize.LoadDictionaryFromXml<int, int>(Path.ChangeExtension(path, ArtConst.CoordinatesIdExt))
            };
        }

        /// <summary>
        ///     Loads the Transitions for a Map
        /// </summary>
        /// <param name="campaignName">Name of the campaign</param>
        /// <param name="transitionMapName">Name of the Map</param>
        /// <returns>Dictionary with String</returns>
        private static Dictionary<int, List<int>> LoadTransitionMap(string campaignName, string transitionMapName)
        {
            var path = LoadHelper.GetCampaignpath(campaignName, transitionMapName);
            return
                DenDeSerialize.XmlDeSerializerTransitionDictionaryDictionary(Path.ChangeExtension(path,
                    ArtConst.TransitionFileExt));
        }

        /// <summary>
        ///     Collects the EventMasterCollection of a specific Map
        /// </summary>
        /// <param name="campaignName">Name of Campaign</param>
        /// <param name="mapName">Name of Map</param>
        /// <returns>EventTypeCollection</returns>
        private static EventContainer LoadSpecifiedEvent(string campaignName, string mapName)
        {
            if (string.IsNullOrEmpty(mapName)) return null;

            var mapNameExt = Path.GetFileNameWithoutExtension(mapName);
            var autosave = LoadHelper.GetCampaignAutoSavePathWithMap(campaignName, mapName);
            var path = LoadHelper.GetCampaignpath(campaignName, mapName);

            var root = Path.Combine(autosave, mapNameExt);

            return new EventContainer
            {
                EventTypeDictionary =
                    DeSerialize.LoadDictionaryFromXml<int, EventType>(Path.ChangeExtension(root,
                        ArtConst.EventTypeExt)),
                EventTypeExtensionDictionary =
                    DeSerialize.LoadDictionaryFromXml<int, EventTypeExtension>(Path.ChangeExtension(path,
                        ArtConst.EventTypeExtensionExt)),
                CoordinatesId =
                    DeSerialize.LoadDictionaryFromXml<int, int>(Path.ChangeExtension(path, ArtConst.CoordinatesIdExt))
            };
        }

        /// <summary>
        ///     Loads a MasterTile
        /// </summary>
        /// <param name="path">Location</param>
        /// <returns>Master Tiles</returns>
        private static Dictionary<int, Tile> LoadTileDctPath(string path)
        {
            return DeSerialize.LoadDictionaryFromXml<int, Tile>(path);
        }
    }
}