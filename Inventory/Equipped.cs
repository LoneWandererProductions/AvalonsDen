/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/Equipped.cs
 * PURPOSE:     Basic Equipment Slots for a Character
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Inventory
{
    /// <inheritdoc />
    /// <summary>
    ///     Equipped Items
    ///     Character Id will be used as the Key
    ///     int Id is the item in Question
    ///     positive Position Values of Slot indicate Item Slot.
    ///     negative Position Values of Slot indicate Plac in Inventory
    /// </summary>
    /// <seealso cref="ObservableObject" />
    public sealed class Equipped : ObservableObject
    {
        /// <summary>
        ///     The amulet
        /// </summary>
        private int _amulet;

        /// <summary>
        ///     The belt
        /// </summary>
        private int _belt;

        /// <summary>
        ///     The chest
        /// </summary>
        private int _chest;

        /// <summary>
        ///     The equipment slot one
        /// </summary>
        private int _equipmentSlotOne;

        /// <summary>
        ///     The equipment slot two
        /// </summary>
        private int _equipmentSlotTwo;

        /// <summary>
        ///     The gloves
        /// </summary>
        private int _gloves;

        /// <summary>
        ///     The head
        /// </summary>
        private int _head;

        /// <summary>
        ///     The main hand
        /// </summary>
        private int _mainHand;

        /// <summary>
        ///     The off hand
        /// </summary>
        private int _offHand;

        /// <summary>
        ///     The ring left
        /// </summary>
        private int _ringLeft;

        /// <summary>
        ///     The ring right
        /// </summary>
        private int _ringRight;

        /// <summary>
        ///     The secondary hand
        /// </summary>
        private int _secondaryHand;

        /// <summary>
        ///     The shoes
        /// </summary>
        private int _shoes;

        /// <summary>
        ///     The trousers
        /// </summary>
        private int _trousers;

        /// <summary>
        ///     Gets or sets the head.
        /// </summary>
        /// <value>
        ///     The head.
        /// </value>
        public int Head
        {
            get => _head;
            set
            {
                _head = value;
                RaisePropertyChangedEvent(nameof(Head));
            }
        }

        /// <summary>
        ///     Gets or sets the amulet.
        /// </summary>
        /// <value>
        ///     The amulet.
        /// </value>
        public int Amulet
        {
            get => _amulet;
            set
            {
                _amulet = value;
                RaisePropertyChangedEvent(nameof(Amulet));
            }
        }

        /// <summary>
        ///     Gets or sets the chest.
        /// </summary>
        /// <value>
        ///     The chest.
        /// </value>
        public int Chest
        {
            get => _chest;
            set
            {
                _chest = value;
                RaisePropertyChangedEvent(nameof(Chest));
            }
        }

        /// <summary>
        ///     Gets or sets the gloves.
        /// </summary>
        /// <value>
        ///     The gloves.
        /// </value>
        public int Gloves
        {
            get => _gloves;
            set
            {
                _gloves = value;
                RaisePropertyChangedEvent(nameof(Gloves));
            }
        }

        /// <summary>
        ///     Gets or sets the ring left.
        /// </summary>
        /// <value>
        ///     The ring left.
        /// </value>
        public int RingLeft
        {
            get => _ringLeft;
            set
            {
                _ringLeft = value;
                RaisePropertyChangedEvent(nameof(RingLeft));
            }
        }

        /// <summary>
        ///     Gets or sets the ring right.
        /// </summary>
        /// <value>
        ///     The ring right.
        /// </value>
        public int RingRight
        {
            get => _ringRight;
            set
            {
                _ringRight = value;
                RaisePropertyChangedEvent(nameof(RingRight));
            }
        }

        /// <summary>
        ///     Gets or sets the main hand.
        /// </summary>
        /// <value>
        ///     The main hand.
        /// </value>
        public int MainHand
        {
            get => _mainHand;
            set
            {
                _mainHand = value;
                RaisePropertyChangedEvent(nameof(MainHand));
            }
        }

        /// <summary>
        ///     Gets or sets the off hand.
        /// </summary>
        /// <value>
        ///     The off hand.
        /// </value>
        public int OffHand
        {
            get => _offHand;
            set
            {
                _offHand = value;
                RaisePropertyChangedEvent(nameof(OffHand));
            }
        }

        /// <summary>
        ///     Gets or sets the secondary hand.
        /// </summary>
        /// <value>
        ///     The secondary hand.
        /// </value>
        public int SecondaryHand
        {
            get => _secondaryHand;
            set
            {
                _secondaryHand = value;
                RaisePropertyChangedEvent(nameof(SecondaryHand));
            }
        }

        /// <summary>
        ///     Gets or sets the belt.
        /// </summary>
        /// <value>
        ///     The belt.
        /// </value>
        public int Belt
        {
            get => _belt;
            set
            {
                _belt = value;
                RaisePropertyChangedEvent(nameof(Belt));
            }
        }

        /// <summary>
        ///     Gets or sets the trousers.
        /// </summary>
        /// <value>
        ///     The trousers.
        /// </value>
        public int Trousers
        {
            get => _trousers;
            set
            {
                _trousers = value;
                RaisePropertyChangedEvent(nameof(Trousers));
            }
        }

        /// <summary>
        ///     Gets or sets the shoes.
        /// </summary>
        /// <value>
        ///     The shoes.
        /// </value>
        public int Shoes
        {
            get => _shoes;
            set
            {
                _shoes = value;
                RaisePropertyChangedEvent(nameof(Shoes));
            }
        }

        /// <summary>
        ///     Gets or sets the equipment slot one.
        /// </summary>
        /// <value>
        ///     The equipment slot one.
        /// </value>
        public int EquipmentSlotOne
        {
            get => _equipmentSlotOne;
            set
            {
                _equipmentSlotOne = value;
                RaisePropertyChangedEvent(nameof(EquipmentSlotOne));
            }
        }

        /// <summary>
        ///     Gets or sets the equipment slot two.
        /// </summary>
        /// <value>
        ///     The equipment slot two.
        /// </value>
        public int EquipmentSlotTwo
        {
            get => _equipmentSlotTwo;
            set
            {
                _equipmentSlotTwo = value;
                RaisePropertyChangedEvent(nameof(EquipmentSlotTwo));
            }
        }
    }
}