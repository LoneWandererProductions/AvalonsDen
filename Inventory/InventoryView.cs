using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ViewModel;

namespace Inventory
{
    public sealed class InventoryView : INotifyPropertyChanged
    {
        /// <summary>
        ///     The carrying
        /// </summary>
        private ObservableCollection<Slot> _carrying;

        private DelegateCommand<object> _discardCommand;

        private DelegateCommand<object> _equipCommand;

        /// <summary>
        ///     The equipment
        /// </summary>
        private ObservableCollection<Equipped> _equipment;

        private DelegateCommand<object> _infoCommand;

        /// <summary>
        ///     The item command
        /// </summary>
        private DelegateCommand<object> _itemCommand;

        private DelegateCommand<object> _removeCommand;

        /// <summary>
        ///     The selected source
        /// </summary>
        private List<string> _selectedSource;

        /// <summary>
        ///     The source select
        /// </summary>
        private string _sourceSelect;

        private DelegateCommand<object> _useCommand;

        /// <summary>
        ///     Gets the selected source.
        /// </summary>
        /// <value>
        ///     The selected source.
        /// </value>
        public List<string> SelectedSource
        {
            get => _selectedSource;
            set
            {
                if (_selectedSource == value) return;

                _selectedSource = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedSource)));
            }
        }

        /// <summary>
        ///     Gets or sets the source select.
        /// </summary>
        /// <value>
        ///     The source select.
        /// </value>
        public string SourceSelect
        {
            get => _sourceSelect;
            set
            {
                if (_sourceSelect == value) return;

                _sourceSelect = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceSelect)));
            }
        }

        /// <summary>
        ///     Gets or sets the carrying.
        /// </summary>
        /// <value>
        ///     The carrying.
        /// </value>
        public ObservableCollection<Slot> Carrying
        {
            get => _carrying;
            set
            {
                if (_carrying == value) return;

                _carrying = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Carrying)));
            }
        }

        /// <summary>
        ///     Gets or sets the equipment.
        /// </summary>
        /// <value>
        ///     The equipment.
        /// </value>
        public ObservableCollection<Equipped> Equipment
        {
            get => _equipment;
            set
            {
                if (_equipment == value) return;

                _equipment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Equipment)));
            }
        }

        /// <summary>
        ///     Gets the item command.
        /// </summary>
        /// <value>
        ///     The item command.
        /// </value>
        public ICommand ItemCommand =>
            _itemCommand ?? (_itemCommand = new DelegateCommand<object>(ItemAction, CanExecute));

        public ICommand ItemEquipCommand =>
            _equipCommand ?? (_equipCommand = new DelegateCommand<object>(EquipAction, CanExecute));

        public ICommand RemoveCommand =>
            _removeCommand ?? (_removeCommand = new DelegateCommand<object>(RemoveAction, CanExecute));

        public ICommand DiscardCommand =>
            _discardCommand ?? (_discardCommand = new DelegateCommand<object>(DiscardAction, CanExecute));

        public ICommand UseCommand =>
            _useCommand ?? (_useCommand = new DelegateCommand<object>(UseAction, CanExecute));

        public ICommand InfoCommand =>
            _infoCommand ?? (_infoCommand = new DelegateCommand<object>(InfoAction, CanExecute));

        /// <inheritdoc />
        /// <summary>
        ///     Tells the Components something was changed
        ///     Needed since we have to trigger it user defined
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets a value indicating whether this instance can execute.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified object; otherwise, <c>false</c>.
        /// </returns>
        /// <value>
        ///     <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </value>
        public bool CanExecute(object obj)
        {
            // check if executing is allowed, not used right now
            return true;
        }

        /// <summary>
        ///     Initiates the specified character.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="inventory">The inventory.</param>
        internal void Initiate(Dictionary<int, Slot> inventory, Dictionary<int, string> character)
        {
            //sort out the Data
            EquipmentProcessing.GetEquipment(inventory);
            //set our Character Register
            InventoryRegister.Character = character;
            if (character == null) return;

            SelectedSource = InventoryRegister.Names = character.Values.ToList();
        }

        private void ItemAction(object obj)
        {
            var data = (Slot) obj;
        }

        private void EquipAction(object obj)
        {
            var data = (Slot) obj;
        }

        private void RemoveAction(object obj)
        {
            var data = (Slot) obj;
        }

        private void DiscardAction(object obj)
        {
            var data = (Slot) obj;
        }

        private void UseAction(object obj)
        {
            var data = (Slot) obj;
        }

        private void InfoAction(object obj)
        {
            var data = (Slot) obj;
        }
    }
}