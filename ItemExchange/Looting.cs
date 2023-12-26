/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/Looting.cs
 * PURPOSE:     Implementation of the Interface
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

namespace ItemExchange
{
    /// <inheritdoc />
    /// <summary>
    ///     Basic Interface
    /// </summary>
    /// <seealso cref="T:ItemExchange.ILooting" />
    public sealed class Looting : ILooting
    {
        /// <inheritdoc />
        /// <summary>
        ///     Starts the screen.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="imagePath">The image path.</param>
        /// <returns>Lootet Items, Key id, Value Amount</returns>
        public List<KeyValuePair<int, int>> StartScreen(Dictionary<int, LootingItemView> item, string imagePath)
        {
            LootResources.ImagePath = imagePath;
            var lootWindow = new Loot(item)
            {
                Topmost = true
            };
            lootWindow.ShowDialog();

            return StackExchange.GetLoot();
        }
    }
}