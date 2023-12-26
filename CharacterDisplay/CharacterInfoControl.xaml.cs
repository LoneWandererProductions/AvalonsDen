/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CharacterInfoControl.xaml.cs
 * PURPOSE:     Display all Data of the Character
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * Sources:     http://stackoverflow.com/questions/31609312/wpf-twoway-binding-to-a-static-class-property
 */

using System.Windows;
using GameEngine;

namespace CharacterDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Display Control for all Character Informations
    /// </summary>
    internal sealed partial class CharacterInfoControl
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:CharacterDisplay.CharacterInfoControl" /> class.
        /// </summary>
        public CharacterInfoControl()
        {
            CharacterControl.CharacterStats += CharacterControl_CharacterStats;
            InitializeComponent();
        }

        /// <summary>
        ///     Set Data Context
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The Char Statistics.</param>
        private void CharacterControl_CharacterStats(object sender, CharStatistics e)
        {
            DataContext = e;
        }
    }
}