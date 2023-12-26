/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/EventOutput.cs
 * PURPOSE:     Loads Various kinds of Events on Runtime, to preserve Memory, also Handles Saving
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using AvalonRuntime;
using Debugger;
using Loader;
using Resources;

namespace EventEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     Here we Load Assets on Runtime
    /// </summary>
    /// <seealso cref="T:EventEngine.IEventOutput" />
    public sealed class EventOutput : IEventOutput
    {
        /// <summary>
        ///     List of changed Events
        /// </summary>
        private List<string> ChangedEvents { get; set; } = new();

        /// <inheritdoc />
        /// <summary>
        ///     Folder is equal to Map Name
        ///     Get the Dialog
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>The Dialog Name as <see cref="string" />.</returns>
        public string GetDialogName(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            CheckException(campaignName, mapName);

            var asset = WorkLoader.LoadEventTypeExt(campaignName,
                mapName);
            var eventTypeExtension = asset.Values.FirstOrDefault(assets => assets.Id == idForFurtherEventInfo);
            //if we get null return null else return strings
            return eventTypeExtension?.Value;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Input Value is Asset and the CampaignName
        ///     Sadly we can't use Register here, since we load it into the File with Map Change
        ///     Optional collects the Files from the Autosave Folder
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="mapChanged">Was map changed</param>
        /// <returns>The changed LoadUpCollection Object as<see cref="T:Resources.LoaderContainer" />.</returns>
        public LoaderContainer GetMapObjects(string campaignName, string mapName, bool mapChanged)
        {
            CheckException(campaignName, mapName);

            //scrap over our disk and folder and Collect Files in the Autosave Folder

            return mapChanged
                ? WorkLoader.LoadCollectionMap(campaignName, mapName)
                : WorkLoader.LoadCampaign(campaignName, mapName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the inventory.
        /// </summary>
        /// <param name="campaignName">Name of the campaign.</param>
        /// <param name="mapChanged">if set to <c>true</c> [map changed].</param>
        /// <returns>Inventory, changed Inventory if we load a save Campaign</returns>
        public PartyInventory GetInventory(string campaignName, bool mapChanged)
        {
            if (string.IsNullOrWhiteSpace(campaignName))
                throw new ArgumentException(EventEngineResources.ErrorNoValidCampaignName,
                    nameof(campaignName));

            return mapChanged
                ? WorkLoader.LoadSavedInventory(campaignName)
                : WorkLoader.LoadInventory(campaignName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Used for Map change, we load the position on the new Map
        ///     eventCoordinatesList is looked up with idForFurtherEventInfo for the new position Value
        ///     Map change Event describes the Event Number of the New Map and Vice Versa
        /// </summary>
        /// <param name="mapcoordinate">Id for eventCoordinatesList</param>
        /// <param name="coordinatesId">
        ///     Location and Id, Coordinate Id and Id of Event, Event is the first (Key), Coordinate Id is
        ///     second (Value)
        /// </param>
        /// <param name="mapLength">Length of Map</param>
        /// <param name="characterId">Tile Id</param>
        /// <param name="playerLayer">Layer of the Player</param>
        /// <returns>The new Start Point as <see cref="Coordinates" />.</returns>
        /// <exception cref="ArgumentException"></exception>
        public Coordinates GetNewStart(int mapcoordinate, Dictionary<int, int> coordinatesId,
            int mapLength, int characterId, int playerLayer)
        {
            if (mapcoordinate == -1) return null;

            if (!coordinatesId.ContainsKey(mapcoordinate))
            {
                DebugLog.CreateLogFile(
                    string.Concat(EventEngineResources.ErrorCouldNotFindCoordinateKey, mapcoordinate), ErCode.Error);

                //Should not happen, but yeah catch it
                throw new ArgumentException(EventEngineResources.ErrorCouldNotFindCoordinateKey,
                    nameof(mapcoordinate));
            }

            var point = coordinatesId[mapcoordinate];

            return ArtShared.IdToCoordinate(point, mapLength, playerLayer, characterId);
        }

        /// <inheritdoc />
        /// <summary>
        ///     For External use only to Convert the needed data
        ///     Get a map name.
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>The Name of the Map as<see cref="string" />.</returns>
        public string GetMapName(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            return GetAssetAsString(idForFurtherEventInfo, campaignName, mapName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get the id as int
        ///     For External use only to Convert the needed data
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Return a id if needed as <see cref="int" />.</returns>
        public int GetId(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            CheckException(campaignName, mapName);
            return GetAssetAsInt(idForFurtherEventInfo, campaignName, mapName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get the changed events. Checks if Element is in the List
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the EventType, Map without Extension</param>
        /// <returns>Item Status as <see cref="bool" />.</returns>
        public bool GetChangedEvents(string campaignName, string mapName)
        {
            CheckException(campaignName, mapName);

            ChangedEvents = SaveHandleProcessing.GetFilesFromSaveFolder(campaignName, mapName,
                EventEngineResources.EventTypeExt);
            return ChangedEvents.Contains(mapName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get amount of gold
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>The Amount of Gold as <see cref="int" />.</returns>
        public int GetGold(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            CheckException(campaignName, mapName);
            var amount = GetAssetAsInt(idForFurtherEventInfo, campaignName, mapName);

            //Error but we still can continue so we should
            if (amount == -1)
            {
                DebugLog.CreateLogFile(EventEngineResources.ErrorAssets, ErCode.Error);
                return 0;
            }

            return amount;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get Item List
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>List of Items as <see cref="T:List{ItemContainer}" /> and Amount. Can return null.</returns>
        public List<InventoryContainer> GetItemList(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            CheckException(campaignName, mapName);

            //Get the string and convert it to ItemContainer
            var asset = GetAssetAsString(idForFurtherEventInfo, campaignName, mapName);

            if (asset != null) return StringParser.ParseItems(asset);

            DebugLog.CreateLogFile(EventEngineResources.ErrorAssets, ErCode.Warning);

            return null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Adds the Name if it does not already Contain it
        /// </summary>
        /// <param name="mapName">Name of the EventType Map, without Extension</param>
        public void SetEvent(string mapName)
        {
            if (mapName.Length == 0) return;

            if (!ChangedEvents.Contains(mapName)) ChangedEvents.Add(mapName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Generate Autosave Folder and move the changed Dictionary into the Autosave Folder
        ///     /Content/Campaign/"Name of the Campaign"/"Name of the Map"/"Event File"
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="partyInventory">The Party Inventory</param>
        public void SetAutosave(string campaignName, string mapName, PartyInventory partyInventory)
        {
            CheckException(campaignName, mapName);

            //Put the current Inventory into the Temp Folder
            WorkLoader.SaveInventory(partyInventory, campaignName);

            if (EventInput.EventChanged)
                SaveHandleProcessing.Autosave(EventInput.EventTypeDictionary, campaignName, mapName);
        }

        /// <summary>
        ///     For internal use
        ///     Load the Extension File as string, Handle the string as script
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map. Can return null.</param>
        /// <returns>String script. Can return null.</returns>
        private static string GetAssetAsString(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            foreach (var asset in GetAssetStringStringList(idForFurtherEventInfo, campaignName, mapName))
            {
                //basic Check if it is a string and not a int
                var check = int.TryParse(asset, out _);
                if (!check) return asset;
            }

            return null;
        }

        /// <summary>
        ///     For internal use
        ///     Get the Value as Int
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Return a Int if needed as Asset</returns>
        internal static int GetAssetAsInt(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            var asset = GetAssetStringStringList(idForFurtherEventInfo, campaignName, mapName);

            if (asset == null)
            {
                DebugLog.CreateLogFile(EventEngineResources.ErrorAssets, ErCode.Warning);
                return -1;
            }

            foreach (var data in asset)
            {
                var check = int.TryParse(data, out var id);
                if (check) return id;
            }

            return -1;
        }

        /// <summary>
        ///     For internal use
        ///     Load the Extension File as string, Handle the string as List String
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Return a List string if needed as Asset. Can return null.</returns>
        private static IEnumerable<string> GetAssetStringStringList(int idForFurtherEventInfo, string campaignName,
            string mapName)
        {
            var asset = WorkLoader.LoadEventTypeExt(campaignName, mapName);

            return asset == null
                ? null
                : (from item in asset.Values where item.Id == idForFurtherEventInfo select item.Value).ToList();
        }

        /// <summary>
        ///     For internal use
        ///     Checks for Valid Values
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <exception cref="ArgumentException">Wrong Values were provided</exception>
        private static void CheckException(string campaignName, string mapName)
        {
            if (string.IsNullOrWhiteSpace(campaignName))
                throw new ArgumentException(EventEngineResources.ErrorNoValidCampaignName,
                    nameof(campaignName));

            if (string.IsNullOrWhiteSpace(mapName))
                throw new ArgumentException(EventEngineResources.ErrorNoValidMapName,
                    nameof(mapName));
        }
    }
}