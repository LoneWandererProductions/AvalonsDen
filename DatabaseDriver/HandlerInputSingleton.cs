/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DatabaseDriver/HandlerInputSingleton.cs
 * PURPOSE:     SingleTon Home brewed, all Editor Inputs
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeMadeStatic.Global, would defeat the purpose of the SingleTon
// ReSharper disable MemberCanBeInternal
// ReSharper disable UnusedMember.Global
// ReSharper disable UseNullPropagationWhenPossible
// ReSharper disable ConvertToExpressionBodyWhenPossible
// ReSharper disable ConvertIfStatementToReturnStatement

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Debugger;
using Resources;
using SQLiteHelper;
using ViewModel;

namespace DatabaseDriver
{
    /// <summary>
    ///     Editor Only
    /// </summary>
    public static class HandlerInputSingleton
    {
        /// <summary>
        ///     The lazy db Instance, Singleton Pattern
        /// </summary>
        private static Lazy<DbInput> _lazyDb;

        /// <summary>
        ///     Get Instance
        /// </summary>
        /// <returns>Single Instance of DB Interface</returns>
        /// <exception cref="ArgumentNullException">Instance was not initialized</exception>
        public static DbInput Instance
        {
            get
            {
                if (_lazyDb == null)
                    //TODO Replace
                    throw new ArgumentNullException(DatabaseDriverResources.ErrorDbNotInitialized);

                return _lazyDb.Value;
            }
        }

        /// <summary>
        ///     Initiate Database, Single Instance
        /// </summary>
        /// <param name="folder">Target Path to DB</param>
        /// <param name="dbName">DB Name</param>
        /// <returns>Db Handler for Editor</returns>
        /// <exception cref="ArgumentNullException">Parameters were Empty</exception>
        public static DbInput Create(string folder, string dbName)
        {
            if (string.IsNullOrEmpty(dbName)) throw new IOException(DatabaseDriverResources.ErrorWrongPathProvided);

            //Set Db Name
            dbName = Path.ChangeExtension(dbName, DatabaseDriverResources.DbExtension);

            _lazyDb = new Lazy<DbInput>(() => new DbInput(folder, dbName));

            if (_lazyDb.Value == null)
                DebugLog.CreateLogFile(DatabaseDriverResources.ErrorLazyInitiation, ErCode.Error);

            DebugLog.CreateLogFile(DatabaseDriverResources.InformationStartUpIn, ErCode.Information);

            return _lazyDb.Value;
        }
    }

    /// <summary>
    ///     Editor Only Db Interface
    /// </summary>
    public sealed class DbInput
    {
        /// <summary>
        ///     The sqlite Db Handler.
        /// </summary>
        private static SqlLiteDatabase _isql;

        /// <summary>
        ///     The sqlite Utility.
        /// </summary>
        private static SqlLiteUtility _isqlUtil;

        /// <summary>
        ///     Initiate DB
        /// </summary>
        /// <param name="location">Complete Path</param>
        /// <param name="dbName">Name of DB</param>
        internal DbInput(string location, string dbName)
        {
            _isql = new SqlLiteDatabase(location, dbName);

            //Create database if it does not exist.
            _isql.CreateDatabase(false);
            _isqlUtil = new SqlLiteUtility();

            //initiate Debug
            _isql.SendMessage += DebugPrints;
        }

        /// <summary>
        ///     Check if we do have a correct Database
        /// </summary>
        /// <returns>True if correct Database</returns>
        public bool CheckDatabase()
        {
            var db = _isql.GetTables();

            if (db == null) return false;

            if (!db.Contains(DatabaseDriverResources.DbNameMaster)) return false;

            if (!db.Contains(DatabaseDriverResources.DbNameWeapon)) return false;

            if (!db.Contains(DatabaseDriverResources.DbNameArmor)) return false;

            if (!db.Contains(DatabaseDriverResources.DbNameMiscellaneous)) return false;

            if (!db.Contains(DatabaseDriverResources.DbImageTableName)) return false;

            return true;
        }

