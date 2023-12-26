using System.Collections.Generic;
using System.Linq;
using ExtendedSystemObjects;
using Resources;

namespace GameEngine
{
    /// <summary>
    ///     Basic processing of the Inventory
    /// </summary>
    internal static class InventoryProcessing
    {
        /// <summary>
        ///     Get free Slot and add.
        /// </summary>
        /// <param name="carrying">The carrying.</param>
        /// <param name="inventoryItem">The inventory item.</param>
        /// <returns>The Inventory with the Elements added</returns>
        internal static Dictionary<int, InventorySlot> GetFreeSlotAndAdd(Dictionary<int, InventorySlot> carrying,
            List<InventorySlot> inventoryItem)
        {
            if (carrying.IsNullOrEmpty())
            {
                var i = -1;
                carrying = new Dictionary<int, InventorySlot>();

                foreach (var item in inventoryItem)
                {
                    i++;
                    item.Position = i;
                    carrying.Add(i, item);
                }

                return carrying;
            }

            var number = Utility.GetAvailableIndexes(carrying.Keys.ToList(), inventoryItem.Count);

            foreach (var item in inventoryItem)
            {
                item.Position = number[0];
                carrying.Add(item.Position, item);
                number.RemoveAt(0);
            }

            return carrying;
        }
    }
}