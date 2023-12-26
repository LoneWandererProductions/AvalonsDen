/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTestsCampaign/ResourcesEventEngine.cs
 * PURPOSE:     Holds the Data for basic Event Testing
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     The resources event engine class.
    /// </summary>
    internal static class ResourcesEventEngine
    {
        /// <summary>
        ///     The height (const). Value: 3.
        /// </summary>
        internal const int Height = 3;

        /// <summary>
        ///     The length (const). Value: 4.
        /// </summary>
        internal const int Length = 4;

        /// <summary>
        ///     The tile id (const). Value: 18.
        /// </summary>
        internal const int TileId = 18;

        /// <summary>
        ///     The layer (const). Value: 1.
        /// </summary>
        internal const int Layer = 1;

        /// <summary>
        ///     The border one (readonly). Value: new List&lt;int&gt; { 1, 2, 3, 5, 7, 11 }.
        /// </summary>
        internal static readonly List<int> BorderOne = new() {1, 2, 3, 5, 7, 11};

        /// <summary>
        ///     0, Gold event,
        ///     1, Talk event,
        ///     2, Items event,
        ///     Event is the first (Key), Coordinate Id is second (Value)
        ///     9 is Coordinate Id,
        ///     3 is Coordinate Id
        ///     0,1,2 is the EventType Dictionary Key
        ///     Key Value( EventType ,CoordinatesId)
        ///     Event Dictionary(Key, EventType)
        ///     Id to Coordinates, Reference to CoordinatesId Dictionary(Event.Key, EventType.CoordinatesId)
        /// </summary>
        internal static readonly Dictionary<int, int> EventIdOne = new() {{0, 9}, {1, 3}, {2, 9}};

        /// <summary>
        ///     Reduced EventType Dictionary, with items
        /// </summary>
        internal static readonly Dictionary<int, EventType> EventTypeWithAddItem = new()
        {
            {
                0,
                new EventType
                {
                    //references the Coordinate Id Key
                    CoordinatesId = 0,
                    IsStepOn = true,
                    IsRepeatable = true,
                    IsActive = true,
                    TypeOfEvent = EventType.TypeOfEvents.AddGold,
                    Description = "Add Gold test Event",
                    IdForFurtherEventInfo = 0
                }
            },
            {
                1,
                new EventType
                {
                    //references the Coordinate Id Key
                    CoordinatesId = 1,
                    IsStepOn = false,
                    IsRepeatable = false,
                    IsActive = true,
                    TypeOfEvent = EventType.TypeOfEvents.Talk,
                    Description = "Talk test Event"
                }
            },
            {
                2,
                new EventType
                {
                    //references the Coordinate Id Key
                    CoordinatesId = 2,
                    IsStepOn = true,
                    IsRepeatable = true,
                    IsActive = true,
                    TypeOfEvent = EventType.TypeOfEvents.AddItems,
                    Description = "Add Items test Event",
                    IdForFurtherEventInfo = 1
                }
            }
        };

        /// <summary>
        ///     Reduced EventType Dictionary, with gold only
        ///     Key Value( EventType ,CoordinatesId)
        ///     Event Dictionary(Key, EventType)
        ///     Id to Coordinates, Reference to CoordinatesId Dictionary(Event.Key, EventType.CoordinatesId)
        /// </summary>
        internal static readonly Dictionary<int, EventType> EventTypeWithAddGold = new()
        {
            {
                0,
                new EventType
                {
                    //references the Coordinate Id Key
                    CoordinatesId = 0,
                    IsStepOn = true,
                    IsRepeatable = true,
                    IsActive = true,
                    TypeOfEvent = EventType.TypeOfEvents.AddGold,
                    Description = "Add Gold test Event"
                }
            },
            {
                1,
                new EventType
                {
                    //references the Coordinate Id Key
                    CoordinatesId = 1,
                    IsStepOn = false,
                    IsRepeatable = false,
                    IsActive = true,
                    TypeOfEvent = EventType.TypeOfEvents.Talk,
                    Description = "Talk test Event"
                }
            }
        };

        /// <summary>
        ///     Further Informations
        /// </summary>
        internal static Dictionary<int, EventTypeExtension> EventTypeItem { get; } = new()
        {
            {
                0,
                new EventTypeExtension
                {
                    Id = 0,
                    Value = "100"
                }
            },
            {
                2,
                new EventTypeExtension
                {
                    Id = 1,
                    Value = "(0    ,1 ) ( 4,2 )        (2,3)"
                }
            }
        };
    }
}