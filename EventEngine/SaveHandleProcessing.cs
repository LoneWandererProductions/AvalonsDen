/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EventEngine/SaveHandleProcessing.cs
 * PURPOSE:     Handles all File Operations in the Save Game Generation
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Resources;
using Serializer;

namespace EventEngine
{
    /// <summary>
    ///     The save handle processing class.
    /// </summary>
    internal static class SaveHandleProcessing
    {
        /// <summary>
        ///     \Content\Campaign\
        /// </summary>
        private static readonly string CampaignFolder = Path.Combine(Directory.GetCurrentDirectory(),
            EventEngineResources.CampaignsFolderExtended);

        /// <summary>
        ///     \Content\Campaigns\"CampaignName"\Autosave\"Map Name"\
        /// </summary>
        /// <param name="campaignName">Name of the Campaign, used in Path</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="appendix">File Extension</param>
        /// <returns>List of File Names</returns>
        internal static List<string> GetFilesFromSaveFolder(string campaignName, string mapName, string appendix)
        {
            var path = Path.Combine(CampaignFolder, campaignName, EventEngineResources.Autosave, mapName);
            return GetFilesByExtension(path, appendix);
        }

        /// <summary>
        ///     Saves the changed Event Dictionary into a specific Folder, with a specific naming Convention for Autosave purposes
        ///     Used by Campaign only
        ///     /Content/Campaign/"Name of the Campaign"/"Name of the Map"/"Event File"
        /// </summary>
        /// <param name="eventTypeDictionary">Event Dictionary that we save</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map and the Dictionary</param>
        internal static void Autosave(Dictionary<int, EventType> eventTypeDictionary,
            string campaignName, string mapName)
        {
            // ReSharper disable once AssignNullToNotNullAttribute, can't be null, we check it before we pass the Values
            var path = Path.Combine(GeneratePathForAutoSaveMap(campaignName, mapName),
                Path.GetFileNameWithoutExtension(mapName));
            Serialize.SaveDctObjectToXml(eventTypeDictionary,
                Path.ChangeExtension(path, EventEngineResources.EventTypeExt));
        }

        /// <summary>
        ///     Returns Autosave Path for Folder
        ///     Creates the Folder
        ///     Path:
        ///     \Content\Campaigns\"CampaignName"\Autosave\
        ///     \Content\Campaigns\"CampaignName"\Autosave\"Map Name"\
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <returns>Generated Path</returns>
        private static string GeneratePathForAutoSaveMap(string campaignName, string mapName)
        {
            var folder = Path.Combine(CampaignFolder, campaignName, EventEngineResources.Autosave, mapName);

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            return folder;
        }

        /// <summary>
        ///     Collects all files with a specific Extension
        /// </summary>
        /// <param name="path">Target Folder</param>
        /// <param name="appendix">File Extension</param>
        /// <returns>List of Files</returns>
        private static List<string> GetFilesByExtension(string path, string appendix)
        {
            var files = new List<string>();
            if (!Directory.Exists(path)) return files;

            if (string.IsNullOrEmpty(appendix)) return files;

            //cleanups just in Case
            appendix = appendix.Replace(EventEngineResources.Dot, string.Empty);

            var list = Directory.GetFiles(path, string.Concat(EventEngineResources.Star, appendix));

            files.AddRange(list.Select(Path.GetFileNameWithoutExtension));
            return files;
        }
    }
}