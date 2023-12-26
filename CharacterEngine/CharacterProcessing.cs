/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/CharacterProcessing.cs
 * PURPOSE:     Simple Helper Class to save and Load Dialogs into the fitting Folders
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AvalonRuntime;
using Debugger;
using ExtendedSystemObjects;
using FileHandler;
using Resources;
using Serializer;

namespace CharacterEngine
{
    /// <summary>
    ///     The character processing class.
    /// </summary>
    internal static class CharacterProcessing
    {
        /// <summary>
        ///     Returns Standard Path of Character
        ///     To add with user Input
        ///     Stat Object won't be changed
        ///     Path: \"root"\Content\Campaigns\"CampaignsName"\Character\"Files"
        ///     Path: \"root"\Content\Campaigns\"CampaignsName"\AutoSave\"Files"
        /// </summary>
        private static readonly string CampaignFolder = Path.Combine(Directory.GetCurrentDirectory(),
            CharacterEngineResources.CampaignsFolderExtended);

        /// <summary>
        ///     Collect Character Sheets from Save Folder and Campaign Folder
        ///     Characters from Save get Priority over Campaign Folder
        ///     Path: \"root"\Content\Campaigns\"CampaignsName"\Character\"Files"
        ///     Path: \"root"\Content\Campaigns\"CampaignsName"\AutoSave\"Files"
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <returns>Character Sheets</returns>
        [return: MaybeNull]
        internal static Dictionary<int, CharacterSheet> CollectMasterData(string campaignName)
        {
            //all base Files from Character
            var path = Path.Combine(CampaignFolder, campaignName, CharacterEngineResources.CharacterFolder);

            var filesBase = FileHandleSearch.GetFilesByExtensionFullPath(path, ArtConst.CharacterBiographyExt, false);

            //no Files found
            if (filesBase == null)
            {
                DebugLog.CreateLogFile(CharacterEngineResources.ErrorNoCharactersFound, ErCode.Error);
                return null;
            }

            //all files from Normal
            var pathAutoSave = Path.Combine(CampaignFolder, campaignName, CharacterEngineResources.AutoSave);

            //all files from Autosave
            var filesAutoSave = FileHandleSearch.GetFilesByExtensionFullPath(pathAutoSave,
                ArtConst.CharacterBiographyExt, false);

            //Merged Character Profiles
            var lst = MergedFilePaths(filesBase, filesAutoSave);

            //Catch the Error
            if (lst.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(CharacterEngineResources.ErrorNoCharactersFound, ErCode.Error);
                return null;
            }

            //Generate the Result Set
            var sheets = new Dictionary<int, CharacterSheet>(lst.Count);

            //all base Files from Character
            sheets = CollectMasterData(lst, sheets);

            return CollectCharData(sheets, campaignName);
        }

        /// <summary>
        ///     Editor Mode
        ///     Character Sheets from specified Folder
        /// </summary>
        /// <param name="path">Target path</param>
        /// <returns>Character Sheets</returns>
        internal static Dictionary<string, string> GetCharacterFiles(string path)
        {
            var files = FileHandleSearch.GetFilesByExtensionFullPath(path,
                ArtConst.CharacterBiographyExt, false);

            return files.IsNullOrEmpty() ? null : files.ToDictionary(Path.GetFileNameWithoutExtension);
        }

        /// <summary>
        ///     Save Character Bio to specified Path
        /// </summary>
        /// <param name="sheets">Character Sheets</param>
        /// <param name="path">Target Path</param>
        internal static void SaveCharacterBiography(CharacterBiography sheets, string path)
        {
            Serialize.SaveObjectToXml(sheets, path);
        }

        /// <summary>
        ///     Save Character Stats to specified Path
        /// </summary>
        /// <param name="sheets">Character Sheets</param>
        /// <param name="path">Target Path</param>
        internal static void SaveCharacterBaseStats(CharacterBaseStats sheets, string path)
        {
            Serialize.SaveObjectToXml(sheets, path);
        }

        /// <summary>
        ///     Load Character Bio from specified Path
        /// </summary>
        /// <param name="path">Target Path</param>
        internal static CharacterBiography LoadCharacterBiography(string path)
        {
            return DeSerialize.LoadObjectFromXml<CharacterBiography>(path);
        }

        /// <summary>
        ///     Load Character Stats from specified Path
        /// </summary>
        /// <param name="path">Target Path</param>
        internal static CharacterBaseStats LoadCharacterBaseStats(string path)
        {
            return DeSerialize.LoadObjectFromXml<CharacterBaseStats>(path);
        }

        /// <summary>
        ///     Clever hack to get the Important files we need
        /// </summary>
        /// <param name="filesBase">Campaign Folder</param>
        /// <param name="filesAutoSave">AutoSave Folder</param>
        /// <returns>Character Sheets</returns>
        private static List<string> MergedFilePaths(IReadOnlyCollection<string> filesBase,
            IEnumerable<string> filesAutoSave)
        {
            var paths = new Dictionary<string, string>(filesBase.Count);

            foreach (var file in filesBase)
            {
                var path = Path.GetFileNameWithoutExtension(file);
                if (path != null) paths.Add(path, file);
            }

            if (filesAutoSave != null)
                foreach (var file in filesAutoSave)
                {
                    var path = Path.GetFileNameWithoutExtension(file);
                    if (path != null) paths.AddDistinct(path, file);
                }

            return paths.Select(kvp => kvp.Value).ToList();
        }

        /// <summary>
        ///     Open the Character XML, look up Id and Npc Status,
        ///     if Player get the Stat File as well.
        ///     We load all Information needed from the File Name and the Contents of the File and Create a new Overview Object
        /// </summary>
        /// <param name="filesChanged">List of changed Stat Files</param>
        /// <param name="sheets">Basic Character information</param>
        /// <returns>The Character Infos</returns>
        private static Dictionary<int, CharacterSheet> CollectMasterData(IEnumerable<string> filesChanged,
            Dictionary<int, CharacterSheet> sheets)
        {
            //Open the XML Files and Located the Id and Character Status
            foreach (var file in filesChanged)
            {
                var num = XmlTools.GetFirstAttributeFromXml(file, CharacterEngineResources.XmlId);
                var check = XmlTools.GetFirstAttributeFromXml(file, CharacterEngineResources.XmlNpc);

                var id = int.Parse(num);
                var npc = bool.Parse(check);

                var sheet = new CharacterSheet
                {
                    Npc = npc,
                    Id = id,
                    CharacterBiographyPath = file,
                    Name = Path.GetFileNameWithoutExtension(file)
                };

                sheets.AddDistinct(id, sheet);
            }

            return sheets;
        }

        /// <summary>
        ///     Set the Path to the Stat Files of all PC Characters
        /// </summary>
        /// <param name="sheets">Basic Character informations</param>
        /// <param name="campaignName">Name of the Campaigns</param>
        /// <returns>Character Sheets</returns>
        private static Dictionary<int, CharacterSheet> CollectCharData(Dictionary<int, CharacterSheet> sheets,
            string campaignName)
        {
            foreach (var sheet in sheets.Where(sheet => sheet.Value.Npc))
                sheets[sheet.Key].CharacterStatsPath = Path.Combine(
                    CampaignFolder,
                    campaignName,
                    CharacterEngineResources.CharacterFolder,
                    Path.ChangeExtension(sheets[sheet.Key].Name, CharacterEngineResources.CharacterStatsExt));

            return sheets;
        }
    }
}