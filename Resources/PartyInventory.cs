/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/PartyInventory.cs
 * PURPOSE:     Simple Save and load Container for the Inventory
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace Resources
{
    /// <summary>
    ///     Container File
    ///     Well we will add an object for the Inventory. For Saving
    ///     One Inventory
    ///     One for Gold
    ///     One for Items we are wearing
    /// </summary>
    public sealed class PartyInventory
    {
        /// <summary>
        ///     Gets or sets the party overview.
        /// </summary>
        /// <value>
        ///     The party overview.
        /// </value>
        public Party PartyOverview { get; set; } = new();

        /// <summary>
        ///     Gets or sets the carrying.
        /// </summary>
        /// <value>
        ///     The carrying.
        /// </value>
        public Dictionary<int, InventorySlot> Carrying { get; set; } = new();

        /// <summary>
        ///     Gets or sets the equipment Key.
        ///     Key should be equal to the Character
        /// </summary>
        /// <value>
        ///     The equipment.
        ///     Each Id represents an Item Id
        /// </value>
        public Dictionary<int, Equipped> Equipment { get; set; } = new();
    }
}