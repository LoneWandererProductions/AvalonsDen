/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/EventContainer.cs
 * PURPOSE:     Description of Event
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace Resources
{
    /// <summary>
    ///     Collection of all Files needed for one Map
    ///     CoordinatesId, Key is the Event Id, Value is the Coordinate on the Map
    ///     EventTypeDictionary contains the Information about the Event
    ///     EventTypeExtensionDictionary contains further instructions based on the Type of the Event
    /// </summary>
    public sealed class EventContainer
    {
        /// <summary>
        ///     Dictionary with Id, EventType with Id(IdForFurtherEventInfo) to add basic info for the Event in
        ///     EventTypeExtensionDictionary
        /// </summary>
        public Dictionary<int, EventType> EventTypeDictionary { get; set; }

        /// <summary>
        ///     Dictionary with Id, EventAsset with Id of Event to add further info for the Event
        ///     id of Asset and string with further syntax
        ///     Value is only used.
        ///     Key is unused for the Game logic
        /// </summary>
        public Dictionary<int, EventTypeExtension> EventTypeExtensionDictionary { get; set; }

        /// <summary>
        ///     Dictionary with Coordinate Id and Event Id
        ///     Multiple Events per Coordinate possible
        ///     Multiple Events per Coordinate, that's why we have this strange construct
        ///     Drawback no Event for Multiple Coordinates -> Solution create Event that redirects to an existing Event, or Just
        ///     copy it ...
        ///     Event Dictionary(Key, EventType)
        ///     Id to Coordinates, Reference to CoordinatesId Dictionary(Event.Key, EventType.CoordinatesId)
        /// </summary>
        public Dictionary<int, int> CoordinatesId { get; set; }
    }
}