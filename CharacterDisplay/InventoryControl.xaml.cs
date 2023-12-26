/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/InventoryControl.xaml.cs
 * PURPOSE:     Inventory, for now shared for all Characters
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeInternal

using System.Windows;

namespace CharacterDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The inventory control class.
    /// </summary>
    public sealed partial class InventoryControl
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:CharacterDisplay.InventoryControl" /> class.
        /// </summary>
        public InventoryControl()
        {
            InitializeComponent();
        }
    }
}