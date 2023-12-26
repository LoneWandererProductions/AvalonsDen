/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/IAdventure.cs
 * PURPOSE:     Interface for the Adventure
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

// ReSharper disable UnusedMemberInSuper.Global

namespace GameEngine
{
    /// <summary>
    ///     The IAdventure interface.
    /// </summary>
    internal interface IAdventure
    {
        /// <summary>
        ///     The initiate.
        /// </summary>
        void Initiate();

        /// <summary>
        ///     Load the characters.
        /// </summary>
        /// <param name="characterId">The characterId.</param>
        void LoadCharacters(List<int> characterId);

        /// <summary>
        ///     Add the character.
        /// </summary>
        /// <param name="characterId">The characterId.</param>
        /// <returns>The <see cref="T:List{int}" />.</returns>
        List<int> AddCharacter(int characterId);

        /// <summary>
        ///     Set the status.
        /// </summary>
        /// <param name="characterId">The characterId.</param>
        /// <param name="status">The status.</param>
        void SetStatus(int characterId, bool status);

        /// <summary>
        ///     Remove the character.
        /// </summary>
        /// <param name="characterId">The characterId.</param>
        void RemoveCharacter(int characterId);
    }
}