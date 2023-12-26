/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/InventoryCharacterControl.xaml.cs
 * PURPOSE:     Inventory, for now shared for all Characters
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeInternal

using System.Windows;

namespace CharacterDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The inventory character control class.
    /// </summary>
    public sealed partial class InventoryCharacterControl
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="InventoryCharacterControl" /> class.
        /// </summary>
        public InventoryCharacterControl()
        {
            InitializeComponent();
        }
    }
}