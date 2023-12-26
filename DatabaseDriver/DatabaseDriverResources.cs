/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DatabaseDriver/DatabaseDriverResources.cs
 * PURPOSE:     String Resources
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using SQLiteHelper;

namespace DatabaseDriver
{
    /// <summary>
    ///     The database driver resources class.
    /// </summary>
    internal static class DatabaseDriverResources
    {
        /// <summary>
        ///     The db index name (const). Value: "Id_Index_".
        /// </summary>
        internal const string DbIndexName = "Id_Index_";

        /// <summary>
        ///     The db column id (const). Value: "Id".
        /// </summary>
        internal const string DbColumnId = "Id";

        /// <summary>
        ///     The db column base name (const). Value: "BaseName".
        /// </summary>
        private const string DbColumnBaseName = "BaseName";

        /// <summary>
        ///     The db column table name (const). Value: "Table_Name".
        /// </summary>
        internal const string DbColumnTableName = "Table_Name";

        /// <summary>
        ///     The db image header name (const). Value: "Image_Name".
        /// </summary>
        internal const string DbImageHeaderName = "Image_Name";

        /// <summary>
        ///     The db name master (const). Value: "Master".
        /// </summary>
        internal const string DbNameMaster = "Master";

        /// <summary>
        ///     The db image table name (const). Value: "Image".
        /// </summary>
        internal const string DbImageTableName = "Image";

        /// <summary>
        ///     The db extension (const). Value: ".db".
        /// </summary>
        internal const string DbExtension = ".db";

        /// <summary>
        ///     The db extension (const). Value:@"Content\Campaigns".
        /// </summary>
        internal const string CorePath = @"Content\Campaigns";

        /// <summary>
        ///     used in Enum MasterTables
        /// </summary>
        internal const string DbNameArmor = "Armor";

        /// <summary>
        ///     used in Enum MasterTables
        /// </summary>
        internal const string DbNameWeapon = "Weapon";

        /// <summary>
        ///     used in Enum MasterTables
        /// </summary>
        internal const string DbNameMiscellaneous = "Miscellaneous";

        /// <summary>
        ///     Message for Database Checks
        /// </summary>
        internal const string MessageDbOkay = "All tables in Place";

        /// <summary>
        ///     The message DB was fixed.
        /// </summary>
        internal const string MessageDbWasFixed = "Tables were added";

        /// <summary>
        ///     Path Dummy
        /// </summary>
        public const string ImageDummy = "Dummy";

        /// <summary>
        ///     Error db not initialized (const). Value: "Database not initialized".
        /// </summary>
        internal const string ErrorDbNotInitialized = "Database not initialized";

        /// <summary>
        ///     The error db not created (const). Value: "Database was not created".
        /// </summary>
        internal const string ErrorDbNotCreated = "Database was not created";

        /// <summary>
        ///     Error wrong path provided (const). Value: "Wrong Path Provided".
        /// </summary>
        internal const string ErrorWrongPathProvided = "Wrong Path Provided: ";

        /// <summary>
        ///     Error wrong path provided (const). Value: "Wrong Path Provided".
        /// </summary>
        internal const string ErrorFileNotFound = "File in Path not found: ";

        /// <summary>
        ///     The error lazy initiation (const). Value: "Could not initiate Lazy Object".
        /// </summary>
        internal const string ErrorLazyInitiation = "Could not initiate Lazy Object";

        /// <summary>
        ///     Information start up in (const). Value: "Started up DbInput".
        /// </summary>
        internal const string InformationStartUpIn = "Started up DbInput";

        /// <summary>
        ///     Information start up out (const). Value: "Started up DbOutput".
        /// </summary>
        internal const string InformationStartUpOut = "Started up DbOutput";

        /// <summary>
        ///     The message DB was not fixed.
        /// </summary>
        internal const string MessageDbWasNotFixed = "Tables were not added";

        /// <summary>
        ///     The error could not create master table.
        /// </summary>
        internal const string ErrorCouldNotCreateMasterTable = "Could not create Master Table";

        /// <summary>
        ///     The error could not create image table.
        /// </summary>
        internal const string ErrorCouldNotCreateImageTable = "Could not create Image Table";

        /// <summary>
        ///     The error could not find table
        /// </summary>
        internal const string ErrorCouldNotFindTable = "Could not find the Tabl for the selected Item Type: ";

        /// <summary>
        ///     The error key violation
        /// </summary>
        internal const string ErrorKeyViolation = "Key Violation at adding the Item Table";

        /// <summary>
        ///     The warning image not found
        /// </summary>
        internal const string WarningImageNotFound = " ,Image data would have been overwritten";

        /// <summary>
        ///     The error key could not convert to int
        /// </summary>
        internal const string ErrorKeyCouldNotConvertToInt = "Key could not be converted to int: ";

        /// <summary>
        ///     Database Headers
        /// </summary>
        internal static readonly List<string> Headers = new()
        {
            DbColumnId,
            DbColumnBaseName
        };

        /// <summary>
        ///     Database Attributes, for Master Table
        /// </summary>
        internal static readonly TableColumns IdColumnMaster = new()
        {
            DataType = SqLiteDataTypes.Integer,
            PrimaryKey = true,
            Unique = true,
            NotNull = true
        };

        /// <summary>
        ///     Database Attributes, for Master Table
        /// </summary>
        internal static readonly TableColumns NameColumnMaster = new()
        {
            DataType = SqLiteDataTypes.Text,
            PrimaryKey = false,
            Unique = false,
            NotNull = true
        };

        /// <summary>
        ///     Database Attributes, for Master Table
        /// </summary>
        internal static readonly TableColumns NameColumnImage = new()
        {
            DataType = SqLiteDataTypes.Text,
            PrimaryKey = false,
            Unique = false,
            NotNull = true
        };
    }
}