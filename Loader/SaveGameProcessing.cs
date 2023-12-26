/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/SaveGameProcessing.cs
 * PURPOSE:     Handles all File Operations in the Save Game Generation
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AvalonRuntime;
using Debugger;
using ExtendedSystemObjects;
using FileHandler;

namespace Loader
{
    /// <summary>
    ///     The save handle processing class.
    /// </summary>
    internal static class SaveGameProcessing
    {
        /// <summary>
        ///     List of Save Files
        /// </summary>
        /// <returns>List of all Save Objects, can return null</returns>
        internal static List<string> GetSaveFiles()
        {
            var files = new List<string>();

            //File Name without Extension
            var lst = FileHandleSearch.GetFileByExtensionWithExtension(LoaderRessource.SavePath,
                LoaderRessource.SaveExt, false);

            if (lst.IsNullOrEmpty()) return null;

            files.AddRange(lst.Select(Path.GetFileNameWithoutExtension));

            return files;
        }

        /// <summary>
        ///     Could be streamlined
        ///     Just copies folder to folder with all subfolders, not elegant but meh works
        ///     Inverted: CopyGameFilesToSave
        ///     Path:
        ///     Source;
        ///     \SaveFiles\"saveName"\
        ///     \SaveFiles\"saveName"\Temp
        ///     Target:
        ///     \Campaign\Core\"CampaignName"\Autosave\
        ///     \Campaign\Core\"CampaignName"\Temp
        /// </summary>
        /// <param name="campaignName">Name of the Campaign, used in Path</param>
        /// <param name="saveName">Name of the Save</param>
        internal static void CopySaveFilesToGame(string campaignName, string saveName)
        {
            var source = Path.Combine(LoaderRessource.SavePath, saveName);
            var target = Path.Combine(LoaderRessource.CpgnPath, campaignName, LoaderRessource.Autosave);

            FileHandleCreate.CreateFolder(target);
            var check = FileHandleCopy.CopyFiles(source, target, true);

            if (!check) Trace.WriteLine(LoaderRessource.InformationNoSaveFilesFound);

            //add the Temp Folder and Inventory
            source = Path.Combine(LoaderRessource.SavePath, saveName, LoaderRessource.Temp);
            target = Path.Combine(LoaderRessource.CpgnPath, campaignName, LoaderRessource.Temp);

            check = FileHandleCopy.CopyFiles(source, target, true);

            if (!check) DebugLog.CreateLogFile(LoaderRessource.InformationNoInventoryFound, ErCode.Information);
        }

        /// <summary>
        ///     Could be streamlined
        ///     Just copies folder to folder with all subfolders, not elegant but meh works
        ///     Inverted CopySaveFilesToGame
        ///     Path:
        ///     Source;
        ///     \SaveFiles\"saveName"\
        ///     \SaveFiles\"saveName"\Temp
        ///     Target:
        ///     \Campaign\Core\"CampaignName"\Autosave\
        ///     \Campaign\Core\"CampaignName"\Temp\
        /// </summary>
        /// <param name="campaignName">Name of the Campaign, used in Path</param>
        /// <param name="saveName">Name of the Save</param>
        /// <returns>Success Status</returns>
        internal static bool CopyGameFilesToSave(string campaignName, string saveName)
        {
            var source = Path.Combine(LoaderRessource.CpgnPath, campaignName, LoaderRessource.Autosave);
            var target = Path.Combine(LoaderRessource.SavePath, saveName);

            FileHandleCreate.CreateFolder(target);
            var check = FileHandleCopy.CopyFiles(source, target, true);

            //Bail out early
            if (!check) return false;

            //add the Temp Folder and Inventory
            source = Path.Combine(LoaderRessource.CpgnPath, campaignName, LoaderRessource.Temp);
            target = Path.Combine(LoaderRessource.SavePath, saveName, LoaderRessource.Temp);

            check = FileHandleCopy.CopyFiles(source, target, true);
            if (!check) DebugLog.CreateLogFile(LoaderRessource.InformationNoInventoryFound, ErCode.Information);

            return check;
        }

        /// <summary>
        ///     Deletes:
        ///     \SaveFiles\"Save Name" , all contents and SubFolders
        ///     \SaveFiles\"Save Name" , deletes the File itself
        /// </summary>
        /// <param name="saveName">Name of the Save</param>
        internal static void DeleteSaveFile(string saveName)
        {
            if (string.IsNullOrEmpty(saveName))
            {
                DebugLog.CreateLogFile(LoaderRessource.ErrorCouldNotDelete, ErCode.Error);
                return;
            }

            var saveFolder = Path.Combine(LoaderRessource.SavePath, saveName);

            //Copy Contents From AutoSave to Generated Folder and Status
            FileHandleDelete.DeleteCompleteFolder(saveFolder);
            //delete Save File
            FileHandleDelete.DeleteFile(Path.ChangeExtension(saveFolder, LoaderRessource.SaveExt));
        }

        /// <summary>
        ///     Delete the Contents of the Autosave Folder for the Campaign
        ///     \Campaign\Core\"CampaignName"\Autosave\
        ///     For now mostly Inventory
        ///     \Campaign\Core\"CampaignName"\Temp\
        /// </summary>
        /// <param name="campaignName">Name of the Campaign, used in Path</param>
        internal static void DeleteAutoSaveFile(string campaignName)
        {
            var path = Path.Combine(LoaderRessource.CpgnPath, campaignName, LoaderRessource.Autosave);
            //Delete the Autosave Path
            FileHandleDelete.DeleteCompleteFolder(path);

            //Delete Temp File, not actual needed but we want to keep everything clean
            path = Path.Combine(ArtConst.CampaignsFolder, campaignName, LoaderRessource.Temp);
            FileHandleDelete.DeleteCompleteFolder(path);
        }
    }
}