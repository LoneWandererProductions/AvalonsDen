/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/CharacterBaseStats.cs
 * PURPOSE:     Basic Character Data for Persistent Saving
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     All basic Stats collected with Level up etc
    ///     Will fill up all Data-binding Objects
    ///     Basic Head for Character Data, won't change at runtime! Will be consistent, aside from the generic special
    ///     parameters that will be set at runtime
    ///     Here we just have PC Stats, will change along in the Campaign
    /// </summary>
    public sealed class CharacterBaseStats : ObservableObject
    {
        /// <summary>
        ///     The agility stat.
        /// </summary>
        private int _agility;

        /// <summary>
        ///     The character class.
        /// </summary>
        private string _characterClass;

        /// <summary>
        ///     The charisma stat.
        /// </summary>
        private int _charisma;

        /// <summary>
        ///     The class type.
        /// </summary>
        private int _classType;

        /// <summary>
        ///     The endurance stat.
        /// </summary>
        private int _endurance;

        /// <summary>
        ///     The intelligence stat.
        /// </summary>
        private int _intelligence;

        /// <summary>
        ///     The level stat.
        /// </summary>
        private int _level;

        /// <summary>
        ///     The strength stat.
        /// </summary>
        private int _strength;

        /// <summary>
        ///     The will stat.
        /// </summary>
        private int _will;

        /// <summary>
        ///     The wisdom.
        /// </summary>
        private int _wisdom;

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                RaisePropertyChangedEvent(nameof(Level));
            }
        }

        /// <summary>
        ///     Gets or sets the character class.
        /// </summary>
        public string CharacterClass
        {
            get => _characterClass;
            set
            {
                _characterClass = value;
                RaisePropertyChangedEvent(nameof(CharacterClass));
            }
        }

        /// <summary>
        ///     Gets or sets the class type.
        /// </summary>
        public int ClassType
        {
            get => _classType;
            set
            {
                _classType = value;
                RaisePropertyChangedEvent(nameof(ClassType));
            }
        }

        /// <summary>
        ///     Gets or sets the wisdom.
        /// </summary>
        public int Wisdom
        {
            get => _wisdom;
            set
            {
                _wisdom = value;
                RaisePropertyChangedEvent(nameof(Wisdom));
            }
        }

        /// <summary>
        ///     Gets or sets the strength.
        /// </summary>
        public int Strength
        {
            get => _strength;
            set
            {
                _strength = value;
                RaisePropertyChangedEvent(nameof(Strength));
            }
        }

        /// <summary>
        ///     Gets or sets the agility.
        /// </summary>
        public int Agility
        {
            get => _agility;
            set
            {
                _agility = value;
                RaisePropertyChangedEvent(nameof(Agility));
            }
        }

        /// <summary>
        ///     Gets or sets the intelligence.
        /// </summary>
        public int Intelligence
        {
            get => _intelligence;
            set
            {
                _intelligence = value;
                RaisePropertyChangedEvent(nameof(Intelligence));
            }
        }

        /// <summary>
        ///     Gets or sets the will.
        /// </summary>
        public int Will
        {
            get => _will;
            set
            {
                _will = value;
                RaisePropertyChangedEvent(nameof(Will));
            }
        }

        /// <summary>
        ///     Gets or sets the charisma.
        /// </summary>
        public int Charisma
        {
            get => _charisma;
            set
            {
                _charisma = value;
                RaisePropertyChangedEvent(nameof(Charisma));
            }
        }

        /// <summary>
        ///     Gets or sets the endurance.
        /// </summary>
        public int Endurance
        {
            get => _endurance;
            set
            {
                _endurance = value;
                RaisePropertyChangedEvent(nameof(Endurance));
            }
        }
    }
}