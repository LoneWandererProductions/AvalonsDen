/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorEvent/EditorEventResources.cs
 * PURPOSE:     Helper Class that collects all Resource Strings
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace EditorEvent
{
    /// <summary>
    ///     The editor event resources class.
    /// </summary>
    internal static class EditorEventResources
    {
        /// <summary>
        ///     The error key already in use (const). Value: "Key is already in Use".
        /// </summary>
        internal const string WarningEventTypeKeyAlreadyInUse = "Warning: EventType Key is already in Use: ";

        /// <summary>
        ///     The error key already in use (const). Value: "Key is already in Use".
        /// </summary>
        internal const string WarningCoordinatesDisplayKeyAlreadyInUse =
            "Warning: CoordinatesDisplay Key is already in Use: ";

        /// <summary>
        ///     The warning EventTypExtension is empty (const). Value: "EventTypExtension is empty".
        /// </summary>
        internal const string WarningEventTypeExtensionIsEmpty = "Warning: EventTypExtension is empty";

        /// <summary>
        ///     The warning EventTypeExtended is empty (const). Value: "EventTypeExtended is empty".
        /// </summary>
        internal const string WarningEventTypeExtendedIsEmpty = "Warning: EventTypeExtended is empty";

        /// <summary>
        ///     The warning CoordinatesDisplay is empty (const). Value: "CoordinatesDisplay is empty".
        /// </summary>
        internal const string WarningCoordinatesDisplayIsEmpty = "Warning: CoordinatesDisplay is empty";

        /// <summary>
        ///     The Error IdForFurtherEventInfo is not available (const). Value: "IdForFurtherEventInfo is not available in
        ///     EventType: ".
        /// </summary>
        internal const string ErrorIdForFurtherEventInfoIsNotAvailable =
            "Error: IdForFurtherEventInfo is not available in EventType: ";

        /// <summary>
        ///     The warning coordinate for event not available (const). Value: "EventType Coordinate is not available in
        ///     Coordinate Dictionary: ".
        /// </summary>
        internal const string WarningCoordinateForEventNotAvailable =
            "Warning: EventType Coordinate is not available in Coordinate Dictionary: ";

        /// <summary>
        ///     The event dialog (const). Value: "Event Files(*.evd)|*.evd|All files (*.*)|*.*".
        /// </summary>
        internal const string EventDialog = "Event Files(*.evd)|*.evd|All files (*.*)|*.*";

        /// <summary>
        ///     The error load events (const). Value: "Warning: Called by OpenEvent_Click EditorEvent " + "\n" + "Type: Object
        ///     was empty, EventMasterCollection".
        /// </summary>
        internal const string ErrorLoadEvents = "Warning: Called by OpenEvent_Click EditorEvent " + "\n" +
                                                "Type: Object was empty, EventMasterCollection";

        /// <summary>
        ///     The campaigns folder (const). Value: @"Content\Campaigns".
        /// </summary>
        internal const string CampaignsFolder = @"Content\Campaigns";
    }
}