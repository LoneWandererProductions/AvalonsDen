using System;
using System.Collections.Generic;

namespace InventoryHandler
{
    public sealed class ItemA : ICloneable
    {
        /// <summary>
        /// The slots
        /// </summary>
        private List<int> _slots = new();

        /// <summary>
        /// The slot
        /// </summary>
        private int _slot = -1;

        /// <summary>
        ///     The allowed Slot, that are allowed for this item.
        /// </summary>
        /// <value>
        ///     One Slot per item
        /// </value>
        public int Slot
        {
            get => _slot;
            set
            {
                // Ensure mutual exclusivity
                if (_slots.Count > 0)
                {
                    throw new InvalidOperationException("SingleValue and MultipleValues are mutually exclusive.");
                }
                _slot = value;
            }
        }

        /// <summary>
        ///     List of Slots, that are not allowed by this Character!
        /// </summary>
        /// <value>
        ///     The List of Slots that are allowed
        ///     2 slots will be used
        /// </value>
        public List<int> Slots
        {
            get => _slots;
            set
            {
                // Ensure mutual exclusivity
                if (Slot != -1)
                {
                    throw new InvalidOperationException("SingleValue and MultipleValues are mutually exclusive.");
                }

                _slots = value;
            }
        }

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

        public int Name { get; set; }

        public int Stack { get; set; }

        public int MaxStack { get; set; }

        public int Weight { get; set; }

        public int ItemId { get; set; }

        public string Tooltip { get; set; }

        public object Clone()
        {
            var clone = (ItemA) MemberwiseClone();

            // Deep copy for the List<int>
            if (Slots != null) clone.Slots = new List<int>(Slots);

            return clone;
        }
    }
}