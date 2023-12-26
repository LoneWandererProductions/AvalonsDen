/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CampaignDriver/CampaignsHandlerDisplay.cs
 * PURPOSE:     Basic Translator between User Input and Game Engine, Mostly graphic Works
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using CharacterDisplay;
using DatabaseDriver;
using Debugger;
using DialogsDisplay;
using EventEngine;
using ExtendedSystemObjects;
using GameEngine;
using ItemExchange;
using Resources;

namespace Campaigns
{
    /// <summary>
    ///     The campaigns handler display class.
    /// </summary>
    internal static class CampaignsHandlerDisplay
    {
        /// <summary>
        ///     Display Window with Character
        /// </summary>
        /// <param name="campaignName">Name of the Campaign, needed for loading</param>
        /// <param name="characterId">Id of Character</param>
        internal static void DisplayChar(string campaignName, List<int> characterId)
        {
            var chr = new CharacterInteraction();

            try
            {
                chr.Initiate(campaignName, characterId);
            }
            catch (ArgumentNullException e)
            {
                DebugLog.CreateLogFile(e.ToString(), ErCode.Error);
            }
        }

        /// <summary>
        ///     Display the Dialog
        /// </summary>
        /// <param name="campaignName">Name of the Campaign, needed for loading</param>
        /// <param name="mapName">Name of the Map, needed for loading</param>
        /// <param name="dialogName">Name of the dialog</param>
        internal static void DisplayTalk(string campaignName, string mapName, string dialogName)
        {
            var dlgDisplay = new DialogInteraction();
            DialogInteraction.EventTriggered += DialogInteraction_EventTriggered;

            try
            {
                dlgDisplay.StartDisplay(campaignName, mapName, dialogName);
            }
            catch (ArgumentNullException e)
            {
                DebugLog.CreateLogFile(e.ToString(), ErCode.Error);
            }
        }

        /// <summary>
        ///     Display the Dialog
        /// </summary>
        /// <param name="itemId">List of Items</param>
        /// <param name="campaignName">Name of the Campaign</param>
        internal static void DisplayAddItem(IEnumerable<InventoryContainer> itemId, string campaignName)
        {
            //Get the Items? Where did I put them??????
            //Get the Item by Id, get the Image and the needed attributes
            var handle = HandlerOutputSingleton.CreateInstanceByName(campaignName);

            var items = handle.GetItems(itemId);
            var data = CampaignsHelper.ConvertItems(items);

            if (data.Count == 0)
            {
                DebugLog.CreateLogFile(CampaignsResources.ErrorCouldNotItemData, ErCode.Error);
                return;
            }

            //Generate Looting screen
            var loot = new Looting();

            try
            {
                var itm = loot.StartScreen(data, CampaignsResources.CoreImages);
                if (itm.IsNullOrEmpty()) return;
                //Add to inventory
                var inventoryItem = CampaignsHelper.ConvertToSlot(itm);

                if (inventoryItem.IsNullOrEmpty()) return;

                //So we are done here. Time to pack this stuff into the player Inventory
                CampaignsRegister.SetInventoryCarrying(
                    InventoryHandler.AddToInventory(CampaignsRegister.PartyInventory, inventoryItem));
            }
            catch (ArgumentNullException e)
            {
                DebugLog.CreateLogFile(e.ToString(), ErCode.Error);
            }
        }

        /// <summary>
        ///     Display the Dialog
        /// </summary>
        /// <param name="amount">Amount of Money</param>
        internal static void DisplayAddGold(int amount)
        {
            if (amount == 0) return;
            //So we are done here. Time to pack this stuff into the player Inventory
            CampaignsRegister.SetGold(InventoryHandler.AddGold(CampaignsRegister.PartyInventory.PartyOverview.Gold,
                amount, true));
        }

        /// <summary>
        ///     The dialog interaction event triggered.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The dialog interaction event arguments.</param>
        private static void DialogInteraction_EventTriggered(object sender, DialogInteractionEventArgs e)
        {
            if (!EventInput.EventTypeDictionary.ContainsKey(e.EventId)) return;

            var eventAction = EventInput.EventTypeDictionary[e.EventId];

            CampaignsInputHandler.DisplayChoices(new KeyValuePair<int, EventType>(e.EventId, eventAction));
        }
    }
}