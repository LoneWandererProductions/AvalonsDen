/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/Equipped.cs
 * PURPOSE:     Basic Equipment Slots for a Character
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     Equipped Items
    ///     Character Id will be used as the Key
    ///     int Id is the item in Question
    /// </summary>
    public sealed class Equipped
    {
        /// <summary>
        ///     Gets or sets the head.
        /// </summary>
        /// <value>
        ///     The head.
        /// </value>
        public int Head { get; init; }

        /// <summary>
        ///     Gets or sets the amulet.
        /// </summary>
        /// <value>
        ///     The amulet.
        /// </value>
        public int Amulet { get; init; }

        /// <summary>
        ///     Gets or sets the chest.
        /// </summary>
        /// <value>
        ///     The chest.
        /// </value>
        public int Chest { get; set; }

        /// <summary>
        ///     Gets or sets the gloves.
        /// </summary>
        /// <value>
        ///     The gloves.
        /// </value>
        public int Gloves { get; set; }

        /// <summary>
        ///     Gets or sets the ring left.
        /// </summary>
        /// <value>
        ///     The ring left.
        /// </value>
        public int RingLeft { get; set; }

        /// <summary>
        ///     Gets or sets the ring right.
        /// </summary>
        /// <value>
        ///     The ring right.
        /// </value>
        public int RingRight { get; set; }

        /// <summary>
        ///     Gets or sets the main hand.
        /// </summary>
        /// <value>
        ///     The main hand.
        /// </value>
        public int MainHand { get; set; }

        /// <summary>
        ///     Gets or sets the off hand.
        /// </summary>
        /// <value>
        ///     The off hand.
        /// </value>
        public int OffHand { get; set; }

        /// <summary>
        ///     Gets or sets the secondary hand.
        /// </summary>
        /// <value>
        ///     The secondary hand.
        /// </value>
        public int SecondaryHand { get; set; }

        /// <summary>
        ///     Gets or sets the belt.
        /// </summary>
        /// <value>
        ///     The belt.
        /// </value>
        public int Belt { get; set; }

        /// <summary>
        ///     Gets or sets the trousers.
        /// </summary>
        /// <value>
        ///     The trousers.
        /// </value>
        public int Trousers { get; set; }

        /// <summary>
        ///     Gets or sets the shoes.
        /// </summary>
        /// <value>
        ///     The shoes.
        /// </value>
        public int Shoes { get; set; }

        /// <summary>
        ///     Gets or sets the equipment slot one.
        /// </summary>
        /// <value>
        ///     The equipment slot one.
        /// </value>
        public int EquipmentSlotOne { get; set; }

        /// <summary>
        ///     Gets or sets the equipment slot two.
        /// </summary>
        /// <value>
        ///     The equipment slot two.
        /// </value>
        public int EquipmentSlotTwo { get; set; }
    }
}