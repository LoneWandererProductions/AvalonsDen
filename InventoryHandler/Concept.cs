using System.Collections.Generic;
using System.Linq;

namespace InventoryHandler
{
    public class Concept
    {
        /// <summary>
        ///     The identifier mapping
        /// </summary>
        private readonly Dictionary<int, string> idMapping = new()
        {
            {0, "Artifact 0"},
            {1, "Artifact 1"},
            {2, "Artifact 2"},
            {3, "Artifact 3"},
            {4, "Artifact 4"},
            {5, "Artifact 5"},
            {6, "Head"},
            {8, "Shoulder"},
            {9, "Amulet"},
            {10, "Left Hand"},
            {11, "Right Hand"},
            {12, "Off Hand"},
            {13, "Ring Left"},
            {14, "Ring Right"},
            {15, "Belt"},
            {16, "Chest"},
            {17, "Gloves"},
            {18, "Trousers"},
            {19, "Shoes"}
        };

        /// <summary>
        ///     Gets or sets the nameof the character
        ///     Optional Data
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        public int CharacterId { get; set; }

        public int Weight { get; set; }

        /// <summary>
        ///     List of Slots, that are not allowed by this Character!
        /// </summary>
        public List<int> Limitations { get; set; }

        /// <summary>
        ///     First 19 Slots are reserved for the character Slots
        ///     All the rest is Inventory Space.
        ///     id: -1 means that stuff is only used in the inventory
        ///     id: 0-5, Artifact 0-5, interchangeable
        ///     id: 6, Head
        ///     id: 8, Shoulder
        ///     id: 9, Amulet
        ///     id: 10, Left Hand
        ///     id: 11, Right Hand
        ///     id: 12, Off Hand
        ///     id: 13, Ring Left
        ///     id: 14, Ring Right
        ///     id: 15, Belt
        ///     id: 16, Chest
        ///     id: 17, Gloves
        ///     id: 18, Trousers
        ///     id, 19, Shoes
        ///     id 20+, Inventory
        /// </summary>
        /// <value>
        ///     The ItemA and Infos about the ItemA
        /// </value>
        public Dictionary<int, ItemA> Inventory { get; set; } = new();

        /// <summary>
        ///     First 18 Slots are reserved for the character Slots
        ///     We must now define, if some slots are allowed to field more numbers of Items, e.g. Off Hand arrows
        /// </summary>
        /// <value>
        ///     The slot Id and the amount of Items allowed
        /// </value>
        public Dictionary<int, int> ItemCount { get; set; } = new();

        /// <summary>
        ///     List of used Slots, in the Inventory Area
        /// </summary>
        /// <value>
        ///     itemslots Used
        /// </value>
        private List<int> Backpack { get; } = new();

        /// <summary>
        ///     Auto add ItemA to Inventory
        ///     If allowed Slot is free, it will go to Inventory, if not. It will go to the inventory
        ///     In any case the item will be at least moved to the Inventory. It returns false, if it was moved to inventory
        /// </summary>
        /// <param name="item">The item we'd like to add.</param>
        /// <param name="position">Optional Parameter, if we add a position we handle it.</param>
        /// <returns>Was item added or not, if not, no slot was available.</returns>
        public bool AddItem(ItemA item, int position = -1)
        {
            //basic inventory area
            if (!idMapping.ContainsKey(position))
            {
                if (Inventory.ContainsKey(position))
                    //slot not empty? well find an empty spot
                    MoveToInventory(item, true);
                else
                    //if empty just slot it into position
                    Inventory.Add(position, item);

                return true;
            }

            //character area
            if (item.SingleSlot)
            {
                if (item.MultiSlot) return HandleMultiSlot(item);
                return HandleSingleSlot(item);
            }

            return HandleDualSlot(item);
        }

        /// <summary>
        ///     Remove item from specified Slot
        /// </summary>
        /// <param name="position">Removes item at position.</param>
        /// <returns>The removed item, mull, if none was removed.</returns>
        public ItemA RemoveItem(int position)
        {
            if (Inventory.ContainsKey(position))
            {
                var cache = Inventory[position];
                Inventory.Remove(position);
                Weight += cache.Weight;
                return cache;
            }

            return null;
        }

