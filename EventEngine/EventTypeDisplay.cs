/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/EventDisplay.cs
 * PURPOSE:     Helper Object for the Movement and event Display
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using ExtendedSystemObjects;
using Resources;

namespace EventEngine
{
    /// <summary>
    ///     The event type display class. Contains all Events and Traps
    /// </summary>
    public sealed class EventTypeDisplay
    {
        /// <summary>
        ///     Max movement Count
        /// </summary>
        public int MoveRange => PathTravel.Count;

        /// <summary>
        ///     List of Coordinates for the Movement path
        /// </summary>
        public List<int> PathTravel { get; private set; }

        /// <summary>
        ///     List of Coordinates for the PathDisplay
        /// </summary>
        public List<int> PathDisplay { get; private set; }

        /// <summary>
        ///     0. Move
        ///     1. Trap
        ///     2. Move and Display
        ///     3. Do nothing
        ///     4. Display only
        ///     -6. well we clicked on ourselves
        /// </summary>
        public int Typ { get; private set; }

        /// <summary>
        ///     Collection of EventType for execution
        ///     Only on at a Time
        /// </summary>
        public Dictionary<int, EventType> MyEventsTypes { get; private set; }

        /// <summary>
        ///     Collection of EventType for Display
        /// </summary>
        public Dictionary<int, EventType> MyDisplayEventsTypes { get; private set; }

        /// <summary>
        ///     Path Count we can Display
        /// </summary>
        public bool DisplayPath => !PathDisplay.IsNullOrEmpty();

        /// <summary>
        ///     Path Count we actual move
        /// </summary>
        public bool TravelPath => !PathTravel.IsNullOrEmpty();

        /// <summary>
        ///     Are we in Range or not?
        /// </summary>
        public bool DoSomething { get; internal set; }

        /// <summary>
        ///     Cleanup Our act, blank slate Display
        /// </summary>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay StartNew()
        {
            return new EventTypeDisplay
            {
                PathDisplay = new List<int>(),
                PathTravel = new List<int>(),
                DoSomething = true
            };
        }

        /// <summary>
        ///     Display Event
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay DisplayChar(int type)
        {
            return new EventTypeDisplay
            {
                Typ = type,
                MyDisplayEventsTypes = new Dictionary<int, EventType> { { 0, new EventType() } },
                PathDisplay = new List<int>(),
                PathTravel = new List<int>(),
                DoSomething = true
            };
        }

        /// <summary>
        ///     Display Event
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay EventNothing(int type)
        {
            return new EventTypeDisplay
            {
                Typ = type,
                DoSomething = false
            };
        }

        /// <summary>
        ///     Display Event
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <param name="pathtravel">Movement we will do</param>
        /// <param name="pathDisplay">Displayed Movement</param>
        /// <param name="myDisplayEvents">Events we Display</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay EventDisplay(int type, List<int> pathtravel, List<int> pathDisplay,
            Dictionary<int, EventType> myDisplayEvents)
        {
            return new EventTypeDisplay
            {
                Typ = type,
                PathTravel = pathtravel,
                PathDisplay = pathDisplay,
                MyDisplayEventsTypes = myDisplayEvents,
                MyEventsTypes = myDisplayEvents,
                DoSomething = true
            };
        }

        /// <summary>
        ///     Display Event with Trap
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <param name="pathtravel">Movement we will do</param>
        /// <param name="pathDisplay">Displayed Movement</param>
        /// <param name="myDisplayEvents">Events we Display</param>
        /// <param name="trapdisplay">Traps we have triggered</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay EventTrap(int type, List<int> pathtravel, List<int> pathDisplay,
            Dictionary<int, EventType> myDisplayEvents, Dictionary<int, EventType> trapdisplay)
        {
            return new EventTypeDisplay
            {
                Typ = type,
                PathTravel = pathtravel,
                PathDisplay = pathDisplay,
                MyDisplayEventsTypes = myDisplayEvents,
                MyEventsTypes = trapdisplay,
                DoSomething = true
            };
        }

        /// <summary>
        ///     Display Event
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <param name="pathtravel">Movement we will do</param>
        /// <param name="pathDisplay">Displayed Movement</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay EventMove(int type, List<int> pathtravel, List<int> pathDisplay)
        {
            return new EventTypeDisplay
            {
                Typ = type,
                PathTravel = pathtravel,
                PathDisplay = pathDisplay,
                DoSomething = true
            };
        }
    }

    /// <summary>
    ///     The event type display factory class.
    /// </summary>
    internal static class EventTypeDisplayFactory
    {
        /// <summary>
        ///     Generic Display Object
        /// </summary>
        private static EventTypeDisplay _match;

        /// <summary>
        ///     Calculate Max possible Move Range if we have a limited amount
        /// </summary>
        /// <param name="maxMove">Max allows Moves</param>
        /// <returns></returns>
        internal static EventTypeDisplay GetMovementRange(int maxMove)
        {
            var moves = _match.PathTravel.Count;

            //first case
            if (moves <= maxMove) return _match;

            _match.DoSomething = false;
            //just do, nothing interesting is happening!

            _match.PathTravel.RemoveRange(maxMove, moves - maxMove);

            return _match;
        }

        /// <summary>
        ///     Not yet used but might be useful
        /// </summary>
        /// <returns>Generate an empty Display</returns>
        internal static EventTypeDisplay StartNew()
        {
            return EventTypeDisplay.StartNew();
        }

        /// <summary>
        ///     Basic do nothing Event
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <returns>Return Display to do nothing</returns>
        internal static EventTypeDisplay EventNothing(int type)
        {
            return EventTypeDisplay.EventNothing(type);
        }

        /// <summary>
        ///     Display Event with Trap
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <param name="pathtravel">Movement we will do</param>
        /// <param name="pathDisplay">Displayed Movement</param>
        /// <param name="myDisplayEvents">Events we Display</param>
        /// <param name="trapdisplay">Traps we have triggered</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay EventTrap(int type, List<int> pathtravel, List<int> pathDisplay,
            Dictionary<int, EventType> myDisplayEvents, Dictionary<int, EventType> trapdisplay)
        {
            _match = EventTypeDisplay.EventTrap(type, pathtravel, pathDisplay, myDisplayEvents, trapdisplay);
            return _match;
        }

        /// <summary>
        ///     Display Event
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <param name="pathtravel">Movement we will do</param>
        /// <param name="pathDisplay">Displayed Movement</param>
        /// <param name="myDisplayEvents">Events we Display</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay EventDisplay(int type, List<int> pathtravel, List<int> pathDisplay,
            Dictionary<int, EventType> myDisplayEvents)
        {
            _match = EventTypeDisplay.EventDisplay(type, pathtravel, pathDisplay, myDisplayEvents);
            return _match;
        }

        /// <summary>
        ///     Display Event
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay DisplayChar(int type)
        {
            return EventTypeDisplay.DisplayChar(type);
        }

        /// <summary>
        ///     Display Event
        /// </summary>
        /// <param name="type">Type of Event Display</param>
        /// <param name="pathtravel">Movement we will do</param>
        /// <param name="pathDisplay">Displayed Movement</param>
        /// <returns>Return Display</returns>
        internal static EventTypeDisplay EventMove(int type, List<int> pathtravel, List<int> pathDisplay)
        {
            _match = EventTypeDisplay.EventMove(type, pathtravel, pathDisplay);
            return _match;
        }
    }
}