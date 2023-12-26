/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/Loot.xaml.cs
 * PURPOSE:     Basic Window to Project our Loot
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeInternal

using System.Collections.Generic;
using Resources;

namespace ItemExchange
{
    /// <inheritdoc cref="System.Windows.Window" />
    /// <summary>
    ///     Basic Loot Window
    /// </summary>
    public sealed partial class Loot
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="Loot" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public Loot(Dictionary<int, LootingItemView> items)
        {
            InitializeComponent();
            Screen.SetData(items);
        }
    }
}