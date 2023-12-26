/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/EditorCharacterHandler.cs
 * PURPOSE:     Class that handles interaction of all Character related Systems, Editor Only
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using ExtendedSystemObjects;
using FileHandler;
using Resources;

namespace CharacterEngine
{
    /// <summary>
    ///     The editor character handler class.
    /// </summary>
    public sealed class EditorCharacterHandler : IEditorCharacterHandler
    {
        /// <inheritdoc />
        /// <summary>
        ///     Editor Mode
        /// </summary>
        /// <param name="stats"></param>
        /// <param name="path">Target Path</param>
        /// <returns>Success Status</returns>
        public bool SaveCharacter(CharacterBaseStats stats, string path)
        {
            if (stats == null) return false;

            CharacterProcessing.SaveCharacterBaseStats(stats, path);
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Editor Mode
        /// </summary>
        /// <param name="biography">Character Object</param>
        /// <param name="path">Target Path</param>
        /// <returns>Success Status</returns>
        public bool SaveCharacter(CharacterBiography biography, string path)
        {
            if (biography == null) return false;

            CharacterProcessing.SaveCharacterBiography(biography, path);
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Editor Mode
        /// </summary>
        /// <param name="path">Target Path</param>
        /// <returns>Success Status</returns>
        public CharacterBundle LoadCharacter(string path)
        {
            var bundle = new CharacterBundle { Bio = CharacterProcessing.LoadCharacterBiography(path) };

            path = PathInformation.GetPathWithoutExtension(path);
            path = Path.ChangeExtension(path, CharacterEngineResources.CharacterStatsExt);

            if (bundle.Bio.Npc) bundle.Stats = CharacterProcessing.LoadCharacterBaseStats(path);

            return bundle;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Editor Mode
        ///     Gets a File List of possible Characters
        /// </summary>
        /// <param name="path">Name of the Campaign</param>
        /// <returns>Dictionary with Name and Id</returns>
        public Dictionary<string, string> GetCharacters(string path)
        {
            var sheets = CharacterProcessing.GetCharacterFiles(path);

            return sheets.IsNullOrEmpty() ? null : sheets;
        }
    }
}