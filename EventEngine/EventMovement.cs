/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/EventMovement.cs
 * PURPOSE:     Basic Handler class, that handles all Events on the Map
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Debugger;
using ExtendedSystemObjects;
using PathFinding;
using Resources;

namespace EventEngine
{
    /// <summary>
    ///     Class that handles all Event Triggers
    /// </summary>
    internal static class EventMovement
    {
        /// <summary>
        ///     The scout.
        /// </summary>
        private static Pathfinder _scout;

        /// <summary>
        ///     Switch here!
        ///     Event is the first (Key), Coordinate Id is second (Value)
        /// </summary>
        private static Dictionary<int, int> _myActiveEvents;

        /// <summary>
        ///     Switch here!
        ///     Event is the first (Key), Coordinate Id is second (Value)
        /// </summary>
        private static Dictionary<int, int> _myActiveTraps;

        /// <summary>
        ///     Location and Id, Coordinate Id and Id of Event, Event is the first (Key), Coordinate Id is second (Value)
        /// </summary>
        private static Dictionary<int, int> _coordinatesId;

        /// <summary>
        ///     The height.
        /// </summary>
        private static int _height;

        /// <summary>
        ///     The length.
        /// </summary>
        private static int _length;

        /// <summary>
        ///     needed in 2 Places so must be global
        ///     Contains the Id of the Event
        /// </summary>
        private static List<int> _trapId;

        /// <summary>
        ///     needed in 2 Places so must be global
        /// </summary>
        private static bool _isTrap;

        /// <summary>
        ///     Deactivates Events with specific Key if not repeatable
        /// </summary>
        /// <param name="eventId">Id of the Event</param>
        /// <returns>was something changed</returns>
        public static bool SetEventInactive(int eventId)
        {
            if (!EventInput.EventTypeDictionary.ContainsKey(eventId))
            {
                DebugLog.CreateLogFile(string.Concat(EventEngineResources.ErrorCouldNotFindEventKey, eventId),
                    ErCode.Error,
                    EventInput.EventTypeDictionary);
                return false;
            }

            if (EventInput.EventTypeDictionary[eventId].IsRepeatable) return false;

            EventInput.EventTypeDictionary[eventId].IsActive = false;

            var events = EventInput.EventTypeDictionary[eventId];

            if (events.IsStepOn)
                ClearmyActiveTraps(eventId);
            else
                ClearmyActiveEvents(eventId);

            return true;
        }

        /// <summary>
        ///     Get thePathfinding Engine run
        ///     And the Movement Engine
        /// </summary>
        /// <param name="coordinatesId">
        ///     Location and Id, Coordinate Id and Id of Event, Event is the first (Key), Coordinate Id is
        ///     second (Value)
        /// </param>
        /// <param name="eventTypeDictionary">Id and Event Description</param>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        /// <param name="mapBorders">Border Map</param>
        /// <returns>If everything worked</returns>
        internal static bool SetEventEngine(Dictionary<int, int> coordinatesId,
            Dictionary<int, EventType> eventTypeDictionary, int height, int length, List<string> mapBorders)
        {
            //Set Data
            _coordinatesId = coordinatesId;
            _height = height;
            _length = length;

            //Convert the Map
            var borders = SaveBorderMapToArray(mapBorders, height, length);

            //catch the error, rollback needed
            if (borders == null) return false;

            //Initiate Map Engine
            _scout = new Pathfinder(borders);

            //No Events? Continue
            if (eventTypeDictionary.IsNullOrEmpty())
            {
                _myActiveEvents = _myActiveTraps = new Dictionary<int, int>();
                return true;
            }

            //Initiate variables
            _trapId = new List<int>();

            //Get Active Events without Traps
            _myActiveEvents = GetEventList(eventTypeDictionary, false);
            //Get Active Traps
            _myActiveTraps = GetEventList(eventTypeDictionary, true);

            return true;
        }

        /// <summary>
        ///     Sorts Events by Traps and Click events sorts out the Start Point
        /// </summary>
        /// <param name="eventTypes">All Events</param>
        /// <param name="istrap">Trap or not</param>
        /// <returns>Sorted Event List</returns>
        private static Dictionary<int, int> GetEventList(Dictionary<int, EventType> eventTypes, bool istrap)
        {
            var dct = new Dictionary<int, int>();

            foreach (var element in eventTypes)
                if (element.Value.IsActive && element.Value.IsStepOn == istrap &&
                    //references the Key in CoordinatesId, Value of it represents Coordinate Value as Id
                    _coordinatesId.ContainsKey(element.Value.CoordinatesId))
                {
                    //Coordinate ID
                    var coordinate = _coordinatesId[element.Value.CoordinatesId];

                    dct.Add(element.Key, coordinate);
                }

            return dct;
        }

