/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CampaignStrings.cs
 * PURPOSE:     Simple Collection of all Strings for cleaner Code
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;

namespace Campaigns
{
    /// <summary>
    ///     The campaigns resources class.
    /// </summary>
    internal static class CampaignsResources
    {
        /// <summary>
        ///     TODO FIXME IMPLEMENT
        /// </summary>
        internal const int MovementCost = 3;

        /// <summary>
        ///     TODO FIXME IMPLEMENT
        /// </summary>
        internal const int DayCycle = 10;

        /// <summary>
        ///     TODO FIXME IMPLEMENT
        /// </summary>
        internal const int Year = 365;

        //Path

        /// <summary>
        ///     The core sounds (const). Value: @"\Core\Sounds\".
        /// </summary>
        internal const string CoreSounds = @"\Core\Sounds\";

        /// <summary>
        ///     The core Images (const). Value: @"\Core\Images\Items".
        /// </summary>
        internal const string CoreImages = @"Core\Images\Items";

        /// <summary>
        ///     The prompt campaigns (const). Value: "Debugging prompt, for debug use only might break stuff badly".
        /// </summary>
        internal const string PromptCampaigns = "Debugging prompt, for debug use only might break stuff badly";

        /// <summary>
        ///     The error prompt (const). Value: "Prompt was called with incorrect Number, Register seems to be damaged".
        /// </summary>
        internal const string ErrorPrompt = "Prompt was called with incorrect Number, Register seems to be damaged";

        /// <summary>
        ///     The error could not load data (const). Value: "Could not load Map data".
        /// </summary>
        internal const string ErrorCouldNotLoadData = "Could not load Map data: ";

        /// <summary>
        ///     The Error could not load Inventory (const). Value: "Could not load saved Inventory and we could not even load the
        ///     Master Inventory.
        /// </summary>
        internal const string ErrorCouldNotLoadInventory =
            "Could not load saved Inventory and we could not even load the Master Inventory.";

        /// <summary>
        ///     The error unhandled event type (const). Value: "Could not handle the Event Type".
        /// </summary>
        internal const string ErrorUnhandledEventType = "Could not handle the Event Type";

        /// <summary>
        ///     The error could not get coordinate (const). Value: "Could not get Coordinate".
        /// </summary>
        internal const string ErrorCouldNotGetCoordinate = "Could not get Coordinate";

        /// <summary>
        ///     The error no valid map name (const). Value: "No valid Map Name was provided by:".
        /// </summary>
        internal const string ErrorNoValidMapName = "No valid Map Name was provided by: ";

        /// <summary>
        ///     The error no valid campaign name (const). Value: "No valid Campaign Name was provided by:".
        /// </summary>
        internal const string ErrorNoValidCampaignName = "No valid Campaign Name was provided by: ";

        /// <summary>
        ///     The error initiate movement (const). Value: "Could not Initiate Movement Engine".
        /// </summary>
        internal const string ErrorInitiateMovement = "Could not Initiate Movement Engine";

        /// <summary>
        ///     The warning dialog could not be loaded (const). Value: "Error Dialog could not be loaded".
        /// </summary>
        internal const string WarningDialogCouldNotBeLoaded = "Error Dialog could not be loaded";

        /// <summary>
        ///     The error number (const). Value: -1.
        /// </summary>
        internal const int ErrorNumber = -1;

        /// <summary>
        ///     The Information could not load Inventory (const). Value: "Could not load Inventory".
        /// </summary>
        internal const string InformationCouldNotLoadInventory = "Could not load Inventory.";

        /// <summary>
        ///     The information map changed (const). Value: "Was Map changed: ".
        /// </summary>
        internal const string InformationMapChanged = "Was Map changed: ";

        /// <summary>
        ///     The information id of map (const). Value: "Id of Map: ".
        /// </summary>
        internal const string InformationIdofMap = "Id of Map: ";

        /// <summary>
        ///     The information map name (const). Value: "Name of Map: ".
        /// </summary>
        internal const string InformationMapName = "Name of Map: ";

