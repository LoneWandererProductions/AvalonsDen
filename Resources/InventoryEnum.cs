/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/InventoryTest/InventoryEnum.cs
 * PURPOSE:     Basic Equipment Slots for a Character
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     Holds the enum
    /// </summary>
    public static class InventoryEnum
    {
        /// <summary>
        ///     the Inventory Slots on a Character
        ///     TODO
        ///     https://stackoverflow.com/questions/45194217/is-it-possible-to-restrict-public-enum-values-in-c
        /// </summary>
        public enum EnumSlot
        {
            Head = 0,
            Amulet = 1,
            Chest = 2,
            Gloves = 3,
            RingLeft = 4,
            RingRight = 5,
            MainHand = 6,
            OffHand = 7,
            Support = 8,
            DualWield = 9,
            Belt = 10,
            Trousers = 11,
            Shoes = 12,
            EquipmentSlotOne = 13,
            EquipmentSlotTwo = 14,
            None = 15,
            BeltItem = 16
        }
    }
}