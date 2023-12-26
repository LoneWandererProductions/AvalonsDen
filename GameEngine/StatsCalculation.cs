/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterDisplay/StatsCalculation.cs
 * PURPOSE:     Convert and Calculate to something useful
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;
using Resources;

namespace GameEngine
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The stats calculation class.
    /// </summary>
    public sealed class StatsCalculation : IStatsCalculation
    {
        /// <inheritdoc />
        /// <summary>
        ///     Calculate the character informations.
        /// </summary>
        /// <param name="biography">The biography.</param>
        /// <returns>The <see cref="T:GameEngine.CharCharta" />.</returns>
        public CharCharta CalculateCharacterInformation(CharacterBiography biography)
        {
            return new CharCharta(biography);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Calculate the character statistics.
        /// </summary>
        /// <param name="stats">The stats.</param>
        /// <returns>The <see cref="T:GameEngine.CharStatistics" />.</returns>
        public CharStatistics CalculateCharacterStatistics(CharacterBaseStats stats)
        {
            return new CharStatistics(stats);
        }
    }
}