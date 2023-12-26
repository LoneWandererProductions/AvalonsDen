/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/ISaveGameHandle.cs
 * PURPOSE:     The Interface for Save Handling
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

using System.Collections.Generic;
using CommonControls;
using Resources;

namespace Loader
{
    /// <summary>
    ///     The ISaveHandle interface.
    /// </summary>
    internal interface ISaveGameHandle
    {
        /// <summary>
        ///     Get a List of Saved Files
        /// </summary>
        /// <returns>List of Save Files <see cref="T:List{string}" />.</returns>
        List<DataItem> GetSaveFiles();

        /// <summary>
        ///     Creates a Save Folder and copies the Content into the Save Folder
        /// </summary>
        /// <param name="save">The save.</param>
        /// <returns>
        ///     The Success Status<see cref="bool" />.
        /// </returns>
        bool CreateSaveFile(SaveInfos save);

        /// <summary>
        ///     Delete a Save within in the Save File Folder
        /// </summary>
        /// <param name="saveName">The save Name.</param>
        void DeleteSaveFile(string saveName);

        /// <summary>
        ///     Deletes the Autosave Folder of a specific Campaign
        /// </summary>
        /// <param name="campaignName">The campaign Name.</param>
        void DeleteAutoSaveFile(string campaignName);

        /// <summary>
        ///     Loads the save file.
        /// </summary>
        /// <param name="saveName">Name of the save.</param>
        /// <returns></returns>
        SaveInfos LoadSaveFile(string saveName);
    }
}