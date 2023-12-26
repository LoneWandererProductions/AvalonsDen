/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/IDialogInteraction.cs
 * PURPOSE:     For ease of pain separate Game logic into it is own Class, this class is for Dialog Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

namespace DialogsDisplay
{
    /// <summary>
    ///     The IDialogInteraction interface.
    /// </summary>
    internal interface IDialogInteraction
    {
        /// <summary>
        ///     Start the display.
        /// </summary>
        /// <param name="campaignName">The campaign Name.</param>
        /// <param name="mapName">The map Name.</param>
        /// <param name="dialogName">The dialog Name.</param>
        void StartDisplay(string campaignName, string mapName, string dialogName);
    }
}