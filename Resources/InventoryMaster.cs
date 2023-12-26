/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/InventoryMaster.cs
 * PURPOSE:     Basic Item Container for Inventory
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace Resources
{
    /// <summary>
    ///     The Basic needs a container needs to hold Inventory Items
    /// </summary>
    public class InventoryMaster
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InventoryMaster" /> class.
        /// </summary>
        protected InventoryMaster()
        {
            ArmorItems = new Dictionary<int, Armor>();
            MiscellaneousItems = new Dictionary<int, Miscellaneous>();
            WeaponItems = new Dictionary<int, Weapon>();
            Image = new Dictionary<int, string>();
        }

        /// <summary>
        ///     Gets or sets the armor items.
        /// </summary>
        /// <value>
        ///     The armor items.
        /// </value>
        public Dictionary<int, Armor> ArmorItems { get; set; }

        /// <summary>
        ///     Gets or sets the miscellaneous items.
        /// </summary>
        /// <value>
        ///     The miscellaneous items.
        /// </value>
        public Dictionary<int, Miscellaneous> MiscellaneousItems { get; set; }

        /// <summary>
        ///     Gets or sets the m weapon items.
        /// </summary>
        /// <value>
        ///     The m weapon items.
        /// </value>
        public Dictionary<int, Weapon> WeaponItems { get; set; }

        /// <summary>
        ///     Gets or sets the image.
        /// </summary>
        /// <value>
        ///     The image.
        /// </value>
        public Dictionary<int, string> Image { get; set; }
    }
}