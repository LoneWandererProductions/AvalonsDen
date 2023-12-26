/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DatabaseDriver/DatabaseDriverHelper.cs
 * PURPOSE:     Helper Class
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using Debugger;
using Resources;

namespace DatabaseDriver
{
    /// <summary>
    ///     Helper Class for DatabaseDriver
    /// </summary>
    internal static class DatabaseDriverHelper
    {
        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <param name="holder">The holder.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>The Id of the Item</returns>
        internal static int GetId(InventoryHolder holder, string itemId)
        {
            var check = int.TryParse(itemId, out var id);
            if (!check)
            {
                DebugLog.CreateLogFile(
                    string.Concat(DatabaseDriverResources.ErrorKeyCouldNotConvertToInt, itemId), ErCode.Error);
                return -1;
            }

            if (!holder.Amount.ContainsKey(id)) return id;

            DebugLog.CreateLogFile(DatabaseDriverResources.ErrorKeyViolation, ErCode.Error);
            return -1;
        }
    }
}