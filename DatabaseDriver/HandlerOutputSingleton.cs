/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DatabaseDriver/HandlerOutputSingleton.cs
 * PURPOSE:     SingleTon Home brewed, all Campaign Inputs
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeMadeStatic.Global, would defeat the purpose of the SingleTon

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Debugger;
using Resources;
using SQLiteHelper;

// ReSharper disable ConvertIfStatementToReturnStatement, we might change this in the future so leave it be

namespace DatabaseDriver
{
    /// <summary>
    ///     Campaign Only
    /// </summary>
    public static class HandlerOutputSingleton
    {
        /// <summary>
        ///     The lazy DB Instance, Singleton
        /// </summary>
        private static Lazy<DbOutput> _lazyDb;

        /// <summary>
        ///     Get Instance
        /// </summary>
        /// <returns>Single Instance of DB Interface</returns>
        /// <exception cref="ArgumentNullException">Instance was not initialized</exception>
        /// <exception cref="IOException">Could not open the Database</exception>
        public static DbOutput GetInstance()
        {
            if (_lazyDb == null) throw new IOException(DatabaseDriverResources.ErrorDbNotInitialized);

            return _lazyDb.Value;
        }

        /// <summary>
        ///     Initiate Database, Single Instance
        /// </summary>
        /// <param name="path">Target Path to DB</param>
        /// <returns>Db Handler for Campaign</returns>
        /// <exception cref="ArgumentNullException">Parameters were Empty</exception>
        public static DbOutput CreateInstance(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(string.Concat(DatabaseDriverResources.ErrorWrongPathProvided, path));

            if (!File.Exists(path))
                throw new ArgumentNullException(string.Concat(DatabaseDriverResources.ErrorFileNotFound, path));

            var location = Path.GetDirectoryName(path);
            var dbName = Path.GetFileName(path);

            _lazyDb = new Lazy<DbOutput>(() => new DbOutput(location, dbName));

            DebugLog.CreateLogFile(DatabaseDriverResources.InformationStartUpOut, ErCode.Information);

            return _lazyDb.Value;
        }

        /// <summary>
        ///     Initiate Database, Single Instance
        /// </summary>
        /// <param name="campaignName">Name of the campaign</param>
        /// <returns>Db Handler for Campaign</returns>
        /// <exception cref="ArgumentNullException">Parameters were Empty</exception>
        public static DbOutput CreateInstanceByName(string campaignName)
        {
            if (string.IsNullOrEmpty(campaignName))
                throw new ArgumentNullException(string.Concat(DatabaseDriverResources.ErrorWrongPathProvided,
                    campaignName));

            var location = Path.Combine(Directory.GetCurrentDirectory(), DatabaseDriverResources.CorePath,
                campaignName);
            var dbName = string.Concat(campaignName, DatabaseDriverResources.DbExtension);

            if (!File.Exists(Path.Combine(location, dbName)))
                throw new ArgumentNullException(string.Concat(DatabaseDriverResources.ErrorFileNotFound,
                    Path.Combine(location, dbName)));

            _lazyDb = new Lazy<DbOutput>(() => new DbOutput(location, dbName));

            DebugLog.CreateLogFile(DatabaseDriverResources.InformationStartUpOut, ErCode.Information);

            return _lazyDb.Value;
        }
    }

    /// <summary>
    ///     Campaign Only Db Interface
    /// </summary>
    public sealed class DbOutput
    {
        /// <summary>
        ///     The SqlLite Interface.
        /// </summary>
        private static SqlLiteDatabase _isql;

        /// <summary>
        ///     The SqlLite utility.
        /// </summary>
        private static SqlLiteUtility _isqlUtil;

        /// <summary>
        ///     Initiate DB
        /// </summary>
        /// <param name="location">Complete Path</param>
        /// <param name="dbName">Name of DB, campaign Name and .db as Extension</param>
        internal DbOutput(string location, string dbName)
        {
            _isql = new SqlLiteDatabase(location, dbName);
            _isqlUtil = new SqlLiteUtility();

            //initiate Debug
            _isql.SendMessage += DebugPrints;
        }

