/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/ResourcesEventEngine.cs
 * PURPOSE:     Holds the Data for basic Event Testing
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

namespace AvalonsDenTests
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
        ///     The border two (readonly). Value: new List&lt;int&gt; { 6, 7 }.
        /// </summary>
        internal static readonly List<int> BorderTwo = new() {6, 7};

        /// <summary>
        ///     0, Look event,
        ///     1, Talk event,
        ///     Event is the first (Key), Coordinate Id is second (Value)
        /// </summary>
        internal static readonly Dictionary<int, int> EventIdOne = new() {{0, 9}, {1, 3}};

        /// <summary>
        ///     4, Look event,
        ///     Event is the first (Key), Coordinate Id is second (Value)
        /// </summary>
        internal static readonly Dictionary<int, int> EventIdTwo = new() {{4, 6}};

        /// <summary>
        ///     Reduced EventType Dictionary
        /// </summary>
        internal static readonly Dictionary<int, EventType> EventTypeWithTrap = new()
        {
            {
                0,
                new EventType
                {
                    CoordinatesId = 0,
                    IsStepOn = true,
                    IsRepeatable = true,
                    IsActive = true,
                    TypeOfEvent = EventType.TypeOfEvents.Look,
                    Description = "Look test Event"
                }
            },
            {
                1,
                new EventType
                {
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
                    CoordinatesId = 2,
                    IsStepOn = true,
                    IsRepeatable = false,
                    IsActive = true,
                    TypeOfEvent = EventType.TypeOfEvents.Look,
                    Description = "Look test Event"
                }
            }
        };

        /// <summary>
        ///     Reduced EventType Dictionary
        /// </summary>
        internal static readonly Dictionary<int, EventType> EventSingle = new()
        {
            {
                4,
                new EventType
                {
                    CoordinatesId = 4,
                    IsStepOn = false,
                    IsRepeatable = true,
                    IsActive = true,
                    TypeOfEvent = EventType.TypeOfEvents.Look,
                    Description = "Look test Event"
                }
            }
        };
    }
}