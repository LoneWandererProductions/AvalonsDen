/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/IEventInput.cs
 * PURPOSE:     Basic Interface for EventOutput, that handles all Event Results on the Map, interplay between Input and Output
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace EventEngine
{
    /// <summary>
    ///     The IEventOutput interface.
    /// </summary>
    internal interface IEventOutput
    {
        /// <summary>
        ///     Get the dialog name.
        /// </summary>
        /// <param name="idForFurtherEventInfo">The idForFurtherEventInfo.</param>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="mapName">The mapName.</param>
        /// <returns>The <see cref="string" />.</returns>
        string GetDialogName(int idForFurtherEventInfo, string campaignName, string mapName);

        /// <summary>
        ///     Get the map objects.
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="mapChanged">Was map changed</param>
        /// <returns>The changed LoadUpCollection Object as<see cref="LoaderContainer" />.</returns>
        LoaderContainer GetMapObjects(string campaignName, string mapName, bool mapChanged);

        /// <summary>
        ///     Gets the inventory.
        /// </summary>
        /// <param name="campaignName">Name of the campaign.</param>
        /// <param name="mapChanged">if set to <c>true</c> [map changed].</param>
        /// <returns>Inventory, changed Inventory if we load a save Campaign</returns>
        PartyInventory GetInventory(string campaignName, bool mapChanged);

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
        Coordinates GetNewStart(int mapcoordinate, Dictionary<int, int> coordinatesId, int mapLength,
            int characterId, int playerLayer);

        /// <summary>
        ///     For External use only to Convert the needed data
        ///     Get a map name.
        /// </summary>
        /// <param name="idForFurtherEventInfo">The idForFurtherEventInfo.</param>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="mapName">The mapName.</param>
        /// <returns>The Name of the Map as<see cref="string" />.</returns>
        string GetMapName(int idForFurtherEventInfo, string campaignName, string mapName);

        /// <summary>
        ///     Get the id as int
        ///     For External use only to Convert the needed data
        /// </summary>
        /// <param name="idForFurtherEventInfo">The idForFurtherEventInfo.</param>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="mapName">The mapName.</param>
        /// <returns>Return a id if needed as <see cref="int" />.</returns>
        int GetId(int idForFurtherEventInfo, string campaignName, string mapName);

        /// <summary>
        ///     Get the item list.
        /// </summary>
        /// <param name="idForFurtherEventInfo">The idForFurtherEventInfo.</param>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="mapName">The mapName.</param>
        /// <returns>List of Items as <see cref="T:List{ItemContainer}" /> and Amount.</returns>
        List<InventoryContainer> GetItemList(int idForFurtherEventInfo, string campaignName, string mapName);

        /// <summary>
        ///     Get the gold.
        /// </summary>
        /// <param name="idForFurtherEventInfo">The idForFurtherEventInfo.</param>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="mapName">The mapName.</param>
        /// <returns>The Amount of Gold as <see cref="int" />.</returns>
        int GetGold(int idForFurtherEventInfo, string campaignName, string mapName);

        /// <summary>
        ///     Get the changed events. Checks if Element is in the List
        /// </summary>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="mapName">The mapName.</param>
        /// <returns>Item Status as <see cref="bool" />.</returns>
        bool GetChangedEvents(string campaignName, string mapName);

        /// <summary>
        ///     Set the event.
        /// </summary>
        /// <param name="mapName">The mapName.</param>
        void SetEvent(string mapName);

        /// <summary>
        ///     Set the autosave.
        /// </summary>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="mapName">The mapName.</param>
        /// <param name="partyInventory">The Party Inventory</param>
        void SetAutosave(string campaignName, string mapName, PartyInventory partyInventory);
    }
}