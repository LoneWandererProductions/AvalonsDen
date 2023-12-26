/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorEvent/EditorEventProcessing.cs
 * PURPOSE:     Helper Window to Load and Save Events
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommonControls;
using Debugger;
using ExtendedSystemObjects;
using Loader;
using Resources;

namespace EditorEvent
{
    /// <summary>
    ///     The editor event processing class.
    /// </summary>
    internal static class EditorEventProcessing
    {
        /// <summary>
        ///     The standard folder for the Campaign
        /// </summary>
        private static readonly string StandardFolder = Path.Combine(Directory.GetCurrentDirectory(),
            EditorEventResources.CampaignsFolder);

        /// <summary>
        ///     Send the Message to the Outside World
        /// </summary>
        internal static EventHandler<string> SendMessage { get; set; }

        /// <summary>
        ///     Load all Events
        /// </summary>
        /// <returns>Collection of Events</returns>
        internal static EventContainer LoadEvents()
        {
            var pathObj = FileIoHandler.HandleFileOpen(EditorEventResources.EventDialog,
                StandardFolder);
            //Empty Return Value
            if (pathObj == null) return null;
            //load Map
            var eventMaster = WorkLoader.LoadEventCollection(pathObj.FilePath);

            if (eventMaster != null) return eventMaster;

            DebugLog.CreateLogFile(EditorEventResources.ErrorLoadEvents, ErCode.Error);

            return null;
        }

        /// <summary>
        ///     Save Events
        /// </summary>
        /// <param name="eventMaster"></param>
        /// <returns>Collection of Events</returns>
        internal static void SaveEvents(EventContainer eventMaster)
        {
            var pathObj = FileIoHandler.HandleFileSave(EditorEventResources.EventDialog, StandardFolder);

            //null value check
            if (pathObj == null) return;
            //if it is empty well return
            if (eventMaster == null)
            {
                DebugLog.CreateLogFile(EditorEventResources.ErrorLoadEvents, ErCode.Error);
                return;
            }

            EditorSave.SaveEventMasterCollection(pathObj.FilePath, eventMaster);
        }

        /// <summary>
        ///     Get the event type extended.
        /// </summary>
        /// <param name="eventTypeCollection">The eventTypeCollection.</param>
        /// <returns>The <see cref="EventTypeExtended" />.</returns>
        internal static EventTypeExtended GetEventTypeExtended(
            ObservableCollection<EventTypeExtended> eventTypeCollection)
        {
            if (eventTypeCollection.Count == 0) return new EventTypeExtended {Id = 0};

            var lst = new List<int>();

            foreach (var eventType in eventTypeCollection) lst.Add(eventType.Id);

            return new EventTypeExtended {Id = Utility.GetFirstAvailableIndex(lst)};
        }

        /// <summary>
        ///     Get the coordinates display.
        /// </summary>
        /// <param name="observableCoordinates">The observableCoordinates.</param>
        /// <returns>The <see cref="CoordinatesDisplay" />.</returns>
        internal static CoordinatesDisplay GetCoordinatesDisplay(
            ObservableCollection<CoordinatesDisplay> observableCoordinates)
        {
            //Key is Event Id and it must be unique
            if (observableCoordinates.Count == 0) return new CoordinatesDisplay {EventId = 0};

            var lst = new List<int>();

            foreach (var eventId in observableCoordinates) lst.Add(eventId.EventId);

            return new CoordinatesDisplay {EventId = Utility.GetFirstAvailableIndex(lst)};
        }

        /// <summary>
        ///     Get the coordinates id.
        /// </summary>
        /// <param name="coordinatesId">The coordinatesId.</param>
        /// <returns>The <see cref="T:List{CoordinatesDisplay}" />.</returns>
        internal static IEnumerable<CoordinatesDisplay> GetCoordinatesId(Dictionary<int, int> coordinatesId)
        {
            if (coordinatesId.IsNullOrEmpty()) return new List<CoordinatesDisplay>();

            return coordinatesId.Select(coordinate => new CoordinatesDisplay
            {
                EventId = coordinate.Key,
                CoordinatesId = coordinate.Value
            }).ToList();
        }

        /// <summary>
        ///     Get the event type extended list.
        /// </summary>
        /// <param name="eventTypeDictionary">The eventTypeDictionary.</param>
        /// <returns>The <see cref="EventTypeExtended" />.</returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static IEnumerable<EventTypeExtended> GetEventTypeExtendedList(
            Dictionary<int, EventType> eventTypeDictionary)
        {
            var evt = new List<EventTypeExtended>();
            if (eventTypeDictionary.IsNullOrEmpty()) return evt;

            foreach (var eventType in eventTypeDictionary)
            {
                var ext = new EventTypeExtended
                {
                    Id = eventType.Key,
                    CoordinatesId = eventType.Value.CoordinatesId,
                    Description = eventType.Value.Description,
                    IdForFurtherEventInfo = eventType.Value.IdForFurtherEventInfo,
                    IsActive = eventType.Value.IsActive,
                    IsDependent = eventType.Value.IsDependent,
                    IsRandom = eventType.Value.IsRandom,
                    IsRepeatable = eventType.Value.IsRepeatable,
                    IsStepOn = eventType.Value.IsStepOn,
                    LabelEvent = eventType.Value.LabelEvent,
                    TypeOfEvent = eventType.Value.TypeOfEvent
                };

                evt.Add(ext);
            }

            return evt;
        }

