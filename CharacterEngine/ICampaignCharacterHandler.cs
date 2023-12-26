/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/ICampaignCharacterHandler.cs
 * PURPOSE:     Interface for the CharacterHandler, Campaign Mode
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace CharacterEngine
{
    /// <summary>
    ///     The ICampaignCharacterHandler interface.
    /// </summary>
    internal interface ICampaignCharacterHandler
    {
        /// <summary>
        ///     Campaigns only
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        void InitiateCampaign(string campaignName);

        /// <summary>
        ///     Campaigns only
        /// </summary>
        /// <param name="id">Id of Character</param>
        /// <returns>Character Info</returns>
        CharacterBundle LoadCharacter(int id);

        /// <summary>
        ///     Campaigns only
        ///     Just change the stats
        /// </summary>
        /// <param name="biography">Character Bio</param>
        void ChangeStats(CharacterBiography biography);
    }
}