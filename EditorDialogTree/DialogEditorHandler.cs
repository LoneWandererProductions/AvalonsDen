/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorDialogTree/DialogEditorHandler.cs
 * PURPOSE:     Helper Engine to convert Graph into a Dialog Structure
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using DialogEngine;

namespace EditorDialogTree
{
    /// <summary>
    ///     The dialog editor handler class.
    /// </summary>
    internal static class DialogEditorHandler
    {
        /// <summary>
        ///     Add the element.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="level">The level.</param>
        internal static void AddElement(int id, int level)
        {
            var dialog = new DialogDisplay
            {
                BaseDialog = new DialogItem
                {
                    MasterId = id,
                    IsItemactive = true,
                    IsRepeatable = true,
                    IsMaster = true,
                    Level = level
                }
            };

            Register.DialogTree.Add(dialog.BaseDialog.MasterId, dialog);
        }

        /// <summary>
        ///     Insert the tree.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="level">The level.</param>
        internal static void InsertTree(int id, int level)
        {
            var tree = new Dictionary<int, DialogDisplay>(Register.DialogTree);

            if (level != Register.Level)
                for (var i = Register.DialogTreeCount; i >= id; i--)
                {
                    var element = Register.DialogTree[i];
                    tree.Remove(i);
                    element.BaseDialog.MasterId++;

                    tree.Add(element.BaseDialog.MasterId, element);
                }

            Register.DialogTree = new Dictionary<int, DialogDisplay>(tree);
            AddElement(id, level);
        }
    }
}