        /// <summary>
        ///     Select Id and get Item Type,
        ///     Weapon, Sword, Amour, etc ...
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Return Type of Item</returns>
        public string GetItemType(string id)
        {
            //master
            var row = _isql.SimpleSelect(DatabaseDriverResources.DbNameMaster, DatabaseDriverResources.DbColumnId,
                CompareOperator.Equal, id);

            if (!string.IsNullOrEmpty(row?.Cell(0, 1))) return row.Cell(0, 1);

            return null;
        }

        /// <summary>
        ///     Miscellaneous Item from Table
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Miscellaneous Object</returns>
        public Miscellaneous GetItemMiscellaneous(string id)
        {
            //Object
            var row = _isql.SimpleSelect(DatabaseDriverResources.DbNameMiscellaneous,
                DatabaseDriverResources.DbColumnId, CompareOperator.Equal, id);

            if (row == null) return null;

            var lst = row.Columns(0);

            var miscellaneous = new Miscellaneous();
            return (Miscellaneous) _isqlUtil.FillObject(lst, miscellaneous);
        }

        /// <summary>
        ///     Weapon Item from Table
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Weapon Object</returns>
        public Weapon GetItemWeapon(string id)
        {
            //Object
            var row = _isql.SimpleSelect(DatabaseDriverResources.DbNameWeapon, DatabaseDriverResources.DbColumnId,
                CompareOperator.Equal, id);

            if (row == null) return null;

            var lst = row.Columns(0);

            var weapon = new Weapon();
            return (Weapon) _isqlUtil.FillObject(lst, weapon);
        }

        /// <summary>
        ///     Armor Item from Table
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Armor Object</returns>
        public Armor GetItemArmor(string id)
        {
            //Object
            var row = _isql.SimpleSelect(DatabaseDriverResources.DbNameArmor, DatabaseDriverResources.DbColumnId,
                CompareOperator.Equal, id);

            if (row == null) return null;

            var lst = row.Columns(0);

            var armor = new Armor();
            return (Armor) _isqlUtil.FillObject(lst, armor);
        }

        /// <summary>
        ///     Get the Image Object of the Id
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Image Object</returns>
        public Images GetItemImage(string id)
        {
            //Object
            var row = _isql.SimpleSelect(DatabaseDriverResources.DbImageTableName, DatabaseDriverResources.DbColumnId,
                CompareOperator.Equal, id);

            if (row == null) return null;

            var lst = row.Columns(0);

            var image = new Images();
            return (Images) _isqlUtil.FillObject(lst, image);
        }

        /// <summary>
        ///     Get the Image Name of the Id
        /// </summary>
        /// <param name="id">Id of Image</param>
        /// <returns>Path to Image</returns>
        public string GetImagePath(string id)
        {
            //Object
            var row = _isql.SimpleSelect(DatabaseDriverResources.DbImageTableName, DatabaseDriverResources.DbColumnId,
                CompareOperator.Equal, id);

            return row?.Cell(0, 1);
        }

