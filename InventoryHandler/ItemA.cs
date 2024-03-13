using System;
using System.Collections.Generic;
using ExtendedSystemObjects;

namespace InventoryHandler
{
    public sealed class ItemA : ICloneable
    {
        /// <summary>
        ///     Constructor for creating an ItemA with all attributes.
        /// </summary>
        /// <param name="slots">The list of slots that are allowed.</param>
        /// <param name="stack">The stack size of the item.</param>
        /// <param name="maxStack">The maximum stack size allowed for the item.</param>
        /// <param name="weight">The weight of the item.</param>
        /// <param name="itemId">The ID of the item.</param>
        /// <param name="slot">The allowed slot for this item.</param>
        /// <param name="singleSlot">Whether the item uses a single slot.</param>
        /// <param name="multiSlot">Whether the item can be interchanged in various slots.</param>
        /// <param name="name">The name of the item.</param>
        /// <param name="tooltip">The tooltip information for the item.</param>
        public ItemA(
            List<int> slots,
            int stack,
            int maxStack,
            int weight,
            int itemId,
            int slot = -1,
            bool singleSlot = true,
            bool multiSlot = false,
            string name = "",
            string tooltip = "")
        {
            Slot = slot;
            Slots = slots;
            SingleSlot = singleSlot;
            MultiSlot = multiSlot;
            Name = name;
            Stack = stack;
            MaxStack = maxStack;
            Weight = weight;
            ItemId = itemId;
            Tooltip = tooltip;

            // Ensure required attributes are set, slot or Slots
            if (Slot == -1 && slots.IsNullOrEmpty())
                throw new ArgumentException(
                    $"RequiredAttribute must be set.{nameof(Slot)} or  RequiredAttribute must be set.{nameof(Slots)}");
            if (stack == 0) throw new ArgumentException("RequiredAttribute must be set.", nameof(stack));
            if (maxStack == 0) throw new ArgumentException("RequiredAttribute must be set.", nameof(maxStack));
            if (maxStack < stack)
                throw new ArgumentException($"Logical Error. {nameof(stack)} must be smaller than {nameof(maxStack)}.");
            if (Slot == -1 && slots.IsNullOrEmpty())
                throw new ArgumentException("Either Slot or Slots mist be set.");
            if (Slot != -1 && !slots.IsNullOrEmpty())
                throw new ArgumentException("Slot and Slots are mutually exclusive.");

        }

        public ItemA(int stack, int maxStack, int weight, int itemId) : this(new List<int>(), stack, maxStack, weight, itemId)
        {
        }


        public ItemA()
        {
            Slots = new List<int>();
            ItemId = -1;
        }

        /// <summary>
        ///     The allowed Slot, that are allowed for this item.
        /// </summary>
        /// <value>
        ///     One Slot per item
        /// </value>
        public int Slot { get; set; }

        /// <summary>
        ///     List of Slots, that are not allowed by this Character!
        /// </summary>
        /// <value>
        ///     The List of Slots that are allowed
        ///     2 slots will be used
        /// </value>
        public List<int> Slots { get; set; }

        /// <summary>
        ///     Determines if this ItemA uses a single Slot or multiple.
        /// </summary>
        /// <value>
        ///     If <c>true</c> this items only use one Slot <c>false</c>.
        /// </value>
        public bool SingleSlot { get; set; }

        /// <summary>
        ///     If SingleSlot, we might ask, if we can dual wield?
        /// </summary>
        /// <value>
        ///     If <c>true</c> this items can be interchanged in the hand slots, ring slots, artifact slots <c>false</c>.
        /// </value>
        public bool MultiSlot { get; set; }

        public string Name { get; set; }

        public int Stack { get; set; }

        public int MaxStack { get; set; }

        public int Weight { get; set; }

        public int ItemId { get; set; } 

        public string Tooltip { get; set; }

        public object Clone()
        {
            var clone = (ItemA)MemberwiseClone();

            // Deep copy for the List<int>
            if (Slots != null) clone.Slots = new List<int>(Slots);

            return clone;
        }
    }
}