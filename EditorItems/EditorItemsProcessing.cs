/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorItems/EditorItemsProcessing.cs
 * PURPOSE:     Handle all external Database accesses and Calculations
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using DatabaseDriver;
using ExtendedSystemObjects;
using Resources;

namespace EditorItems
{
    /// <summary>
    ///     Interface to our Database SingleTon, does all the heavy lifting with direct Database Manipulations
    /// </summary>
    internal static class EditorItemsProcessing
    {
        /// <summary>
        ///     Master Index, only used if we want to add a new index for an Item
        /// </summary>
        private static List<int> _index;

        /// <summary>
        ///     Master Index, only used if we want to add a new index for an Image
        /// </summary>
        private static List<int> _imageIndex;

        /// <summary>
        ///     Element added,deleted,updated
        /// </summary>
        internal static EventHandler RefreshTable { get; set; }

        /// <summary>
        ///     Set Index
        /// </summary>
        public static void SetIndex()
        {
            var dbIn = HandlerInputSingleton.Instance;
            //Mostly for internal use if we want to create a new Object from the Template
            var index = dbIn.GetIdsMasterTable(EditorItemsResources.DbNameMaster) ?? new List<int>();
            var imageIndex = dbIn.GetIdsMasterTable(EditorItemsResources.DbNameImage) ?? new List<int>();

            _index = index;
            _imageIndex = imageIndex;
        }

        /// <summary>
        ///     Implemented, Adds the Armor to the Master Table
        /// </summary>
        internal static void AddArmor()
        {
            var dbIn = HandlerInputSingleton.Instance;
            var id = Utility.GetFirstAvailableIndex(_index);
            var armor = new Armor { Id = id, ImageId = id };
            var check = dbIn.AddItemToArmor(armor);

            if (!check) return;
            //add our new Item to the Index, if successful
            _index.Add(id);
            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Implemented, Adds the Weapon to the Master Table
        /// </summary>
        internal static void AddWeapon()
        {
            var dbIn = HandlerInputSingleton.Instance;
            var id = Utility.GetFirstAvailableIndex(_index);
            var weapon = new Weapon { Id = id, ImageId = id };
            var check = dbIn.AddItemToWeapon(weapon);

            if (!check) return;
            //add our new Item to the Index, if successful
            _index.Add(id);
            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Implemented, Adds the Miscellaneous to the Master Table
        /// </summary>
        internal static void AddMiscellaneous()
        {
            var dbIn = HandlerInputSingleton.Instance;
            var id = Utility.GetFirstAvailableIndex(_index);
            var miscellaneous = new Miscellaneous { Id = id, ImageId = id };
            var check = dbIn.AddItemToMiscellaneous(miscellaneous);

            if (!check) return;
            //add our new Item to the Index, if successful
            _index.Add(id);
            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Implemented, Adds a Image to the Master Table
        /// </summary>
        internal static void AddImage()
        {
            var dbIn = HandlerInputSingleton.Instance;
            var id = Utility.GetFirstAvailableIndex(_imageIndex);
            var images = new Images { IdImage = id };
            var check = dbIn.AddItemToImage(images);

            if (!check) return;
            //add our new Item to the Index, if successful
            _imageIndex.Add(id);
            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Implemented, Save the changes to Armor
        /// </summary>
        /// <param name="armor">Changed Armor Item</param>
        internal static void SaveArmor(Armor armor)
        {
            var dbIn = HandlerInputSingleton.Instance;
            var check = dbIn.UpdateArmorItem(armor);

            if (!check) return;

            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Implemented, Save the changes to Miscellaneous
        /// </summary>
        /// <param name="miscellaneous">Changed Miscellaneous Item</param>
        internal static void SaveMiscellaneous(Miscellaneous miscellaneous)
        {
            var dbIn = HandlerInputSingleton.Instance;
            var check = dbIn.UpdateMiscellaneousItem(miscellaneous);

            if (!check) return;

            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Implemented, Save the changes to Miscellaneous
        /// </summary>
        /// <param name="weapon">Changed Weapon Item</param>
        internal static void SaveWeapon(Weapon weapon)
        {
            var dbIn = HandlerInputSingleton.Instance;
            var check = dbIn.UpdateWeaponItem(weapon);

            if (!check) return;

            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Implemented, save Changes to Image Table
        /// </summary>
        /// <param name="image">Image</param>
        internal static void SaveImages(Images image)
        {
            var dbIn = HandlerInputSingleton.Instance;
            var check = dbIn.UpdateImage(image);

            if (!check) return;

            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Implemented, delete Item from Master List and specific Table
        /// </summary>
        /// <param name="id">Id of Item</param>
        internal static void DeleteItem(string id)
        {
            var dbIn = HandlerInputSingleton.Instance;
            var check = dbIn.DeleteItem(id);
            if (!check) return;

            //add our new Item to the Index, if successful
            check = int.TryParse(id, out var index);
            if (check) _index.Remove(index);

            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Just delete an Image
        /// </summary>
        /// <param name="id">Id of Item</param>
        internal static void DeleteImages(string id)
        {
            var dbIn = HandlerInputSingleton.Instance;
            var check = dbIn.DeleteImages(id);
            if (!check) return;

            //add our new Item to the Index, if successful
            check = int.TryParse(id, out var index);
            if (check) _imageIndex.Remove(index);

            RefreshTable?.Invoke(EditorItemsResources.Sender, EventArgs.Empty);
        }
    }
}