        /// <summary>
        ///     The method name execute map change (const). Value: "ExecuteMapChange(idForFurtherEventInfo, Collection&lt;
        ///     eventType&gt;)".
        /// </summary>
        internal const string MethodNameExecuteMapChange =
            "ExecuteMapChange(idForFurtherEventInfo, Collection<eventType>)";

        /// <summary>
        ///     The method name load save (const). Value: "LoadSave(SaveInfos save)".
        /// </summary>
        internal const string MethodNameLoadSave = "LoadSave(SaveInfos save)";

        /// <summary>
        ///     The method name initiate map (const). Value: "InitiateMap(map Name, string campaignName)".
        /// </summary>
        internal const string MethodNameInitiateMap = "InitiateMap(map Name, string campaignName)";

        //Icons
        /// <summary>
        ///     The icon move (const). Value: "move.png".
        /// </summary>
        internal const string IconMove = "move.png";

        /// <summary>
        ///     The icon char (const). Value: "char.png".
        /// </summary>
        internal const string IconChar = "char.png";

        /// <summary>
        ///     The icon inventory (const). Value: "inventory.png".
        /// </summary>
        internal const string IconInventory = "inventory.png";

        /// <summary>
        ///     The icon log (const). Value: "log.png".
        /// </summary>
        internal const string IconLog = "log.png";

        /// <summary>
        ///     The icon interact (const). Value: "interact.png".
        /// </summary>
        internal const string IconInteract = "interact.png";

        /// <summary>
        ///     The icon trade (const). Value: "trade.png".
        /// </summary>
        internal const string IconTrade = "trade.png";

        /// <summary>
        ///     The icon talk (const). Value: "talk.png".
        /// </summary>
        internal const string IconTalk = "talk.png";

        /// <summary>
        ///     The icon fight (const). Value: "fight.png".
        /// </summary>
        internal const string IconFight = "fight.png";

        /// <summary>
        ///     The icon look (const). Value: "look.png".
        /// </summary>
        internal const string IconLook = "look.png";

        /// <summary>
        ///     Timer for Animation
        /// </summary>
        internal const int Timer = 200;

        /// <summary>
        ///     The UI do move (const). Value: -1.
        /// </summary>
        internal const int UiDoMove = -1;

        /// <summary>
        ///     The UI do nothing (const). Value: -7.
        /// </summary>
        internal const int UiDoNothing = -7;

        /// <summary>
        ///     The UI do display char (const). Value: -6.
        /// </summary>
        internal const int UiDoDisplayChar = -6;

        /// <summary>
        ///     The UI do display inventory (const). Value: -5.
        /// </summary>
        internal const int UiDoDisplayInventory = -5;

        /// <summary>
        ///     The UI do display log (const). Value: -4.
        /// </summary>
        internal const int UiDoDisplayLog = -4;

        /// <summary>
        ///     The UI save menu (const). Value: 6.
        /// </summary>
        internal const int UiSaveMenu = 6;

        //Types of Movement
        /// <summary>
        ///     The move (const). Value: 0.
        /// </summary>
        internal const int Move = 0;

        /// <summary>
        ///     The trap type (const). Value: 1.
        /// </summary>
        internal const int TrapType = 1;

        /// <summary>
        ///     The blocked and no event (const). Value: 3.
        /// </summary>
        internal const int BlockedAndNoEvent = 3;

        /// <summary>
        ///     The error event engine rollback (const). Value: "Could not Initiate EventEngine, Rollback initiated".
        /// </summary>
        internal const string ErrorEventEngineRollback = "Could not Initiate EventEngine, Rollback initiated";

        /// <summary>
        ///     The error could not load Item from Database (const). Value: "Could not load Items from Database".
        /// </summary>
        internal const string ErrorCouldNotItemData = "Could not load Items from Database";

        /// <summary>
        ///     Save State (const). Value: false.
        /// </summary>
        internal const bool SaveStateFalse = false;

        /// <summary>
        ///     The prompt done (readonly). Value: string.Concat("Command executed", Environment.NewLine).
        /// </summary>
        internal static readonly string PromptDone = string.Concat("Command executed", Environment.NewLine);
    }
}