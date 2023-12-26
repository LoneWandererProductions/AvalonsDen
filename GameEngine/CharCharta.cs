/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/GameEngine/CharCharta.cs
 * PURPOSE:     Basic Character Charta
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * Sources: http://gamedev.stackexchange.com/questions/13638/algorithm-for-dynamically-calculating-a-level-based-on-experience-points
 */

using Resources;
using ViewModel;

namespace GameEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     The Charta Class. Contains all Infos of the Character
    /// </summary>
    public sealed class CharCharta : ObservableObject
    {
        /// <summary>
        ///     The lvl Parameter (const). Value: 100.
        /// </summary>
        private const int LvlParam = 100;

        /// <summary>
        ///     The name.
        /// </summary>
        private static string _name;

        /// <summary>
        ///     The character class.
        /// </summary>
        private static string _characterClass;

        /// <summary>
        ///     The biography.
        /// </summary>
        private static string _biography;

        /// <summary>
        ///     The Skill Tree.
        /// </summary>
        private static string _skilltree;

        /// <summary>
        ///     The actual level.
        /// </summary>
        private static int _level;

        /// <summary>
        ///     The maximum level.
        /// </summary>
        private static int _maximumLevel;

        /// <summary>
        ///     Character experience.
        /// </summary>
        private static int _experience;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CharCharta" /> class.
        /// </summary>
        /// <param name="biography">The biography.</param>
        public CharCharta(CharacterBiography biography)
        {
            Name = biography.Name;
            Biography = biography.Biography;
            Level = 1;
        }

        /// <summary>
        ///     Gets the Next level.
        /// </summary>
        public static int Nextlevel => (_level ^ (2 + _level)) / 2 * LvlParam - _level * LvlParam;

        //level = constant * sqrt(XP)

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChangedEvent(nameof(Name));
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
        ///     Gets or sets the biography.
        /// </summary>
        public string Biography
        {
            get => _biography;
            set
            {
                _biography = value;
                RaisePropertyChangedEvent(nameof(Biography));
            }
        }

        /// <summary>
        ///     Gets or sets the Skill tree.
        /// </summary>
        public string Skilltree
        {
            get => _skilltree;
            set
            {
                _skilltree = value;
                RaisePropertyChangedEvent(nameof(Skilltree));
            }
        }

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
        ///     Gets or sets the maximum level.
        /// </summary>
        public int MaximumLevel
        {
            get => _maximumLevel;
            set
            {
                _maximumLevel = value;
                RaisePropertyChangedEvent(nameof(MaximumLevel));
            }
        }

        /// <summary>
        ///     Gets or sets the experience.
        /// </summary>
        public int Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                RaisePropertyChangedEvent(nameof(Experience));
            }
        }
    }
}