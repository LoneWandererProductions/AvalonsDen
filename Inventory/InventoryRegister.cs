using System.Collections.Generic;
using Resources;

namespace Inventory
{
    /// <summary>
    ///     Generic Register for all Inventory Stuff
    /// </summary>
    internal static class InventoryRegister
    {
        /// <summary>
        ///     Gets or sets the item master.
        /// </summary>
        /// <value>
        ///     The item master.
        /// </value>
        internal static InventoryRegistry ItemMaster { get; set; }

        /// <summary>
        ///     Gets or sets the character Id and Name
        /// </summary>
        /// <value>
        ///     The character Name string, must be Unique!
        /// </value>
        internal static Dictionary<int, string> Character { get; set; }

        // we need
        //character and Equipment, Unique Id, Character
        internal static Dictionary<int, CharacterOverview> Party { get; set; }

        /// <summary>
        ///     Gets or sets the names.
        /// </summary>
        /// <value>
        ///     The names.
        /// </value>
        internal static List<string> Names { get; set; }
    }
}