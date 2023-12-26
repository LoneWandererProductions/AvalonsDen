/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/DialogInteractionRegister.cs
 * PURPOSE:     Basic Register for specific Variables
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;

namespace DialogsDisplay
{
    /// <summary>
    ///     The dialog interaction register class.
    /// </summary>
    internal static class DialogInteractionRegister
    {
        /// <summary>
        ///     Name of the Campaign
        /// </summary>
        internal static string CampaignName { private get; set; }

        /// <summary>
        ///     Needed for the Images
        /// </summary>
        internal static string PortraitPath
            =>
                Path.Combine(Directory.GetCurrentDirectory(), DialogsDisplayResources.CorePathCampaign, CampaignName,
                    DialogsDisplayResources.ImageCharacterFolder);
    }
}