/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorEvent/ObservableObjects.cs
 * PURPOSE:     Helper Objects to convert Item because of limitations in the framework and to add some further Infos
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;
using ViewModel;

// ReSharper disable MemberCanBeInternal, WPF and Observable Collections needs this to be public, don't fuck with it

namespace EditorEvent
{
    /// <inheritdoc />
    /// <summary>
    ///     The coordinates display class.
    /// </summary>
    internal sealed class CoordinatesDisplay : ObservableObject
    {
        /// <summary>
        ///     The coordinates id.
        /// </summary>
        private int _coordinatesId;

        /// <summary>
        ///     The event id.
        /// </summary>
        private int _eventId;

        /// <summary>
        ///     Gets or sets the coordinates id.
        ///     EventType.CoordinatesId, can be used for multiple Events
        /// </summary>
        public int CoordinatesId
        {
            get => _coordinatesId;
            set
            {
                _coordinatesId = value;
                RaisePropertyChangedEvent(nameof(CoordinatesId));
            }
        }

        /// <summary>
        ///     Gets or sets the event id.
        ///     Event.Key, must be unique
        /// </summary>
        public int EventId
        {
            get => _eventId;
            set
            {
                _eventId = value;
                RaisePropertyChangedEvent(nameof(EventId));
            }
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     The event type extended class.
    /// </summary>
    public sealed class EventTypeExtended : EventType
    {
        /// <summary>
        ///     The id.
        /// </summary>
        private int _id;

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
    }
}