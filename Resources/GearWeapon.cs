/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/GearWeapon.cs
 * PURPOSE:     Item Template for all Weapon Items
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     The gear weapon class.
    /// </summary>
    public static class GearWeapon
    {
        /// <summary>
        ///     The damage type enum.
        /// </summary>
        public enum DamageType
        {
            /// <summary>
            ///     Cut Damage = 0.
            /// </summary>
            Cut = 0,

            /// <summary>
            ///     Slash Damage = 1.
            /// </summary>
            Slash = 1,

            /// <summary>
            ///     Blunt Damage = 2.
            /// </summary>
            Blunt = 2,

            /// <summary>
            ///     Pierce Damage = 3.
            /// </summary>
            Pierce = 3,

            /// <summary>
            ///     None = 4.
            /// </summary>
            None = 4
        }

        /// <summary>
        ///     Specific Combat Ranges of the Weapon, Ranged Melee, Mix of both, and specific corner cases
        /// </summary>
        public enum Range
        {
            /// <summary>
            ///     The Melee = 0.
            /// </summary>
            Melee = 0,

            /// <summary>
            ///     The Ranged = 1.
            /// </summary>
            Ranged = 1,

            /// <summary>
            ///     The Short = 2.
            /// </summary>
            Short = 2,

            /// <summary>
            ///     The Long = 3.
            /// </summary>
            Long = 3
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     The weapon class.
    /// </summary>
    public sealed class Weapon : Item
    {
        /// <summary>
        ///     The armor.
        /// </summary>
        private int _armor;

        /// <summary>
        ///     The damage.
        /// </summary>
        private int _damage;

        /// <summary>
        ///     The damage range.
        /// </summary>
        private int _damageRange;

        /// <summary>
        ///     The damage type.
        /// </summary>
        private GearWeapon.DamageType _damageType;

        /// <summary>
        ///     The durability.
        /// </summary>
        private int _durability;

        /// <summary>
        ///     The range.
        /// </summary>
        private GearWeapon.Range _range;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="Weapon" /> class.
        /// </summary>
        public Weapon()
        {
            MaxStack = 1;
        }

        /// <summary>
        ///     Gets or sets the damage.
        /// </summary>
        public int Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                RaisePropertyChangedEvent(nameof(Damage));
            }
        }

        /// <summary>
        ///     Gets or sets the damage range.
        /// </summary>
        public int DamageRange
        {
            get => _damageRange;
            set
            {
                _damageRange = value;
                RaisePropertyChangedEvent(nameof(DamageRange));
            }
        }

        /// <summary>
        ///     Gets or sets the armor.
        /// </summary>
        public int Armor
        {
            get => _armor;
            set
            {
                _armor = value;
                RaisePropertyChangedEvent(nameof(Armor));
            }
        }

        /// <summary>
        ///     Gets or sets the damage type.
        /// </summary>
        public GearWeapon.DamageType DamageType
        {
            get => _damageType;
            set
            {
                _damageType = value;
                RaisePropertyChangedEvent(nameof(DamageType));
            }
        }

        /// <summary>
        ///     Gets or sets the range.
        /// </summary>
        public GearWeapon.Range Range
        {
            get => _range;
            set
            {
                _range = value;
                RaisePropertyChangedEvent(nameof(Range));
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