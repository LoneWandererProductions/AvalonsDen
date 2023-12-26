/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/EditorResources.cs
 * PURPOSE:     Helper Class that collects all Resource Strings
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;

namespace Editors
{
    /// <summary>
    ///     The editor resources class.
    /// </summary>
    internal static class EditorResources
    {
        //LogMessages
        /// <summary>
        ///     The information load (const). Value: "Loaded up Editor".
        /// </summary>
        internal const string InformationLoad = "Loaded up Editor";

        /// <summary>
        ///     The information rendering start (const). Value: "Rendering Started".
        /// </summary>
        internal const string InformationRenderingStart = "Rendering Started";

        /// <summary>
        ///     The information rendering end (const). Value: "Rendering Ended".
        /// </summary>
        internal const string InformationRenderingEnd = "Rendering Ended";

        /// <summary>
        ///     The information nothing to do (const). Value: "Nothing to do".
        /// </summary>
        internal const string InformationNothingToDo = "Nothing to do";

        /// <summary>
        ///     The error could not load basic files (const). Value: "Error could not load Basic Files".
        /// </summary>
        internal const string ErrorCouldNotLoadBasicFiles = "Error could not load Basic Files";

        //File Dialog
        /// <summary>
        ///     The map dialog (const). Value: "Map File(*.anp)|*.anp|All files (*.*)|*.*".
        /// </summary>
        internal const string MapDialog = "Map File(*.anp)|*.anp|All files (*.*)|*.*";

        /// <summary>
        ///     The XML files (const). Value: "XML files (*.XML)|*.XML|All files (*.*)|*.*".
        /// </summary>
        internal const string XmlFiles = "XML files (*.XML)|*.XML|All files (*.*)|*.*";

        /// <summary>
        ///     The png files (const). Value: "Image File(*.png)|*.png|All files (*.*)|*.*".
        /// </summary>
        internal const string PngFiles = "Image File(*.png)|*.png|All files (*.*)|*.*";

        //Paths
        /// <summary>
        ///     The core Path (const). Value: "\Content\".
        /// </summary>
        internal const string CorePath = @"\Content\";

        /// <summary>
        ///     The core files (const). Value: "\Core\Files".
        /// </summary>
        internal const string CoreFiles = @"\Core\Files";

        /// <summary>
        ///     Path Information where we expect the Tiles
        /// </summary>
        internal const string TilesFolder = "Tiles";

        /// <summary>
        ///     The campaigns folder (const). Value: "Content\Campaigns".
        /// </summary>
        internal const string CampaignsFolder = @"Content\Campaigns";

        //Ids
        /// <summary>
        ///     The idle (const). Value: 0.
        /// </summary>
        internal const int Idle = 0;

        /// <summary>
        ///     The id of delete passable tile (const). Value: -2.
        /// </summary>
        internal const int IdOfDeletePassableTile = -2;

        /// <summary>
        ///     The id of delete event (const). Value: -3.
        /// </summary>
        internal const int IdOfDeleteEvent = -3;

        /// <summary>
        ///     The id of event (const). Value: -4.
        /// </summary>
        internal const int IdOfEvent = -4;

        /// <summary>
        ///     The id of multi terrain (const). Value: -5.
        /// </summary>
        internal const int IdOfMultiTerrain = -5;

        //Display Symbol, TODO Remove
        /// <summary>
        ///     The symbol of event (const). Value: 4.
        /// </summary>
        internal const int SymbolOfEvent = 4;

        /// <summary>
        ///     The prompt editors (const). Value: "Shortcut prompt, for people who prefer typing".
        /// </summary>
        internal const string PromptEditors = "Shortcut prompt, for people who prefer typing";

        /// <summary>
        ///     The prompt feedback message (readonly). Value: string.Concat("Command executed", Environment.NewLine).
        /// </summary>
        internal static readonly string PromptDone = string.Concat("Command executed", Environment.NewLine);
    }
}