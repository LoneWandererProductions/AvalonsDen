/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorEvent/EditorEvents.xaml.cs
 * PURPOSE:     Helper Window to edit Events
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * NOTES:       I kind of dislike the way Microsoft handles Databinding
 */

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Resources;

namespace EditorEvent
{
    /// <inheritdoc cref="EventEditor" />
    /// <summary>
    ///     Editing of Events of a map
    /// </summary>
    internal sealed partial class EditorEvents : INotifyPropertyChanged
    {
        /// <inheritdoc />
        /// <summary>
        ///     Load Events from existing EventList
        /// </summary>
        internal EditorEvents()
        {
            SetBasicDatas();
            InitializeComponent();
            EditorEventProcessing.SendMessage += DebugPrints;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorEvent.EditorEvents" /> class.
        /// </summary>
        /// <param name="eventMasterCollect">The eventMasterCollect.</param>
        internal EditorEvents(EventContainer eventMasterCollect)
        {
            SetBasicDatas(eventMasterCollect);
            InitializeComponent();
            EditorEventProcessing.SendMessage += DebugPrints;
        }

        /// <summary>
        ///     EventDescription
        /// </summary>
        internal EventContainer EventMaster { get; private set; }

        /// <summary>
        ///     Databinding for EventType
        /// </summary>
        public ObservableCollection<EventTypeExtended> ObservableEventType { get; set; }

        /// <summary>
        ///     Databinding for EventType
        /// </summary>
        public ObservableCollection<CoordinatesDisplay> ObservableCoordinates { get; set; }

        /// <summary>
        ///     Databinding for EventType
        /// </summary>
        public ObservableCollection<EventTypeExtension> ObservableEventTypeExtension { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Tells the Components something was changed
        ///     Needed since we have to trigger it user defined
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Load Everything into the Observable Collections
        ///     mostly initialization
        /// </summary>
        private void SetBasicDatas()
        {
            ObservableEventTypeExtension = new ObservableCollection<EventTypeExtension>();
            ObservableEventType = new ObservableCollection<EventTypeExtended>();
            ObservableCoordinates = new ObservableCollection<CoordinatesDisplay>();
        }

        /// <summary>
        ///     Load Everything into the Observable Collections
        /// </summary>
        /// <param name="eventMasterCollect">Collection of all Events and other Parameters</param>
        private void SetBasicDatas(EventContainer eventMasterCollect)
        {
            var evt = EditorEventProcessing.GetEventTypeExtendedList(eventMasterCollect.EventTypeDictionary);
            var eventTypeExtension = eventMasterCollect.EventTypeExtensionDictionary.Values.ToList();

            ObservableEventTypeExtension = new ObservableCollection<EventTypeExtension>(eventTypeExtension);
            ObservableEventType = new ObservableCollection<EventTypeExtended>(evt);

            var cid = EditorEventProcessing.GetCoordinatesId(eventMasterCollect.CoordinatesId);
            ObservableCoordinates = new ObservableCollection<CoordinatesDisplay>(cid);

            EventMaster = eventMasterCollect;
        }

        /// <summary>
        ///     Clean the Window and create something new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewEvent_Click(object sender, RoutedEventArgs e)
        {
            SetBasicDatas();
            NotifyPropertyChanged();
        }

        /// <summary>
        ///     Opens an existing Event List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenEvent_Click(object sender, RoutedEventArgs e)
        {
            var eventMaster = EditorEventProcessing.LoadEvents();
            if (eventMaster == null) return;

            SetBasicDatas(eventMaster);
            NotifyPropertyChanged();
        }

        /// <summary>
        ///     Saves your opened Event List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEvent_Click(object sender, RoutedEventArgs e)
        {
            WriteDataBack();
            EditorEventProcessing.SaveEvents(EventMaster);
        }

        /// <summary>
        ///     remove selected Entry and depending Data
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">Type</param>
        private void EntryDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridEventTypeCollection.SelectedItem is EventTypeExtended eventTypeExtended)
                ObservableEventType.Remove(eventTypeExtended);

            if (DataGridEventTypeExtensionCollection.SelectedItem is EventTypeExtension myEventAsset)
                ObservableEventTypeExtension.Remove(myEventAsset);

            if (DataGridCoordinatesDisplayCollection.SelectedItem is CoordinatesDisplay myCoordinates)
                ObservableCoordinates.Remove(myCoordinates);

            NotifyPropertyChanged();
        }

        /// <summary>
        ///     Close the event click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void CloseEvent_Click(object sender, RoutedEventArgs e)
        {
            WriteDataBack();
            Close();
        }

        /// <summary>
        ///     Add the event type extension click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void AddEventTypeExtension_Click(object sender, RoutedEventArgs e)
        {
            ObservableEventTypeExtension.Add(new EventTypeExtension());
            NotifyPropertyChanged();
        }

        /// <summary>
        ///     Add the coordinates id click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void AddCoordinatesId_Click(object sender, RoutedEventArgs e)
        {
            ObservableCoordinates.Add(EditorEventProcessing.GetCoordinatesDisplay(ObservableCoordinates));
            NotifyPropertyChanged();
        }

        /// <summary>
        ///     Add the event type click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void AddEventType_Click(object sender, RoutedEventArgs e)
        {
            ObservableEventType.Add(EditorEventProcessing.GetEventTypeExtended(ObservableEventType));
            NotifyPropertyChanged();
        }

        /// <summary>
        ///     The entry sanity click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void EntrySanity_Click(object sender, RoutedEventArgs e)
        {
            EditorEventProcessing.CheckIdForFurtherEventInfo(ObservableEventType, ObservableEventTypeExtension,
                ObservableCoordinates);
        }

        /// <summary>
        ///     Convert Observables back to their standard format
        /// </summary>
        private void WriteDataBack()
        {
            EventMaster ??= new EventContainer();

            EventMaster.EventTypeDictionary = EditorEventProcessing.ConvertEventTypeDictionary(ObservableEventType);
            EventMaster.EventTypeExtensionDictionary =
                EditorEventProcessing.ConvertEventTypeExtensionDictionary(ObservableEventTypeExtension);

            EventMaster.CoordinatesId = EditorEventProcessing.ConvertCoordinatesId(ObservableCoordinates);

            EditorEventProcessing.CheckIdForFurtherEventInfo(ObservableEventType, ObservableEventTypeExtension,
                ObservableCoordinates);
        }

        /// <summary>
        ///     The debug prints.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void DebugPrints(object sender, string e)
        {
            TxtBoxLog.Text = string.Concat(TxtBoxLog.Text, e, Environment.NewLine);
        }

        /// <summary>
        ///     The notify property changed.
        /// </summary>
        private void NotifyPropertyChanged()
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(ObservableEventTypeExtension)));
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(ObservableEventType)));
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(ObservableCoordinates)));
        }
    }
}