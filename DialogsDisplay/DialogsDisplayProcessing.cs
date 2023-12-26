/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/DialogsDisplayProcessing.cs
 * PURPOSE:     Here we do all the hard work
 * PROGRAMER:   Peter Geinitz (Wayfarer) (Peter Geinitz)
 */

using System;
using System.Collections.Generic;
using CharacterEngine;
using Debugger;
using DialogEngine;
using ExtendedSystemObjects;
using Resources;
using ViewModel;

namespace DialogsDisplay
{
    /// <summary>
    ///     The dialogs display processing class.
    /// </summary>
    internal static class DialogsDisplayProcessing
    {
        /// <summary>
        ///     Interface of Dialog Engine that does all the work
        /// </summary>
        private static DialogCampaign _dlgEngine;

        /// <summary>
        ///     Interface of Dialog Engine that does all the work
        /// </summary>
        private static CampaignCharacterHandler _chrEngine;

        /// <summary>
        ///     The campaign name.
        /// </summary>
        private static string _campaignName;

        /// <summary>
        ///     The map name.
        /// </summary>
        private static string _mapName;

        /// <summary>
        ///     The dialog name.
        /// </summary>
        private static string _dialogName;

        /// <summary>
        ///     Selected Interaction
        /// </summary>
        public static ChoiceItem SelectedItem { get; set; }

        /// <summary>
        ///     Public Event for refreshing/filling the Images in DialogDisplayControl
        /// </summary>
        public static event EventHandler<ImagePath> ReloadImages;

        /// <summary>
        ///     Public Event for refreshing/filling the Dialog Choice Window
        /// </summary>
        public static event EventHandler<DlgDisplay> DialogChoice;

        /// <summary>
        ///     Public Event for refreshing/filling the Dialog Window
        /// </summary>
        public static event EventHandler<DlgLine> DialogText;

        /// <summary>
        ///     Public Event that will contain all Infos about the Character
        /// </summary>
        public static event EventHandler<CharInfo> PlayerDisplay;

        /// <summary>
        ///     Public Event that will contain all Infos about the Character
        /// </summary>
        public static event EventHandler<CharInfo> AntagonistDisplay;

        /// <summary>
        ///     Public Event for Closing the Dialog Windows
        /// </summary>
        public static event EventHandler DialogClose;

        /// <summary>
        ///     Load Dialog
        ///     Switches between Auto-save and Standard
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Name of the Dialog</param>
        internal static void StartDialog(string campaignName, string mapName, string dialogName)
        {
            if (string.IsNullOrEmpty(campaignName) || string.IsNullOrEmpty(mapName)) return;

            //Save for saving purposes
            _campaignName = campaignName;
            _mapName = mapName;
            _dialogName = dialogName;

            //Initiate Interfaces
            _dlgEngine = new DialogCampaign();
            _chrEngine = new CampaignCharacterHandler();
            _chrEngine.InitiateCampaign(campaignName);

            //Load Data
            var dialogObject = _dlgEngine.LoadCampaignDialogObjects(campaignName, mapName, dialogName);

            if (dialogObject.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(string.Concat(DialogsDisplayResources.ErrorDialogNotFound, dialogName),
                    ErCode.Error);
                dialogObject = GetDummyDialogObject();
            }

            //Start Processing
            _dlgEngine.InitiateDialog(dialogObject);
            _dlgEngine.StartDialog();

            //Load into the Window
            var dlgLine = new DlgLine {Line = _dlgEngine.DlgObject.BaseDialog.DialogLine};
            //only get Active Dialogs
            var dlgChoice = new DlgDisplay {DisplayDialog = _dlgEngine.GetChoiceDialog()};

            //Load Character Biography
            LoadBiography(_dlgEngine.DlgObject.BaseDialog.CharacterId);

            DialogChoice?.Invoke(null, dlgChoice);
            DialogText?.Invoke(null, dlgLine);

            // Load Background Image
            var image = _dlgEngine.DlgObject.BaseDialog.BackgroundImage;
            if (string.IsNullOrEmpty(image)) return;

            //Load Images
            var imgPath = new ImagePath {ImagePaths = image};
            ReloadImages?.Invoke(null, imgPath);
        }

