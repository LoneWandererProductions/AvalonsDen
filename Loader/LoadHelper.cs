/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/LoadHelper.cs
 * PURPOSE:     Shared Methods between Loader and Saving
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;

namespace Loader
{
    /// <summary>
    ///     Basic Helper Functions for the Loader
    ///     Shared between Save Files and normal loader
    /// </summary>
    internal static class LoadHelper
    {
        /// <summary>
        ///     Since a map consists of multiple files that just differ by the extension
        ///     We return the full path with the filename without extension
        ///     Path:
        ///     "root"\Content\Campaigns\"CampaignName"\"FileName"
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="fileName">Name of the File</param>
        /// <returns>Generates a clean path without any Filename Extension</returns>
        internal static string GetCampaignpath(string campaignName, string fileName)
        {
            return string.IsNullOrEmpty(fileName)
                ? string.Empty
                : Path.Combine(LoaderRessource.CpgnPath, campaignName, Path.GetFileNameWithoutExtension(fileName));
        }

        /// <summary>
        ///     A map consists of multiple files that just differ by the extension
        ///     We return the to the path
        ///     Return Autosave Folder of specific Campaign
        ///     Path:
        ///     "root"\Content\Campaigns\"CampaignName"\Autosave\MapName\"FileName"
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Generates a clean path to the File</returns>
        internal static string GetCampaignAutoSavePathWithMap(string campaignName, string mapName)
        {
            return Path.Combine(LoaderRessource.CpgnPath,
                campaignName,
                LoaderRessource.Autosave,
                mapName);
        }

        /// <summary>
        ///     Returns Standard Path of Save Games of Campaign
        ///     Path:
        ///     "root"\SaveFiles\"SaveFoldername"
        /// </summary>
        /// <param name="saveFoldername">Name of the Game</param>
        /// <returns> Standard Path of Save Games </returns>
        internal static string GetPathForSaveGame(string saveFoldername)
        {
            return Path.Combine(LoaderRessource.Save,
                saveFoldername);
        }

        /// <summary>
        ///     Creates the Save Folder
        ///     "root"\SaveFiles\"SaveFoldername"\"Campaign Name"\"Name of Map"
        /// </summary>
        /// <param name="saveFolderName">Folder Name</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Generated Path for Saving</returns>
        internal static string CreateSaveFolder(string saveFolderName, string campaignName, string mapName)
        {
            var root = Path.Combine(LoaderRessource.Save,
                saveFolderName,
                campaignName,
                mapName);

            if (!Directory.Exists(root)) Directory.CreateDirectory(root);

            return root;
        }
    }
}