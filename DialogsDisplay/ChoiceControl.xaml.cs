/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/DialogControl.xaml.cs
 * PURPOSE:     User-control to Display the Core Choice
 * PROGRAMER:   Peter Geinitz (Wayfarer) (Peter Geinitz)
 * SOURCE:      https://stackoverflow.com/questions/32064348/wpf-binding-custom-class-with-list-to-listbox
 */

// ReSharper disable MemberCanBeInternal

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DialogEngine;

namespace DialogsDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Display Choices
    /// </summary>
    public sealed partial class ChoiceControl
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DialogsDisplay.ChoiceControl" /> class.
        /// </summary>
        public ChoiceControl()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     List of Choices
        /// </summary>
        private ObservableCollection<ChoiceItem> ChoiceList { get; set; }

        /// <summary>
        ///     Update Contents
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void ChoiceControl_UserInput(object sender, DlgDisplay e)
        {
            UpdatedContents(e);
        }

        /// <summary>
        ///     Update the Choices the Player has
        /// </summary>
        /// <param name="e">Event Parameter: Choices</param>
        private void UpdatedContents(DlgDisplay e)
        {
            if (e?.DisplayDialog == null) return;

            ChoiceList = new ObservableCollection<ChoiceItem>(e.DisplayDialog);
            LstBoxChoices.ItemsSource = ChoiceList;
            LstBoxChoices.Items.Refresh();
        }

        /// <summary>
        ///     Set Selection for Dialog, mostly used for Enter Event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void LstBoxChoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstBoxChoices.SelectedItem != null)
                DialogsDisplayProcessing.SelectedItem = LstBoxChoices.SelectedItem as ChoiceItem;
        }

        /// <summary>
        ///     Selection of Dialog
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void LstBoxChoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DialogsDisplayProcessing.SelectedItem != null) DialogsDisplayProcessing.HandleInput();
        }

        /// <summary>
        ///     Selection of Dialog
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DialogsDisplayProcessing.SelectedItem != null)
                DialogsDisplayProcessing.HandleInput();
        }

        /// <summary>
        ///     Initiate the Choices
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void ChoiceControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            DialogsDisplayProcessing.DialogChoice += ChoiceControl_UserInput;
        }
    }
}