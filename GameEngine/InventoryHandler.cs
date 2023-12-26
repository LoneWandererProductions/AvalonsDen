/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/InventoryHandler.cs
 * PURPOSE:     Basic Inventory Handler
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

namespace GameEngine
{
    /// <summary>
    ///     Inventory Handling Entry
    /// </summary>
    public static class InventoryHandler
    {
        /// <summary>
        ///     Adds to inventory.
        ///     Negative Id is an equipped Slot
        ///     Positive is Inventory
        /// </summary>
        /// <param name="partyInventory">The party inventory.</param>
        /// <param name="inventoryItem">The inventory item.</param>
        /// <returns>Reorganiced Inventory</returns>
        public static Dictionary<int, InventorySlot> AddToInventory(PartyInventory partyInventory,
            List<InventorySlot> inventoryItem)
        {
            return InventoryProcessing.GetFreeSlotAndAdd(partyInventory.Carrying, inventoryItem);
        }

        /// <summary>
        ///     Adds the gold.
        /// </summary>
        /// <param name="gold">The gold.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="add">if set to <c>true</c> [add] gold es substract.</param>
        /// <returns>Money we get</returns>
        public static int AddGold(int gold, int amount, bool add)
        {
            if (add)
                gold += amount;
            else
                gold -= amount;
            return gold;
        }
    }
}