/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/DialogsDisplayResources.cs
 * PURPOSE:     Resource File
 * PROGRAMER:   Peter Geinitz (Wayfarer) (Peter Geinitz)
 */

using Resources;

namespace DialogsDisplay
{
    /// <summary>
    ///     The dialogs display Resources class.
    /// </summary>
    internal static class DialogsDisplayResources
    {
        /// <summary>
        ///     The condition (const). Value: "You don't qualify for the Condition".
        /// </summary>
        internal const string Condition = "You don't qualify for the Condition";

        /// <summary>
        ///     with this we don't need any Directory Current Folder. Value:  @"Images\Portraits".
        /// </summary>
        internal const string ImageCharacterFolder = @"Images\Portraits";

        /// <summary>
        ///     The core path campaign (const). Value: "Content\Campaigns".
        /// </summary>
        internal const string CorePathCampaign = @"Content\Campaigns";

        /// <summary>
        ///     The error campaign name (const). Value: "CampaignName was not correct".
        /// </summary>
        internal const string ErrorCampaignName = "CampaignName was not correct";

        /// <summary>
        ///     The error map name (const). Value: "MapName was not correct".
        /// </summary>
        internal const string ErrorMapName = "MapName was not correct";

        /// <summary>
        ///     The error image not set (const). Value: "Image Name was not set".
        /// </summary>
        internal const string ErrorImageNotSet = "Image Name was not set";

        /// <summary>
        ///     The error dialog name (const). Value: "DialogName was not correct".
        /// </summary>
        internal const string ErrorDialogName = "DialogName was not correct";

        /// <summary>
        ///     The error dialog not Found (const). Value: "The Dialog was not found, Name: ".
        /// </summary>
        internal const string ErrorDialogNotFound = "The Dialog was not found, Name: ";

        /// <summary>
        ///     The dialog end (readonly). Value: new DialogObject { MasterId = 1, IsMaster = false, DialogLine = "You
        ///     went on", IsItemActive = true, IsRepeatable = false, IsEndPoint = true, CharacterId = -1, EventId = -1 }.
        /// </summary>
        internal static readonly DialogObject DialogEnd = new()
        {
            MasterId = 1,
            IsMaster = false,
            DialogLine = "You went on",
            IsItemActive = true,
            IsRepeatable = false,
            IsEndPoint = true,
            CharacterId = -1,
            EventId = -1
        };

        /// <summary>
        ///     The dialog start (readonly). Value: new DialogObject { MasterId = 1, IsMaster = true, DialogLine = "You
        ///     thought you heard something, but it was nothing", IsItemActive = true, IsRepeatable = false, CharacterId = -1,
        ///     EventId = -1 }.
        /// </summary>
        internal static readonly DialogObject DialogStart = new()
        {
            MasterId = 1,
            IsMaster = true,
            DialogLine = "You thought you heard something, but it was nothing",
            IsItemActive = true,
            IsRepeatable = false,
            CharacterId = -1,
            EventId = -1
        };
    }
}