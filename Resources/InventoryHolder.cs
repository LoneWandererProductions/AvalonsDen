/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/InventoryHolder.cs
 * PURPOSE:     Basic Item Container for Inventory
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace Resources
{
    /// <summary>
    ///     Contains the Items we got for the Looting screen
    /// </summary>
    public sealed class InventoryHolder : InventoryMaster
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="InventoryMaster" /> class.
        /// </summary>
        public InventoryHolder()
        {
            ArmorItems = new Dictionary<int, Armor>();
            MiscellaneousItems = new Dictionary<int, Miscellaneous>();
            WeaponItems = new Dictionary<int, Weapon>();
            Amount = new Dictionary<int, int>();
            Image = new Dictionary<int, string>();
        }

        /// <summary>
        ///     Gets or sets the amount of Items we have.
        /// </summary>
        /// <value>
        ///     The amounts.
        /// </value>
        public Dictionary<int, int> Amount { get; set; }
    }
}