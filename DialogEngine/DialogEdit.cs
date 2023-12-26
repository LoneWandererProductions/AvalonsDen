/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogEngine/DialogEdit.cs
 * PURPOSE:     Dialog Handler that will handle the complete Dialog, mostly Editor
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

namespace DialogEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     Head: List of all Characters
    ///     Dialog: List of: 0 is starting Dialog, Predecessor is unique|Type Dialog|Id of Character|Text they say Player and
    ///     NPCs|optional Text is checked by Character if Character is optional|Successor
    ///     Choice: List of: Predecessor same choice is equal is not unique|Type Choice|Id of Character|Optional Condition|Text
    ///     they say Player and NPCs|Optional Event with Id|Successor
    ///     Choice: List of: Predecessor same choice is equal is not unique|Type Choice|Id of Character|Optional Condition|Text
    ///     they say Player and NPCs|Optional Event with Id|Successor
    ///     Choice: List of: Predecessor same choice is equal is not unique|Type Choice|Id of Character|Optional Condition|Text
    ///     they say Player and NPCs|Optional Event with Id|Successor
    ///     One List one Master Object that contains all fields that are filled defined by Type
    ///     Optional Conditions for now Character only!
    ///     Optional Event will be Handled by AssetLoader
    /// </summary>
    public sealed class DialogEdit : IDialogEdit
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the dialog tree.
        /// </summary>
        public Dictionary<int, DialogDisplay> DialogTree { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign, Editor use
        /// </summary>
        /// <param name="dialog">Standard Dialog Object</param>
        public void InitiateDialog(List<DialogObject> dialog)
        {
            DialogTree = DialogProcessing.CollectDialogs(dialog);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load a Dialog
        ///     Editor Only
        /// </summary>
        /// <param name="dialogTree">Dialog</param>
        /// <param name="path">Target Path</param>
        public void SaveDialogObjects(string path, Dictionary<int, DialogDisplay> dialogTree)
        {
            var dialog = DialogProcessing.DialogConvertToSave(dialogTree);
            DialogProcessing.SaveDialog(dialog, path);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load the Dialog
        ///     Editor only
        /// </summary>
        /// <param name="path">Target Path</param>
        /// <returns>Dialog Object</returns>
        public List<DialogObject> LoadDialogObject(string path)
        {
            return DialogProcessing.LoadDialogObject(path);
        }
    }
}