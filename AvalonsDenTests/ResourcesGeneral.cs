/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/ResourcesGeneral.cs
 * PURPOSE:     Basic Tests for Avalon, Resource Files
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Resources;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The resources general class.
    /// </summary>
    internal static class ResourcesGeneral
    {
        /// <summary>
        ///     The path (const). Value: @"Chapter\bin\Debug\net5.0-windows\Core\Files\".
        /// </summary>
        internal const string Path = @"Chapter\bin\Debug\net5.0-windows\Core\Files\";

        /// <summary>
        ///     The campaigns folder (const). Value: "Content\Campaigns".
        /// </summary>
        internal const string CampaignsFolder = @"Content\Campaigns";

        /// <summary>
        ///     The master border Dictionary (const). Value: "MasterBorder.xml".
        /// </summary>
        internal const string MasterBorderDct = "MasterBorder.xml";

        /// <summary>
        ///     The master tile Dictionary (const). Value: "MasterTile.xml".
        /// </summary>
        internal const string MasterTileDct = "MasterTile.xml";

        /// <summary>
        ///     The map name (const). Value: "MapNameTest".
        /// </summary>
        internal const string MapName = "MapNameTest";

        /// <summary>
        ///     The map name new (const). Value: "Test".
        /// </summary>
        internal const string MapNameNew = "Test";

        /// <summary>
        ///     The map name new (const). Value: "Test".
        /// </summary>
        internal const string MapDialogOne = "MapDialogOne";

        /// <summary>
        ///     The map name new (const). Value: "Test".
        /// </summary>
        internal const string MapDialogTwo = "MapDialogTwo";

        /// <summary>
        ///     The campaign name (const). Value: "CampaignNameTest".
        /// </summary>
        internal const string CampaignName = "CampaignNameTest";

        /// <summary>
        ///     The campaign name new (const). Value: "CTest".
        /// </summary>
        internal const string CampaignNameNew = "CTest";

        /// <summary>
        ///     The auto save (const). Value: "AutoSave".
        /// </summary>
        internal const string AutoSave = "AutoSave";

        /// <summary>
        ///     The dialog object ext (const). Value: ".adg".
        /// </summary>
        internal const string DialogObjectExt = ".adg";

        /// <summary>
        ///     The dialog start (readonly). Value: new DialogObject { MasterId = 2, IsMaster = true, DialogLine = "you
        ///     see a beautiful woman", IsItemActive = true, IsRepeatable = false }.
        /// </summary>
        internal static readonly DialogObject DialogStart = new()
        {
            MasterId = 2,
            IsMaster = true,
            DialogLine = "you see a beautiful woman",
            IsItemActive = true,
            IsRepeatable = false
        };

        /// <summary>
        ///     The dialog dummy (readonly). Value: new DialogObject { MasterId = 1, IsMaster = true, DialogLine = "Test
        ///     should not be shown", IsItemActive = false, IsRepeatable = false }.
        /// </summary>
        internal static readonly DialogObject DialogDummy = new()
        {
            MasterId = 1,
            IsMaster = true,
            DialogLine = "Test should not be shown",
            IsItemActive = false,
            IsRepeatable = false
        };

        /// <summary>
        ///     The dialog end (readonly). Value: new DialogObject { MasterId = 3, IsMaster = true, DialogLine = "You come
        ///     to an End", IsItemActive = true, IsRepeatable = false }.
        /// </summary>
        internal static readonly DialogObject DialogEnd = new()
        {
            MasterId = 3,
            IsMaster = true,
            DialogLine = "You come to an End",
            IsItemActive = true,
            IsRepeatable = false
        };

        /// <summary>
        ///     The coordinate one (readonly). Value: new Coordinates { XRow = 1, YColumn = 1, ZLayer = 1, TileId = 1 }.
        /// </summary>
        internal static readonly Coordinates CoordinateOne = new()
        {
            XRow = 1,
            YColumn = 1,
            ZLayer = 1,
            TileId = 1
        };

        /// <summary>
        ///     The coordinate two (readonly). Value: new Coordinates { XRow = 1, YColumn = 1, ZLayer = 1, TileId = 0 }.
        /// </summary>
        internal static readonly Coordinates CoordinateTwo = new()
        {
            XRow = 1,
            YColumn = 1,
            ZLayer = 1,
            TileId = 0
        };

        /// <summary>
        ///     The coordinate three (readonly). Value: new Coordinates { XRow = 1, YColumn = 1, ZLayer = 2, TileId = 2 }.
        /// </summary>
        internal static readonly Coordinates CoordinateThree = new()
        {
            XRow = 1,
            YColumn = 1,
            ZLayer = 2,
            TileId = 2
        };

        /// <summary>
        ///     The biography (readonly). Value: new CharacterBiography { Id = 1, Name = "test", Npc = true }.
        /// </summary>
        internal static readonly CharacterBiography Biography = new()
        {
            Id = 1,
            Name = "test",
            Npc = true
        };

        /// <summary>
        ///     The biography two (readonly). Value: new CharacterBiography { Id = 2, Name = "One", Npc = false }.
        /// </summary>
        internal static readonly CharacterBiography BiographyTwo = new()
        {
            Id = 2,
            Name = "One",
            Npc = false
        };

        /// <summary>
        ///     The stats (readonly). Value: new CharacterBaseStats { Level = 1, Strength = 1, Will = 1, Wisdom = 1, Intelligence =
        ///     1 }.
        /// </summary>
        internal static readonly CharacterBaseStats Stats = new()
        {
            Level = 1,
            Strength = 1,
            Will = 1,
            Wisdom = 1,
            Intelligence = 1
        };
    }
}