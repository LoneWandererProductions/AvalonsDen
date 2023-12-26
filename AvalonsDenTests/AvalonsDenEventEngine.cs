/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenEventEngine.cs
 * PURPOSE:     Basic Tests for Avalon Event Engine
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AvalonRuntime;
using CampaignDriver;
using EventEngine;
using ExtendedSystemObjects;
using FileHandler;
using MapGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;

// ReSharper disable ArrangeBraces_foreach

namespace AvalonsDenTests
{
    /// <summary>
    ///     Basic Event Tests not complete yet
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenEventEngine
    {
        /// <summary>
        ///     The output (readonly). Value: new EventOutput().
        /// </summary>
        private static readonly EventOutput Output = new();

        /// <summary>
        ///     The input.
        /// </summary>
        private static EventInput _input;

        /// <summary>
        ///     Map Layout: 4 * 3
        ///     Name:       EngineTest
        ///     Campaign:   AvalonsDenTests
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     Border Base:
        ///     1|1|1|1|1|1|1|1|1|1|1|1
        ///     1|0|0|0|0|0|0|0|0|0|0|1
        ///     1|0|1|1|0|1|1|0|1|1|0|1
        ///     1|0|1|1|0|1|1|0|1|1|0|1
        ///     1|0|0|0|0|0|0|0|0|0|0|1
        ///     1|0|1|1|0|1|1|0|1|1|0|1
        ///     1|0|1|1|0|1|1|0|1|1|0|1
        ///     1|0|0|0|0|0|0|0|0|0|0|1
        ///     1|1|1|1|1|1|1|1|1|1|1|1
        ///     Border with Blockers:
        ///     1|1|1|1|1|1|1|1|1|1|1|1
        ///     1|0|0|1|1|1|1|1|1|1|1|1
        ///     1|0|1|1|1|1|1|1|1|1|1|1
        ///     1|0|1|1|1|1|1|0|1|1|1|1
        ///     1|0|0|1|1|1|0|0|0|1|1|1
        ///     1|0|1|1|1|1|1|0|1|1|1|1
        ///     1|0|1|1|0|1|1|0|1|1|1|1
        ///     1|0|0|0|0|0|0|0|0|1|1|1
        ///     1|1|1|1|1|1|1|1|1|1|1|1
        ///     New Route:
        ///     0111
        ///     0101
        ///     0001
        ///     1 is pass ways, Blockers: 1,2,3,5,7,11
        ///     Just a basic Move and Event Trigger Test, nothing deep yet. Shall be the base for further more complex Tests
        /// </summary>
        [TestMethod]
        public void InitiateEvents()
        {
            _input = new EventInput();

            //Get all pieces in place
            BasicInitiation(ResourcesEventEngine.EventTypeWithTrap, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderOne, ResourcesEventEngine.EventIdOne);

            //Look Event
            var eventR = _input.GetDisplay(0, 3);
            DisplayEventData(eventR);

            DisplayMovementData(eventR.PathTravel, true);
            DisplayMovementData(eventR.PathDisplay, false);

            Assert.IsFalse(eventR.PathTravel.IsNullOrEmpty(), "Something went wrong, this should not be empty");

            //Move
            Assert.AreEqual(4, eventR.PathTravel.Count, "Stepped on Trap");
            //Display
            Assert.AreEqual(5, eventR.PathDisplay.Count, "Correct Display");

            //EventInputHandler Event
            eventR = _input.GetDisplay(9, 3);
            DisplayEventData(eventR);

            DisplayMovementData(eventR.PathTravel, true);
            DisplayMovementData(eventR.PathDisplay, false);

            Assert.AreEqual(3, eventR.PathTravel.Count, "Reached Target");
            //Display
            Assert.AreEqual(2, eventR.PathDisplay.Count, "Correct Display");
        }

        /// <summary>
        ///     Map Layout: 4 * 3
        ///     Name:       EngineTest
        ///     Campaign:   AvalonsDenTests
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     Border Base:
        ///     1|1|1|1|1|1|1|1|1|1|1|1
        ///     1|0|0|0|0|0|0|0|0|0|0|1
        ///     1|0|1|1|0|1|1|0|1|1|0|1
        ///     1|0|1|1|0|1|1|0|1|1|0|1
        ///     1|0|0|0|0|0|0|0|0|0|0|1
        ///     1|0|1|1|0|1|1|0|1|1|0|1
        ///     1|0|1|1|0|1|1|0|1|1|0|1
        ///     1|0|0|0|0|0|0|0|0|0|0|1
        ///     1|1|1|1|1|1|1|1|1|1|1|1
        ///     Border with Blockers:
        ///     1|1|1|1|1|1|1|1|1|1|1|1
        ///     1|0|0|1|1|1|1|1|1|1|1|1
        ///     1|0|1|1|1|1|1|1|1|1|1|1
        ///     1|0|1|1|1|1|1|0|1|1|1|1
        ///     1|0|0|1|1|1|0|0|0|1|1|1
        ///     1|0|1|1|1|1|1|0|1|1|1|1
        ///     1|0|1|1|0|1|1|0|1|1|1|1
        ///     1|0|0|0|0|0|0|0|0|1|1|1
        ///     1|1|1|1|1|1|1|1|1|1|1|1
        ///     New Route:
        ///     0111
        ///     0101
        ///     0001
        ///     1 is pass ways, Blockers: 1,2,3,5,7,11
        /// </summary>
        [TestMethod]
        public void InitiateEventsMaxMovement()
        {
            _input = new EventInput();

            //Get all pieces in place
            BasicInitiation(ResourcesEventEngine.EventTypeWithTrap, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderOne, ResourcesEventEngine.EventIdOne);

            //Look Event
            var eventR = _input.GetDisplay(0, 3, 2);
            DisplayEventData(eventR);

            DisplayMovementData(eventR.PathTravel, true);
            DisplayMovementData(eventR.PathDisplay, false);

            Assert.IsFalse(eventR.PathTravel.IsNullOrEmpty(), "Something went wrong, this should not be empty");
            //Move
            Assert.AreEqual(2, eventR.PathTravel.Count, "Stepped on Trap 1");
            //Display
            Assert.AreEqual(5, eventR.PathDisplay.Count, "Correct Display 1");
            //Check if Display
            Assert.IsTrue(eventR.DisplayPath, "Correct Check 1");
            //Check Point
            Assert.AreEqual(4, eventR.PathTravel.Last(), "Correct Move for 0 to 2");
            //Check Active
            Assert.IsFalse(eventR.DoSomething, "Check if we do something 1");

            //Look Event
            eventR = _input.GetDisplay(0, 3, 5);
            DisplayEventData(eventR);

            DisplayMovementData(eventR.PathTravel, true);
            DisplayMovementData(eventR.PathDisplay, false);

            //Move
            Assert.AreEqual(4, eventR.PathTravel.Count, "Stepped on Trap 2");
            //Display
            Assert.AreEqual(5, eventR.PathDisplay.Count, "Correct Display 2");
            //Check if Display
            Assert.IsTrue(eventR.DisplayPath, "Correct Check 2");
            //Check Point
            Assert.AreEqual(9, eventR.PathTravel.Last(), "Correct Move for 0 to 9");
            //Check Active
            Assert.IsTrue(eventR.DoSomething, "Check if we do something 1");

            //Get all pieces in place, Reboot
            BasicInitiation(ResourcesEventEngine.EventTypeWithTrap, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderOne, ResourcesEventEngine.EventIdOne);

            //Look Event
            eventR = _input.GetDisplay(0, 3, 3);

            DisplayMovementData(eventR.PathTravel, true);
            DisplayMovementData(eventR.PathDisplay, false);

            //Move
            Assert.AreEqual(3, eventR.PathTravel.Count, "Stepped on Trap 3");
            //Display
            Assert.AreEqual(5, eventR.PathDisplay.Count, "Correct Display 3");
            //Check if Display
            Assert.IsTrue(eventR.DisplayPath, "Correct Check 3");

            DisplayEventData(eventR);
        }

        /// <summary>
        ///     Map Layout: 4 * 3
        ///     Name:       EngineTest
        ///     Campaign:   AvalonsDenTests
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     Border Base:
        ///     0111
        ///     0101
        ///     0001
        ///     Just Test if the whole thing is empty
        /// </summary>
        [TestMethod]
        public void InitiateEventEmpty()
        {
            _input = new EventInput();

            //Get all pieces in place, Reboot
            BasicInitiation(new Dictionary<int, EventType>(), ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderOne, ResourcesEventEngine.EventIdOne);

            //Look Event
            var eventR = _input.GetDisplay(0, 6, 3);

            DisplayMovementData(eventR.PathTravel, true);
            DisplayMovementData(eventR.PathDisplay, false);

            //Move
            Assert.AreEqual(3, eventR.PathTravel.Count, "Expected Path 3, Type: " + eventR.Typ);
            //Display
            Assert.AreEqual(5, eventR.PathDisplay.Count, "Correct Display 3, Type: " + eventR.Typ);
            //Check if Display
            Assert.IsTrue(eventR.DisplayPath, "Correct Check 3, Type: " + eventR.Typ);

            DisplayEventData(eventR);
        }

        /// <summary>
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     New Route:
        ///     0111
        ///     0101
        ///     0001
        ///     1 is pass ways, Blockers: 1,2,3,5,7,11
        ///     Trap: is 9
        ///     Test do nothing
        ///     Test Self Click
        /// </summary>
        [TestMethod]
        public void InitiateEventsNonMoveEventDisplay()
        {
            _input = new EventInput();

            //Get all pieces in place
            BasicInitiation(ResourcesEventEngine.EventTypeWithTrap, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderOne, ResourcesEventEngine.EventIdOne);

            //do nothing
            var eventR = _input.GetDisplay(6, 3, 2);
            Assert.IsTrue(eventR.DoSomething, "Do nothing! One, boolean: " + eventR.Typ);

            Assert.AreEqual(4, eventR.Typ, "Do nothing! One, Type: " + eventR.Typ);

            eventR = _input.GetDisplay(6, 6, 2);

            Assert.AreEqual(-6, eventR.Typ, "Self Click Correct Type: " + eventR.Typ);
        }

        /// <summary>
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     New Route:
        ///     0111
        ///     0101
        ///     0001
        ///     1 is pass ways, Blockers: 1,2,3,5,7,11
        ///     Trap: is 9
        /// </summary>
        [TestMethod]
        public void InitiateEventsInactiveDisplay()
        {
            _input = new EventInput();

            //Get all pieces in place
            BasicInitiation(ResourcesEventEngine.EventTypeWithTrap, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderOne, ResourcesEventEngine.EventIdOne);

            var eventR = _input.GetDisplay(0, 3, 2);

            Assert.IsFalse(eventR.PathTravel.IsNullOrEmpty(), "Something went wrong, this should not be empty");

            Assert.IsFalse(eventR.DoSomething, "Do nothing! One");
            Debug.WriteLine("Last Step " + eventR.PathTravel.Last());

            eventR = _input.GetDisplay(eventR.PathTravel.Last(), 3, 8);
            Assert.IsTrue(eventR.DoSomething, "Do nothing! Two");
            Assert.AreEqual(9, eventR.PathTravel.Last(), "Trap step");
            Debug.WriteLine("Last Step " + eventR.PathTravel.Last());

            eventR = _input.GetDisplay(eventR.PathTravel.Last(), 3, 2);
            Assert.IsFalse(eventR.DoSomething, "Do nothing! Three");

            eventR = _input.GetDisplay(eventR.PathTravel.Last(), 3, 5);
            Assert.IsTrue(eventR.DoSomething, "Do nothing! Four");
        }

        /// <summary>
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     New Route:
        ///     0111
        ///     0101
        ///     0001
        ///     1 is pass ways, Blockers: 1,2,3,5,7,11
        ///     Trap: is 9
        /// </summary>
        [TestMethod]
        public void InitiateEventsPlainField()
        {
            _input = new EventInput();

            //Get all pieces in place
            BasicInitiation(ResourcesEventEngine.EventSingle, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderTwo, ResourcesEventEngine.EventIdTwo);

            var eventR = _input.GetDisplay(5, 6, 2);

            Debug.WriteLine("Type: " + eventR.Typ);

            Assert.IsTrue(eventR.PathTravel.IsNullOrEmpty(), "Right Path one, Type: " + eventR.Typ);
            Assert.AreEqual(4, eventR.Typ, "Right Type one");

            eventR = _input.GetDisplay(4, 6, 4);

            Assert.AreEqual(2, eventR.PathTravel.Count, "Right Path one, Type: " + +eventR.Typ);
        }

        /// <summary>
        ///     Test empty Event Data
        ///     Test Small Steps
        ///     0,1,2,3
        ///     0,  0,1,2,3
        ///     1,  4,5,6,7
        ///     2,  8,9,10,11
        ///     New Route:
        ///     0111
        ///     0101
        ///     0001
        ///     1 is pass ways, Blockers: 1,2,3,5,7,11
        ///     A little bit Hacked but it works
        /// </summary>
        [TestMethod]
        public void InitiateEventsMaxMovementCornerCase()
        {
            _input = new EventInput();

            //Get all pieces in place
            BasicInitiation(null, 10, 1, false, ResourcesEventEngine.BorderOne, null);

            //Initiate move
            var eventR = _input.GetDisplay(0, 6, 5);

            DisplayEventData(eventR);

            //Move
            Assert.AreEqual(5, eventR.PathTravel.Count, "Correct move One: " + eventR.Typ);
            //Display
            Assert.AreEqual(6, eventR.PathDisplay.Count, "Correct display One: " + eventR.Typ);
            //Check Active
            Assert.IsFalse(eventR.DoSomething, "Check if we do something One");

            //Initiate move
            eventR = _input.GetDisplay(0, 6, 4);

            DisplayEventData(eventR);

            //Move
            Assert.AreEqual(4, eventR.PathTravel.Count, "Correct move Two, Type: " + eventR.Typ);
            //Display
            Assert.AreEqual(6, eventR.PathDisplay.Count, "Correct display Two, Type: " + eventR.Typ);
            //Check Active
            Assert.IsFalse(eventR.DoSomething, "Check if we do something Two");
        }

        /// <summary>
        ///     Test empty Event Data
        ///     Test Trap event
        /// </summary>
        [TestMethod]
        public void InitiateEventsMaxMovementTrap()
        {
            _input = new EventInput();

            //Get all pieces in place
            BasicInitiation(ResourcesEventEngine.EventTypeWithTrap, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length, true, ResourcesEventEngine.BorderOne, ResourcesEventEngine.EventIdOne);

            //Initiate move
            var eventR = _input.GetDisplay(0, 9, 8);

            //Move
            Assert.AreEqual(4, eventR.PathTravel.Count, "Correct move One" + eventR.PathTravel.Count);
            //Display
            Assert.AreEqual(3, eventR.PathDisplay.Count, "Correct display One " + eventR.PathDisplay.Count);
            //Check Active
            Assert.IsTrue(eventR.DoSomething, "Check if we do something");
        }

        /// <summary>
        ///     Check Saving
        /// </summary>
        [TestMethod]
        public void SaveTesting()
        {
            var mapE = new EditorMapEngine();
            _input = new EventInput();

            var map = mapE.Generate(ResourcesEventEngine.Height, ResourcesEventEngine.Length);
            var check = _input.InitiateMove(ResourcesEventEngine.EventIdOne, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length,
                map.Borders, ResourcesEventEngine.EventTypeWithTrap);

            Assert.IsTrue(check, "Check if Move Engine was initiated correct failed");

            //SingleTone, initiate
            var cpnIn = HandlerInputSingleton.Create();

            //not step on, not repeatable
            cpnIn.SetEventInactive(1);
            Assert.IsTrue(EventInput.EventChanged, "One: Check if we did register the change, repeatable");

            //step on, repeatable
            cpnIn.SetEventInactive(0);
            Assert.IsTrue(EventInput.EventChanged, "Null: Check if we did register the change, not repeatable");
            Assert.IsFalse(EventInput.EventTypeDictionary[1].IsActive,
                "Check if the Dictionary was changed, not repeatable");

            Output.SetAutosave(ResourcesGeneral.CampaignNameNew, ResourcesGeneral.MapNameNew, new PartyInventory());

            var expectedpath = Path.Combine(Directory.GetCurrentDirectory(),
                ResourcesGeneral.CampaignsFolder,
                ResourcesGeneral.CampaignNameNew,
                ResourcesGeneral.AutoSave,
                ResourcesGeneral.MapNameNew);

            Debug.WriteLine(expectedpath);

            Assert.IsTrue(FileHandleSearch.CheckIfFolderContainsElement(expectedpath),
                "Check if we created the File");

            FileHandleDelete.DeleteCompleteFolder(expectedpath);
        }

        /// <summary>
        ///     Sets the Event inactive and test results.
        /// </summary>
        [TestMethod]
        public void SetInactiveTesting()
        {
            var mapE = new EditorMapEngine();
            _input = new EventInput();

            var map = mapE.Generate(ResourcesEventEngine.Height, ResourcesEventEngine.Length);
            var check = _input.InitiateMove(ResourcesEventEngine.EventIdOne, ResourcesEventEngine.Height,
                ResourcesEventEngine.Length,
                map.Borders, ResourcesEventEngine.EventTypeWithTrap);

            Assert.IsTrue(check, "Check if Move Engine was initiated correct failed");

            //SingleTone, initiate
            var cpnIn = HandlerInputSingleton.Create();

            //step on, not repeatable
            cpnIn.SetEventInactive(2);
            Assert.IsTrue(EventInput.EventChanged, "Check if we did register the change, not repeatable");
            Assert.IsFalse(EventInput.EventTypeDictionary[1].IsActive,
                "Check if the Dictionary was changed, not repeatable");
        }

        /// <summary>
        ///     The basic initiation.
        ///     Setting up all basic pieces of a map
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="height">The height.</param>
        /// <param name="length">The length.</param>
        /// <param name="blocker">The blocker.</param>
        /// <param name="lst">The List.</param>
        /// <param name="eventId">The eventId.</param>
        private static void BasicInitiation(Dictionary<int, EventType> events, int height, int length, bool blocker,
            IEnumerable<int> lst, Dictionary<int, int> eventId)
        {
            _input = new EventInput();

            //Just Preparations
            //Create Basic Map
            var mapE = new EditorMapEngine();
            var map = mapE.Generate(height, length);

            if (blocker)
            {
                //added Blockers

                var one =
                    lst.Select(
                        element =>
                            ArtShared.IdToCoordinate(element, length, ResourcesEventEngine.Layer,
                                ResourcesEventEngine.TileId)).ToList();

                //Generate Changes
                var load = HelperMethods.GetData();
                map = mapE.ChangeMap(one, map, load.MasterBordersDictionary, load.MasterTileDictionary);

                foreach (var element in map.Borders)
                {
                    Debug.WriteLine(element);
                }
            }

            //Initiate Start point
            //Event Register
            //Generate new CoordinatesId and EventType
            var check = _input.InitiateMove(eventId, height, length, map.Borders,
                events);
            //End Preparations

            Assert.IsTrue(check, "Check if Move Engine was initiated correct failed");
        }

        /// <summary>
        ///     Just Display Data, for Review Purposes
        /// </summary>
        /// <param name="eventDisplay">Collected Event Data</param>
        private static void DisplayEventData(EventTypeDisplay eventDisplay)
        {
            Assert.IsNotNull(eventDisplay, "Object was empty");

            Debug.WriteLine("Type:" + eventDisplay.Typ);

            Debug.WriteLine("EventType");
            Debug.WriteLine(eventDisplay.Typ);
            Debug.WriteLine("Steps");
            if (!eventDisplay.PathTravel.IsNullOrEmpty())
            {
                Debug.WriteLine(eventDisplay.PathTravel.Count);
            }

            Debug.WriteLine("Steps shown");
            if (!eventDisplay.PathDisplay.IsNullOrEmpty())
            {
                Debug.WriteLine(eventDisplay.PathDisplay.Count);
            }

            Debug.WriteLine("Event Type internal");
            if (eventDisplay.MyEventsTypes == null)
            {
                return;
            }

            foreach (var element in eventDisplay.MyEventsTypes.Values)
            {
                Debug.WriteLine("EventDisplay Coordinate Id: " + element.CoordinatesId);
                Debug.WriteLine("EventDisplay Description: " + element.Description);
                Debug.WriteLine("EventDisplay Step On: " + element.IsStepOn);
                Debug.WriteLine("EventDisplay Label Event: " + element.LabelEvent);
            }
        }

        /// <summary>
        ///     Debug Displays
        /// </summary>
        /// <param name="pathList">List of Points</param>
        /// <param name="chk">Variable to check if we display Displays or real Path</param>
        private static void DisplayMovementData(List<int> pathList, bool chk)
        {
            if (pathList.IsNullOrEmpty())
            {
                Debug.WriteLine("Empty pathList");
                return;
            }

            foreach (var id in pathList)
            {
                if (chk)
                {
                    Debug.WriteLine("Point: " + id);
                }
                else
                {
                    Debug.WriteLine("Display Point: " + id);
                }
            }
        }
    }
}