        /// <summary>
        ///     Handle Input from ChoiceView
        ///     Use the appropriate route
        ///     Set Inactive if not repeatable
        /// </summary>
        internal static void HandleInput()
        {
            //check if Condition
            if (!CheckCondition(SelectedItem.ConditionId))
            {
                var dlgLine = new DlgLine {Line = DialogsDisplayResources.Condition};
                DialogText?.Invoke(null, dlgLine);
                return;
            }

            //check if Condition, Careful! if you have a follow Up with an Display Window Trigger the Close Dialog!
            if (SelectedItem.EventId != -1)
            {
                var id = new DialogInteractionEventArgs {EventId = SelectedItem.EventId};
                DialogInteraction.EventTrigger(id);
            }

            switch (SelectedItem.DialogHandler)
            {
                case ChoiceItem.DialogHandle.Follow:
                    var id = SelectedItem.SuccessorId;
                    var followup = _dlgEngine.ContinueDialog(id);
                    LoadBiography(followup.BaseDialog.CharacterId);
                    DisplayDialog(followup);
                    break;

                case ChoiceItem.DialogHandle.Circle:
                    LoadBiography(SelectedItem.CharacterId);
                    LoadBiography(SelectedItem.AntagonistId);
                    CircleDialog(SelectedItem);
                    break;

                case ChoiceItem.DialogHandle.Close:
                    CloseDialog();
                    break;

                case ChoiceItem.DialogHandle.None:
                    break;

                case ChoiceItem.DialogHandle.Inactive:
                    break;

                default:
                    return;
            }
        }

        /// <summary>
        ///     Checks if Condition exists and if it is fulfilled
        /// </summary>
        /// <param name="conditionId">Id of Condition</param>
        /// <returns>If Condition is fulfilled</returns>
        private static bool CheckCondition(int conditionId)
        {
            //TODO FIXME IMPLEMENT
            if (conditionId == 0) return true;

            return false;
        }

        /// <summary>
        ///     Display the Dialog
        /// </summary>
        /// <param name="followup">Follow up Item</param>
        private static void DisplayDialog(DialogDisplay followup)
        {
            if (followup == null)
            {
                CloseDialog();
                return;
            }

            //Display Choice + Follow Up Text of Dialog
            var displayText = string.Concat(SelectedItem.DialogLine, Environment.NewLine,
                followup.BaseDialog.DialogLine);

            var dlgLine = new DlgLine {Line = displayText};
            var dlgChoice = new DlgDisplay {DisplayDialog = followup.ChoiceDialog};
            var imgPath = new ImagePath {ImagePaths = followup.BaseDialog.BackgroundImage};

            //Load Text
            DialogChoice?.Invoke(null, dlgChoice);
            DialogText?.Invoke(null, dlgLine);
            ReloadImages?.Invoke(null, imgPath);

            if (!SelectedItem.IsRepeatable) _dlgEngine.SetInactive(SelectedItem.MasterId);
            //clean up Selected
            SelectedItem = null;
        }

        /// <summary>
        ///     Displays a Circle Dialog
        /// </summary>
        /// <param name="selectedItem">Selected Dialog Item</param>
        private static void CircleDialog(ChoiceItem selectedItem)
        {
            var displayText = string.Concat(selectedItem.DialogLine, Environment.NewLine,
                selectedItem.FollowUpDialog);

            var dlgLine = new DlgLine {Line = displayText};
            DialogText?.Invoke(null, dlgLine);

            if (!SelectedItem.IsRepeatable) _dlgEngine.SetInactive(SelectedItem.MasterId, selectedItem.ChildId);
            //clean up Selected
            SelectedItem = null;
        }

