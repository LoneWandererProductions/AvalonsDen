/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/ILooting.cs
 * PURPOSE:     Looting Interface
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

using System.Collections.Generic;
using Resources;

namespace ItemExchange
{
    /// <summary>
    ///     The Looting Interface
    /// </summary>
    internal interface ILooting
    {
        /// <summary>
        ///     Starts the screen.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="imagePath">The image path.</param>
        /// <returns>Looted Items</returns>
        List<KeyValuePair<int, int>> StartScreen(Dictionary<int, LootingItemView> item, string imagePath);
    }
}