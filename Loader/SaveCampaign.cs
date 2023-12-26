/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/SaveCampaign.cs
 * PURPOSE:     Load a Save File
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;
using Resources;
using Serializer;

namespace Loader
{
    /// <summary>
    ///     The save loader class.
    ///     Only Used by Campaign
    /// </summary>
    internal static class SaveCampaign
    {
        /// <summary>
        ///     Returns Standard Path of Save Games
        ///     Path:
        ///     \SaveFiles\
        /// </summary>
        private static readonly string GetSaveGamePath = Path.Combine(Directory.GetCurrentDirectory(),
            LoaderRessource.SavePath);

        /// <summary>
        ///     Saves SaveInfos
        ///     Used by Campaign
        /// </summary>
        /// <param name="save">Save Object</param>
        /// <param name="saveFolderName">Name of the Save</param>
        internal static void SaveSaveGame(SaveInfos save, string saveFolderName)
        {
            Serialize.SaveObjectToXml(save,
                Path.Combine(GetSaveGamePath, Path.ChangeExtension(saveFolderName, LoaderRessource.SaveExt)));
        }

        /// <summary>
        ///     Loads SaveInfos Object
        /// </summary>
        /// <param name="saveFolderName">Name of the Save File</param>
        /// <returns>SaveInfos Object</returns>
        internal static SaveInfos LoadSaveFile(string saveFolderName)
        {
            var path = Path.ChangeExtension(saveFolderName, LoaderRessource.SaveExt);

            if (!string.IsNullOrEmpty(path))
                return DeSerialize.LoadObjectFromXml<SaveInfos>(Path.Combine(
                    GetSaveGamePath, path));

            return null;
        }
    }
}