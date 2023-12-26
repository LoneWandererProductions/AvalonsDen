/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CampaignsHelper.cs
 * PURPOSE:     Basic Helper Functions Across the Campaign Folder
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using CampaignDriver;
using Resources;

namespace Campaigns
{
    /// <summary>
    ///     The campaigns helper class.
    /// </summary>
    internal static class CampaignsHelper
    {
        /// <summary>
        ///     The index
        /// </summary>
        private static int _index;

        /// <summary>
        ///     The generate save.
        /// </summary>
        /// <param name="saveName">The saveName.</param>
        /// <returns>The Save Object <see cref="SaveInfos" />.</returns>
        internal static SaveInfos GenerateSave(string saveName)
        {
            //get Handler
            var cpn = HandlerInputSingleton.Create();

            var characterId = cpn.GetParty();

            return new SaveInfos
            {
                ImageId = CampaignsRegister.ImageId,
                PositionId = CampaignsRegister.TileId(),
                MapName = CampaignsRegister.MapName,
                CampaignName = CampaignsRegister.CampaignName,
                CharacterId = characterId,
                ActualTime = CampaignsRegister.ActualTime,
                SaveName = saveName
            };
        }

        /// <summary>
        ///     Converts the items.
        ///     Max input are 10 Unique Items
        ///     No Failre Checks should all be catched in the DB Connection
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>Converted Item for the Loot Window</returns>
        internal static Dictionary<int, LootingItemView> ConvertItems(InventoryHolder items)
        {
            var holder = new Dictionary<int, LootingItemView>();
            //TODO add check!

            if (items.ArmorItems.Count != 0)
                foreach (var item in items.ArmorItems)
                {
                    var view = new LootingItemView
                    {
                        Id = item.Key,
                        Image = items.Image[item.Key],
                        Amount = items.Amount[item.Key]
                    };
                    holder.Add(GetKey(), view);
                }

            if (items.MiscellaneousItems.Count != 0)
                foreach (var item in items.MiscellaneousItems)
                {
                    var view = new LootingItemView
                    {
                        Id = item.Key,
                        Image = items.Image[item.Key],
                        Amount = items.Amount[item.Key]
                    };
                    holder.Add(GetKey(), view);
                }

            if (items.WeaponItems.Count != 0)
                foreach (var item in items.WeaponItems)
                {
                    var view = new LootingItemView
                    {
                        Id = item.Key,
                        Image = items.Image[item.Key],
                        Amount = items.Amount[item.Key]
                    };
                    holder.Add(GetKey(), view);
                }

            //TODO Simplify ugly as the night plus adds Amounts
            for (var i = _index; i < 20; i++)
            {
                var view = new LootingItemView
                {
                    Id = -1,
                    Image = string.Empty,
                    Amount = 0
                };
                holder.Add(i, view);
            }

            return holder;
        }

        /// <summary>
        ///     Convers to slot.
        /// </summary>
        /// <param name="itm">The itm.</param>
        /// <returns>Items as slot Element</returns>
        internal static List<InventorySlot> ConvertToSlot(IEnumerable<KeyValuePair<int, int>> itm)
        {
            return itm.Select(item => new InventorySlot {Id = item.Key, Amount = item.Value}).ToList();
        }

        /// <summary>
        ///     Gets the key.
        /// </summary>
        /// <returns>0increased Index</returns>
        private static int GetKey()
        {
            return _index++;
        }
    }
}