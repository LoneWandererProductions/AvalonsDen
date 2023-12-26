/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CampaignsInputHandler.cs
 * PURPOSE:     For ease of pain separate Game logic into it is own Class, this class is for Player Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CampaignDriver;
using Debugger;
using EventEngine;
using ExtendedSystemObjects;
using GameEngine;
using Resources;

namespace Campaigns
{
    /// <summary>
    ///     The campaigns input handler class.
    /// </summary>
    internal static class CampaignsInputHandler
    {
        /// <summary>
        ///     Name of the Map, used over various Procedures
        /// </summary>
        private static string _mapName;

        /// <summary>
        ///     Only on Load and Initiate
        /// </summary>
        /// <param name="mapName">Name of the Map</param>
        internal static void StartCampaignInputHandler(string mapName)
        {
            _mapName = mapName;

            //Initiate Game Timer
            EngineGameTurns.Initiate(CampaignsResources.DayCycle, 0, CampaignsResources.Year);
        }

        /// <summary>
        ///     only on MapChange
        /// </summary>
        /// <param name="mapName">Name of the Map</param>
        internal static void LoadCampaignInputHandler(string mapName)
        {
            _mapName = mapName;
        }

        /// <summary>
        ///     External
        ///     React to Choices of the Player, selected Action
        ///     User Input, second Switch
        /// </summary>
        /// <param name="id">Type of Choice presented by id</param>
        /// <param name="eventR">Complete Player Interaction and Result</param>
        /// <param name="button">Left or Right Button</param>
        internal static void DisplayPlayerClick(int id, EventTypeDisplay eventR, bool button)
        {
            //something went completely wrong, or do we need to do something? well tough luck
            if (eventR?.DoSomething != true) return;

            //Left Button do the first stuff
            if (button)
            {
                if (eventR.MyDisplayEventsTypes.IsNullOrEmpty()) return;

                DisplayChoices(eventR.MyDisplayEventsTypes.First());
                return;
            }

            //second Switch
            //Only Handles UserInput
            switch (id)
            {
                case CampaignsResources.Move:
                    //Do nothing
                    break;

                case CampaignsResources.UiDoDisplayInventory:
                    DisplayInventory();
                    break;

                case CampaignsResources.UiDoDisplayLog:
                    DisplayLog();
                    break;

                case CampaignsResources.UiDoMove:
                    //Do nothing
                    return;

                case CampaignsResources.UiDoDisplayChar:
                    //get Handler
                    var cpn = HandlerInputSingleton.Create();

                    var character = cpn.GetParty();
                    CampaignsHandlerDisplay.DisplayChar(CampaignsRegister.CampaignName, character);
                    break;

                default:
                {
                    if (eventR.MyDisplayEventsTypes.IsNullOrEmpty()) return;

                    //Handle Left click
                    DisplayChoices(new KeyValuePair<int, EventType>(id, eventR.MyEventsTypes[id]));
                }

                    break;
            }
        }

        /// <summary>
        ///     Increment Actions to count Time and Turns
        /// </summary>
        /// <param name="moveRange">Action Points Used</param>
        internal static void SetActionPoints(int moveRange)
        {
            if (moveRange < 0) return;

            EngineGameTurns.CountActions(moveRange);
            DebugLog.CreateLogFile(
                "Time " + EngineGameTurns.CycleModulo + " Day " + EngineGameTurns.CurrentCyle + " Year " +
                EngineGameTurns.CurrentYear, ErCode.Information);
        }

