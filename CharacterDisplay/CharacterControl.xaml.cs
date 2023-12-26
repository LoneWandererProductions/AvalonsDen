/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CharacterControl.xaml.cs
 * PURPOSE:     Generic Control for a single Character, will be dynamically added on runtime for each Character
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Windows;
using CharacterEngine;
using GameEngine;

namespace CharacterDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The character control class.
    /// </summary>
    internal sealed partial class CharacterControl
    {
        /// <summary>
        ///     The Stats Calculation (readonly).
        /// </summary>
        private readonly StatsCalculation _statsCalc;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:CharacterDisplay.CharacterControl" /> class.
        /// </summary>
        public CharacterControl()
        {
            InitializeComponent();
            _statsCalc = new StatsCalculation();
        }

        /// <summary>
        ///     The character biography event of the <see cref="EventHandler{TEventArgs}" />.
        /// </summary>
        public static event EventHandler<CharCharta> CharacterBiography;

        /// <summary>
        ///     The character stats event of the <see cref="EventHandler{CharStatistics}" />.
        /// </summary>
        public static event EventHandler<CharStatistics> CharacterStats;

        /// <summary>
        ///     Set the values.
        /// </summary>
        /// <param name="rslt">The Character Info.</param>
        internal void SetValues(CharacterBundle rslt)
        {
            var bio = _statsCalc.CalculateCharacterInformation(rslt.Bio);
            var stats = _statsCalc.CalculateCharacterStatistics(rslt.Stats);

            CharacterBiography?.Invoke(null, bio);
            CharacterStats?.Invoke(null, stats);
        }
    }
}