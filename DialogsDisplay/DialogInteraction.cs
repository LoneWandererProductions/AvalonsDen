/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/DialogInteraction.cs
 * PURPOSE:     For ease of pain separate Game logic into it is own Class, this class is for Dialog Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;

namespace DialogsDisplay
{
    /// <inheritdoc />
    /// <summary>
    ///     The dialog interaction class.
    /// </summary>
    public sealed class DialogInteraction : IDialogInteraction
    {
        /// <inheritdoc />
        /// <summary>
        ///     Start the Dialog Chain
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Complete Dialog</param>
        public void StartDisplay(string campaignName, string mapName, string dialogName)
        {
            CheckExceptions(campaignName, mapName, dialogName);

            //Set our Register
            DialogInteractionRegister.CampaignName = campaignName;

            //TODO buggy, test, especial second Event

            var display = new DialogWindow(campaignName, mapName, dialogName)
            {
                Topmost = true
            };
            _ = display.ShowDialog();
        }

        /// <summary>
        ///     TODO Implement Trigger to Transfer Event Trigger
        ///     Public Event for publishing the click on Image
        /// </summary>
        public static event EventHandler<DialogInteractionEventArgs> EventTriggered;

        /// <summary>
        ///     Basic Sanity Checks
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Complete Dialog</param>
        /// <exception cref="ArgumentException"></exception>
        private static void CheckExceptions(string campaignName, string mapName, string dialogName)
        {
            if (string.IsNullOrWhiteSpace(campaignName))
                throw new ArgumentException(DialogsDisplayResources.ErrorCampaignName, nameof(campaignName));

            if (string.IsNullOrWhiteSpace(mapName))
                throw new ArgumentException(DialogsDisplayResources.ErrorMapName, nameof(mapName));

            //special case since we can handle it gracefully

            if (dialogName?.Length == 0)
                throw new ArgumentException(DialogsDisplayResources.ErrorDialogName, nameof(dialogName));
        }

        /// <summary>
        ///     Notifies Subscribers to the Click Event in Cell
        /// </summary>
        /// <param name="args">The dialog interaction event arguments.</param>
        internal static void EventTrigger(DialogInteractionEventArgs args)
        {
            EventTriggered?.Invoke(null, args);
        }
    }

    /// <summary>
    ///     Event Id Object that will be send from The DialogDisplay
    /// </summary>
    public sealed class DialogInteractionEventArgs
    {
        /// <summary>
        ///     Gets or sets the event id.
        /// </summary>
        public int EventId { get; internal init; }
    }
}