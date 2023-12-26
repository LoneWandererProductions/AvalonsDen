/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogEngine/Processing.cs
 * PURPOSE:     Load the Dialog into Memory so we can handle it
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using AvalonRuntime;
using ExtendedSystemObjects;
using FileHandler;
using Resources;
using Serializer;

// ReSharper disable ArrangeBraces_foreach

namespace DialogEngine
{
    /// <summary>
    ///     The dialog processing class.
    /// </summary>
    internal static class DialogProcessing
    {
        /// <summary>
        ///     basic path
        /// </summary>
        private static readonly string Root = Path.Combine(Directory.GetCurrentDirectory(),
            DialogEngineResources.CampaignsFolderExtended);

        /// <summary>
        ///     loads the Dialog into appreciated Files
        ///     Not shown to the Outside
        /// </summary>
        /// <param name="loadedDialog">Complete DialogStructure</param>
        /// <returns>Dialog Tree, can return null.</returns>
        internal static Dictionary<int, DialogDisplay> CollectDialogs(List<DialogObject> loadedDialog)
        {
            //Container
            var dialogTree = new Dictionary<int, DialogDisplay>();

            //Error here but whatever
            if (loadedDialog == null)
            {
                return null;
            }

            //add Master Items
            foreach (var dialogs in loadedDialog)
            {
                if (dialogs.IsMaster)
                {
                    var master = new DialogItem
                    {
                        CharacterId = dialogs.CharacterId,
                        DialogLine = dialogs.DialogLine,
                        InternalRemarksDialogItem = dialogs.InternalRemarks,
                        DialogType = dialogs.DialogType,
                        MasterId = dialogs.MasterId,
                        Level = dialogs.Level,
                        IsItemactive = dialogs.IsItemActive,
                        IsRepeatable = dialogs.IsRepeatable,
                        IsMaster = dialogs.IsMaster,
                        BackgroundImage = dialogs.BackGroundImage
                    };

                    //loaded Master List
                    var dialog = new DialogDisplay { BaseDialog = master };
                    dialogTree.Add(dialogs.MasterId, dialog);
                }
            }

            //add Choice Items
            foreach (var id in dialogTree.Select(dialogs => dialogs.Key))
            {
                //Slave
                dialogTree[id].ChoiceDialog = (from leaf in loadedDialog
                    where
                        !leaf.IsMaster
                        && leaf.MasterId == id
                    select new ChoiceItem

                    {
                        MasterId = leaf.MasterId,
                        CharacterId = leaf.CharacterId,
                        AntagonistId = leaf.AntagonistId,
                        DialogLine = leaf.DialogLine,
                        FollowUpDialog = leaf.FollowUpDialog,
                        InternalRemarksChoiceItem = leaf.InternalRemarks,
                        ConditionId = leaf.ConditionId,
                        EventId = leaf.EventId,
                        SuccessorId = leaf.SuccessorId,
                        IsEndPoint = leaf.IsEndPoint,
                        IsRepeatable = leaf.IsRepeatable,
                        IsMaster = leaf.IsMaster,
                        IsItemactive = leaf.IsItemActive,
                        ChildId = leaf.ChildId
                    }).ToList();
            }

            return dialogTree.Sort();
        }

        /// <summary>
        ///     Converts DialogStructure into a savable Object
        /// </summary>
        /// <param name="tree">Dialog Tree</param>
        /// <returns>Converted into a DialogObject</returns>
        internal static List<DialogObject> DialogConvertToSave(Dictionary<int, DialogDisplay> tree)
        {
            var savedDialog = new List<DialogObject>();

            //loop though Master
            foreach (var dialogs in tree.Values)
            {
                var master = new DialogObject
                {
                    CharacterId = dialogs.BaseDialog.CharacterId,
                    Level = dialogs.BaseDialog.Level,
                    IsRepeatable = dialogs.BaseDialog.IsRepeatable,
                    IsMaster = dialogs.BaseDialog.IsMaster,
                    DialogLine = dialogs.BaseDialog.DialogLine,
                    InternalRemarks = dialogs.BaseDialog.InternalRemarksDialogItem,
                    DialogType = dialogs.BaseDialog.DialogType,
                    MasterId = dialogs.BaseDialog.MasterId,
                    IsItemActive = dialogs.BaseDialog.IsItemactive,
                    BackGroundImage = dialogs.BaseDialog.BackgroundImage
                };
                savedDialog.Add(master);

                //add slave Elements from the Master
                savedDialog.AddRange(dialogs.ChoiceDialog.Select(item => new DialogObject
                {
                    MasterId = item.MasterId,
                    CharacterId = item.CharacterId,
                    AntagonistId = item.AntagonistId,
                    DialogLine = item.DialogLine,
                    FollowUpDialog = item.FollowUpDialog,
                    InternalRemarks = item.InternalRemarksChoiceItem,
                    ConditionId = item.ConditionId,
                    EventId = item.EventId,
                    SuccessorId = item.SuccessorId,
                    IsEndPoint = item.IsEndPoint,
                    IsRepeatable = item.IsRepeatable,
                    IsMaster = item.IsMaster,
                    IsItemActive = item.IsItemactive,
                    ChildId = item.ChildId
                }));
            }

            return savedDialog;
        }