        /// <summary>
        ///     Handles the Events
        ///     Third Switch
        /// </summary>
        /// <param name="eventAction">Type of Event</param>
        internal static void DisplayChoices(KeyValuePair<int, EventType> eventAction)
        {
            //get Handler
            var cpnIn = HandlerInputSingleton.Create();
            var cpnOut = HandlerOutputSingleton.Create();

            //split into key Value
            var (key, value) = eventAction;

            //Third Switch split into Graphic works and internal actions without Displays
            switch (value.TypeOfEvent)
            {
                //Interaction
                case EventType.TypeOfEvents.Interaction:
                    DisplayInteraction(value);
                    break;
                //Trade
                case EventType.TypeOfEvents.Trade:
                    DisplayTrade(value);
                    break;
                //Talk
                case EventType.TypeOfEvents.Talk:
                    var dialogName = cpnOut.GetDialogName(value.IdForFurtherEventInfo,
                        CampaignsRegister.CampaignName, CampaignsRegister.MapName);

                    if (string.IsNullOrEmpty(dialogName))
                        DebugLog.CreateLogFile(CampaignsResources.WarningDialogCouldNotBeLoaded, ErCode.Error);

                    CampaignsHandlerDisplay.DisplayTalk(CampaignsRegister.CampaignName, CampaignsRegister.MapName,
                        dialogName);
                    break;
                //Fight
                case EventType.TypeOfEvents.Fight:
                    DisplayFight(value);
                    break;
                //Look, modified Talk
                case EventType.TypeOfEvents.Look:
                    var dialogLookName = cpnOut.GetDialogName(value.IdForFurtherEventInfo,
                        CampaignsRegister.CampaignName, CampaignsRegister.MapName);
                    CampaignsHandlerDisplay.DisplayTalk(CampaignsRegister.CampaignName, CampaignsRegister.MapName,
                        dialogLookName);
                    break;
                //change your location on the current Map
                case EventType.TypeOfEvents.LocationChange:
                    var id = cpnOut.GetId(value.IdForFurtherEventInfo,
                        CampaignsRegister.CampaignName,
                        CampaignsRegister.MapName);

                    CampaignsHandler.ChangeLocation(id);
                    break;
                //change map
                case EventType.TypeOfEvents.MapChange:
                    CampaignsHandler.ChangeMap(value.IdForFurtherEventInfo);
                    break;
                //Add Item
                case EventType.TypeOfEvents.AddItems:
                    var items = cpnOut.GetItemList(value.IdForFurtherEventInfo,
                        CampaignsRegister.CampaignName,
                        CampaignsRegister.MapName);

                    CampaignsHandlerDisplay.DisplayAddItem(items, CampaignsRegister.CampaignName);
                    break;
                //Add Gold
                case EventType.TypeOfEvents.AddGold:
                    var amount = cpnOut.AddGold(value.IdForFurtherEventInfo,
                        CampaignsRegister.CampaignName,
                        CampaignsRegister.MapName);

                    CampaignsHandlerDisplay.DisplayAddGold(amount);
                    break;
                //add Character
                case EventType.TypeOfEvents.AddCharacter:
                    cpnIn.AddPartyMember(value, CampaignsRegister.CampaignName,
                        CampaignsRegister.MapName);
                    break;
                //Remove Character
                case EventType.TypeOfEvents.RemoveCharacter:
                    cpnIn.RemovePartyMember(value, CampaignsRegister.CampaignName,
                        CampaignsRegister.MapName);
                    break;

                default:
                    DebugLog.CreateLogFile(CampaignsResources.ErrorUnhandledEventType, ErCode.Warning);
                    break;
            }

            //Save Changes
            cpnOut.SetEvent(_mapName);

            //Set Event inActive, important must be always called
            //TODO FIXME Inventory does not get reset!
            cpnIn.SetEventInactive(key);
        }

        /// <summary>
        ///     TODO FIXME IMPLEMENT
        /// </summary>
        private static void DisplayLog()
        {
            MessageBox.Show("This shows your Log, not NYI");
        }

        /// <summary>
        ///     TODO FIXME IMPLEMENT
        ///     Replace with make Camp
        /// </summary>
        private static void DisplayInventory()
        {
            MessageBox.Show("This shows your Inventory, not NYI");
        }

        /// <summary>
        ///     TODO FIXME IMPLEMENT
        /// </summary>
        /// <param name="eventAction"></param>
        private static void DisplayInteraction(EventType eventAction)
        {
            MessageBox.Show("This shows an Interaction, not NYI: " + eventAction);
        }

        /// <summary>
        ///     TODO FIXME IMPLEMENT
        /// </summary>
        /// <param name="eventAction"></param>
        private static void DisplayTrade(EventType eventAction)
        {
            MessageBox.Show("This opens a Trade, not NYI: " + eventAction);
        }

        /// <summary>
        ///     TODO FIXME IMPLEMENT
        /// </summary>
        /// <param name="eventAction"></param>
        private static void DisplayFight(EventType eventAction)
        {
            MessageBox.Show("This shows a Fight, not NYI: " + eventAction);
        }
    }
}