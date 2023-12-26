/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/GearInfoControl.xaml.cs
 * PURPOSE:     Display all Data of the Character
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;

namespace CharacterDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The gear info control class.
    /// </summary>
    public partial class GearInfoControl
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:CharacterDisplay.GearInfoControl" /> class.
        /// </summary>
        internal GearInfoControl()
        {
            DataContext = this;
            InitializeComponent();
        }
    }
}