        /// <summary>
        ///     Handles Dialog Choice Base Dialog and Circle Dialog
        ///     Invoke The Character Details
        /// </summary>
        /// <param name="charId">Id of Character</param>
        private static void LoadBiography(int charId)
        {
            var bio = _chrEngine.LoadCharacter(charId);
            if (bio?.Bio == null) return;

            var chr = new CharInfo
            {
                Biography = bio.Bio.Biography,
                Image = bio.Bio.Image,
                Alignment = bio.Bio.Alignment,
                Faction = bio.Bio.Faction,
                Name = bio.Bio.Name,
                FullImage = bio.Bio.FullImage
            };

            if (bio.Bio.TypeId == CharacterBiography.TypeIds.Player)
                PlayerDisplay?.Invoke(null, chr);
            else
                AntagonistDisplay?.Invoke(null, chr);
        }

        /// <summary>
        ///     Dummy DialogObject
        ///     In case of an Error use this as an Replacement
        /// </summary>
        /// <returns>A quick Dialog that plays in case something is broken or empty</returns>
        private static List<DialogObject> GetDummyDialogObject()
        {
            return new()
            {
                DialogsDisplayResources.DialogStart,
                DialogsDisplayResources.DialogEnd
            };
        }

        /// <summary>
        ///     Close the Dialog
        ///     And save changed if we had some
        /// </summary>
        private static void CloseDialog()
        {
            _dlgEngine.SaveCampaignDialogObjects(_campaignName, _mapName, _dialogName);
            DialogClose?.Invoke(null, EventArgs.Empty);
        }
    }

    /// <summary>
    ///     Image Path
    /// </summary>
    internal sealed class ImagePath
    {
        /// <summary>
        ///     Gets or sets the image paths.
        /// </summary>
        internal string ImagePaths { get; init; }
    }

    /// <summary>
    ///     Text of the Dialog
    /// </summary>
    internal sealed class DlgLine
    {
        /// <summary>
        ///     Gets or sets the line.
        /// </summary>
        internal string Line { get; init; }
    }

    /// <summary>
    ///     The Dialog display class.
    /// </summary>
    internal sealed class DlgDisplay
    {
        /// <summary>
        ///     Gets or sets the display dialog.
        /// </summary>
        internal List<ChoiceItem> DisplayDialog { get; init; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Complete Object for Display Reasons
    /// </summary>
    public sealed class CharInfo : ObservableObject
    {
        /// <summary>
        ///     The alignment.
        /// </summary>
        private readonly CharacterBiography.Alignments _alignment;

        /// <summary>
        ///     The biography.
        /// </summary>
        private readonly string _biography;

        /// <summary>
        ///     The faction.
        /// </summary>
        private readonly string _faction;

        /// <summary>
        ///     The full image.
        /// </summary>
        private readonly string _fullImage;

        /// <summary>
        ///     The image.
        /// </summary>
        private readonly string _image;

        /// <summary>
        ///     The name.
        /// </summary>
        private readonly string _name;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => _name;
            init
            {
                _name = value;
                RaisePropertyChangedEvent(nameof(Name));
            }
        }

        /// <summary>
        ///     Gets or sets the image.
        /// </summary>
        public string Image
        {
            get => _image;
            init
            {
                _image = value;
                RaisePropertyChangedEvent(nameof(Image));
            }
        }

        /// <summary>
        ///     Gets or sets the biography.
        /// </summary>
        public string Biography
        {
            get => _biography;
            init
            {
                _biography = value;
                RaisePropertyChangedEvent(nameof(Biography));
            }
        }

        /// <summary>
        ///     Gets or sets the faction.
        /// </summary>
        public string Faction
        {
            get => _faction;
            init
            {
                _faction = value;
                RaisePropertyChangedEvent(nameof(Faction));
            }
        }

        /// <summary>
        ///     Gets or sets the alignment.
        /// </summary>
        public CharacterBiography.Alignments Alignment
        {
            get => _alignment;
            init
            {
                _alignment = value;
                RaisePropertyChangedEvent(nameof(Alignment));
            }
        }

        /// <summary>
        ///     Gets or sets the full image.
        /// </summary>
        public string FullImage
        {
            get => _fullImage;

            init
            {
                _fullImage = value;
                RaisePropertyChangedEvent(nameof(FullImage));
            }
        }
    }
}