/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/EventEngineResources.cs
 * PURPOSE:     Holds all Magic Numbers and used Strings
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace EventEngine
{
    /// <summary>
    ///     The event engine resources class.
    /// </summary>
    internal static class EventEngineResources
    {
        /// <summary>
        ///     core paths (const). Value: @"Content\Campaigns".
        /// </summary>
        internal const string CampaignsFolderExtended = @"Content\Campaigns";

        /// <summary>
        ///     core paths (const). Value: "Autosave".
        /// </summary>
        internal const string Autosave = "Autosave";

        /// <summary>
        ///     Warning
        /// </summary>
        internal const string WarningClearEvents =
            "EventEngine: ClearmyActiveTrapCoordinatesList, EventCoordinatesList was empty";

        /// <summary>
        ///     Error
        /// </summary>
        internal const string ErrorCouldNotFindCoordinateKey =
            "EventEngine: Could not find Coordinate Key in coordinatesId, Id: ";

        /// <summary>
        ///     Error
        /// </summary>
        internal const string ErrorCouldNotFindEventKey =
            "SetEventInactive: Could not find EventType Key in EventTypeDictionary, Id: ";

        /// <summary>
        ///     Error wrong MapName
        /// </summary>
        internal const string ErrorNoValidMapName = "No valid Map Name was provided by:";

        /// <summary>
        ///     Error wrong CampaignName
        /// </summary>
        internal const string ErrorNoValidCampaignName = "No valid Campaign Name was provided by:";

        /// <summary>
        ///     Error Could not Load Borders
        /// </summary>
        internal const string ErrorBorders = "In SaveBorderMapToArray, Borders were empty";

        /// <summary>
        ///     Error Could not Load Assets
        /// </summary>
        internal const string ErrorAssets = "Could not Load Assets";

        /// <summary>
        ///     The event type ext (const). Value: ".evd".
        /// </summary>
        internal const string EventTypeExt = ".evd";

        /// <summary>
        ///     The star (const). Value: "*".
        /// </summary>
        internal const string Star = "*";

        /// <summary>
        ///     The dot (const). Value: ".".
        /// </summary>
        internal const string Dot = ".";

        /// <summary>
        ///     The cell count (const). Value: 3.
        /// </summary>
        internal const int CellCount = 3;

        /// <summary>
        ///     The map splitter (const). Value: '|'.
        /// </summary>
        internal const char MapSplitter = '|';

        /// <summary>
        ///     The event trap (const). Value: 1.
        /// </summary>
        internal const int EventTrap = 1;

        /// <summary>
        ///     The event move (const). Value: 2.
        /// </summary>
        internal const int EventMove = 2;

        /// <summary>
        ///     The event nothing (const). Value: 3.
        /// </summary>
        internal const int EventNothing = 3;

        /// <summary>
        ///     The event display (const). Value: 4.
        /// </summary>
        internal const int EventDisplay = 4;

        /// <summary>
        ///     The display char (const). Value: -6.
        /// </summary>
        internal const int DisplayChar = -6;

        /// <summary>
        ///     The bracket right (const). Value: ")".
        /// </summary>
        internal const string BracketRight = ")";

        /// <summary>
        ///     The bracket left (const). Value: "(".
        /// </summary>
        internal const string BracketLeft = "(";
    }
}