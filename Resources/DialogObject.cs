/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/DialogObjects.cs
 * PURPOSE:     Dialog Object, for saving Data
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     Object only for saving
    ///     All Data in one, will be saved as List
    /// </summary>
    public sealed class DialogObject
    {
        /// <summary>
        ///     Links to the Id of the Master Dialog text
        ///     1 is Start Element
        /// </summary>
        public int MasterId { get; init; }

        /// <summary>
        ///     For future Reference:
        ///     Look Trade,
        ///     Talk
        ///     Interact
        /// </summary>
        public int DialogType { get; init; }

        /// <summary>
        ///     Master Object
        ///     Option Object
        /// </summary>
        public bool IsMaster { get; init; }

        /// <summary>
        ///     is path active?
        /// </summary>
        public bool IsItemActive { get; init; }

        /// <summary>
        ///     is dialog repeatable?
        /// </summary>
        public bool IsRepeatable { get; init; }

        /// <summary>
        ///     Get out of the Dialog
        /// </summary>
        public bool IsEndPoint { get; init; }

        /// <summary>
        ///     Played Dialog
        ///     Plan Text
        /// </summary>
        public string DialogLine { get; init; }

        /// <summary>
        ///     Complete Text or Dialog
        ///     One Liner Answer from the Character used for circuit Answers
        /// </summary>
        public string FollowUpDialog { get; init; }

        /// <summary>
        ///     Write your diary here. No one cares
        /// </summary>
        public string InternalRemarks { get; init; }

        /// <summary>
        ///     Needed for conditions and Image displays
        ///     -1 if not set
        /// </summary>
        public int CharacterId { get; init; }

        /// <summary>
        ///     Needed for conditions and Image displays
        ///     Only used in circular Dialog
        /// </summary>
        public int AntagonistId { get; init; }

        /// <summary>
        ///     Needed for conditions and Image displays
        /// </summary>
        public int EventId { get; init; }

        /// <summary>
        ///     Needed for conditions and Image displays
        /// </summary>
        public int ConditionId { get; init; }

        /// <summary>
        ///     Id of what follows
        /// </summary>
        public int SuccessorId { get; init; }

        /// <summary>
        ///     Level where the Dialog is Set, needed for the three
        /// </summary>
        public int Level { get; init; }

        /// <summary>
        ///     Level where the Dialog is Set, needed for the three
        /// </summary>
        public string BackGroundImage { get; init; }

        /// <summary>
        ///     Gets or sets the Child id.
        ///     Only used in Choice Dialogs as identifier
        /// </summary>
        public int ChildId { get; init; }
    }
}