        /// <summary>
        ///     Convert back from Observable Collection
        /// </summary>
        /// <param name="observableCoordinates">From Frontend Observable Collection</param>
        /// <returns>CoordinatesId Collection, can return null.</returns>
        public static Dictionary<int, int> ConvertCoordinatesId(IEnumerable<CoordinatesDisplay> observableCoordinates)
        {
            if (observableCoordinates == null) return null;

            var dct = new Dictionary<int, int>();

            foreach (var item in observableCoordinates)
            {
                if (dct.ContainsKey(item.EventId))
                {
                    DebugLog.CreateLogFile(
                        string.Concat(EditorEventResources.WarningCoordinatesDisplayKeyAlreadyInUse, item.EventId),
                        ErCode.Warning, item);
                    SendMessage?.Invoke(nameof(EditorEventProcessing),
                        string.Concat(EditorEventResources.WarningCoordinatesDisplayKeyAlreadyInUse, item.EventId));
                    continue;
                }

                dct.Add(item.EventId, item.CoordinatesId);
            }

            return dct;
        }

        /// <summary>
        ///     Convert back from Observable Collection
        /// </summary>
        /// <param name="observableEventType">From Frontend Observable Collection</param>
        /// <returns>EventType Collection, can return null.</returns>
        internal static Dictionary<int, EventType> ConvertEventTypeDictionary(
            ObservableCollection<EventTypeExtended> observableEventType)
        {
            if (observableEventType == null) return null;

            var dct = new Dictionary<int, EventType>();

            foreach (var item in observableEventType)
            {
                if (dct.ContainsKey(item.Id))
                {
                    DebugLog.CreateLogFile(string.Concat(EditorEventResources.WarningEventTypeKeyAlreadyInUse, item.Id),
                        ErCode.Warning, item);
                    SendMessage?.Invoke(nameof(EditorEventProcessing),
                        string.Concat(EditorEventResources.WarningEventTypeKeyAlreadyInUse, item.Id));
                    continue;
                }

                dct[item.Id] = item;
            }

            return dct;
        }

        /// <summary>
        ///     Convert back from Observable Collection
        /// </summary>
        /// <param name="observableEventTypeExtension">From Frontend Observable Collection</param>
        /// <returns>EventTypeExtension Collection, can return null.</returns>
        internal static Dictionary<int, EventTypeExtension> ConvertEventTypeExtensionDictionary(
            ObservableCollection<EventTypeExtension> observableEventTypeExtension)
        {
            if (observableEventTypeExtension == null) return null;

            var dict = new Dictionary<int, EventTypeExtension>();
            var index = 0;

            foreach (var item in observableEventTypeExtension) dict[index++] = item;

            return dict;
        }

        /// <summary>
        ///     Check some Basic logic of the Event Container
        /// </summary>
        /// <param name="eventType">The eventType.</param>
        /// <param name="eventTypeExtension">The eventTypeExtension.</param>
        /// <param name="coordinates">The coordinates</param>
        internal static void CheckIdForFurtherEventInfo(ObservableCollection<EventTypeExtended> eventType,
            ObservableCollection<EventTypeExtension> eventTypeExtension,
            ObservableCollection<CoordinatesDisplay> coordinates)
        {
            if (eventType.Count == 0)
                SendMessage?.Invoke(nameof(CheckIdForFurtherEventInfo),
                    EditorEventResources.WarningEventTypeExtendedIsEmpty);

            if (eventTypeExtension.Count == 0)
                SendMessage?.Invoke(nameof(CheckIdForFurtherEventInfo),
                    EditorEventResources.WarningEventTypeExtensionIsEmpty);

            if (coordinates.Count == 0)
                SendMessage?.Invoke(nameof(CheckIdForFurtherEventInfo),
                    EditorEventResources.WarningCoordinatesDisplayIsEmpty);

            foreach (var item in from item in eventType
                let lst = eventTypeExtension.Where(x => x.Id == item.IdForFurtherEventInfo)
                where !lst.Any()
                select item)
                SendMessage?.Invoke(nameof(CheckIdForFurtherEventInfo),
                    string.Concat(EditorEventResources.ErrorIdForFurtherEventInfoIsNotAvailable, item.Id));

            foreach (var item in from item in eventType
                let lst = coordinates.Where(x => x.EventId == item.Id)
                where !lst.Any()
                select item)
                SendMessage?.Invoke(nameof(CheckIdForFurtherEventInfo),
                    string.Concat(EditorEventResources.WarningCoordinateForEventNotAvailable, item.Id));
        }
    }
}