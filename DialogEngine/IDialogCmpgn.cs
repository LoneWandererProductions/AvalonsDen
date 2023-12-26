/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogEngine/IDialogCmpgn.cs
 * PURPOSE:     Interface for the Dialog Handler, Campaign
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace DialogEngine
{
    /// <summary>
    ///     The IDialogCampaign interface.
    /// </summary>
    internal interface IDialogCampaign
    {
        /// <summary>
        ///     Campaign
        ///     Gets or sets the dialog tree.
        /// </summary>
        /// <value>The Dialog Tree.</value>
        Dictionary<int, DialogDisplay> DialogTree { get; set; }

        /// <summary>
        ///     Campaign, Editor use
        /// </summary>
        /// <param name="dialog">Standard Dialog Object</param>
        void InitiateDialog(List<DialogObject> dialog);

        /// <summary>
        ///     Campaign Only
        ///     1 is always the Start Element
        ///     Must always be a DialogText
        /// </summary>
        /// <returns>Information for Display</returns>
        DialogDisplay StartDialog();

        /// <summary>
        ///     Campaign Only
        ///     Continues the Dialog
        /// </summary>
        /// <param name="id">Selected Choice If -1 just another Dialog String</param>
        /// <returns>Next Display</returns>
        DialogDisplay ContinueDialog(int id);

        /// <summary>
        ///     Campaign
        ///     Set the Dialog inactive.
        /// </summary>
        /// <param name="id">The Dialog id.</param>
        void SetInactive(int id);

        /// <summary>
        ///     Set the inactive.
        /// </summary>
        /// <param name="id">The masterId.</param>
        /// <param name="childId">The Choice child Id.</param>
        void SetInactive(int id, int childId);

        /// <summary>
        ///     Campaign Only
        ///     Set specific Dialog active
        /// </summary>
        /// <param name="id">The Dialog id.</param>
        void SetActive(int id);

        /// <summary>
        ///     Set the I active.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="childId">The Choice child Id.</param>
        void SetIActive(int id, int childId);

        /// <summary>
        ///     Saves the Dialog in Campaign Mode,
        ///     if we had changed we will Save the File in Autosave, if not we do nothing
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Name of the Dialog</param>
        void SaveCampaignDialogObjects(string campaignName, string mapName, string dialogName);

        /// <summary>
        ///     Load the Dialog
        ///     Campaign only
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Name of the Dialog</param>
        /// <returns>Dialog Object</returns>
        List<DialogObject> LoadCampaignDialogObjects(string campaignName, string mapName, string dialogName);
    }
}