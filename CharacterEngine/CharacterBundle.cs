/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/CharacterBundle.cs
 * PURPOSE:     Helper Object that contains all needed Data
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

namespace CharacterEngine
{
    /// <summary>
    ///     Container that Holds the Bio and the Stats
    /// </summary>
    public sealed class CharacterBundle
    {
        /// <summary>
        ///     Gets or sets the Biography.
        /// </summary>
        public CharacterBiography Bio { get; internal init; }

        /// <summary>
        ///     Gets or sets the stats.
        /// </summary>
        public CharacterBaseStats Stats { get; internal set; }
    }
}