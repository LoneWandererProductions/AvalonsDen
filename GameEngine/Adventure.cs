/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/GameEngine/Adventure.cs
 * PURPOSE:     Here we hold the basic data of our group
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Windows;
using ExtendedSystemObjects;

namespace GameEngine
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The adventure class.
    /// </summary>
    public sealed class Adventure : IAdventure
    {
        /// <summary>
        ///     The party.
        /// </summary>
        private static Dictionary<int, Members> _party;

        /// <summary>
        ///     The characters
        /// </summary>
        public List<int> Characters { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Initiate Party
        /// </summary>
        public void Initiate()
        {
            _party = new Dictionary<int, Members>();
            Characters = new List<int>();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load the characters.
        /// </summary>
        /// <param name="characterId">The characterId.</param>
        public void LoadCharacters(List<int> characterId)
        {
            Characters = characterId;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Add a Character
        /// </summary>
        /// <param name="characterId">Character Id</param>
        public List<int> AddCharacter(int characterId)
        {
            var mbr = new Members
            {
                CharacterId = characterId,
                IsActive = true
            };

            _party.AddDistinct(characterId, mbr);
            Characters.AddDistinct(characterId);
            return Characters;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Remove Character from the Party
        /// </summary>
        /// <param name="characterId">Character Id</param>
        public void RemoveCharacter(int characterId)
        {
            if (!_party.ContainsKey(characterId)) return;

            _party.Remove(characterId);
        }

        /// <inheritdoc />
        /// <summary>
        ///     TODO Implement
        /// </summary>
        /// <param name="characterId">Character Id</param>
        /// <param name="status">Switch between active and Inactive</param>
        public void SetStatus(int characterId, bool status)
        {
            if (!_party.ContainsKey(characterId)) return;

            _party[characterId].IsActive = status;
        }
    }

    /// <summary>
    ///     The members class.
    /// </summary>
    internal sealed class Members
    {
        /// <summary>
        ///     Gets or sets the character id.
        /// </summary>
        internal int CharacterId { get; init; }

        /// <summary>
        ///     Gets or sets a value indicating whether
        /// </summary>
        internal bool IsActive { get; set; }
    }
}