/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorItems/EditorItemsResources.cs
 * PURPOSE:     String Resources
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace EditorItems
{
    /// <summary>
    ///     The editor items resources class.
    /// </summary>
    internal static class EditorItemsResources
    {
        /// <summary>
        ///     Extensions we allow
        /// </summary>
        internal const string DbExtension = "Item Database File(*.db)|*.db|All files (*.*)|*.*";

        /// <summary>
        ///     Master Table
        /// </summary>
        internal const string DbNameMaster = "Master";

        /// <summary>
        ///     used in Enum MasterTables
        /// </summary>
        internal const string DbNameArmor = "Armor";

        /// <summary>
        ///     used in Enum MasterTables
        /// </summary>
        internal const string DbNameWeapon = "Weapon";

        /// <summary>
        ///     used to Handle Image Table
        /// </summary>
        internal const string DbNameImage = "Image";

        /// <summary>
        ///     used in Enum MasterTables
        /// </summary>
        internal const string DbNameMiscellaneous = "Miscellaneous";

        /// <summary>
        ///     Sender of Notifications
        /// </summary>
        internal const string Sender = "EditorItemsProcessing";
    }
}