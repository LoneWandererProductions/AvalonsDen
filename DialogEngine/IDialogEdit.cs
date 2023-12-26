/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogEngine/IDialogEdit.cs
 * PURPOSE:     Interface for the Dialog Edit
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace DialogEngine
{
    /// <summary>
    ///     The IDialogEdit interface.
    /// </summary>
    internal interface IDialogEdit
    {
        /// <summary>
        ///     Editor
        ///     Gets or sets the dialog tree.
        /// </summary>
        /// <value>The Dialog Tree.</value>
        Dictionary<int, DialogDisplay> DialogTree { get; set; }

        /// <summary>
        ///     Campaign, Editor use
        /// </summary>
        /// <param name="dialog">Standard Dialog Object</param>
        void InitiateDialog(List<DialogObject> dialog);

        /// <summary>
        ///     Load a Dialog
        ///     Editor Only
        /// </summary>
        /// <param name="path">Target Path</param>
        /// <param name="dialogTree">Dialog</param>
        void SaveDialogObjects(string path, Dictionary<int, DialogDisplay> dialogTree);

        /// <summary>
        ///     Load the Dialog
        ///     Editor only
        /// </summary>
        /// <param name="path">Target Path</param>
        /// <returns>Dialog Object</returns>
        List<DialogObject> LoadDialogObject(string path);
    }
}