using System.Collections.Generic;

namespace Inventory
{
    /// <summary>
    ///     Inventory Overview
    /// </summary>
    public sealed class CharacterOverview
    {
        /// <summary>
        ///     Gets or sets the character identifier.
        /// </summary>
        /// <value>
        ///     The character identifier.
        /// </value>
        public int CharacterId { get; set; }

        /// <summary>
        ///     Gets or sets the inventory.
        /// </summary>
        /// <value>
        ///     The inventory.
        /// </value>
        public Dictionary<int, Slot> Inventory { get; set; }

        /// <summary>
        ///     Gets or sets the head.
        /// </summary>
        /// <value>
        ///     The head.
        /// </value>
        public int Head { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed head.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed head; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedHead { get; set; }

        /// <summary>
        ///     Gets or sets the amulet.
        /// </summary>
        /// <value>
        ///     The amulet.
        /// </value>
        public int Amulet { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed amulet.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed amulet; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedAmulet { get; set; }

        /// <summary>
        ///     Gets or sets the chest.
        /// </summary>
        /// <value>
        ///     The chest.
        /// </value>
        public int Chest { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed chest.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed chest; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedChest { get; set; }

        /// <summary>
        ///     Gets or sets the gloves.
        /// </summary>
        /// <value>
        ///     The gloves.
        /// </value>
        public int Gloves { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed gloves.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed gloves; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedGloves { get; set; }

        /// <summary>
        ///     Gets or sets the ring left.
        /// </summary>
        /// <value>
        ///     The ring left.
        /// </value>
        public int RingLeft { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed ring left.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed ring left; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedRingLeft { get; set; }

        /// <summary>
        ///     Gets or sets the ring right.
        /// </summary>
        /// <value>
        ///     The ring right.
        /// </value>
        public int RingRight { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed ring right.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed ring right; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedRingRight { get; set; }

        /// <summary>
        ///     Gets or sets the main hand.
        /// </summary>
        /// <value>
        ///     The main hand.
        /// </value>
        public int MainHand { get; set; }

        public bool IsAllowedMainHand { get; set; }

        /// <summary>
        ///     Gets or sets the off hand.
        /// </summary>
        /// <value>
        ///     The off hand.
        /// </value>
        public int OffHand { get; set; }

        public bool IsAllowedOffHand { get; set; }

        /// <summary>
        ///     Gets or sets the secondary hand.
        /// </summary>
        /// <value>
        ///     The secondary hand.
        /// </value>
        public int SecondaryHand { get; set; }

        public bool IsAllowedSecondaryHand { get; set; }

        /// <summary>
        ///     Gets or sets the belt.
        /// </summary>
        /// <value>
        ///     The belt.
        /// </value>
        public int Belt { get; set; }

        public bool IsAllowedBelt { get; set; }

        /// <summary>
        ///     Gets or sets the trousers.
        /// </summary>
        /// <value>
        ///     The trousers.
        /// </value>
        public int Trousers { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed trousers.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed trousers; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedTrousers { get; set; }

        /// <summary>
        ///     Gets or sets the shoes.
        /// </summary>
        /// <value>
        ///     The shoes.
        /// </value>
        public int Shoes { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed shoes.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed shoes; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedShoes { get; set; }

        /// <summary>
        ///     Gets or sets the equipment slot one.
        /// </summary>
        /// <value>
        ///     The equipment slot one.
        /// </value>
        public int EquipmentSlotOne { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is allowed equipment slot one.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is allowed equipment slot one; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowedEquipmentSlotOne { get; set; }

        /// <summary>
        ///     Gets or sets the equipment slot two.
        /// </summary>
        /// <value>
        ///     The equipment slot two.
        /// </value>
        public int EquipmentSlotTwo { get; set; }

        public bool IsAllowedEquipmentSlotTwo { get; set; }
    }
}