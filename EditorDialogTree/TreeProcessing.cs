/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorDialogTree/TreeProcessing.cs
 * PURPOSE:     Helper Engine to build the Graph for the Dialog Structure
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using DialogEngine;
using ExtendedSystemObjects;

namespace EditorDialogTree
{
    /// <summary>
    ///     The tree processing class.
    /// </summary>
    internal static class TreeProcessing
    {
        /// <summary>
        ///     Convert the to tree display.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns>The <see cref="T:Dictionary{int, Node}" />.</returns>
        internal static Dictionary<int, Node> ConvertToTreeDisplay(Dictionary<int, DialogDisplay> tree)
        {
            var dlgtree = new Dictionary<int, Node>();

            foreach (var node in tree.Values)
            {
                var leaf = new Node
                {
                    Id = node.BaseDialog.MasterId,
                    Level = node.BaseDialog.Level
                };
                dlgtree.Add(leaf.Id, leaf);

                if (node.ChoiceDialog.IsNullOrEmpty()) continue;

                dlgtree[leaf.Id].ParentId = node.ChoiceDialog.ConvertAll(dialog => dialog.SuccessorId);
            }

            return dlgtree;
        }

        /// <summary>
        ///     The initiate tree display.
        /// </summary>
        /// <param name="tree">The tree.</param>
        internal static void InitiateTreeDisplay(Dictionary<int, Node> tree)
        {
            Register.Level = MaxLevel(tree);

            var maxNode = 1;

            for (var i = Register.Level; i >= 1; i--)
            {
                var branch = GetBranch(i, tree);
                maxNode = CalcMax(branch, maxNode);

                branch = CalcXValues(branch, i);

                foreach (var node in branch.Values)
                {
                    tree.Remove(node.Id);
                    tree.Add(node.Id, node);
                }
            }

            Register.DialogStructure = tree;
            Register.ColumnCellCount = maxNode * 2;
        }

        /// <summary>
        ///     Add another Level to the Dialog
        /// </summary>
        internal static int AddLevel()
        {
            var id = MaxId(Register.DialogStructure);
            var node = new Node
            {
                Level = Register.Level + 1,
                Id = id + 1
            };
            Register.DialogStructure.Add(node.Id, node);
            return node.Id;
        }

        /// <summary>
        ///     Add Leaf at the defined Level
        /// </summary>
        /// <param name="level">Level of Dialog</param>
        internal static int AddElement(int level)
        {
            //add at the right position
            var branch = GetBranch(level, Register.DialogStructure);
            var id = MaxId(branch) + 1;

            var node = new Node
            {
                Level = level,
                Id = id
            };

            SortIDs(node.Level, id);

            Register.DialogStructure.Add(node.Id, node);
            return node.Id;
        }

        /// <summary>
        ///     Get the branch.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="branch">The branch.</param>
        /// <returns>The <see cref="T:Dictionary{int, Node}" />.</returns>
        private static Dictionary<int, Node> GetBranch(int level, Dictionary<int, Node> branch)
        {
            return branch.Values.Where(node => node.Level == level).ToDictionary(node => node.Id);
        }

        /// <summary>
        ///     Calculate x values.
        /// </summary>
        /// <param name="branch">The branch.</param>
        /// <param name="level">The level.</param>
        /// <returns>The Tree Branch<see cref="T:Dictionary{int, Node}" />.</returns>
        private static Dictionary<int, Node> CalcXValues(Dictionary<int, Node> branch, int level)
        {
            var treebranch = new Dictionary<int, Node>();
            var i = -2;

            foreach (var leaf in branch.Values)
            {
                i += 2;
                leaf.XValue = i;
                leaf.Level = level;
                treebranch.Add(leaf.Id, leaf);
            }

            return treebranch;
        }

        /// <summary>
        ///     Calculate max node.
        /// </summary>
        /// <param name="branch">The branch.</param>
        /// <param name="maxNode">The maxNode.</param>
        /// <returns>The max Node<see cref="int" />.</returns>
        private static int CalcMax(Dictionary<int, Node> branch, int maxNode)
        {
            if (branch.Count > maxNode) maxNode = branch.Count;

            return maxNode;
        }

        /// <summary>
        ///     The max level.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns>The max Level<see cref="int" />.</returns>
        private static int MaxLevel(Dictionary<int, Node> tree)
        {
            return tree.Values.Select(leaf => leaf.Level).Concat(new[] {1}).Max();
        }

        /// <summary>
        ///     The max id.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns>The Max Id<see cref="int" />.</returns>
        private static int MaxId(Dictionary<int, Node> tree)
        {
            return tree.Values.Select(leaf => leaf.Id).Concat(new[] {1}).Max();
        }

        /// <summary>
        ///     Fit a new Dialog into the Dictionary and increase all ids for the follow ups
        /// </summary>
        /// <param name="level">Level of the Dialog Tree</param>
        /// <param name="id">id of the new Dialog Item</param>
        private static void SortIDs(int level, int id)
        {
            if (level == Register.Level) return;

            var tree = new Dictionary<int, Node>(Register.DialogStructure);

            for (var i = Register.DialogCount; i >= id; i--)
            {
                var node = tree[i];
                tree.Remove(i);
                node.Id++;

                tree.Add(node.Id, node);
            }

            tree = ReorderParents(tree, id);

            Register.DialogStructure = new Dictionary<int, Node>(tree);
        }

        /// <summary>
        ///     The reorder parents.
        /// </summary>
        /// <param name="masterNodes">The masterNodes.</param>
        /// <param name="changedParent">The changedParent.</param>
        /// <returns>The Reordered Tree<see cref="T:Dictionary{int, Node}" />.</returns>
        private static Dictionary<int, Node> ReorderParents(IDictionary<int, Node> masterNodes, int changedParent)
        {
            var tree = new Dictionary<int, Node>(masterNodes);

            foreach (var node in masterNodes)
            {
                var parentList = new List<int>();

                foreach (var parents in node.Value.ParentId)
                    if (parents < changedParent)
                        parentList.Add(parents);
                    else
                        parentList.Add(parents + 1);

                node.Value.ParentId = parentList;
                tree[node.Key] = node.Value;
            }

            return tree;
        }
    }
}