        /// <summary>
        ///     Create Master Table
        ///     do not make static
        /// </summary>
        public bool CreateMasterTable()
        {
            var check = _isql.CreateDatabase(true);

            if (!check)
            {
                DebugLog.CreateLogFile(DatabaseDriverResources.ErrorDbNotCreated, ErCode.Error);
                return false;
            }

            //Create Master Table
            var columns = new DictionaryTableColumns();

            columns.DColumns.Add(DatabaseDriverResources.DbColumnId, DatabaseDriverResources.IdColumnMaster);
            columns.DColumns.Add(DatabaseDriverResources.DbColumnTableName, DatabaseDriverResources.NameColumnMaster);

            check = _isql.CreateTable(DatabaseDriverResources.DbNameMaster, columns);
            if (!check)
            {
                DebugLog.CreateLogFile(DatabaseDriverResources.ErrorCouldNotCreateMasterTable, ErCode.Error);
                return false;
            }

            //Create Image Table
            columns = new DictionaryTableColumns();

            columns.DColumns.Add(DatabaseDriverResources.DbColumnId, DatabaseDriverResources.IdColumnMaster);
            columns.DColumns.Add(DatabaseDriverResources.DbImageHeaderName, DatabaseDriverResources.NameColumnImage);

            check = _isql.CreateTable(DatabaseDriverResources.DbImageTableName, columns);
            if (!check)
            {
                DebugLog.CreateLogFile(DatabaseDriverResources.ErrorCouldNotCreateImageTable, ErCode.Error);
                return false;
            }

            CreateItemTables();

            return true;
        }

        /// <summary>
        ///     Here we try to add the missing Database Tables
        /// </summary>
        /// <returns>Status Message</returns>
        public string TryRepair()
        {
            if (CheckDatabase()) return DatabaseDriverResources.MessageDbOkay;

            var check = CreateMasterTable();
            if (check) return DatabaseDriverResources.MessageDbWasFixed;

            DebugLog.CreateLogFile(DatabaseDriverResources.MessageDbWasNotFixed, ErCode.Error);
            return DatabaseDriverResources.MessageDbWasNotFixed;
        }

        /// <summary>
        ///     Add Miscellaneous to Master and specific Table
        /// </summary>
        /// <param name="miscellaneous">Object Miscellaneous could be generic but it is more safer</param>
        /// <returns>Success Status</returns>
        public bool AddItemToMiscellaneous(Miscellaneous miscellaneous)
        {
            if (miscellaneous == null) return false;

            var row = new TableSet
            {
                Row = new List<string> {miscellaneous.Id.ToString(), DatabaseDriverResources.DbNameMiscellaneous}
            };

            //Insert into Master
            var check = _isql.InsertSingleRow(DatabaseDriverResources.DbNameMaster, row, true);

            if (!check) return false;

            var data = _isqlUtil.ConvertToAttribute(miscellaneous);
            row = new TableSet {Row = data};

            //Insert into Detail Table
            return _isql.InsertSingleRow(DatabaseDriverResources.DbNameMiscellaneous, row, true);
        }

        /// <summary>
        ///     Add Weapon to Master and specific Table
        /// </summary>
        /// <param name="weapon">Object Weapon could be generic but it is more safer</param>
        /// <returns>Success Status</returns>
        public bool AddItemToWeapon(Weapon weapon)
        {
            if (weapon == null) return false;

            var row = new TableSet
            {
                Row = new List<string> {weapon.Id.ToString(), DatabaseDriverResources.DbNameWeapon}
            };

            //Insert into Master
            var check = _isql.InsertSingleRow(DatabaseDriverResources.DbNameMaster, row, true);

            if (!check) return false;

            var data = _isqlUtil.ConvertToAttribute(weapon);
            row = new TableSet {Row = data};

            //Insert into Detail Table
            return _isql.InsertSingleRow(DatabaseDriverResources.DbNameWeapon, row, true);
        }

        /// <summary>
        ///     Add Armor to Master and specific Table
        /// </summary>
        /// <param name="armor">Object Armor could be generic but it is more safer</param>
        /// <returns>Success Status</returns>
        public bool AddItemToArmor(Armor armor)
        {
            if (armor == null) return false;

            var row = new TableSet {Row = new List<string> {armor.Id.ToString(), DatabaseDriverResources.DbNameArmor}};

            //Insert into Master
            var check = _isql.InsertSingleRow(DatabaseDriverResources.DbNameMaster, row, true);

            if (!check) return false;

            var data = _isqlUtil.ConvertToAttribute(armor);
            row = new TableSet {Row = data};

            //Insert into Detail Table
            return _isql.InsertSingleRow(DatabaseDriverResources.DbNameArmor, row, true);
        }

