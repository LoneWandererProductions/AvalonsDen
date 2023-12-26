/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Engine/EditorEventHandler.cs
 * PURPOSE:     Part of the Editor Window, Basic Handler for Transitions, also transmits all Events into Cells for display
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using AvalonRuntime;
using Resources;

namespace Editors
{
    /// <summary>
    ///     Displays all Events
    /// </summary>
    internal static class EditorEventHandler
    {
        /// <summary>
        ///     The length.
        /// </summary>
        private static int _length;

        /// <summary>
        ///     Initiation
        ///     Only done once at Startup for a new Map
        /// </summary>
        internal static void BuildNewMap(int length)
        {
            //Initiate the basics
            EditorRegister.SetEventTypeDictionary();
            InitiateValues(length);
        }

        /// <summary>
        ///     Initiation
        ///     Only done once at Startup for an existing Map
        ///     Transfers Object to List(Coordinates)
        /// </summary>
        /// <param name="maxLayer">Max Layer</param>
        /// <returns></returns>
        internal static List<Coordinates> RebuildMap(int maxLayer)
        {
            //something went really wrong
            if (EditorRegister.MapObjct == null) return null;

            //initiate Values
            InitiateValues(EditorRegister.MapObjct.Length);

            //if null do nothing of course, still possible though and initiate
            if (EditorRegister.EventTypeObjct.CoordinatesId == null)
            {
                EditorRegister.EventTypeObjct.CoordinatesId = new Dictionary<int, int>();
                return null;
            }

            //Just Paint the Events on the Map
            var eventlist =
                EditorRegister.EventTypeObjct.CoordinatesId.Values.Select(
                        id => ArtShared.IdToCoordinate(id, _length, maxLayer,
                            EditorResources.SymbolOfEvent))
                    .ToList();

            return eventlist;
        }

        /// <summary>
        ///     Get Tile Register
        /// </summary>
        /// <param name="length">Length of Map</param>
        private static void InitiateValues(int length)
        {
            _length = length;
        }

        /// <summary>
        ///     Check Status of Id
        /// </summary>
        /// <param name="id">id of Event</param>
        /// <returns>Event Registered?</returns>
        internal static bool CheckEvenStatus(int id)
        {
            return EditorRegister.EventTypeObjct.CoordinatesId.ContainsValue(id);
        }

        /// <summary>
        ///     Delete Event
        /// </summary>
        /// <param name="id">id of Target Point</param>
        internal static void DeleteEvent(int id)
        {
            if (EditorRegister.EventTypeObjct.CoordinatesId.ContainsKey(id))
                EditorRegister.EventTypeObjct.CoordinatesId.Remove(id);
        }

        /// <summary>
        ///     Add Event
        /// </summary>
        /// <param name="item">Target Point</param>
        /// <param name="id">id of Target Point</param>
        /// <returns>Changed Coordinate </returns>
        public static void AddEvent(Coordinates item, int id)
        {
            var count = EditorRegister.EventTypeObjct.CoordinatesId.Count;

            //Feed to Cells
            item.TileId = EditorResources.SymbolOfEvent;

            //Add multiple Events with the same Coordinate Id, working now
            EditorRegister.EventTypeObjct.CoordinatesId.Add(count, id);
        }
    }
}