/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorCharacter/CharStat.xaml.cs
 * PURPOSE:     Extended Player Stats
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;
using Resources;

namespace EditorCharacter
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Must be public for fucks sake
    /// </summary>
    public sealed partial class CharStat
    {
        /// <summary>
        ///     The character stats.
        /// </summary>
        private CharacterBaseStats _charStats;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorCharacter.CharStat" /> class.
        /// </summary>
        public CharStat()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Get the character stats.
        /// </summary>
        /// <returns>The <see cref="CharacterBaseStats" />.</returns>
        internal CharacterBaseStats GetStats()
        {
            return _charStats;
        }

        /// <summary>
        ///     Set the character stats.
        /// </summary>
        /// <param name="stats">The stats.</param>
        internal void SetStats(CharacterBaseStats stats)
        {
            _charStats = stats;
            DataContext = _charStats;
        }
    }
}