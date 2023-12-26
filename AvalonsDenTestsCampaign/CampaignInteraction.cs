/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTestsCampaign/CampaignInteraction.cs
 * PURPOSE:     Test Campaign Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     The campaign interaction, mostly checks Exceptions
    /// </summary>
    [TestClass]
    public sealed class CampaignInteraction
    {
        /// <summary>
        ///     Check our Exceptions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid Input. String was Empty")]
        public void ExceptionCheckInitiateMap()
        {
            CampaignsProcessing.InitiateCampaign(string.Empty, string.Empty, 0, 0, 0);
        }

        /// <summary>
        ///     Check our Exceptions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid Input. String was Empty")]
        public void CampaignProcessingExceptionCheck()
        {
            var saveInfos = new SaveInfos();
            CampaignsProcessing.LoadSave(saveInfos);
        }
    }
}