        /// <summary>
        ///     Calculates Movement Path
        ///     Collects all possible Events
        ///     Collects all Events that can be displayed
        /// </summary>
        /// <param name="startCoordinateId">Starting Point</param>
        /// <param name="targetCoordinateId">Possible target Tile</param>
        /// <returns>Collected Event Data</returns>
        internal static EventTypeDisplay GetDisplay(int startCoordinateId, int targetCoordinateId)
        {
            var type = EventEngineResources.EventNothing;
            _isTrap = false;
            var next = false;
            _trapId = new List<int>();
            var avaibleCoordinates = new List<int>();

            EventTypeDisplayFactory.StartNew();

            //calculations
            var pathtravel = _scout.GetPath(startCoordinateId, targetCoordinateId, false);
            var myDisplayEvents = IsEventClick(targetCoordinateId);

            //Parameters
            var clickEvent = false;

            //Calculate Parameters
            var blocked = _scout.IsTileBlocked(targetCoordinateId);

            if (pathtravel.Count != 1) blocked = false;

            if (myDisplayEvents != null) clickEvent = true;

            //Handle the non Block
            if (!blocked) type = EventEngineResources.EventMove;

            //now we need the new Coordinates
            if (blocked) avaibleCoordinates = Surroundings.GenerateCoordinates(targetCoordinateId, _height, _length);

            //return if already here, no movement path
            if (blocked) next = Checkpoint(startCoordinateId, avaibleCoordinates);

            if (clickEvent && next) type = EventEngineResources.EventDisplay;

            //Checks if Point is identical, so you want to just open the char menu
            if (startCoordinateId == targetCoordinateId) type = EventEngineResources.DisplayChar;

            //Checks if Point is valid, is it blocked or even possible?
            if (clickEvent && !next)
            {
                //now check for the fastest point allowed near the event
                pathtravel = GetNewPoint(startCoordinateId, avaibleCoordinates);

                //Set new Type
                type = pathtravel.Count == 1 ? EventEngineResources.EventNothing : EventEngineResources.EventDisplay;
            }

            //Calculate Display
            var pathDisplay = GetDisplayPath(pathtravel);

            //calculate Traps
            pathtravel = SetTrapCoordinatesId(pathtravel);

            if (_isTrap) type = EventEngineResources.EventTrap;

            switch (type)
            {
                case EventEngineResources.EventNothing:
                    return EventTypeDisplayFactory.EventNothing(type);

                case EventEngineResources.DisplayChar:
                    return EventTypeDisplayFactory.DisplayChar(type);

                case EventEngineResources.EventMove:
                    return EventTypeDisplayFactory.EventMove(type, pathtravel, pathDisplay);

                case EventEngineResources.EventDisplay:
                    if (pathtravel.IsNullOrEmpty() || pathtravel.Count == 1) pathtravel = pathDisplay = new List<int>();

                    return EventTypeDisplayFactory.EventDisplay(type, pathtravel, pathDisplay, myDisplayEvents);

                case EventEngineResources.EventTrap:

                    //_myActiveTraps: Event is the first(Key) unique, Coordinate Id is second (Value)
                    //Convert Ids to Trap Infos
                    var trapdisplay = new Dictionary<int, EventType>();

                    foreach (var traps in _trapId)
                    {
                        var eventCoordinate = EventInput.EventTypeDictionary[traps];
                        trapdisplay.Add(traps, eventCoordinate);
                    }

                    return EventTypeDisplayFactory.EventTrap(type, pathtravel, pathDisplay, myDisplayEvents,
                        trapdisplay);
            }

            return EventTypeDisplayFactory.EventNothing(type);
        }

        /// <summary>
        ///     Checks if you are already next to the event
        /// </summary>
        /// <param name="startCoordinateId">Start Point for the path</param>
        /// <param name="avaibleCoordinates">Endpoint of the path</param>
        /// <returns>True we are next to the event else false</returns>
        private static bool Checkpoint(int startCoordinateId, IEnumerable<int> avaibleCoordinates)
        {
            return avaibleCoordinates.Any(
                coordinates => startCoordinateId == coordinates);
        }

        /// <summary>
        ///     Returns fastest possible point and path to the Event
        /// </summary>
        /// <param name="startCoordinateId">Start Point for the path</param>
        /// <param name="avaibleCoordinates">List of possible points always eight</param>
        /// <returns>List of Coordinates that describes the path</returns>
        private static List<int> GetNewPoint(int startCoordinateId, IEnumerable<int> avaibleCoordinates)
        {
            var path = new List<int>();
            var firstone = true;

            foreach (
                var mypath in
                avaibleCoordinates.Select(coordinates => _scout.GetPath(startCoordinateId, coordinates, false))
            )
            {
                if (firstone && mypath.Count > 1)
                {
                    path = mypath;
                    firstone = false;
                }

                //Empty path
                if (mypath.Count == 1) continue;
                //else
                if (mypath.Count < path.Count) path = mypath;
            }

            return path;
        }

