/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogEngine/DialogDisplay.cs
 * PURPOSE:     Result that we present the Player, with Data binding Integration
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using ViewModel;

namespace DialogEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     Container that holds basic Dialog and all Choices
    ///     ObservableObject
    /// </summary>
    public sealed class DialogDisplay : ObservableObject
    {
        /// <summary>
        ///     Gets or sets the base dialog. Observable Collection
        /// </summary>
        public DialogItem BaseDialog { get; set; } = new();

        /// <summary>
        ///     Gets or sets the choice dialog. Observable Collection
        /// </summary>
        public List<ChoiceItem> ChoiceDialog { get; set; } = new();
    }

    /// <inheritdoc />
    /// <summary>
    ///     Base Dialog
    /// </summary>
    public sealed class DialogItem : ObservableObject
    {
        /// <summary>
        ///     The background image.
        /// </summary>
        private string _backgroundImage;

        /// <summary>
        ///     The character id.
        /// </summary>
        private int _characterId;

        /// <summary>
        ///     The dialog line.
        /// </summary>
        private string _dialogLine;

        /// <summary>
        ///     The dialog type.
        /// </summary>
        private int _dialogType;

        /// <summary>
        ///     Internal remarks dialog item.
        /// </summary>
        private string _internalRemarksDialogItem;

        /// <summary>
        ///     Is item active.
        /// </summary>
        private bool _isItemactive;

        /// <summary>
        ///     Is repeatable.
        /// </summary>
        private bool _isRepeatable;

        /// <summary>
        ///     His own Id
        ///     Order in the graph by Numbers
        ///     No need for Data bind
        /// </summary>
        public int MasterId { get; set; }

        /// <summary>
        ///     For future Reference:
        ///     Look Trade,
        ///     Talk
        ///     Interact
        /// </summary>
        public int DialogType
        {
            get => _dialogType;
            set
            {
                _dialogType = value;
                RaisePropertyChangedEvent(nameof(DialogType));
            }
        }

        /// <summary>
        ///     Id of Character
        /// </summary>
        public int CharacterId
        {
            get => _characterId;
            set
            {
                _characterId = value;
                RaisePropertyChangedEvent(nameof(CharacterId));
            }
        }

        /// <summary>
        ///     Level where the Dialog is Set, needed for the three
        ///     No need for Data bind
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///     Complete Text or Dialog
        /// </summary>
        public string DialogLine
        {
            get => _dialogLine;
            set
            {
                _dialogLine = value;
                RaisePropertyChangedEvent(nameof(DialogLine));
            }
        }

        /// <summary>
        ///     Write your diary here. No one cares
        /// </summary>
        public string InternalRemarksDialogItem
        {
            get => _internalRemarksDialogItem;
            set
            {
                _internalRemarksDialogItem = value;
                RaisePropertyChangedEvent(nameof(InternalRemarksDialogItem));
            }
        }

        /// <summary>
        ///     is path active?
        /// </summary>
        public bool IsItemactive
        {
            get => _isItemactive;
            set
            {
                _isItemactive = value;
                RaisePropertyChangedEvent(nameof(IsItemactive));
            }
        }

        /// <summary>
        ///     is dialog repeatable?
        /// </summary>
        public bool IsRepeatable
        {
            get => _isRepeatable;
            set
            {
                _isRepeatable = value;
                RaisePropertyChangedEvent(nameof(IsRepeatable));
            }
        }

        /// <summary>
        ///     Master Object
        ///     Option Object
        ///     No need for Data bind
        /// </summary>
        public bool IsMaster { internal get; set; }

        /// <summary>
        ///     Background Image
        /// </summary>
        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                RaisePropertyChangedEvent(nameof(BackgroundImage));
            }
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Object that holds the Dialog Options
    ///     ObservableObject
    /// </summary>
    public sealed class ChoiceItem : ObservableObject
    {
        /// <summary>
        ///     The dialog handle enum.
        /// </summary>
        public enum DialogHandle
        {
            /// <summary>
            ///     None.
            /// </summary>
            None,

            /// <summary>
            ///     Close Dialog.
            /// </summary>
            Close,

            /// <summary>
            ///     Circle Dialog.
            /// </summary>
            Circle,

            /// <summary>
            ///     Dialog Inactive.
            /// </summary>
            Inactive,

            /// <summary>
            ///     Dialog Follow up.
            /// </summary>
            Follow
        }

        /// <summary>
        ///     The antagonist id.
        /// </summary>
        private int _antagonistId;

        /// <summary>
        ///     The character id.
        /// </summary>
        private int _characterId;

        /// <summary>
        ///     The condition id.
        /// </summary>
        private int _conditionId;

        /// <summary>
        ///     The dialog line.
        /// </summary>
        private string _dialogLine;

        /// <summary>
        ///     The event id.
        /// </summary>
        private int _eventId;

        /// <summary>
        ///     The follow up dialog.
        /// </summary>
        private string _followUpDialog;

        /// <summary>
        ///     Internal remarks choice item.
        /// </summary>
        private string _internalRemarksChoiceItem;

        /// <summary>
        ///     Is it the end point.
        /// </summary>
        private bool _isEndPoint;

        /// <summary>
        ///     Is item active.
        /// </summary>
        private bool _isItemactive;

        /// <summary>
        ///     Is repeatable.
        /// </summary>
        private bool _isRepeatable;

        /// <summary>
        ///     The successor id.
        /// </summary>
        private int _successorId;

        /// <summary>
        ///     Gets the dialog handler.
        /// </summary>
        public DialogHandle DialogHandler => CalculateDialogHandle();

        /// <summary>
        ///     His own Id
        ///     No need for DataBind
        /// </summary>
        public int MasterId { get; set; }

        /// <summary>
        ///     His own Id
        ///     No need for DataBind
        ///     Will help to identify child Choices
        /// </summary>
        public int ChildId { get; set; }

        /// <summary>
        ///     Id of Character
        /// </summary>
        public int CharacterId
        {
            get => _characterId;
            set
            {
                _characterId = value;
                RaisePropertyChangedEvent(nameof(CharacterId));
            }
        }

        /// <summary>
        ///     Id of Antagonist
        ///     Only used in circular Dialog
        /// </summary>
        public int AntagonistId
        {
            get => _antagonistId;
            set
            {
                _antagonistId = value;
                RaisePropertyChangedEvent(nameof(AntagonistId));
            }
        }

        /// <summary>
        ///     Id for Conditions yet to be implemented
        /// </summary>
        public int ConditionId
        {
            get => _conditionId;
            set
            {
                _conditionId = value;
                RaisePropertyChangedEvent(nameof(ConditionId));
            }
        }

        /// <summary>
        ///     Id for Events yet to be implemented
        /// </summary>
        public int EventId
        {
            get => _eventId;
            set
            {
                _eventId = value;
                RaisePropertyChangedEvent(nameof(EventId));
            }
        }

        /// <summary>
        ///     Id of what follows
        /// </summary>
        public int SuccessorId
        {
            get => _successorId;
            set
            {
                _successorId = value;
                RaisePropertyChangedEvent(nameof(SuccessorId));
            }
        }

        /// <summary>
        ///     Complete Text or Dialog
        /// </summary>
        public string DialogLine
        {
            get => _dialogLine;
            set
            {
                _dialogLine = value;
                RaisePropertyChangedEvent(nameof(DialogLine));
            }
        }

        /// <summary>
        ///     Complete Text or Dialog
        ///     One Liner Answer from the Character used for circuit Answers
        /// </summary>
        public string FollowUpDialog
        {
            get => _followUpDialog;
            set
            {
                _followUpDialog = value;
                RaisePropertyChangedEvent(nameof(FollowUpDialog));
            }
        }

        /// <summary>
        ///     Write your diary here. No one cares
        /// </summary>
        public string InternalRemarksChoiceItem
        {
            get => _internalRemarksChoiceItem;
            set
            {
                _internalRemarksChoiceItem = value;
                RaisePropertyChangedEvent(nameof(InternalRemarksChoiceItem));
            }
        }

        /// <summary>
        ///     Get out of the Dialog
        /// </summary>
        public bool IsEndPoint
        {
            get => _isEndPoint;
            set
            {
                _isEndPoint = value;
                RaisePropertyChangedEvent(nameof(IsEndPoint));
            }
        }

        /// <summary>
        ///     is dialog repeatable?
        /// </summary>
        public bool IsRepeatable
        {
            get => _isRepeatable;
            set
            {
                _isRepeatable = value;
                RaisePropertyChangedEvent(nameof(IsRepeatable));
            }
        }

        /// <summary>
        ///     Master Object
        ///     Option Object
        ///     No need for Data bind
        ///     False if Option
        ///     True if Master
        /// </summary>
        public bool IsMaster { internal get; set; }

        /// <summary>
        ///     Will be shown, but won't do anything on click
        /// </summary>
        public bool IsItemactive
        {
            get => _isItemactive;
            set
            {
                _isItemactive = value;
                RaisePropertyChangedEvent(nameof(IsItemactive));
            }
        }

        /// <summary>
        ///     Calculate the dialog handle.
        /// </summary>
        /// <returns>The Dialog Type as<see cref="DialogHandle" />.</returns>
        private DialogHandle CalculateDialogHandle()
        {
            //Inactive Dialog
            if (!_isItemactive) return DialogHandle.Inactive;

            //Close Dialog
            if (_isEndPoint) return DialogHandle.Close;

            //Check if it is a Circle Dialog
            if (MasterId == SuccessorId) return DialogHandle.Circle;

            //Follow Up
            return MasterId != SuccessorId ? DialogHandle.Follow : DialogHandle.None;
        }
    }
}