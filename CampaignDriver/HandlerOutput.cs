/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CampaignDriver/HandlerOutputSingleton.cs
 * PURPOSE:     Basic Translator between Output from Game Engine and User, Mostly Internal Systems
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeMadeStatic.Global, would defeat the purpose of the SingleTon

using System;
using System.Collections.Generic;
using Debugger;
using EventEngine;
using Resources;

namespace CampaignDriver
{
    /// <summary>
    ///     Singleton for different Game Engines, Campaign Site, mostly Output
    /// </summary>
    public static class HandlerOutputSingleton
    {
        /// <summary>
        ///     The lazy campaign.
        /// </summary>
        private static Lazy<CampaignOutput> _lazyCampaign;

        /// <summary>
        ///     Create and Initiate Single Instance of EventOutput
        /// </summary>
        /// <returns>Single Instance of EventOutput</returns>
        public static CampaignOutput Create()
        {
            if (_lazyCampaign != null) return _lazyCampaign.Value;

            DebugLog.CreateLogFile(CampaignDriverResources.InformationStartUpOut, ErCode.Information);
            _lazyCampaign = new Lazy<CampaignOutput>(() => new CampaignOutput());

            return _lazyCampaign.Value;
        }
    }

    /// <summary>
    ///     The campaign output class.
    /// </summary>
    public sealed class CampaignOutput
    {
        /// <summary>
        ///     Module that handles Processing of Events Input
        /// </summary>
        private static readonly EventOutput OutputHandle = new();

        /// <summary>
        ///     Initiate the Event Handler
        /// </summary>
        internal CampaignOutput()
        {
        }

        /// <summary>
        ///     Initiate the basic data for the Map
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="mapgChanged">Was map changed</param>
        /// <returns>Map Informations</returns>
        public LoaderContainer GetMapObjects(string campaignName, string mapName, bool mapgChanged)
        {
            return OutputHandle.GetMapObjects(campaignName, mapName, mapgChanged);
        }

        /// <summary>
        ///     Gets the inventory.
        /// </summary>
        /// <param name="campaignName">Name of the campaign.</param>
        /// <param name="mapgChanged">if set to <c>true</c> [save state false].</param>
        /// <returns>The Inventory depending on the save State</returns>
        public PartyInventory GetInventory(string campaignName, bool mapgChanged)
        {
            return OutputHandle.GetInventory(campaignName, mapgChanged);
        }

        /// <summary>
        ///     Load from EventTypeExtension in an fitting Format
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns></returns>
        public string GetMapName(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            return OutputHandle.GetMapName(idForFurtherEventInfo, campaignName, mapName);
        }

        /// <summary>
        ///     Load from EventTypeExtension in an fitting Format
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Get Asset as Int from EventTypeExtension</returns>
        public int GetId(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            return OutputHandle.GetId(idForFurtherEventInfo, campaignName, mapName);
        }

        /// <summary>
        ///     Calculate Start Point
        ///     Used for Map change, we load the position on the new Map
        /// </summary>
        /// <param name="mapCoordinate">Id of Coordinate</param>
        /// <param name="coordinatesId">Ids of Events</param>
        /// <param name="length">Length of the map</param>
        /// <param name="imageId">Image Id of the Player</param>
        /// <param name="layer">Character Layer</param>
        /// <returns>A Coordinate with all processed infos</returns>
        public Coordinates GetNewStart(int mapCoordinate, Dictionary<int, int> coordinatesId,
            int length, int imageId, int layer)
        {
            return OutputHandle.GetNewStart(mapCoordinate, coordinatesId, length, imageId, layer);
        }

        /// <summary>
        ///     Folder is equal to Map Name
        ///     Load from EventTypeExtension in an fitting Format
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Get Asset as Int from EventTypeExtension</returns>
        public string GetDialogName(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            return OutputHandle.GetDialogName(idForFurtherEventInfo, campaignName, mapName);
        }

        /// <summary>
        ///     Check if the event was changed
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Were the events Changed</returns>
        public bool ChangedEvent(string campaignName, string mapName)
        {
            return OutputHandle.GetChangedEvents(campaignName, mapName);
        }

        /// <summary>
        ///     Add a changed Event
        /// </summary>
        /// <param name="mapName">Name of the Map</param>
        public void SetEvent(string mapName)
        {
            OutputHandle.SetEvent(mapName);
        }

        /// <summary>
        ///     Save the events on Map Change
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="partyInventory">The party Inventory</param>
        public void SetAutosave(string campaignName, string mapName, PartyInventory partyInventory)
        {
            OutputHandle.SetAutosave(campaignName, mapName, partyInventory);
        }

        /// <summary>
        ///     Get Item List
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Item List</returns>
        public List<InventoryContainer> GetItemList(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            return OutputHandle.GetItemList(idForFurtherEventInfo, campaignName, mapName);
        }

        /// <summary>
        ///     Add the gold.
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>The amount of Gold <see cref="int" />.</returns>
        public int AddGold(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            return OutputHandle.GetGold(idForFurtherEventInfo, campaignName, mapName);
        }
    }
}