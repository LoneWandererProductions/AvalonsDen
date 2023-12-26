/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/ICampaignsHandler.cs
 * PURPOSE:     Interface for CampaignInterface
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace Campaigns
{
    /// <summary>
    ///     The ICampaignsHandler interface.
    /// </summary>
    internal interface ICampaignsHandler
    {
        /// <summary>
        ///     Start the campaign.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        void StartCampaign(CampaignManifest manifest);

        /// <summary>
        ///     Load the campaign.
        /// </summary>
        /// <param name="saveInfos">The saveInfos.</param>
        void LoadCampaign(SaveInfos saveInfos);
    }
}