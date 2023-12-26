/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CampaignsContextMenu.cs
 * PURPOSE:     Editor Menu for Tiles
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Renderer;
using Resources;

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace Campaigns
{
    /// <summary>
    ///     The campaigns context menu class.
    /// </summary>
    internal static class CampaignsContextMenu
    {
        /// <summary>
        ///     The current Status of the Event Type
        /// </summary>
        private static Status _current;

        /// <summary>
        ///     Return the Fitting Menu Items
        /// </summary>
        /// <param name="myDisplayEventsTypes">Events we Display</param>
        /// <param name="typ">Type of Event</param>
        /// <returns>List of Menu Items</returns>
        internal static List<MenuItems> GenerateItems(Dictionary<int, EventType> myDisplayEventsTypes, int typ)
        {
            var items = new List<MenuItems>();

            //Display Movement _typ 1 should never happen! But better catch than exception.
            if (myDisplayEventsTypes == null && typ != CampaignsResources.UiDoDisplayChar)
            {
                _current = Status.DisplayMovement;
                return DisplayMovement();
            }

            //Display Char
            if (typ == CampaignsResources.UiDoDisplayChar)
            {
                _current = Status.DisplayChar;
                return DisplayChar();
            }

            _current = Status.DisplayActions;

            // ReSharper disable once PossibleNullReferenceException, already done, as a first step, Reshaper bugs out here
            for (var i = 0; i < myDisplayEventsTypes.Count; i++)
            {
                var item = new MenuItems();
                var events = myDisplayEventsTypes.Values.ElementAt(i);

                item.Position = i;
                item.Tooltip = GetStringforTooltip(events.TypeOfEvent);
                item.ImagePath = GetspecificIcon(events.TypeOfEvent);
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        ///     It is Clockwise from 1 t0 5, 0 is Idle and 6 is Save Button
        /// </summary>
        /// <param name="myDisplayEventsTypes">Possible clicks</param>
        /// <param name="id">Id of clicked button</param>
        /// <returns>Chosen Item</returns>
        internal static int ConvertToId(Dictionary<int, EventType> myDisplayEventsTypes, int id)
        {
            //Save Menu
            if (id == 0) return CampaignsResources.UiDoNothing;

            switch (_current)
            {
                case Status.DisplayActions:
                    return myDisplayEventsTypes.Keys.ElementAt(id - 1);

                case Status.DisplayMovement:
                    return CampaignsResources.Move;

                case Status.DisplayChar:
                    //Not Clockwise
                    if (id == 5) return CampaignsResources.UiDoDisplayChar;

                    if (id == 4) return CampaignsResources.UiDoDisplayInventory;

                    if (id == 3) return CampaignsResources.UiDoDisplayLog;

                    break;

                case Status.DisplayIdle:
                    // Idle click don't do nothing
                    return CampaignsResources.UiDoNothing;

                default:
                    //don't do anything
                    return CampaignsResources.UiDoNothing;
            }

            return CampaignsResources.UiDoNothing;
        }

        /// <summary>
        ///     Just generate a Save File
        /// </summary>
        internal static void GenerateSaveFile()
        {
            var graph = new SaveGame
            {
                Topmost = true
            };
            graph.ShowDialog();
        }

        /// <summary>
        ///     Just Display a move Command
        /// </summary>
        private static List<MenuItems> DisplayMovement()
        {
            var items = new List<MenuItems>();

            var item = new MenuItems
            {
                Position = 0,
                Tooltip = CampaignsStringResource.ToolTipMove,
                ImagePath = CampaignsResources.IconMove
            };

            items.Add(item);

            return items;
        }

        /// <summary>
        ///     Display all the Tool tips for the Party menu
        /// </summary>
        private static List<MenuItems> DisplayChar()
        {
            var items = new List<MenuItems>();

            //Flag char
            var item = new MenuItems
            {
                Position = 0,
                Tooltip = CampaignsStringResource.ToolTipChar,
                ImagePath = CampaignsResources.IconChar
            };
            items.Add(item);

            //Flag Inventory
            item = new MenuItems
            {
                Position = 1,
                Tooltip = CampaignsStringResource.ToolTipInventory,
                ImagePath = CampaignsResources.IconInventory
            };
            items.Add(item);

            //Flag Log
            item = new MenuItems
            {
                Position = 2,
                Tooltip = CampaignsStringResource.ToolTipLog,
                ImagePath = CampaignsResources.IconLog
            };
            items.Add(item);

            return items;
        }

        /// <summary>
        ///     TODO Add Icons
        ///     Returns Name of the Icon
        /// </summary>
        /// <param name="typeOfEvent">is the Id of the Event</param>
        /// <returns>Event Image by Id, Id stands for an Image</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static string GetspecificIcon(EventType.TypeOfEvents typeOfEvent)
        {
            string icon = null;
            switch (typeOfEvent)
            {
                //Interaction
                case EventType.TypeOfEvents.Interaction:
                    icon = CampaignsResources.IconInteract;
                    break;
                //Trade
                case EventType.TypeOfEvents.Trade:
                    icon = CampaignsResources.IconTrade;
                    break;
                //Talk
                case EventType.TypeOfEvents.Talk:
                    icon = CampaignsResources.IconTalk;
                    break;
                //Fight
                case EventType.TypeOfEvents.Fight:
                    icon = CampaignsResources.IconFight;
                    break;
                //Look
                case EventType.TypeOfEvents.Look:
                    icon = CampaignsResources.IconLook;
                    break;
                //Interaction
                case EventType.TypeOfEvents.LocationChange:
                    icon = CampaignsResources.IconInteract;
                    break;
                //Map Change
                case EventType.TypeOfEvents.MapChange:
                    icon = CampaignsResources.IconInteract;
                    break;

                case EventType.TypeOfEvents.AddItems:
                    break;

                case EventType.TypeOfEvents.AddCharacter:
                    break;

                case EventType.TypeOfEvents.AddGold:
                    break;

                case EventType.TypeOfEvents.RemoveCharacter:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(typeOfEvent), typeOfEvent, null);
            }

            return icon;
        }

        /// <summary>
        ///     Adds the Tool tip to the Element
        /// </summary>
        /// <param name="typeOfEvent">id of the Action</param>
        /// <returns>the specific Tool tip for the Action</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static string GetStringforTooltip(EventType.TypeOfEvents typeOfEvent)
        {
            switch (typeOfEvent)
            {
                case 0:
                    break;

                case EventType.TypeOfEvents.Interaction:
                    return CampaignsStringResource.ToolTipInteract;

                case EventType.TypeOfEvents.Trade:
                    return CampaignsStringResource.ToolTipTrade;

                case EventType.TypeOfEvents.Talk:
                    return CampaignsStringResource.ToolTipTalk;

                case EventType.TypeOfEvents.Fight:
                    return CampaignsStringResource.ToolTipFight;

                case EventType.TypeOfEvents.Look:
                    return CampaignsStringResource.ToolTipLook;

                case EventType.TypeOfEvents.LocationChange:
                    return CampaignsStringResource.ToolTipInteract;

                case EventType.TypeOfEvents.MapChange:
                    return CampaignsStringResource.ToolTipTeleport;

                case EventType.TypeOfEvents.AddItems:
                    break;

                case EventType.TypeOfEvents.AddCharacter:
                    break;

                case EventType.TypeOfEvents.AddGold:
                    break;

                case EventType.TypeOfEvents.RemoveCharacter:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(typeOfEvent), typeOfEvent,
                        nameof(GetStringforTooltip));
            }

            return string.Empty;
        }

        /// <summary>
        ///     The Status of the Character Menu
        /// </summary>
        private enum Status
        {
            /// <summary>
            ///     The DisplayIdle = 0.
            /// </summary>
            DisplayIdle = 0,

            /// <summary>
            ///     The DisplayChar = 1.
            /// </summary>
            DisplayChar = 1,

            /// <summary>
            ///     The DisplayMovement = 2.
            /// </summary>
            DisplayMovement = 2,

            /// <summary>
            ///     The DisplayActions = 3.
            /// </summary>
            DisplayActions = 3
        }
    }
}