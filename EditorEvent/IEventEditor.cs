/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorEvent/IEventEditor.cs
 * PURPOSE:     Interface for EditorEvent
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace EditorEvent
{
    /// <summary>
    ///     The IEventEditor interface.
    /// </summary>
    internal interface IEventEditor
    {
        /// <summary>
        ///     Show the events.
        /// </summary>
        void ShowEvents();

        /// <summary>
        ///     Show the events.
        /// </summary>
        /// <param name="eventMasterCollect">The eventMasterCollect.</param>
        void ShowEvents(EventContainer eventMasterCollect);

        /// <summary>
        ///     Get the event type collection.
        /// </summary>
        /// <returns>The <see cref="EventContainer" />.</returns>
        EventContainer GetEventTypeContainer();
    }
}