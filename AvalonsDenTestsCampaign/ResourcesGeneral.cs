/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTestsCampaign/ResourcesGeneral.cs
 * PURPOSE:     Basic Tests for Avalon, Resource Files
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     Resource Files
    /// </summary>
    internal static class ResourcesGeneral
    {
        /// <summary>
        ///     The core path (const). Value: @"Content\Campaigns".
        /// </summary>
        internal const string CoreCampaign = @"Content\Campaigns";

        /// <summary>
        ///     The master border list (const). Value: "MasterBorder.xml".
        /// </summary>
        internal const string MasterBorderDct = "MasterBorder.xml";

        /// <summary>
        ///     The master tile list (const). Value: "MasterTile.xml".
        /// </summary>
        internal const string MasterTileDct = "MasterTile.xml";

        /// <summary>
        ///     The map name (const). Value: "MpNmTst".
        /// </summary>
        internal const string MapName = "MpNmTst";

        /// <summary>
        ///     The campaign name (const). Value: "CampaignNmeSave".
        /// </summary>
        internal const string CampaignNmeSave = "CampaignNmeSave";

        /// <summary>
        ///     The campaign name (const). Value: "CampaignNameLoad".
        /// </summary>
        internal const string CampaignNameLoad = "CampaignNameLoad";

        /// <summary>
        ///     The campaign name (const). Value: "CampaignNameDel".
        /// </summary>
        internal const string CampaignNameDel = "CampaignNameDel";

        /// <summary>
        ///     The campaign name (const). Value: "CampaignName".
        /// </summary>
        internal const string CampaignName = "CampaignName";

        /// <summary>
        ///     The path (const). Value: "AvalonsDen\Chapter\bin\Debug\net5.0-windows\Core\Files".
        /// </summary>
        internal const string Root = @"Chapter\bin\Debug\net5.0-windows\Core\Files";

        /// <summary>
        ///     The campaigns folder (const). Value: "Content\Campaigns".
        /// </summary>
        internal const string CampaignsFolder = @"Content\Campaigns";

        /// <summary>
        ///     The save path (const). Value: "SaveFiles".
        /// </summary>
        internal const string SavePath = "SaveFiles";

        /// <summary>
        ///     The test save (const). Value: "SaveName".
        /// </summary>
        internal const string TestSave = "SaveName";

        /// <summary>
        ///     The Autosave (const). Value: "AutoSave".
        /// </summary>
        internal const string AutoSave = "AutoSave";

        /// <summary>
        ///     The map ext (const). Value: ".anp".
        /// </summary>
        private const string MapExt = ".anp";

        /// <summary>
        ///     The event type ext (const). Value: ".evd".
        /// </summary>
        private const string EventTypeExt = ".evd";

        /// <summary>
        ///     The event type extension ext (const). Value: ".avd".
        /// </summary>
        private const string EventTypeExtensionExt = ".avd";

        /// <summary>
        ///     The tile node dictionary ext (const). Value: ".xml".
        /// </summary>
        private const string TileNodeDictionaryExt = ".xml";

        /// <summary>
        ///     The campaign ext (const). Value: ".cpg".
        /// </summary>
        private const string CampaignExt = ".cpg";

        /// <summary>
        ///     The coordinates id ext (const). Value: ".aci".
        /// </summary>
        private const string CoordinatesIdExt = ".aci";

        /// <summary>
        ///     The Save ext (const). Value: ".asv".
        /// </summary>
        internal const string SaveExt = ".asv";

        /// <summary>
        ///     The Campaign Manifest (const). Value: "CampaignManifest.asv".
        /// </summary>
        internal const string CampaignManifest = "CampaignManifest.asv";

        /// <summary>
        ///     The file ext list (readonly). Value: new List&lt;string&gt; { MapExt, EventTypeExt, EventTypeExtensionExt,
        ///     TileNodeDictionaryExt, CampaignExt, CoordinatesIdExt }.
        /// </summary>
        internal static readonly List<string> FileExtList = new()
        {
            MapExt,
            EventTypeExt,
            EventTypeExtensionExt,
            TileNodeDictionaryExt,
            CampaignExt,
            CoordinatesIdExt
        };

        /// <summary>
        ///     Set Core Path
        /// </summary>
        internal static readonly string CPath = Path.Combine(Directory.GetCurrentDirectory(), CoreCampaign,
            CampaignName);
    }
}