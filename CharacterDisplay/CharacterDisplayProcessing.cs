/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterDisplay/CharacterDisplayProcessing.cs
 * PURPOSE:     Loads all Data for specific Character into the Viewer
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using CharacterEngine;

namespace CharacterDisplay
{
    /// <summary>
    ///     The character display processing class.
    /// </summary>
    internal static class CharacterDisplayProcessing
    {
        /// <summary>
        ///     Get the characters.
        /// </summary>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="character">The character.</param>
        /// <returns>The <see cref="CharacterBundle" /> with all Infos about the Character.</returns>
        internal static CharacterBundle GetCharacters(string campaignName, int character)
        {
            var chr = new CampaignCharacterHandler();
            chr.InitiateCampaign(campaignName);
            return chr.LoadCharacter(character);
        }
    }
}