        /// <summary>
        ///     Save a Dialog to target Path
        /// </summary>
        /// <param name="dialog">Dialog</param>
        /// <param name="path">Target Path</param>
        internal static void SaveDialog(List<DialogObject> dialog, string path)
        {
            Serialize.SaveLstObjectToXml(dialog, path);
        }

        /// <summary>
        ///     Save a Dialog to Autosave Path
        /// </summary>
        /// <param name="dialog">Complete Dialog</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Name of the Dialog</param>
        internal static void SaveDialog(List<DialogObject> dialog, string campaignName, string mapName,
            string dialogName)
        {
            var path = Path.Combine(Root, campaignName, DialogEngineResources.AutoSave, mapName,
                Path.ChangeExtension(dialogName, ArtConst.DialogObjectExt));
            SaveDialog(dialog, path);
        }

        /// <summary>
        ///     Return the Selected Dialog with all bells and whistles
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="id">Id of selected Dialog</param>
        /// <returns>Selected Dialog</returns>
        internal static DialogDisplay GetDialog(Dictionary<int, DialogDisplay> tree, int id)
        {
            return tree.ContainsKey(id) ? tree[id] : null;
        }

        /// <summary>
        ///     Get first active Element must not be the one with the Master Id one
        /// </summary>
        /// <param name="dialogTree">Dialog Tree</param>
        /// <returns>First active Dialog</returns>
        internal static DialogDisplay GetStartDialog(Dictionary<int, DialogDisplay> dialogTree)
        {
            return (from leaf in dialogTree where leaf.Value.BaseDialog.IsItemactive select leaf.Value)
                .FirstOrDefault();
        }

        /// <summary>
        ///     Set specific Dialog Active/Inactive
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="id">Id of Dialog</param>
        /// <param name="status">Status of the Dialog</param>
        /// <returns>Dialog Tree, can return null.</returns>
        internal static Dictionary<int, DialogDisplay> SetDialogStatusBaseDialog(Dictionary<int, DialogDisplay> tree,
            int id,
            bool status)
        {
            if (!tree.ContainsKey(id))
            {
                return null;
            }

            tree[id].BaseDialog.IsItemactive = status;
            return tree;
        }

        /// <summary>
        ///     Set specific Dialog Active/Inactive
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="id">Id of Dialog</param>
        /// <param name="childId">Id of Choice</param>
        /// <param name="status">Status of the Dialog</param>
        /// <returns>Dialog Tree, can return null.</returns>
        internal static Dictionary<int, DialogDisplay> SetDialogStatusChoiceDialog(Dictionary<int, DialogDisplay> tree,
            int id,
            int childId, bool status)
        {
            if (!tree.ContainsKey(id))
            {
                return null;
            }

            var choices = tree[id].ChoiceDialog;

            foreach (var choice in choices.Where(x => x.ChildId == childId))
            {
                choice.IsItemactive = status;
            }

            tree[id].ChoiceDialog = choices;

            return tree;
        }

        /// <summary>
        ///     Load the Dialog
        /// </summary>
        /// <param name="path">Target Path</param>
        /// <returns>Dialog Object</returns>
        internal static List<DialogObject> LoadDialogObject(string path)
        {
            return DeSerialize.LoadListFromXml<DialogObject>(path);
        }

        /// <summary>
        ///     Load the Dialog
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Name of the Dialog</param>
        /// <returns>Dialog Object, can return null.</returns>
        internal static List<DialogObject> LoadDialogObject(string campaignName, string mapName, string dialogName)
        {
            if (string.IsNullOrEmpty(dialogName))
            {
                return null;
            }

            dialogName = Path.ChangeExtension(dialogName, ArtConst.DialogObjectExt);

            //changed Dialog
            var path = Path.Combine(Root, campaignName, DialogEngineResources.AutoSave, mapName, dialogName);

            if (FileHandleSearch.FileExists(path))
            {
                return LoadDialogObject(path);
            }

            //unchanged Dialog
            path = Path.Combine(Root, campaignName, mapName, dialogName);
            return !FileHandleSearch.FileExists(path) ? null : LoadDialogObject(path);
        }
    }
}