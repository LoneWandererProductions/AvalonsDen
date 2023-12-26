/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/InventorySlot.cs
 * PURPOSE:     Simple Save Object for the Inventory
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     Slot Save Container for Items, one for party Inventory and one for Equipment we carry
    /// </summary>
    public sealed class InventorySlot
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; init; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public int Position { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public int Amount { get; init; }
    }
}