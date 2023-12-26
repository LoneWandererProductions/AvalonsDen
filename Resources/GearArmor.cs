/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/GearArmor.cs
 * PURPOSE:     Item Template for all wearable Items
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */


namespace Resources
{
    /// <summary>
    ///     The gear armor class.
    /// </summary>
    public static class GearArmor
    {
        /// <summary>
        ///     The armor class enum.
        /// </summary>
        public enum ArmorClass
        {
            /// <summary>
            ///     The Light = 0 Armor Class.
            /// </summary>
            Light = 0,

            /// <summary>
            ///     The Armored = 1 Armor Class.
            /// </summary>
            Armored = 1,

            /// <summary>
            ///     The Medium = 2 Armor Class.
            /// </summary>
            Medium = 2,

            /// <summary>
            ///     The Heavy = 3 Armor Class.
            /// </summary>
            Heavy = 3
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     The armor class.
    /// </summary>
    public sealed class Armor : Item
    {
        /// <summary>
        ///     The armor class.
        /// </summary>
        private GearArmor.ArmorClass _armorClass;

        /// <summary>
        ///     The armor value.
        /// </summary>
        private int _armorValue;

        /// <summary>
        ///     The durability.
        /// </summary>
        private int _durability;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="Armor" /> class.
        /// </summary>
        public Armor()
        {
            MaxStack = 1;
        }

        /// <summary>
        ///     Gets or sets the armor value.
        /// </summary>
        public int ArmorValue
        {
            get => _armorValue;
            set
            {
                _armorValue = value;
                RaisePropertyChangedEvent(nameof(ArmorValue));
            }
        }

        /// <summary>
        ///     Gets or sets the armor class.
        /// </summary>
        public GearArmor.ArmorClass ArmorClass
        {
            get => _armorClass;
            set
            {
                _armorClass = value;
                RaisePropertyChangedEvent(nameof(ArmorClass));
            }
        }

        /// <summary>
        ///     Gets or sets the durability.
        /// </summary>
        public int Durability
        {
            get => _durability;
            set
            {
                _durability = value;
                RaisePropertyChangedEvent(nameof(Durability));
            }
        }
    }
}