        private bool HandleSingleSlot(ItemA item)
        {
            if (Limitations.Contains(item.Slot))
            {
                MoveToInventory(item, false);
                return false;
            }

            //move and check left overs into Inventory, if they do exist
            var max = ItemCount[item.Slot];

            if (max > item.Stack)
            {
                var cache = item.Clone() as ItemA;
                cache.Stack -= max;
                item.Stack = max;
                MoveToInventory(cache, false);
            }

            Weight += item.Weight * item.Stack;

            Inventory.Add(item.Slot, item);

            return true;
        }

        /// <summary>
        ///     First checks if one possible Slot is free and adds it.
        ///     Here for Slots one must be all free.
        ///     E.g. stuff like Rings, Artifacts, Hand Weapons
        ///     If not returns false.
        /// </summary>
        /// <param name="item">The item we'd like to add.</param>
        /// <returns>Was item added or not, if not, no slot was available.</returns>
        private bool HandleMultiSlot(ItemA item)
        {
            foreach (var slot in item.Slots)
                if (!Inventory.ContainsKey(slot))
                {
                    //move and check left overs into Inventory, if they do exist
                    var max = ItemCount[item.Slot];

                    if (max > item.Stack)
                    {
                        var cache = item.Clone() as ItemA;
                        cache.Stack -= max;
                        item.Stack = max;
                        MoveToInventory(cache, false);
                    }

                    Weight += item.Weight * item.Stack;

                    Inventory.Add(slot, item);
                    return true;
                }

            MoveToInventory(item, false);

            return false;
        }

        /// <summary>
        ///     First checks if all needed Slots are free and adds it.
        ///     Here for Slots all must be free
        ///     If not returns false.
        /// </summary>
        /// <param name="item">The item we'd like to add.</param>
        /// <returns>Was item added or not, if not, no slot was available.</returns>
        private bool HandleDualSlot(ItemA item)
        {
            foreach (var slot in item.Slots)
                if (Inventory.ContainsKey(slot))
                {
                    MoveToInventory(item, false);
                    return false;
                }

            //move and check left overs into Inventory, if they do exist
            var max = ItemCount[item.Slot];

            if (max > item.Stack)
            {
                var cache = item.Clone() as ItemA;
                cache.Stack -= max;
                item.Stack = max;
                MoveToInventory(cache, false);
            }

            Weight += item.Weight * item.Stack;

            Inventory.Add(item.Slot, item);

            return true;
        }

        private void MoveToInventory(ItemA item, bool recurse)
        {
            if (item.MaxStack > 1 && !recurse)
            {
                //just check the first item with the same Id if already max add at the end
                //find similar items which are not stacked to max Stack
                //use up all items with less than max stack, and in the end add the leftovers.
                foreach (var kvp in Inventory)
                {
                    var cache = kvp.Value;
                    if (cache.ItemId != item.ItemId) continue;
                    if (cache.MaxStack == item.Stack) continue;

                    //found the first which is not max stack
                    var top = item.Stack + cache.Stack;

                    if (top <= cache.MaxStack)
                    {
                        Inventory[cache.ItemId].Stack = top;
                        //add all the weight
                        AddWeight(item.Weight * item.Stack);
                        //all done and bail
                        return;
                    }

                    AddWeight(cache.Weight * (cache.MaxStack - cache.Stack));
                    Inventory[cache.ItemId].Stack = cache.MaxStack;

                    var newItem = item.Clone() as ItemA;
                    newItem.Stack = cache.MaxStack - top;

                    //still more, but who cares, just add it at the end
                    //here we bail and do it again
                    MoveToInventory(newItem, true);
                }

                AddToNewSlot(item);
            }
            //default path, if we already topped up
            else
            {
                AddToNewSlot(item);
                AddWeight(item.Weight * item.Stack);
            }
        }

        private void AddToNewSlot(ItemA item)
        {
            var id = GetFirstAvailableIndex(Backpack);
            Backpack.Add(id);
            Inventory.Add(id, item);
        }

        private void AddWeight(int weight)
        {
            Weight += weight;
        }

        /// <summary>
        ///     Gets the first index of the available.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns>First available Slot</returns>
        private static int GetFirstAvailableIndex(IEnumerable<int> lst)
        {
            return Enumerable.Range(20, int.MaxValue)
                .Except(lst)
                .FirstOrDefault();
        }
    }
}