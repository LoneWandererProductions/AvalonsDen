/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenDialogs.cs
 * PURPOSE:     Basic Tests for Avalon Dialogs
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DialogEngine;
using ExtendedSystemObjects;
using FileHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resources;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The Avalon's den dialogs unit test class.
    /// </summary>
    [TestClass]
    public sealed class AvalonsDenDialogs
    {
        /// <summary>
        ///     The path (readonly). Value: System.IO.Path.Combine(Directory.GetCurrentDirectory(),
        ///     ResourcesGeneral.CampaignsFolder).
        /// </summary>
        private static readonly string PathBase = Path.Combine(Directory.GetCurrentDirectory(),
            ResourcesGeneral.CampaignsFolder);

        /// <summary>
        ///     The d edit (readonly). Value: new DialogEdit().
        /// </summary>
        private readonly DialogEdit _dEdit = new();

        /// <summary>
        ///     The d handler (readonly). Value: new DialogHandler().
        /// </summary>
        private readonly DialogCampaign _dHandler = new();

        /// <summary>
        ///     Check if our basic functions do Work out
        ///     Uses MapName: MapNameTest
        /// </summary>
        [TestMethod]
        public void DialogsBasics()
        {
            var ddialog = new List<DialogObject>
            {
                ResourcesGeneral.DialogDummy, //1
                ResourcesGeneral.DialogEnd, //2
                ResourcesGeneral.DialogStart //3
            };

            _dHandler.InitiateDialog(ddialog);

            var startD = _dHandler.StartDialog();

            Assert.AreEqual(2, startD.BaseDialog.MasterId, string.Concat(
                "Test passed Get StartElement: ", startD.BaseDialog.MasterId));

            _dHandler.SetInactive(2);

            _dHandler.SetActive(1);

            startD = _dHandler.StartDialog();

            Debug.WriteLine(startD.BaseDialog.MasterId);

            Assert.AreEqual(1, startD.BaseDialog.MasterId, "Test passed set Active/-Inactive");
        }

        /// <summary>
        ///     Create Dialog Object Load it
        ///     Tests Standard Save
        ///     Uses MapName: MapNameTest
        /// </summary>
        [TestMethod]
        public void CreateDialogObject()
        {
            var pathMap = Path.Combine(PathBase, ResourcesGeneral.CampaignName, ResourcesGeneral.MapDialogOne);
            var dialogPath = Path.Combine(pathMap,
                Path.ChangeExtension(ResourcesGeneral.MapDialogOne, ResourcesGeneral.DialogObjectExt));

            FileHandleDelete.DeleteAllContents(pathMap, false);

            var ddialog = new List<DialogObject> {ResourcesGeneral.DialogStart};

            _dEdit.InitiateDialog(ddialog);

            Assert.IsFalse(_dEdit.DialogTree.IsNullOrEmpty(), "Dialog Object not correctly processed");

            _dEdit.SaveDialogObjects(dialogPath, _dEdit.DialogTree);

            Debug.WriteLine(dialogPath);

            Assert.IsTrue(File.Exists(dialogPath),
                string.Concat("Create Dialog Dictionary: ", dialogPath, Environment.NewLine));
            //Custom Test
            var cache = _dHandler.LoadCampaignDialogObjects(ResourcesGeneral.CampaignName,
                ResourcesGeneral.MapDialogOne,
                ResourcesGeneral.MapDialogOne);

            Assert.IsFalse(cache.IsNullOrEmpty(), "Loaded Dialog Dictionary");

            FileHandleDelete.DeleteCompleteFolder(dialogPath);
        }

        /// <summary>
        ///     Load Dialog Object
        ///     Change it, Save it to AutoSave
        ///     Load it from AutoSave
        ///     Uses MapNameNew: Test
        /// </summary>
        [TestMethod]
        public void LoadDialogObjectFromAutosave()
        {
            var pathMap = Path.Combine(PathBase, ResourcesGeneral.CampaignName, ResourcesGeneral.MapDialogTwo);
            var dialogPath = Path.Combine(pathMap,
                Path.ChangeExtension(ResourcesGeneral.MapDialogTwo, ResourcesGeneral.DialogObjectExt));

            var pathAutosave = Path.Combine(PathBase, ResourcesGeneral.CampaignName, ResourcesGeneral.AutoSave,
                ResourcesGeneral.MapDialogTwo,
                Path.ChangeExtension(ResourcesGeneral.MapDialogTwo, ResourcesGeneral.DialogObjectExt));

            FileHandleDelete.DeleteAllContents(pathAutosave, false);
            FileHandleDelete.DeleteAllContents(pathMap, false);

            var ddialog = new List<DialogObject> {ResourcesGeneral.DialogStart};

            _dHandler.InitiateDialog(ddialog);

            Assert.IsFalse(_dHandler.DialogTree.IsNullOrEmpty(), "Dialog Object not correctly processed");

            //Path should be empty
            _dHandler.SaveCampaignDialogObjects(ResourcesGeneral.CampaignName, ResourcesGeneral.MapDialogTwo,
                Path.ChangeExtension(ResourcesGeneral.MapDialogTwo, ResourcesGeneral.DialogObjectExt));

            Assert.IsFalse(File.Exists(pathAutosave), "Path in AutoSave existed");
            Assert.IsFalse(File.Exists(dialogPath), "Path in AutoSave does not exist");

            _dHandler.SetInactive(2);

            //Path should not be empty
            _dHandler.SaveCampaignDialogObjects(ResourcesGeneral.CampaignName, ResourcesGeneral.MapDialogTwo,
                Path.ChangeExtension(ResourcesGeneral.MapDialogTwo, ResourcesGeneral.DialogObjectExt));

            Assert.IsTrue(File.Exists(pathAutosave), "Path in  AutoSave does not exist");
        }
    }
}