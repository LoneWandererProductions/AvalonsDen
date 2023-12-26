/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/LoaderRessource.cs
 * PURPOSE:     String Resources of Loader
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;
using AvalonRuntime;

namespace Loader
{
    /// <summary>
    ///     String Resources
    /// </summary>
    internal static class LoaderRessource
    {
        /// <summary>
        ///     The Temp folder (const). Value: "Temp".
        /// </summary>
        internal const string Temp = "Temp";

        /// <summary>
        ///     The save path (const). Value: "SaveFiles".
        /// </summary>
        internal const string SavePath = "SaveFiles";

        /// <summary>
        ///     The autosave (const). Value: "Autosave".
        /// </summary>
        internal const string Autosave = "Autosave";

        /// <summary>
        ///     The Master Border Dictionary Path (const). Value: @"Core\Files\MasterBorder.xml".
        /// </summary>
        internal const string MasterBorderDct = @"Core\Files\MasterBorder.xml";

        /// <summary>
        ///     The Master Tile Dictionary Path (const). Value: @"Core\Files\MasterTile.xml".
        /// </summary>
        internal const string MasterTileDct = @"Core\Files\MasterTile.xml";

        /// <summary>
        ///     The custom TileBorder Dictionary Name (const). Value: "MasterBorder.xml".
        /// </summary>
        internal const string CustomTileDct = "MasterTile.xml";

        /// <summary>
        ///     The Inventory File (const). Value: "Inventory.aiy".
        /// </summary>
        internal const string InventoryFile = "Inventory.aiy";

        /// <summary>
        ///     The Equipment File (const). Value: "Equipment.aiy".
        /// </summary>
        internal const string EquipmentFile = "Equipment.aiy";

        /// <summary>
        ///     The Party File (const). Value: "Party.aiy".
        /// </summary>
        internal const string PartyFile = "Party.aiy";

        /// <summary>
        ///     The save ext (const). Value: ".asv".
        /// </summary>
        internal const string SaveExt = ".asv";

        /// <summary>
        ///     Error CouldNot Delete. Value: "Error could not delete Save File.".
        /// </summary>
        internal const string ErrorCouldNotDelete = "Error could not delete Save File.";

        /// <summary>
        ///     The information no inventory found
        /// </summary>
        internal const string InformationNoInventoryFound = "Inventory was not found.";

        /// <summary>
        ///     The information no Save Files Found
        /// </summary>
        internal const string InformationNoSaveFilesFound = "No changed Files found.";

        /// <summary>
        ///     "root"\Content\Campaigns\
        /// </summary>
        internal static readonly string CpgnPath =
            Path.Combine(Directory.GetCurrentDirectory(), ArtConst.CampaignsFolder);

        /// <summary>
        ///     "root"\SaveFiles
        /// </summary>
        internal static readonly string Save = Path.Combine(Directory.GetCurrentDirectory(), SavePath);
    }
}