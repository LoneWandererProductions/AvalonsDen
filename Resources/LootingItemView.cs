/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/LootingItemView.cs
 * PURPOSE:     Basic Item Object, mostly used in Looting
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     Describes Inventory Item
    /// </summary>
    public sealed class LootingItemView
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; } = -1;

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public int Amount { get; set; }

        /// <summary>Gets or sets the maximum stack.</summary>
        /// <value>The maximum stack.</value>
        public int MaxStack { get; set; } = 1;

        /// <summary>
        ///     Gets or sets the image.
        /// </summary>
        /// <value>
        ///     The image.
        /// </value>
        public string Image { get; init; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }
    }
}