        /// <summary>
        ///     Gets the items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>The Collection of Items mostly used for Looting</returns>
        [return: MaybeNull]
        public InventoryHolder GetItems(IEnumerable<InventoryContainer> items)
        {
            var holder = new InventoryHolder();

            foreach (var item in items)
            {
                //parse id
                var id = DatabaseDriverHelper.GetId(holder, item.Id);

                if (id == -1) return null;

                holder.Amount.Add(id, item.Amount);

                //get image
                var image = GetImagePath(item.Id);

                //only a Warning, try to continue
                if (holder.Image.ContainsKey(id))
                    DebugLog.CreateLogFile(
                        string.Concat(DatabaseDriverResources.ErrorKeyViolation,
                            DatabaseDriverResources.WarningImageNotFound), ErCode.Warning);
                else
                    holder.Image.Add(id, image);

                //get item
                switch (GetItemType(item.Id))
                {
                    case DatabaseDriverResources.DbNameWeapon:

                        if (holder.WeaponItems.ContainsKey(id))
                        {
                            DebugLog.CreateLogFile(DatabaseDriverResources.ErrorKeyViolation, ErCode.Error);
                            return null;
                        }

                        var weapon = GetItemWeapon(item.Id);
                        holder.WeaponItems.Add(id, weapon);
                        break;

                    case DatabaseDriverResources.DbNameMiscellaneous:

                        if (holder.MiscellaneousItems.ContainsKey(id))
                        {
                            DebugLog.CreateLogFile(DatabaseDriverResources.ErrorKeyViolation, ErCode.Error);
                            return null;
                        }

                        var misc = GetItemMiscellaneous(item.Id);
                        holder.MiscellaneousItems.Add(id, misc);
                        break;

                    case DatabaseDriverResources.DbNameArmor:

                        if (holder.ArmorItems.ContainsKey(id))
                        {
                            DebugLog.CreateLogFile(DatabaseDriverResources.ErrorKeyViolation, ErCode.Error);
                            return null;
                        }

                        var armor = GetItemArmor(item.Id);
                        holder.ArmorItems.Add(id, armor);
                        break;

                    default:
                        DebugLog.CreateLogFile(
                            string.Concat(DatabaseDriverResources.ErrorCouldNotFindTable, GetItemType(item.Id)),
                            ErCode.Error);
                        return null;
                }
            }

            return holder;
        }

        public InventoryRegistry GetItems(IEnumerable<int> items)
        {
            var holder = new InventoryRegistry();

            foreach (var id in items)
            {
                //get image
                var image = GetImagePath(id.ToString());

                //only a Warning, try to continue
                if (holder.Image.ContainsKey(id))
                    DebugLog.CreateLogFile(
                        string.Concat(DatabaseDriverResources.ErrorKeyViolation,
                            DatabaseDriverResources.WarningImageNotFound), ErCode.Warning);
                else
                    holder.Image.Add(id, image);

                //get item
                switch (GetItemType(id.ToString()))
                {
                    case DatabaseDriverResources.DbNameWeapon:

                        if (holder.WeaponItems.ContainsKey(id))
                        {
                            DebugLog.CreateLogFile(DatabaseDriverResources.ErrorKeyViolation, ErCode.Error);
                            return null;
                        }

                        var weapon = GetItemWeapon(id.ToString());
                        holder.WeaponItems.Add(id, weapon);
                        break;

                    case DatabaseDriverResources.DbNameMiscellaneous:

                        if (holder.MiscellaneousItems.ContainsKey(id))
                        {
                            DebugLog.CreateLogFile(DatabaseDriverResources.ErrorKeyViolation, ErCode.Error);
                            return null;
                        }

                        var misc = GetItemMiscellaneous(id.ToString());
                        holder.MiscellaneousItems.Add(id, misc);
                        break;

                    case DatabaseDriverResources.DbNameArmor:

                        if (holder.ArmorItems.ContainsKey(id))
                        {
                            DebugLog.CreateLogFile(DatabaseDriverResources.ErrorKeyViolation, ErCode.Error);
                            return null;
                        }

                        var armor = GetItemArmor(id.ToString());
                        holder.ArmorItems.Add(id, armor);
                        break;

                    default:
                        DebugLog.CreateLogFile(
                            string.Concat(DatabaseDriverResources.ErrorCouldNotFindTable, GetItemType(id.ToString())),
                            ErCode.Error);
                        return null;
                }
            }

            return holder;
        }


        /// <summary>
        ///     Print out Debug Messages, mostly external Sources
        /// </summary>
        /// <param name="sender">Object that sends the Message</param>
        /// <param name="e">Debug Messages</param>
        private static void DebugPrints(object sender, string e)
        {
            DebugLog.CreateLogFile(e, ErCode.External);
        }
    }
}