/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorDialogTree/Register.cs
 * PURPOSE:     Helper Object that holds the raw data of the Visual Tree
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using DialogEngine;
using ExtendedSystemObjects;

namespace EditorDialogTree
{
    /// <summary>
    ///     Shared Variables and Objects
    /// </summary>
    internal static class Register
    {
        /// <summary>
        ///     Used in DialogHandler
        ///     internal Dialog Logic
        ///     Will be loaded into and saved All Changes will be done over the Cursor object
        /// </summary>
        internal static Dictionary<int, DialogDisplay> DialogTree { get; set; } = new();

        /// <summary>
        ///     We handle the data we are currently working on in this Cursor, the End Data will be stored in DialogTree
        ///     Data Context
        /// </summary>
        internal static DialogDisplay Cursor { get; set; } = new();

        /// <summary>
        ///     Will be set manual by Control
        /// </summary>
        internal static int Level { get; set; }

        /// <summary>
        ///     Will be set manual by Control
        /// </summary>
        internal static int DialogCount => DialogStructure.Count;

        /// <summary>
        ///     Will be set manual by Control
        /// </summary>
        internal static int DialogTreeCount => DialogTree.Count;

        /// <summary>
        ///     is the x Axis
        /// </summary>
        internal static int ColumnCellCount { get; set; }

        /// <summary>
        ///     is the y Axis
        /// </summary>
        internal static int RowCellCount => Level * 2;

        /// <summary>
        ///     Used in TreeHandler
        /// </summary>
        public static Dictionary<int, Node> DialogStructure { get; internal set; } = new();

        /// <summary>
        ///     Get the index.
        /// </summary>
        /// <returns>The first free index <see cref="int" />.</returns>
        internal static int GetIndex()
        {
            if (Cursor.ChoiceDialog.Count == 0) return 0;

            var lst = new List<int>();

            foreach (var choice in Cursor.ChoiceDialog) lst.Add(choice.ChildId);

            return Utility.GetFirstAvailableIndex(lst);
        }
    }

    /// <summary>
    ///     Coordinate like Object that describes Position of Dialog in the Tree
    /// </summary>
    internal sealed class Node
    {
        /// <summary>
        ///     X Value
        /// </summary>
        internal int XValue { get; set; }

        /// <summary>
        ///     Unique Id
        /// </summary>
        internal int Id { get; set; }

        /// <summary>
        ///     Y Value,  Layer
        /// </summary>
        internal int Level { get; set; }

        /// <summary>
        ///     ParentId
        /// </summary>
        internal List<int> ParentId { get; set; } = new();
    }
}