        /// <summary>
        ///     Add Image to Image Master
        /// </summary>
        /// <param name="images">Object Images</param>
        /// <returns>Success Status</returns>
        public bool AddItemToImage(Images images)
        {
            if (images == null) return false;

            var name = DatabaseDriverResources.ImageDummy;
            if (!string.IsNullOrEmpty(images.ImagePath)) name = images.ImagePath;

            var row = new TableSet {Row = new List<string> {images.IdImage.ToString(), name}};

            //Insert into Detail Table
            return _isql.InsertSingleRow(DatabaseDriverResources.DbImageTableName, row, true);
        }

        /// <summary>
        ///     Get a List of used Ids
        /// </summary>
        /// <param name="tableName">Name of the Table</param>
        /// <returns>List of Ids</returns>
        public List<int> GetIdsMasterTable(string tableName)
        {
            var headers = new List<string> {DatabaseDriverResources.DbColumnId};
            var tlbMaster = _isql.SimpleSelect(tableName, headers);

            var master = tlbMaster?.Row.ConvertAll(id => id.Row[0]);

            return master?.Select(int.Parse).ToList();
        }

        /// <summary>
        ///     Get our Observable Object for the Weapon Table
        /// </summary>
        /// <returns>Item List</returns>
        public IEnumerable<DbIndex> GetWeaponInfoTable()
        {
            var tlb = GetTbl(DatabaseDriverResources.DbNameWeapon);

            return tlb == null ? null : ConvertData(tlb);
        }

        /// <summary>
        ///     Get our Observable Object for the Miscellaneous Table
        /// </summary>
        /// <returns>Item List</returns>
        public IEnumerable<DbIndex> GetMiscellaneousInfoTable()
        {
            var tlb = GetTbl(DatabaseDriverResources.DbNameMiscellaneous);

            return tlb == null ? null : ConvertData(tlb);
        }

        /// <summary>
        ///     Get our Observable Object for the Armor Table
        /// </summary>
        /// <returns>Item List</returns>
        public IEnumerable<DbIndex> GetAmorInfoTable()
        {
            var tlb = GetTbl(DatabaseDriverResources.DbNameArmor);

            return tlb == null ? null : ConvertData(tlb);
        }

        /// <summary>
        ///     Get our Observable Object for the Image Table
        /// </summary>
        /// <returns>Item List</returns>
        public IEnumerable<DbIndex> GetImageInfoTable()
        {
            var tlbMaster = _isql.SimpleSelect(DatabaseDriverResources.DbImageTableName);

            var tbl = tlbMaster?.Row.ConvertAll(data => new DbIndex
            {
                Id = data.Row[0],
                Name = data.Row[1]
            });

            return tbl?.Count == 0 ? null : tbl;
        }

        /// <summary>
        ///     Delete Item from Master and Sub Table
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Success Status</returns>
        public bool DeleteItem(string id)
        {
            var tlbMaster = _isql.SimpleSelect(DatabaseDriverResources.DbNameMaster, DatabaseDriverResources.DbColumnId,
                CompareOperator.Equal, id);

            if (tlbMaster == null) return false;

            var table = tlbMaster.Cell(0, 1);

            var rows = _isql.DeleteRows(table, DatabaseDriverResources.DbColumnId, id);
            if (rows != 1) return false;

            rows = _isql.DeleteRows(DatabaseDriverResources.DbNameMaster, DatabaseDriverResources.DbColumnId, id);
            return rows == 1;
        }

        /// <summary>
        ///     Delete Image from Image Table
        /// </summary>
        /// <param name="id">Image Id</param>
        /// <returns>Success Status</returns>
        public bool DeleteImages(string id)
            => _isql.DeleteRows(DatabaseDriverResources.DbImageTableName, DatabaseDriverResources.DbColumnId, id) == 1;

        /// <summary>
        ///     Update Armor Item
        /// </summary>
        /// <param name="armor">Armor Item</param>
        /// <returns>Success Status</returns>
        public bool UpdateArmorItem(Armor armor)
        {
            if (armor == null) return false;

            var index = armor.Id.ToString();

            var row = _isql.UpdateTable(DatabaseDriverResources.DbNameArmor, CompareOperator.Equal,
                DatabaseDriverResources.DbColumnId, index, armor);

            return row == 1;
        }

        /// <summary>
        ///     Update Miscellaneous Item
        /// </summary>
        /// <param name="miscellaneous">Miscellaneous Item</param>
        /// <returns>Success Status</returns>
        public bool UpdateMiscellaneousItem(Miscellaneous miscellaneous)
        {
            if (miscellaneous == null) return false;

            var index = miscellaneous.Id.ToString();

            var row = _isql.UpdateTable(DatabaseDriverResources.DbNameMiscellaneous, CompareOperator.Equal,
                DatabaseDriverResources.DbColumnId, index, miscellaneous);

            return row == 1;
        }

