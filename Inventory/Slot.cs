/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/Slot.cs
 * PURPOSE:     Simple Save Object for the Inventory
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Inventory
{
    /// <inheritdoc />
    /// <summary>
    ///     Slot Save Container for Items, one for party Inventory and one for Equipment we carry
    /// </summary>
    public sealed class Slot : ObservableObject
    {
        /// <summary>
        ///     The amount
        /// </summary>
        private readonly int _amount;

        /// <summary>
        ///     The character identifier
        /// </summary>
        private readonly int _characterId;

        /// <summary>
        ///     The identifier, Id of the Item
        /// </summary>
        private readonly int _id;

        /// <summary>
        ///     The position, if negative it is worn by the Character with this id!
        /// </summary>
        private readonly int _position;

        /// <summary>
        ///     Gets or sets the identifier. The Id of the Item
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id
        {
            get => _id;
            init
            {
                _id = value;
                RaisePropertyChangedEvent(nameof(Id));
            }
        }

        /// <summary>
        ///     Gets or sets the position.
        ///     Positive inventory
        ///     0 should be equipped, inventory starts at 1
        ///     All equipped Items will be 0
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public int Position
        {
            get => _position;
            init
            {
                _position = value;
                RaisePropertyChangedEvent(nameof(Position));
            }
        }

        /// <summary>
        ///     Gets or sets the character identifier.
        ///     If someone carries the Item we must identify who
        /// </summary>
        /// <value>
        ///     The character identifier.
        /// </value>
        public int CharacterId
        {
            get => _characterId;
            init
            {
                _characterId = value;
                RaisePropertyChangedEvent(nameof(CharacterId));
            }
        }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public int Amount
        {
            get => _amount;
            init
            {
                _amount = value;
                RaisePropertyChangedEvent(nameof(Amount));
            }
        }
    }
}