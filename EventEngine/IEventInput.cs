/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/IEventInput.cs
 * PURPOSE:     Basic Interface for EventInput, that handles all Events on the Map, interplay between Input and Output
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace EventEngine
{
    /// <summary>
    ///     The IEventInput interface.
    /// </summary>
    internal interface IEventInput
    {
        /// <summary>
        ///     The initiate move.
        /// </summary>
        /// <param name="coordinatesId">The coordinatesId.</param>
        /// <param name="height">The height.</param>
        /// <param name="length">The length.</param>
        /// <param name="borders">The borders.</param>
        /// <param name="eventTypeDictionary">The eventTypeDictionary.</param>
        /// <returns>The <see cref="bool" />.</returns>
        bool InitiateMove(Dictionary<int, int> coordinatesId, int height, int length, List<string> borders,
            Dictionary<int, EventType> eventTypeDictionary);

        /// <summary>
        ///     The initiate party.
        /// </summary>
        /// <param name="characterId">The characterId.</param>
        void InitiateParty(int characterId);

        /// <summary>
        ///     Load the party.
        /// </summary>
        /// <param name="characterId">The characterId.</param>
        void LoadParty(List<int> characterId);

        /// <summary>
        ///     Add the party member.
        /// </summary>
        /// <param name="idForFurtherEventInfo">The idForFurtherEventInfo.</param>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="mapName">The mapName.</param>
        void AddPartyMember(int idForFurtherEventInfo, string campaignName, string mapName);

        /// <summary>
        ///     Remove the party member.
        /// </summary>
        /// <param name="idForFurtherEventInfo">The idForFurtherEventInfo.</param>
        /// <param name="campaignName">The campaign Name.</param>
        /// <param name="mapName">The map Name.</param>
        void RemovePartyMember(int idForFurtherEventInfo, string campaignName, string mapName);

        /// <summary>
        ///     Get the display.
        /// </summary>
        /// <param name="startCoordinateId">The startCoordinateId.</param>
        /// <param name="targetCoordinateId">The targetCoordinateId.</param>
        /// <returns>The <see cref="EventTypeDisplay" />.</returns>
        EventTypeDisplay GetDisplay(int startCoordinateId, int targetCoordinateId);

        /// <summary>
        ///     Get the display.
        /// </summary>
        /// <param name="startCoordinateId">The startCoordinateId.</param>
        /// <param name="targetCoordinateId">The targetCoordinateId.</param>
        /// <param name="maxMove">The maxMove.</param>
        /// <returns>The <see cref="EventTypeDisplay" />.</returns>
        EventTypeDisplay GetDisplay(int startCoordinateId, int targetCoordinateId, int maxMove);

        /// <summary>
        ///     Get the party.
        /// </summary>
        /// <returns>The <see cref="T:List{int}" />.</returns>
        List<int> GetParty();

        /// <summary>
        ///     Set the event inactive.
        /// </summary>
        /// <param name="eventId">The eventId.</param>
        void SetEventInactive(int eventId);
    }
}