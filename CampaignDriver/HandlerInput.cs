/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CampaignDriver/HandlerInputSingleton.cs
 * PURPOSE:     Basic Translator between User Input and Game Engine, Mostly Internal Systems
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using Debugger;
using EventEngine;
using Resources;

// ReSharper disable MemberCanBeMadeStatic.Global, would defeat the purpose of the SingleTon

namespace CampaignDriver
{
    /// <summary>
    ///     Singleton for different Game Engines, Campaign Site, mostly Input
    /// </summary>
    public static class HandlerInputSingleton
    {
        /// <summary>
        ///     The lazy campaign.
        /// </summary>
        private static Lazy<CampaignInput> _lazyCampaign;

        /// <summary>
        ///     Create and Initiate Single Instance of EventInput
        /// </summary>
        /// <returns>Single Instance of EventInput</returns>
        public static CampaignInput Create()
        {
            if (_lazyCampaign != null) return _lazyCampaign.Value;

            DebugLog.CreateLogFile(CampaignDriverResources.InformationStartUpIn, ErCode.Information);
            _lazyCampaign = new Lazy<CampaignInput>(() => new CampaignInput());

            return _lazyCampaign.Value;
        }
    }

    /// <summary>
    ///     Only Initiated once, Interface to the different Engines
    /// </summary>
    public sealed class CampaignInput
    {
        /// <summary>
        ///     Module that handles Processing of Events Input
        /// </summary>
        private static readonly EventInput InputHandle = new();

        /// <summary>
        ///     Initiate the Event Handler
        /// </summary>
        internal CampaignInput()
        {
        }

        /// <summary>
        ///     Generate a party
        /// </summary>
        /// <param name="characterId">Id of character</param>
        /// <returns>Party Object</returns>
        public void InitiateParty(int characterId)
        {
            InputHandle.InitiateParty(characterId);
        }

        /// <summary>
        ///     Add a Party Member
        /// </summary>
        /// <param name="eventAction">Complete Event in question</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        public void AddPartyMember(EventType eventAction, string campaignName, string mapName)
        {
            InputHandle.AddPartyMember(eventAction.IdForFurtherEventInfo, campaignName, mapName);
        }

        /// <summary>
        ///     Add a party Member
        /// </summary>
        /// <param name="eventAction">Complete Event in question</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        public void RemovePartyMember(EventType eventAction, string campaignName, string mapName)
        {
            InputHandle.RemovePartyMember(eventAction.IdForFurtherEventInfo, campaignName, mapName);
        }

        /// <summary>
        ///     Character List from loaded save Game
        /// </summary>
        /// <param name="characterId">List of Party Members</param>
        public void LoadParty(List<int> characterId)
        {
            InputHandle.LoadParty(characterId);
        }

        /// <summary>
        ///     Just Return the actual Party
        /// </summary>
        /// <returns>List of Party Members</returns>
        public List<int> GetParty()
        {
            return InputHandle.GetParty();
        }

        /// <summary>
        ///     Get Movement Path and all Events
        /// </summary>
        /// <param name="idP">Target location on Map</param>
        /// <param name="imagePoint">Start Point on Map</param>
        /// <param name="movementCost">Cost of Movement</param>
        /// <returns>EventTypeDisplay with all Data for processing</returns>
        public EventTypeDisplay GetDisplay(int idP, int imagePoint, int movementCost)
        {
            return InputHandle.GetDisplay(idP, imagePoint, movementCost);
        }

        /// <summary>
        ///     Initiate the Event Engine
        /// </summary>
        /// <param name="coordinatesId">
        ///     Location and Id, Coordinate Id and Id of Event, Event is the first (Key), Coordinate Id is
        ///     second (Value)
        /// </param>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        /// <param name="borders">Borders </param>
        /// <param name="eventTypeDictionary">Event Collection</param>
        /// <returns>Status if initiation was successful, if false try to rollback</returns>
        public bool InitiateMove(Dictionary<int, int> coordinatesId, int height, int length, List<string> borders,
            Dictionary<int, EventType> eventTypeDictionary)
        {
            return InputHandle.InitiateMove(coordinatesId, height, length, borders, eventTypeDictionary);
        }

        /// <summary>
        ///     Set the event to Inactive if possible
        /// </summary>
        /// <param name="eventId">Id of the Event</param>
        public void SetEventInactive(int eventId)
        {
            InputHandle.SetEventInactive(eventId);
        }
    }
}