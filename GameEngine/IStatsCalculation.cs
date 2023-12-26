/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/GameEngine/IStatsCalculation.cs
 * PURPOSE:     Todo Extend Class that describes Items
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace GameEngine
{
    /// <summary>
    ///     The IStatsCalculation interface.
    /// </summary>
    internal interface IStatsCalculation
    {
        /// <summary>
        ///     Calculate the character informations.
        /// </summary>
        /// <param name="biography">The biography.</param>
        /// <returns>The <see cref="CharCharta" />.</returns>
        CharCharta CalculateCharacterInformation(CharacterBiography biography);

        /// <summary>
        ///     Calculate the character statistics.
        /// </summary>
        /// <param name="stats">The stats.</param>
        /// <returns>The <see cref="CharStatistics" />.</returns>
        CharStatistics CalculateCharacterStatistics(CharacterBaseStats stats);
    }
}