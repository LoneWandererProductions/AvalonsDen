/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/LootScreen.cs
 * PURPOSE:     Helper class that handles all Tile Interactions and calculates the Clicked Cell
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using ExtendedSystemObjects;
using Resources;

namespace ItemExchange
{
    /// <summary>
    ///     Possible Actions for the Click
    /// </summary>
    internal enum Movements
    {
        /// <summary>
        ///     The Nothing.
        /// </summary>
        Nothing,

        /// <summary>
        ///     The Move.
        /// </summary>
        Move,

        /// <summary>
        ///     The Swap.
        /// </summary>
        Swap,

        /// <summary>
        ///     The Fill.
        /// </summary>
        Fill,

        /// <summary>
        ///     The Spill.
        /// </summary>
        Spill
    }

    /// <summary>
    ///     Handle all of the Movements of items
    /// </summary>
    internal static class StackExchange
    {
        /// <summary>
        ///     The cell
        /// </summary>
        private static int _cell;

        /// <summary>
        ///     Key:
        ///     Key is the Name as string of the Cell
        ///     The Cube Dictionary
        ///     X, y Coordinates
        ///     Id of the cube
        /// </summary>
        internal static Dictionary<string, Cube> Cubes { get; private set; }

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>
        ///     Key:
        ///     The Key is the id as int of the Cell
        ///     Value:
        ///     The items.
        ///     Key the Position, Item the Information, amount, id and Image
        /// </value>
        internal static Dictionary<int, LootingItemView> Items { get; private set; }

        /// <summary>
        ///     The initiate stack.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="weight">The weight.</param>
        /// <returns>The <see cref="T:Dictionary{int, Item}" />.</returns>
        internal static Dictionary<int, LootingItemView> InitiateStack(int height, int weight)
        {
            if (height * weight <= 0) return null;

            //Initiate
            var loot = new Dictionary<int, LootingItemView>(height * weight);

            for (var i = 0; i < height * weight; i++)
            {
                var matrix = new LootingItemView { Amount = 0 };
                loot.Add(i, matrix);
            }

            return loot;
        }

        /// <summary>
        ///     Generates the cubes.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="length">The length.</param>
        /// <param name="cell">The cell.</param>
        /// <param name="splitter">Length of the splitter, if not needed set this to 0</param>
        /// <param name="row">starting row of the splitter  if not needed set splitter to 0</param>
        internal static void GenerateCubes(int height, int length, int cell, int splitter, int row)
        {
            Cubes = new Dictionary<string, Cube>();

            //don't start at 0
            var count = -1;
            _cell = cell;

            for (var i = 0; i < length; i++)
            for (var j = 0; j < height; j++)
            {
                count++;

                var cube = i >= row
                    ? new Cube { XOne = i * cell + splitter, YOne = j * cell, Id = count }
                    : new Cube { XOne = i * cell, YOne = j * cell, Id = count };

                Cubes.Add(string.Concat(LootResources.NameExtension, i, j), cube);
            }
        }

        /// <summary>
        ///     Initiates the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        internal static void Initiate(Dictionary<int, LootingItemView> items)
        {
            Items = items;
        }

        /// <summary>
        ///     Moves the specified position one.
        /// </summary>
        /// <param name="posOne">The position one.</param>
        /// <param name="posTwo">The position two.</param>
        internal static void Move(int posOne, int posTwo)
        {
            if (!Items.ContainsKey(posOne) || !Items.ContainsKey(posTwo))
                ChangePosition(Movements.Nothing, posOne, posTwo);

            // best case just move
            if (Items[posTwo].Amount == 0)
            {
                ChangePosition(Movements.Move, posOne, posTwo);
            }
            //or their is something
            else
            {
                // not equal swap
                if (Items[posOne].Id != Items[posTwo].Id)
                    ChangePosition(Movements.Swap, posOne, posTwo);
                else
                    //equal but max swap
                    ChangePosition(
                        Items[posOne].Amount + Items[posTwo].Amount > Items[posOne].MaxStack
                            ? Movements.Spill
                            : Movements.Fill, posOne, posTwo);
            }
        }

        /// <summary>
        ///     Gets the cell Name
        /// </summary>
        /// <param name="x">The x Position.</param>
        /// <param name="y">The y Position.</param>
        /// <returns>Name of the Control</returns>
        internal static string GetCell(int x, int y)
        {
            return Cubes.Where(cube => IntervalCheck(x, y, cube.Value.XOne, cube.Value.YOne)).Select(cube => cube.Key)
                .FirstOrDefault();
        }

        /// <summary>
        ///     Gets the loot.
        ///     Max Slots are 20 all items above key 10 will be added to the Inventory
        /// </summary>
        /// <returns>The lootet Items, key is the id, value the amount</returns>
        internal static List<KeyValuePair<int, int>> GetLoot()
        {
            var itm = new List<KeyValuePair<int, int>>();

            //no need to check if key exists, it must exist or else we fucked up big time
            for (var i = LootResources.InventoryLimiter; i < LootRegister.Loot.Count; i++)
                if (LootRegister.Loot[i].Id != -1)
                    itm.Add(new KeyValuePair<int, int>(i, LootRegister.Loot[i].Amount));
            return itm;
        }

        /// <summary>
        ///     Gets the identifier of the Cube and if it exists the item by Id
        /// </summary>
        /// <param name="x">The x Position.</param>
        /// <param name="y">The y. Position</param>
        /// <returns>Id of the cube</returns>
        internal static int GetId(double x, double y)
        {
            var name = GetCell((int)x, (int)y);

            if (string.IsNullOrEmpty(name)) return -1;

            var cube = Cubes[name];

            if (!Items.ContainsKey(cube.Id)) return -1;

            return cube.Id;
        }

        /// <summary>
        ///     The change position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="posOne">The posOne.</param>
        /// <param name="posTwo">The posTwo.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static void ChangePosition(Movements position, int posOne, int posTwo)
        {
            switch (position)
            {
                case Movements.Nothing:
                    //well nothing
                    break;

                case Movements.Move:
                    Items.Swap(posOne, posTwo);
                    //clear old Stack
                    break;

                case Movements.Swap:
                    //swap
                    //update Position within the Object
                    Items.Swap(posOne, posTwo);
                    break;

                case Movements.Fill:
                    //add up
                    Items[posTwo].Amount += Items[posOne].Amount;
                    //clear old Stack
                    Items[posOne] = new LootingItemView();
                    break;

                case Movements.Spill:
                    var sum = Items[posTwo].Amount + Items[posOne].Amount;
                    Items[posTwo].Amount = Items[posTwo].MaxStack;
                    Items[posOne].Amount = sum - Items[posTwo].MaxStack;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }

        /// <summary>
        ///     The interval check.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="xOne">The xOne.</param>
        /// <param name="yOne">The yOne.</param>
        /// <returns>The hit Check if we are within a rage of a specific button return <see cref="bool" /> true.</returns>
        private static bool IntervalCheck(int x, int y, int xOne, int yOne)
        {
            return IntervalCheck(x, xOne, xOne + _cell) && IntervalCheck(y, yOne, yOne + _cell);
        }

        /// <summary>
        ///     The interval check.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>If it is within the Interval return <see cref="bool" /> true.</returns>
        private static bool IntervalCheck(int x, int start, int end)
        {
            return x >= start && x <= end;
        }
    }
}