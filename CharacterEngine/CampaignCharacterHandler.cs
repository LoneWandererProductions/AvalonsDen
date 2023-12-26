/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/ICharacterHandler.cs
 * PURPOSE:     Class that handles interaction of all Character related Systems, Campaign Only
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using Debugger;
using Resources;

namespace CharacterEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     Use one XML file with different file extensions
    ///     one for Biography
    ///     one for Stats for PC Characters
    ///     Only in CampaignMode
    /// </summary>
    public sealed class CampaignCharacterHandler : ICampaignCharacterHandler
    {
        /// <summary>
        ///     The campaign name.
        /// </summary>
        private string _campaignName;

        /// <summary>
        ///     Basic Character sheets.
        /// </summary>
        private static Dictionary<int, CharacterSheet> Sheets { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign Mode
        ///     Initiate the Engine, must be called first
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        public void InitiateCampaign(string campaignName)
        {
            _campaignName = campaignName;
            Sheets = CharacterProcessing.CollectMasterData(campaignName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign Mode
        ///     Load a Character
        /// </summary>
        /// <param name="id">Id of Character</param>
        /// <returns>Selected Character</returns>
        public CharacterBundle LoadCharacter(int id)
        {
            //Not initiated
            if (Sheets == null)
            {
                DebugLog.CreateLogFile(CharacterEngineResources.ErrorNotInitiated, ErCode.Error);
                return null;
            }

            if (id == -1)
            {
                DebugLog.CreateLogFile(CharacterEngineResources.ErrorNoCharactersFound, ErCode.Error);
                return null;
            }

            if (!Sheets.ContainsKey(id))
            {
                DebugLog.CreateLogFile(string.Concat(CharacterEngineResources.ErrorIdOfCharacterNotFound, id),
                    ErCode.Error);
                return null;
            }

            var sheet = Sheets[id];
            var bundle = new CharacterBundle
            {
                Bio = CharacterProcessing.LoadCharacterBiography(sheet.CharacterBiographyPath)
            };

            if (sheet.Npc) bundle.Stats = CharacterProcessing.LoadCharacterBaseStats(sheet.CharacterStatsPath);

            return bundle;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign Mode
        ///     Just change the stats
        /// </summary>
        /// <param name="biography">Character Bio</param>
        public void ChangeStats(CharacterBiography biography)
        {
            //Not initiated
            if (string.IsNullOrEmpty(_campaignName) || Sheets == null)
            {
                DebugLog.CreateLogFile(CharacterEngineResources.ErrorNotInitiated, ErCode.Error);
                return;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), CharacterEngineResources.CampaignsFolderExtended,
                _campaignName, CharacterEngineResources.AutoSave);
            var element = Sheets[biography.Id];
            element.CharacterBiographyPath = Path.Combine(path, element.Name);
            CharacterProcessing.SaveCharacterBiography(biography, element.CharacterBiographyPath);
            Sheets[biography.Id] = element;
        }
    }
}