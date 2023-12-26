/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonRuntime/ArtConst.cs
 * PURPOSE:     Collected String Resources
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace AvalonRuntime
{
    /// <summary>
    ///     Basic Constants that are used all over the Solution
    /// </summary>
    public static class ArtConst
    {
        /// <summary>
        ///     The Argument minimized (const). Value: "/StartMinimized".
        /// </summary>
        public const string ArgsMinimized = "/StartMinimized";

        /// <summary>
        ///     The campaigns folder (const). Value: "Content\Campaigns\".
        /// </summary>
        public const string CampaignsFolder = @"Content\Campaigns\";

        /// <summary>
        ///     The map extension (const). Value: ".anp".
        /// </summary>
        public const string MapExt = ".anp";

        /// <summary>
        ///     The EventType File extension (const). Value: ".evd".
        /// </summary>
        public const string EventTypeExt = ".evd";

        /// <summary>
        ///     The EventTypeExtension File extension (const). Value: ".avd".
        /// </summary>
        public const string EventTypeExtensionExt = ".avd";

        /// <summary>
        ///     The campaign File extension (const). Value: ".cpg".
        /// </summary>
        public const string CampaignExt = ".cpg";

        /// <summary>
        ///     The CoordinatesId File extension (const). Value: ".aci".
        /// </summary>
        public const string CoordinatesIdExt = ".aci";

        /// <summary>
        ///     The dialog object File extension (const). Value: ".adg".
        /// </summary>
        public const string DialogObjectExt = ".adg";

        /// <summary>
        ///     The TransitionFile File extension (const). Value: ".atl".
        /// </summary>
        public const string TransitionFileExt = ".atl";

        /// <summary>
        ///     The  CharacterStats File extension (const). Value: ".acs".
        /// </summary>
        public const string CharacterStatsExt = ".acs";

        /// <summary>
        ///     The  CharacterBiography File extension (const). Value: ".acb".
        /// </summary>
        public const string CharacterBiographyExt = ".acb";

        /// <summary>
        ///     The  Inventory File extension (const). Value: ".aiy".
        /// </summary>
        public const string InventoryFileExt = ".aiy";

        /// <summary>
        ///     Collection of all game Files
        /// </summary>
        public static readonly List<string> FileExtList = new()
        {
            MapExt,
            EventTypeExt,
            EventTypeExtensionExt,
            TransitionFileExt,
            CampaignExt,
            CoordinatesIdExt,
            DialogObjectExt,
            InventoryFileExt
        };
    }
}