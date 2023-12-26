/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorDialogTree/ChoiceDialogView.xaml.cs
 * PURPOSE:     Details for the Visual leaf, Choice Part
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DialogEngine;

namespace EditorDialogTree
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The choice dialog view class.
    /// </summary>
    internal sealed partial class ChoiceDialogView : INotifyPropertyChanged
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorDialogTree.ChoiceDialogView" /> class.
        /// </summary>
        internal ChoiceDialogView()
        {
            DialogOptionsList = new ObservableCollection<ChoiceItem>(Register.Cursor.ChoiceDialog);
            InitializeComponent();
        }

        /// <summary>
        ///     Must be public since it is an ObservableCollection
        ///     Collection of Options
        /// </summary>
        public ObservableCollection<ChoiceItem> DialogOptionsList { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Tells the Components something was changed
        ///     Needed since we have to trigger it user defined
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Tell the Components something was changed so get the data
        /// </summary>
        private void NotifyPropertyChanged()
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(DialogOptionsList)));
        }

        /// <summary>
        ///     Add a new Dialog Option
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = new ChoiceItem
            {
                MasterId = Register.Cursor.BaseDialog.MasterId,
                IsMaster = false,
                EventId = TreeResources.IdNotSet,
                //add Number Count as Id
                ChildId = Register.GetIndex()
            };

            Register.Cursor.ChoiceDialog.Add(item);
            DialogOptionsList = new ObservableCollection<ChoiceItem>(Register.Cursor.ChoiceDialog);
            NotifyPropertyChanged();
        }

        /// <summary>
        ///     Delete Selected Dialog Option
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(DataGridViewDialogOptions.SelectedItem is ChoiceItem selectedItem)) return;

            Register.Cursor.ChoiceDialog.Remove(selectedItem);
            DialogOptionsList = new ObservableCollection<ChoiceItem>(Register.Cursor.ChoiceDialog);
            NotifyPropertyChanged();
        }
    }
}