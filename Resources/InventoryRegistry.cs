/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/InventoryRegister.cs
 * PURPOSE:     Basic Item Container for Inventory
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     Inventory Register, the items are Distinct
    /// </summary>
    /// <seealso cref="InventoryMaster" />
    public sealed class InventoryRegistry : InventoryMaster
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="InventoryRegistry" /> class.
        /// </summary>
        public InventoryRegistry()
        {
            ArmorItems = new Dictionary<int, Armor>();
            MiscellaneousItems = new Dictionary<int, Miscellaneous>();
            WeaponItems = new Dictionary<int, Weapon>();
            Image = new Dictionary<int, string>();
            Id = new List<int>();
        }

        /// <summary>
        ///     Gets or sets the amount of Items we have.
        /// </summary>
        /// <value>
        ///     The amounts.
        /// </value>
        public List<int> Id { get; set; }
    }
}