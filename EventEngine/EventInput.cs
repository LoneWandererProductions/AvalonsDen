/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/EventInput.cs
 * PURPOSE:     Basic Handler class, that handles all Events on the Map
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using GameEngine;
using Resources;

namespace EventEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     Handles all cases of Movement
    /// </summary>
    public sealed class EventInput : IEventInput
    {
        /// <summary>
        ///     Party Interface, well we should rework that
        /// </summary>
        private static readonly Adventure Adventure = new();

        /// <summary>
        ///     Events for the Map
        ///     Data is Map dependent
        /// </summary>
        public static Dictionary<int, EventType> EventTypeDictionary { get; private set; }

        /// <summary>
        ///     Check if Party was changed
        /// </summary>
        internal static bool EventChanged { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Called by Startup and Map change
        ///     Get the Pathfinding Engine running, initiate Maps
        ///     Sort all Events
        /// </summary>
        /// <param name="coordinatesId">
        ///     Location and Id, Coordinate Id and Id of Event, Event is the first (Key), Coordinate Id is
        ///     second (Value)
        /// </param>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        /// <param name="borders">Borders of Map</param>
        /// <param name="eventTypeDictionary"></param>
        /// <returns>Status if initiation was successful, if false try to rollback</returns>
        public bool InitiateMove(Dictionary<int, int> coordinatesId, int height, int length,
            List<string> borders, Dictionary<int, EventType> eventTypeDictionary)
        {
            //Initiate
            EventTypeDictionary = eventTypeDictionary;
            EventChanged = false;

            //Get the EventEngine Running
            return EventMovement.SetEventEngine(coordinatesId, eventTypeDictionary, height, length, borders);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Start the whole Party Engine
        ///     Load Inventory
        /// </summary>
        /// <param name="characterId">Id of Start Character</param>
        /// <returns>Initiated Party List</returns>
        public void InitiateParty(int characterId)
        {
            Adventure.Initiate();
            Adventure.AddCharacter(characterId);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load existing Party from the Save
        ///     Load Inventory
        /// </summary>
        /// <param name="characterId">List of Party Members</param>
        /// <returns>Initiated Party List</returns>
        public void LoadParty(List<int> characterId)
        {
            Adventure.Initiate();
            Adventure.LoadCharacters(characterId);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Add a Member to the Party
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id needed</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        public void AddPartyMember(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            var id = EventOutput.GetAssetAsInt(idForFurtherEventInfo, campaignName, mapName);
            Adventure.AddCharacter(id);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Remove a Member to the Party
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id needed</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        public void RemovePartyMember(int idForFurtherEventInfo, string campaignName, string mapName)
        {
            var id = EventOutput.GetAssetAsInt(idForFurtherEventInfo, campaignName, mapName);
            Adventure.RemoveCharacter(id);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Display the Path and the Events we encountered,
        ///     Without Move Limits!
        /// </summary>
        /// <param name="startCoordinateId">Star Point</param>
        /// <param name="targetCoordinateId">Target Id</param>
        public EventTypeDisplay GetDisplay(int startCoordinateId, int targetCoordinateId)
        {
            return EventMovement.GetDisplay(startCoordinateId, targetCoordinateId);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Display the Path and the Events we encountered, reduced to a allowed Move Range
        /// </summary>
        /// <param name="startCoordinateId">Star Point</param>
        /// <param name="targetCoordinateId">Target Id</param>
        /// <param name="maxMove">Allowed Move Range</param>
        public EventTypeDisplay GetDisplay(int startCoordinateId, int targetCoordinateId, int maxMove)
        {
            var eventDisplay = EventMovement.GetDisplay(startCoordinateId, targetCoordinateId);

            //Check if we need to calculate something
            if (!eventDisplay.DisplayPath) return eventDisplay;

            return EventTypeDisplayFactory.GetMovementRange(maxMove);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load existing Party
        /// </summary>
        /// <returns>Initiated Party List</returns>
        public List<int> GetParty()
        {
            return Adventure?.Characters;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Does the dirty work and sets Events inactive
        /// </summary>
        /// <param name="eventId">Id of the Event</param>
        public void SetEventInactive(int eventId)
        {
            if (EventMovement.SetEventInactive(eventId) && !EventChanged) EventChanged = true;
        }
    }
}