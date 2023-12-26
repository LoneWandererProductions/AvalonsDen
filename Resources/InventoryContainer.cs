/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/InventoryContainer.cs
 * PURPOSE:     Basic Item Container
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     Holds the Items
    /// </summary>
    public sealed class InventoryContainer
    {
        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        public int Amount { get; init; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public string Id { get; init; }
    }
}