/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterDisplay/Gear.cs
 * PURPOSE:     Basic Character Charta, generates the Values of the Item
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace CharacterDisplay
{
    /// <inheritdoc />
    /// <summary>
    ///     TODO Improvement add Calculation to Display Damage Range
    ///     The gear class.
    /// </summary>
    internal class Gear : ObservableObject
    {
        /// <summary>
        ///     The ranged damage.
        /// </summary>
        private static int _rangedDamage;

        /// <summary>
        ///     The light resistance.
        /// </summary>
        private static int _lightResistance;

        /// <summary>
        ///     The dark resistance.
        /// </summary>
        private static int _darkResistance;

        /// <summary>
        ///     The air resistance.
        /// </summary>
        private static int _airResistance;

        /// <summary>
        ///     The earth resistance.
        /// </summary>
        private static int _earthResistance;

        /// <summary>
        ///     The water resistance.
        /// </summary>
        private static int _waterResistance;

        /// <summary>
        ///     The fire resistance.
        /// </summary>
        private static int _fireResistance;

        /// <summary>
        ///     The psychological resistance.
        /// </summary>
        private static int _psychologicalResistance;

        /// <summary>
        ///     The physical resistance.
        /// </summary>
        private static int _physicalResistance;

        /// <summary>
        ///     The ranged damage range.
        /// </summary>
        private static int _rangedDamageRange;

        /// <summary>
        ///     The magic damage type.
        /// </summary>
        private static int _magicDamageType;

        /// <summary>
        ///     The magic damage range.
        /// </summary>
        private static int _magicDamageRange;

        /// <summary>
        ///     The magic damage.
        /// </summary>
        private static int _magicDamage;

        /// <summary>
        ///     The damage type.
        /// </summary>
        private static int _damageType;

        /// <summary>
        ///     The damage range.
        /// </summary>
        private static int _damageRange;

        /// <summary>
        ///     The damage.
        /// </summary>
        private static int _damage;

        /// <summary>
        ///     The armor type.
        /// </summary>
        private static int _armorType;

        /// <summary>
        ///     The armor.
        /// </summary>
        private static int _armor;

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
        ///     Gets or sets the armor type.
        /// </summary>
        public int ArmorType
        {
            get => _armorType;
            set
            {
                _armorType = value;
                RaisePropertyChangedEvent(nameof(ArmorType));
            }
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
        ///     Gets or sets the damage type.
        /// </summary>
        public int DamageType
        {
            get => _damageType;
            set
            {
                _damageType = value;
                RaisePropertyChangedEvent(nameof(DamageType));
            }
        }

        /// <summary>
        ///     Gets or sets the magic damage.
        /// </summary>
        public int MagicDamage
        {
            get => _magicDamage;
            set
            {
                _magicDamage = value;
                RaisePropertyChangedEvent(nameof(MagicDamage));
            }
        }

        /// <summary>
        ///     Gets or sets the magic damage range.
        /// </summary>
        public int MagicDamageRange
        {
            get => _magicDamageRange;
            set
            {
                _magicDamageRange = value;
                RaisePropertyChangedEvent(nameof(MagicDamageRange));
            }
        }

        /// <summary>
        ///     Gets or sets the magic damage type.
        /// </summary>
        public int MagicDamageType
        {
            get => _magicDamageType;
            set
            {
                _magicDamageType = value;
                RaisePropertyChangedEvent(nameof(MagicDamageType));
            }
        }

        /// <summary>
        ///     Gets or sets the ranged damage.
        /// </summary>
        public int RangedDamage
        {
            get => _rangedDamage;
            set
            {
                _rangedDamage = value;
                RaisePropertyChangedEvent(nameof(RangedDamage));
            }
        }

        /// <summary>
        ///     Gets or sets the ranged damage range.
        /// </summary>
        public int RangedDamageRange
        {
            get => _rangedDamageRange;
            set
            {
                _rangedDamageRange = value;
                RaisePropertyChangedEvent(nameof(RangedDamageRange));
            }
        }

        /// <summary>
        ///     Gets or sets the physical resistance.
        /// </summary>
        public int PhysicalResistance
        {
            get => _physicalResistance;
            set
            {
                _physicalResistance = value;
                RaisePropertyChangedEvent(nameof(PhysicalResistance));
            }
        }

        /// <summary>
        ///     Gets or sets the psychological resistance.
        /// </summary>
        public int PsychologicalResistance
        {
            get => _psychologicalResistance;
            set
            {
                _psychologicalResistance = value;
                RaisePropertyChangedEvent(nameof(PsychologicalResistance));
            }
        }

        /// <summary>
        ///     Gets or sets the fire resistance.
        /// </summary>
        public int FireResistance
        {
            get => _fireResistance;
            set
            {
                _fireResistance = value;
                RaisePropertyChangedEvent(nameof(FireResistance));
            }
        }

        /// <summary>
        ///     Gets or sets the water resistance.
        /// </summary>
        public int WaterResistance
        {
            get => _waterResistance;
            set
            {
                _waterResistance = value;
                RaisePropertyChangedEvent(nameof(WaterResistance));
            }
        }

        /// <summary>
        ///     Gets or sets the earth resistance.
        /// </summary>
        public int EarthResistance
        {
            get => _earthResistance;
            set
            {
                _earthResistance = value;
                RaisePropertyChangedEvent(nameof(EarthResistance));
            }
        }

        /// <summary>
        ///     Gets or sets the air resistance.
        /// </summary>
        public int AirResistance
        {
            get => _airResistance;
            set
            {
                _airResistance = value;
                RaisePropertyChangedEvent(nameof(AirResistance));
            }
        }

        /// <summary>
        ///     Gets or sets the dark resistance.
        /// </summary>
        public int DarkResistance
        {
            get => _darkResistance;
            set
            {
                _darkResistance = value;
                RaisePropertyChangedEvent(nameof(DarkResistance));
            }
        }

        /// <summary>
        ///     Gets or sets the light resistance.
        /// </summary>
        public int LightResistance
        {
            get => _lightResistance;
            set
            {
                _lightResistance = value;
                RaisePropertyChangedEvent(nameof(LightResistance));
            }
        }
    }
}