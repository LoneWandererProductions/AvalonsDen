/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorEvent/EventEditor.cs
 * PURPOSE:     EventEditor handles all the Event Editing
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

namespace EditorEvent
{
    /// <inheritdoc />
    /// <summary>
    ///     The event editor class.
    /// </summary>
    public sealed class EventEditor : IEventEditor
    {
        /// <summary>
        ///     The event editor.
        /// </summary>
        private EditorEvents _eventEditor;

        /// <inheritdoc />
        /// <summary>
        ///     Show the events.
        /// </summary>
        public void ShowEvents()
        {
            _eventEditor = new EditorEvents();
            _eventEditor.Show();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Show the events.
        /// </summary>
        /// <param name="eventMasterCollect">The eventMasterCollect.</param>
        public void ShowEvents(EventContainer eventMasterCollect)
        {
            _eventEditor = new EditorEvents(eventMasterCollect);
            _eventEditor.Show();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get the event type collection.
        /// </summary>
        /// <returns>The <see cref="T:Resources.EventTypeContainer" />.</returns>
        public EventContainer GetEventTypeContainer()
        {
            return _eventEditor.EventMaster;
        }
    }
}