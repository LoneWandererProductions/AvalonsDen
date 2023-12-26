/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterDisplay/CharacterInteraction.cs
 * PURPOSE:     Start the Display of your Characters
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using ExtendedSystemObjects;

namespace CharacterDisplay
{
    /// <inheritdoc />
    /// <summary>
    ///     The character interaction class.
    /// </summary>
    public sealed class CharacterInteraction : ICharacterInteraction
    {
        /// <inheritdoc />
        /// <summary>
        ///     Here we go
        /// </summary>
        /// <param name="campaignName">The Name of the Campaign</param>
        /// <param name="characterLst">Ids of Characters</param>
        /// <exception cref="T:System.ArgumentNullException">Wrong Parameter</exception>
        public void Initiate(string campaignName, List<int> characterLst)
        {
            if (characterLst.IsNullOrEmpty()) throw new ArgumentNullException(nameof(characterLst));

            var dsplay = new CharacterWindow(campaignName, characterLst) {Topmost = true};
            dsplay.ShowDialog();
        }
    }
}