/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/StringParser.cs
 * PURPOSE:     LHandle the String Parsing
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExtendedSystemObjects;
using Resources;

namespace EventEngine
{
    /// <summary>
    ///     The string parser class.
    /// </summary>
    internal static class StringParser
    {
        /// <summary>
        ///     The chars to remove (readonly). Value: { EventEngineResources.BracketLeft, EventEngineResources.BracketRight }.
        /// </summary>
        private static readonly string[] CharsToRemove =
        {
            EventEngineResources.BracketLeft,
            EventEngineResources.BracketRight
        };

        /// <summary>
        ///     Converts script into fitting Object
        /// </summary>
        /// <param name="asset">Script string</param>
        /// <returns>Split into correct Object</returns>
        internal static List<InventoryContainer> ParseItems(string asset)
        {
            asset = asset.Trim();
            var items = new List<InventoryContainer>();

            var lst = SplitByBrackets(asset);

            if (lst.IsNullOrEmpty()) return null;

            foreach (var rslt in lst.Select(elemnent => elemnent.Split(',').ToList()))
            {
                var check = int.TryParse(rslt[0], out _);
                if (!check) return null;

                check = int.TryParse(rslt[1], out var amount);
                if (!check) return null;

                var item = new InventoryContainer
                {
                    Id = rslt[0],
                    Amount = amount
                };

                items.Add(item);
            }

            return items;
        }

        /// <summary>
        ///     List of bracket objects
        /// </summary>
        /// <param name="asset">Script string</param>
        /// <returns>Split by brackets</returns>
        private static List<string> SplitByBrackets(string asset)
        {
            var lst = new List<string>();

            do
            {
                var pos1 = asset.IndexOf(EventEngineResources.BracketLeft, StringComparison.Ordinal);
                var pos2 = asset.IndexOf(EventEngineResources.BracketRight, StringComparison.Ordinal);
                var length = pos2 - pos1 + 1;
                var newstr = asset.Substring(pos1, length);
                newstr = Regex.Replace(newstr, @"\s+", string.Empty);

                newstr = CharsToRemove.Aggregate(newstr, (current, c) => current.Replace(c, string.Empty));

                lst.Add(newstr);
                asset = asset.Remove(pos1, length);
            } while (asset.Contains(EventEngineResources.BracketRight));

            return lst;
        }
    }
}