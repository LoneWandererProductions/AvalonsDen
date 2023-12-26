/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/Item.cs
 * PURPOSE:     Item Template all Items will inherit this class
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     The item class. Base item for all specific Items
    /// </summary>
    public abstract class Item : ObservableObject
    {
        /// <summary>
        ///     The base name.
        /// </summary>
        private string _baseName;

        /// <summary>
        ///     The custom description.
        /// </summary>
        private string _customDescription;

        /// <summary>
        ///     The custom name.
        /// </summary>
        private string _customName;

        /// <summary>
        ///     The description.
        /// </summary>
        private string _description;

        /// <summary>
        ///     The id.
        /// </summary>
        private int _id;

        /// <summary>
        ///     The id of attributes.
        /// </summary>
        private int _idOfAttributes;

        /// <summary>
        ///     The image id.
        /// </summary>
        private int _imageId;

        /// <summary>
        ///     The max stack.
        /// </summary>
        private int _maxStack;

        /// <summary>
        ///     The position
        /// </summary>
        private InventoryEnum.EnumSlot _position;

        /// <summary>
        ///     The rarity.
        /// </summary>
        private int _rarity;

        /// <summary>
        ///     The weight.
        /// </summary>
        private int _weight;

        /// <summary>
        ///     The worth.
        /// </summary>
        private int _worth;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        protected Item()
        {
            MaxStack = 1;
            Weight = 0;
            ImageId = -1;
            IdOfAttributes = -1;
            Worth = 0;
            Description = string.Empty;
            CustomDescription = string.Empty;
            CustomName = string.Empty;
            BaseName = string.Empty;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChangedEvent(nameof(Id));
            }
        }

        /// <summary>
        ///     Gets or sets the image id.
        /// </summary>
        public int ImageId
        {
            get => _imageId;
            set
            {
                _imageId = value;
                RaisePropertyChangedEvent(nameof(ImageId));
            }
        }

        /// <summary>
        ///     Gets or sets the custom name.
        /// </summary>
        public string CustomName
        {
            get => _customName;
            set
            {
                _customName = value;
                RaisePropertyChangedEvent(nameof(CustomName));
            }
        }

        /// <summary>
        ///     Gets or sets the base name.
        /// </summary>
        public string BaseName
        {
            get => _baseName;
            set
            {
                _baseName = value;
                RaisePropertyChangedEvent(nameof(BaseName));
            }
        }

        /// <summary>
        ///     Gets or sets the rarity.
        /// </summary>
        public int Rarity
        {
            get => _rarity;
            set
            {
                _rarity = value;
                RaisePropertyChangedEvent(nameof(Rarity));
            }
        }

        /// <summary>
        ///     Gets or sets the worth.
        /// </summary>
        public int Worth
        {
            get => _worth;
            set
            {
                _worth = value;
                RaisePropertyChangedEvent(nameof(Worth));
            }
        }

        /// <summary>
        ///     Gets or sets the max stack.
        /// </summary>
        public int MaxStack
        {
            get => _maxStack;
            set
            {
                _maxStack = value;
                RaisePropertyChangedEvent(nameof(MaxStack));
            }
        }

        /// <summary>
        ///     Gets or sets the weight.
        /// </summary>
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                RaisePropertyChangedEvent(nameof(Weight));
            }
        }

        /// <summary>
        ///     Second Object that collects all Data of Attributes
        /// </summary>
        public int IdOfAttributes
        {
            get => _idOfAttributes;
            set
            {
                _idOfAttributes = value;
                RaisePropertyChangedEvent(nameof(IdOfAttributes));
            }
        }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChangedEvent(nameof(Description));
            }
        }

        /// <summary>
        ///     Gets or sets the custom description.
        /// </summary>
        public string CustomDescription
        {
            get => _customDescription;
            set
            {
                _customDescription = value;
                RaisePropertyChangedEvent(nameof(CustomDescription));
            }
        }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public InventoryEnum.EnumSlot Position
        {
            get => _position;
            set
            {
                _position = value;
                RaisePropertyChangedEvent(nameof(Position));
            }
        }
    }
}