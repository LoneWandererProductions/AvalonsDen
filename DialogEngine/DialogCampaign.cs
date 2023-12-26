using System.Collections.Generic;
using System.Linq;
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
    public sealed class DialogCampaign : IDialogCampaign
    {
        /// <summary>
        ///     The dialog changed.
        /// </summary>
        private bool _dialogChanged;

        /// <summary>
        ///     Gets the Dialog object.
        /// </summary>
        public DialogDisplay DlgObject { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the dialog tree.
        /// </summary>
        public Dictionary<int, DialogDisplay> DialogTree { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign Only
        ///     1 is always the Start Element
        ///     Must always be a DialogText
        /// </summary>
        /// <returns>Information for Display</returns>
        public DialogDisplay StartDialog()
        {
            DlgObject = DialogProcessing.GetStartDialog(DialogTree);
            return DlgObject;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign Only
        ///     Continues the Dialog
        /// </summary>
        /// <param name="id">Selected Choice If -1 just another Dialog String</param>
        /// <returns>Next Display</returns>
        public DialogDisplay ContinueDialog(int id)
        {
            return DialogProcessing.GetDialog(DialogTree, id);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign, Editor use
        /// </summary>
        /// <param name="dialog">Standard Dialog Object</param>
        public void InitiateDialog(List<DialogObject> dialog)
        {
            _dialogChanged = false;
            DialogTree = DialogProcessing.CollectDialogs(dialog);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load the Dialog
        ///     Campaign only
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Name of the Dialog</param>
        /// <returns>Dialog Object</returns>
        public List<DialogObject> LoadCampaignDialogObjects(string campaignName, string mapName, string dialogName)
        {
            return DialogProcessing.LoadDialogObject(campaignName, mapName,
                dialogName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Saves the Dialog in Campaign Mode,
        ///     if we had changed we will Save the File in Autosave, if not we do nothing
        /// </summary>
        /// <param name="dialogName">Name of the Dialog</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        public void SaveCampaignDialogObjects(string campaignName, string mapName, string dialogName)
        {
            if (!_dialogChanged) return;

            var dialog = DialogProcessing.DialogConvertToSave(DialogTree);
            DialogProcessing.SaveDialog(dialog, campaignName, mapName, dialogName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign Only
        ///     Set specific Event inactive
        /// </summary>
        /// <param name="id">The Dialog id.</param>
        public void SetInactive(int id)
        {
            _dialogChanged = true;
            var tree = DialogProcessing.SetDialogStatusBaseDialog(DialogTree, id, false);
            if (tree == null) return;

            DialogTree = new Dictionary<int, DialogDisplay>(tree);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Set the inactive.
        /// </summary>
        /// <param name="id">The masterId.</param>
        /// <param name="childId">The Choice child Id.</param>
        public void SetInactive(int id, int childId)
        {
            _dialogChanged = true;
            var tree = DialogProcessing.SetDialogStatusChoiceDialog(DialogTree, id, childId, false);
            if (tree == null) return;

            DialogTree = new Dictionary<int, DialogDisplay>(tree);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Campaign Only
        ///     Set specific Dialog active
        /// </summary>
        /// <param name="id">The Dialog id.</param>
        public void SetActive(int id)
        {
            _dialogChanged = true;
            var tree = DialogProcessing.SetDialogStatusBaseDialog(DialogTree, id, true);
            if (tree == null) return;

            DialogTree = new Dictionary<int, DialogDisplay>(tree);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Set the I active.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="childId">The Choice child Id.</param>
        public void SetIActive(int id, int childId)
        {
            _dialogChanged = true;
            var tree = DialogProcessing.SetDialogStatusChoiceDialog(DialogTree, id, childId, true);
            if (tree == null) return;

            DialogTree = new Dictionary<int, DialogDisplay>(tree);
        }

        /// <summary>
        ///     Get the choice dialog.
        /// </summary>
        /// <returns>The Active Choices <see cref="T:List{ChoiceItem}" />.</returns>
        public List<ChoiceItem> GetChoiceDialog()
        {
            return DlgObject.ChoiceDialog.Where(choice => choice.IsItemactive).ToList();
        }
    }
}