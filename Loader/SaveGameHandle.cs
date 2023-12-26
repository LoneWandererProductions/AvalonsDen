/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/SaveGameHandle.cs
 * PURPOSE:     Implemented Interface for Save Handling Handles all the Input from the Outside
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using CommonControls;
using Resources;

namespace Loader
{
    /// <inheritdoc />
    /// <summary>
    ///     The save handle class.
    /// </summary>
    public sealed class SaveGameHandle : ISaveGameHandle
    {
        /// <inheritdoc />
        /// <summary>
        ///     Get a List of Saved Files
        ///     File Name without Extension
        /// </summary>
        /// <returns>List of Save Files <see cref="T:List{string}" />.</returns>
        public List<DataItem> GetSaveFiles()
        {
            //return SaveGameProcessing.GetSaveFiles();
            return null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Creates a Save Folder and copies the Content into the Save Folder
        /// </summary>
        /// <param name="save">The save File.</param>
        /// <returns>
        ///     The Success Status<see cref="T:System.Boolean" />.
        /// </returns>
        public bool CreateSaveFile(SaveInfos save)
        {
            if (save == null) return false;

            //Generate Folder and Copy with Name
            var check = SaveGameProcessing.CopyGameFilesToSave(save.CampaignName, save.SaveName);

            //Save File
            SaveCampaign.SaveSaveGame(save, save.SaveName);
            return check;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Delete a Save within in the Save File Folder
        /// </summary>
        /// <param name="saveName">The save Name.</param>
        public void DeleteSaveFile(string saveName)
        {
            SaveGameProcessing.DeleteSaveFile(saveName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Deletes the Autosave Folder of a specific Campaign
        /// </summary>
        /// <param name="campaignName">The campaign Name.</param>
        public void DeleteAutoSaveFile(string campaignName)
        {
            SaveGameProcessing.DeleteAutoSaveFile(campaignName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Copy all Data from Save to Game Folder
        ///     Load the save file.
        /// </summary>
        /// <param name="saveName">The save Name.</param>
        /// <returns>The Save Data<see cref="T:Resources.SaveInfos" />.</returns>
        public SaveInfos LoadSaveFile(string saveName)
        {
            if (string.IsNullOrEmpty(saveName)) return null;
            //Load the actual save File
            var save = SaveCampaign.LoadSaveFile(saveName);

            //Generate the Temp File
            SaveGameProcessing.CopySaveFilesToGame(save.CampaignName, saveName);
            return save;
        }
    }
}