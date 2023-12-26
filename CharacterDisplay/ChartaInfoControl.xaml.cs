/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/ChartaInfoControl.xaml.cs
 * PURPOSE:     Display the Biography of the Character
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;
using GameEngine;

namespace CharacterDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Just the Biography nothing will be changeable by user static Data
    /// </summary>
    internal sealed partial class ChartaInfoControl
    {
        /// <inheritdoc />
        /// <summary>
        ///     Load up the whole mess and load the data
        /// </summary>
        public ChartaInfoControl()
        {
            CharacterControl.CharacterBiography += CharacterControl_CharacterBiography;
            InitializeComponent();
        }

        /// <summary>
        ///     Load the Character Bio
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void CharacterControl_CharacterBiography(object sender, CharCharta e)
        {
            DataContext = e;
        }
    }
}