        /// <summary>
        ///     Returns Tiles that will be illuminated for the Movement Path
        ///     Removes part of the Segments
        /// </summary>
        /// <param name="pathtravel">Movement Path</param>
        /// <returns>List of Coordinates that describes the Display Path</returns>
        private static List<int> GetDisplayPath(List<int> pathtravel)
        {
            if (pathtravel.IsNullOrEmpty()) return new List<int>();

            var mypath = new List<int>(pathtravel);
            mypath.RemoveAt(0);
            return mypath;
        }

        /// <summary>
        ///     Checks if Click is event on Coordinate and add all Events to a Dictionary
        ///     _coordinatesId: Event is the first (Key), Coordinate Id is second (Value)
        /// </summary>
        /// <param name="coordinateId">Target Coordinates</param>
        /// <returns>Dictionary with Id and Event Description, can return null.</returns>
        private static Dictionary<int, EventType> IsEventClick(int coordinateId)
        {
            List<int> lst;

            if (_myActiveEvents.ContainsValue(coordinateId))
                lst = _myActiveEvents.GetKeysByValue(coordinateId);
            else
                return null;

            return EventInput.EventTypeDictionary.GetDictionaryByValues(lst);
        }

        /// <summary>
        ///     Returns the Movement Path, uses _myActiveTraps, Collection of Traps
        ///     Check if it is a Trap
        ///     Gets Id of Trap
        ///     _coordinatesId: Event is the first (Key), Coordinate Id is second (Value)
        ///     Uses an Delegate
        /// </summary>
        /// <param name="pathtravel">Movement Path as int Id</param>
        /// <returns>List of Coordinates that describes the Movement Path with optional Trap, can return null.</returns>
        private static List<int> SetTrapCoordinatesId(List<int> pathtravel)
        {
            if (pathtravel.IsNullOrEmpty()) return null;

            //we remove the first element which we are standing on, if we are on a trap ignore it
            var cache = new List<int>();
            var count = -1;

            //No events? Well than return
            if (_myActiveTraps.IsNullOrEmpty()) return pathtravel;

            //Uses an Delegate, I just don't know why anymore
            void Work()
            {
                foreach (var pointTravel in pathtravel)
                {
                    cache.Add(pointTravel);

                    count++;
                    //catch first step
                    if (count == 0) continue;

                    //collect all Events with the same Value, aka Coordinate
                    if (_myActiveTraps.ContainsValue(pointTravel)) _trapId = _myActiveTraps.GetKeysByValue(pointTravel);

                    //If we found something return!
                    if (_trapId.IsNullOrEmpty()) continue;

                    _isTrap = true;
                    return;
                }
            }

            Work();

            return cache;
        }

        /// <summary>
        ///     Cleans up List with Events, sets new Values
        /// </summary>
        /// <param name="eventId">Id of changed Event</param>
        private static void ClearmyActiveEvents(int eventId)
        {
            if (_myActiveEvents.Equals(null))
            {
                DebugLog.CreateLogFile(EventEngineResources.WarningClearEvents, ErCode.Warning);

                return;
            }

            _myActiveEvents.Remove(eventId);
        }

        /// <summary>
        ///     Cleans up List with Events, sets new Values
        /// </summary>
        /// <param name="eventId">Id of changed Event</param>
        private static void ClearmyActiveTraps(int eventId)
        {
            if (_myActiveTraps.Equals(null))
            {
                DebugLog.CreateLogFile(EventEngineResources.WarningClearEvents, ErCode.Warning);
                return;
            }

            _myActiveTraps.Remove(eventId);
        }

        /// <summary>
        ///     Converts List to Array
        /// </summary>
        /// <returns>Map as Array</returns>
        /// <param name="mapBorders">Border Map</param>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        /// <returns>Converted Array Map, can return null.</returns>
        private static int[,] SaveBorderMapToArray(List<string> mapBorders, int height, int length)
        {
            if (mapBorders.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(EventEngineResources.ErrorBorders, 0);
                return null;
            }

            height *= EventEngineResources.CellCount;
            length *= EventEngineResources.CellCount;

            var border = new int[length, height];

            var borderArray = mapBorders.ToArray();

            for (var y = 0; y < height; y++) // Map.IntMapLengthX
            {
                var root = borderArray[y];
                var file = root.Split(EventEngineResources.MapSplitter);

                for (var x = 0; x < length; x++) border[x, y] = Convert.ToInt16(file[x], CultureInfo.InvariantCulture);
            }

            return border;
        }
    }
}