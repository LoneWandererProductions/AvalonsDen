/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/IEditorCharacterHandler.cs
 * PURPOSE:     Interface for the CharacterHandler, Editor Mode
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

using System.Collections.Generic;
using Resources;

namespace CharacterEngine
{
    internal interface IEditorCharacterHandler
    {
        /// <summary>
        ///     Editor Mode
        /// </summary>
        /// <param name="path">Target Path</param>
        /// <returns></returns>
        CharacterBundle LoadCharacter(string path);

        /// <summary>
        ///     Editor Mode
        /// </summary>
        /// <param name="biography">Character Object</param>
        /// <param name="path">Target Path</param>
        /// <returns>Success Status</returns>
        bool SaveCharacter(CharacterBiography biography, string path);

        /// <summary>
        ///     Editor Mode
        /// </summary>
        /// <param name="stats"></param>
        /// <param name="path">Target Path</param>
        /// <returns>Success Status</returns>
        bool SaveCharacter(CharacterBaseStats stats, string path);

        /// <summary>
        ///     Editor Mode
        /// </summary>
        /// <param name="path">Target Path</param>
        /// <returns>List of Character Files</returns>
        Dictionary<string, string> GetCharacters(string path);
    }
}