        /// <summary>
        ///     Update Weapon Item
        /// </summary>
        /// <param name="weapon">Weapon Item</param>
        /// <returns>Success Status</returns>
        public bool UpdateWeaponItem(Weapon weapon)
        {
            if (weapon == null) return false;

            var index = weapon.Id.ToString();

            var row = _isql.UpdateTable(DatabaseDriverResources.DbNameWeapon, CompareOperator.Equal,
                DatabaseDriverResources.DbColumnId, index, weapon);

            return row == 1;
        }

        /// <summary>
        ///     Update Image Table
        /// </summary>
        /// <param name="image">Image Item</param>
        /// <returns>Success Status</returns>
        public bool UpdateImage(Images image)
        {
            if (image == null) return false;

            var index = image.IdImage.ToString();

            var row = _isql.UpdateTable(DatabaseDriverResources.DbImageTableName, CompareOperator.Equal,
                DatabaseDriverResources.DbColumnId, index, image);

            return row == 1;
        }

        /// <summary>
        ///     Create Sub Tables
        /// </summary>
        private static void CreateItemTables()
        {
            _isqlUtil = new SqlLiteUtility();

            var armor = new Armor();
            CreateItemTables(DatabaseDriverResources.DbNameArmor, armor);

            var weapon = new Weapon();
            CreateItemTables(DatabaseDriverResources.DbNameWeapon, weapon);

            var miscellaneous = new Miscellaneous();
            CreateItemTables(DatabaseDriverResources.DbNameMiscellaneous, miscellaneous);
        }

        /// <summary>
        ///     Create unique Index
        /// </summary>
        /// <param name="name">Name of the Table</param>
        /// <param name="objct">Object Type</param>
        private static void CreateItemTables(string name, object objct)
        {
            var tbl = _isqlUtil.ConvertObject(objct);
            var check = _isql.CreateTable(name, tbl);

            if (check)
                _isql.CreateUniqueIndex(name, DatabaseDriverResources.DbColumnId,
                    string.Concat(DatabaseDriverResources.DbIndexName, name));
        }

        /// <summary>
        ///     Get Collection of Ids, based on Parameter
        /// </summary>
        /// <param name="compare">Name of the Column</param>
        /// <returns>Basic Result Set for the overview Table</returns>
        private static DataSet GetTbl(string compare)
        {
            var tlbMaster = _isql.SimpleSelect(DatabaseDriverResources.DbNameMaster,
                DatabaseDriverResources.DbColumnTableName,
                CompareOperator.Equal, compare);

            if (tlbMaster == null) return null;

            var ids = tlbMaster.Row.ConvertAll(id => id.Row[0]);

            return _isql.SelectIn(compare, DatabaseDriverResources.Headers, DatabaseDriverResources.DbColumnId, ids);
        }

        /// <summary>
        ///     Convert into a another Object to Observer it
        /// </summary>
        /// <param name="tbl">Result Set with the Ids</param>
        /// <returns>Converted Object to Data-binding</returns>
        private static List<DbIndex> ConvertData(DataSet tbl) => tbl.Row.ConvertAll(data => new DbIndex
        {
            Id = data.Row[0],
            Name = data.Row[1]
        });

        /// <summary>
        ///     Print out Debug Messages, mostly external Sources
        /// </summary>
        /// <param name="sender">Object that sends the Message</param>
        /// <param name="e">Debug Messages</param>
        private static void DebugPrints(object sender, string e) => DebugLog.CreateLogFile(e, ErCode.External);
    }

    /// <inheritdoc />
    /// <summary>
    ///     Object for collecting short info of a Table
    /// </summary>
    public sealed class DbIndex : ObservableObject
    {
        /// <summary>
        ///     The id.
        /// </summary>
        private readonly string _id;

        /// <summary>
        ///     The name.
        /// </summary>
        private readonly string _name;

        /// <summary>
        ///     Table Id
        /// </summary>
        public string Id
        {
            get => _id;
            init
            {
                _id = value;
                RaisePropertyChangedEvent(nameof(Id));
            }
        }

        /// <summary>
        ///     Custom Name or Standard Name
        /// </summary>
        public string Name
        {
            get => _name;
            init
            {
                _name = value;
                RaisePropertyChangedEvent(nameof(Name));
            }
        }
    }
}