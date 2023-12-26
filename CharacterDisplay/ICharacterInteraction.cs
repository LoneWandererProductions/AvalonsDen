/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterDisplay/ICharacterInteraction.cs
 * PURPOSE:     Character Interface
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

using System.Collections.Generic;

namespace CharacterDisplay
{
    /// <summary>
    ///     The ICharacterInteraction interface.
    /// </summary>
    internal interface ICharacterInteraction
    {
        /// <summary>
        ///     The initiate.
        /// </summary>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="characterLst">The characters.</param>
        void Initiate(string campaignName, List<int> characterLst);
    }
}