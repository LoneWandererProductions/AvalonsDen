/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CampaignsHandler.cs
 * PURPOSE:     Interface that provides all functions needed
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CampaignDriver;
using InterOp;
using Debugger;
using EventEngine;
using GameEngine;
using Renderer;
using Resources;

// ReSharper disable RemoveRedundantBraces, we like Braces
// ReSharper disable SwitchStatementMissingSomeCases

namespace Campaigns
{
    /// <inheritdoc />
    /// <summary>
    ///     Handles the Complete Campaign Window
    /// </summary>
    public sealed class CampaignsHandler : ICampaignsHandler
    {
        /// <summary>
        ///     Center Menu Activated?
        /// </summary>
        private const bool Centerbutton = true;

        /// <summary>
        ///     Initiate Cell, logging
        /// </summary>
        private const bool Log = false;

        /// <summary>
        ///     Window for Game Display
        /// </summary>
        private static Campaign _myCpgn;

        /// <summary>
        ///     Module for Graphic
        /// </summary>
        private static RenderTile _renderTile;

        /// <summary>
        ///     Module that handles Events
        /// </summary>
        private static EventTypeDisplay _eventR;

        /// <summary>
        ///     Switch needed for left and right clicking
        /// </summary>
        private static bool _button;

        /// <summary>
        ///     Global Keyboard Hook
        /// </summary>
        private static InterOp.WinKeyStrokeListener _listener;

        /// <summary>
        ///     Load the Basics
        /// </summary>
        private static readonly MCursor Crs = new()
        {
            Background = CampaignsStringResource.IconBase,
            Idle = CampaignsStringResource.IconIdle
        };

        /// <summary>
        ///     Send our Message to the Subscribers
        /// </summary>
        public static EventHandler<string> FinishMessage { get; set; }

        /// <summary>
        ///     Handled Id, this one gets null?
        /// </summary>
        private static int ClickedId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Start a fresh Campaign
        /// </summary>
        /// <param name="manifest">Basic Informations about the Campaign</param>
        public void StartCampaign(CampaignManifest manifest)
        {
            try
            {
                //Initiate the Data and Party
                CampaignsProcessing.InitiateCampaign(manifest.StartMap, manifest.CampaignName, manifest.StartPoint,
                    manifest.CharacterId, manifest.Character);
            }
            catch (ArgumentException e)
            {
                //catch error
                DebugLog.CreateLogFile(e.ToString(), ErCode.Error, manifest);
                return;
            }

            //Initiate Game Logic
            InitiateGame(manifest.StartTime);
            //populate Window
            LoadWindow();
            //Load KeyboardListener
            KeyboardListener();
        }

        /// <inheritdoc />
        /// <summary>
        ///     We load the basic map
        /// </summary>
        /// <param name="saveInfos">Saved infos of Map</param>
        public void LoadCampaign(SaveInfos saveInfos)
        {
            //well duh should not happen
            if (saveInfos == null)
            {
                return;
            }

            //Initiate the Data
            try
            {
                //Initiate the Data and Party
                CampaignsProcessing.LoadSave(saveInfos);
            }
            catch (ArgumentException e)
            {
                //catch error
                DebugLog.CreateLogFile(e.ToString(), ErCode.Error, saveInfos);
                return;
            }

            //Initiate Game Logic
            InitiateGame(saveInfos.ActualTime);
            //populate Window
            LoadWindow();

            //Load KeyboardListener
            KeyboardListener();
        }

        /// <summary>
        ///     Create and Display the Map, Set Event Trigger
        /// </summary>
        private static void LoadWindow()
        {
            var myCell = LoadImageCore();

            //Show Campaign
            _myCpgn = new Campaign(myCell);

            //Set Start Character
            _renderTile.ChangeSingleGraphics(CampaignsRegister.CurrentPoint);

#if DEBUG
            _myCpgn.Topmost = true;
            _myCpgn.Show();

#endif

#if RELEASE
            _myCpgn.ShowDialog();
#endif
            CampaignsProcessing.StartSound();
        }

        /// <summary>
        ///     Initiate Window Keyboard Listener
        /// </summary>
        private static void KeyboardListener()
        {
            _listener = new WinKeyStrokeListener();
            _listener.OnKeyPressed += Listener_OnKeyPressed;
            _listener.HookKeyboard();
        }

        /// <summary>
        ///     Callback from the other Modules
        ///     Change the Displayed Map
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of new Map</param>
        internal static void ChangeMap(int idForFurtherEventInfo)
        {
            bool check;
            //make Map click able again
            _renderTile.SetClick();

            //Initiate the Data
            try
            {
                check = CampaignsProcessing.ExecuteMapChange(idForFurtherEventInfo);
            }
            catch (ArgumentException e)
            {
                //catch error
                DebugLog.CreateLogFile(e.ToString(), ErCode.Error);
                return;
            }

            //catch error
            if (!check)
            {
                return;
            }

            var myCell = LoadImageCore();

            //Show Campaign
            _myCpgn.ChangeCell(myCell);

            //Set Start Character
            _renderTile.ChangeSingleGraphics(CampaignsRegister.CurrentPoint);

            //Initiate all Data and Engines
            CampaignsProcessing.StartSound();

            //Initiate Campaign Events
            CampaignsInputHandler.LoadCampaignInputHandler(CampaignsRegister.MapName);
        }

        /// <summary>
        ///     Callback from the other Modules
        ///     Change the location of the Avatar on the current Map
        /// </summary>
        /// <param name="idForFurtherEventInfo"></param>
        internal static void ChangeLocation(int idForFurtherEventInfo)
        {
            _renderTile.DeleteSingleGraphics(CampaignsRegister.CurrentPoint);

            //Get Point and Convert to int, it is the CharacterId
            CampaignsRegister.SetPoints(idForFurtherEventInfo);
            //handle the graphic side
            _renderTile.ChangeSingleGraphics(CampaignsRegister.CurrentPoint);
        }

        /// <summary>
        ///     Shared between Load and Initiate Campaign
        ///     Basic Game Logic
        /// </summary>
        /// <param name="startTime">Time we are in</param>
        private static void InitiateGame(int startTime)
        {
            //Set Register of Time
            CampaignsRegister.SetActualTime(startTime);

            //Initiate Campaign Events
            CampaignsInputHandler.StartCampaignInputHandler(CampaignsRegister.MapName);
        }

        /// <summary>
        ///     Handle the Click on the Map
        ///     Get everything done, Set Event Display, Display and Events
        /// </summary>
        /// <param name="sender">Object that sends the Message</param>
        /// <param name="clickedventArgs">Event Type</param>
        private static void RenderTileOnImageClicked(object sender, EditorClickedEventArgs clickedventArgs)
        {
            //get Handler
            var cpn = HandlerInputSingleton.Create();

            _eventR = cpn.GetDisplay(CampaignsRegister.TileId(), clickedventArgs.ImagePoint,
                CampaignsResources.MovementCost);

            //No need to check further, Player just clicked something impassible and something with no action whatsoever, also catch a possible error
            if (_eventR == null || _eventR.Typ == CampaignsResources.BlockedAndNoEvent)
            {
                return;
            }

            //Wait for Input Mouse Left and Right for now
            switch (clickedventArgs.ClickType.ChangedButton)
            {
                case MouseButton.Left:
                {
                    _button = true;

                    if (_eventR.TravelPath)
                    {
                        Displaymovement(_eventR.PathTravel);
                    }

                    if (_eventR.Typ == CampaignsResources.UiDoDisplayChar)
                    {
                        ShowPlayerChoices(null);
                    }

                    if (!_eventR.TravelPath && _eventR.Typ != CampaignsResources.UiDoDisplayChar)
                    {
                        HandlePlayerInput();
                    }

                    break;
                }

                case MouseButton.Right:
                {
                    _button = false;

                    ClickedId = ShowPlayerChoices(CampaignsRegister.ConvertIdToCoordinate(_eventR.PathDisplay));

                    if (ClickedId == CampaignsResources.UiDoNothing)
                    {
                        return;
                    }

                    //Display some shit
                    //Do we need to Display something? Atm only 0,1,2, TODO Over-watch + Test
                    if (ClickedId >= CampaignsResources.Move)
                    {
                        if (_eventR.TravelPath)
                        {
                            Displaymovement(_eventR.PathTravel);
                        }
                        else
                        {
                            HandlePlayerInput();
                        }
                    }
                    else
                    {
                        CampaignsInputHandler.DisplayPlayerClick(ClickedId, _eventR, _button);
                    }

                    break;
                }
                default:
                    return;
            }
        }

        /// <summary>
        ///     Show Player Interaction menu
        /// </summary>
        /// <param name="pathlst">Display Path</param>
        /// <returns>Selected ACtion</returns>
        private static int ShowPlayerChoices(List<Coordinates> pathlst)
        {
            //if true Display Path
            if (_eventR.DisplayPath)
            {
                _renderTile.DisplaymovementPath(pathlst);
            }

            //Display Interaction and ask for feedback
            var item = CampaignsContextMenu.GenerateItems(_eventR.MyDisplayEventsTypes, _eventR.Typ);
            //clicked ourselves or another Place?
            var orientation = _eventR.Typ != CampaignsResources.UiDoDisplayChar;

            //Get Id from the Context Menu
            var id = _renderTile.DisplayMenu(item, orientation, Centerbutton);

            if (id == CampaignsResources.UiSaveMenu)
            {
                //Get most Data from the Registry
                CampaignsContextMenu.GenerateSaveFile();
                return CampaignsResources.UiDoNothing;
            }

            //Convert Id
            id = CampaignsContextMenu.ConvertToId(_eventR.MyDisplayEventsTypes, id);

            //if it was true delete the Path, cleanup Path
            if (_eventR.DisplayPath)
            {
                _renderTile.DeleteMultipleGraphics(pathlst);
            }

            return id;
        }

        /// <summary>
        ///     Handle all Player Input
        ///     Even if we don't move this Method is called, this will be the last step of every Player Interaction
        ///     Handles all Graphical Interactions on the Map
        ///     First Switch
        /// </summary>
        private static void HandlePlayerInput()
        {
            //do we need to do something? well tough luck, do something in movementEngine
            if (!_eventR.DoSomething)
            {
                return;
            }

            //here we display the Traps, only one active at a Time
            if (_eventR.Typ == CampaignsResources.TrapType)
            {
                //Loop though all Trap Events!
                foreach (var traps in _eventR.MyEventsTypes)
                {
                    CampaignsInputHandler.DisplayChoices(traps);
                }

                return;
            }

            //Only a move no special Events return! TODO hack
            if (_eventR.MyDisplayEventsTypes == null)
            {
                return;
            }

            /*
            * First Switch
            * Here we Handle Map changes
            * First Switch for all graphical Events on the Map
            */
            switch (_eventR.MyDisplayEventsTypes.First().Value.TypeOfEvent)
            {
                case EventType.TypeOfEvents.MapChange:
                    //make Map click able again
                    ChangeMap(_eventR.MyDisplayEventsTypes.First().Value.IdForFurtherEventInfo);
                    break;

                case EventType.TypeOfEvents.LocationChange:
                    //none yet
                    break;

                default:
                    CampaignsInputHandler.DisplayPlayerClick(
                        ClickedId > CampaignsResources.Move ? ClickedId : _eventR.Typ, _eventR, _button);
                    break;
            }

            //Finally Calculate the Costs
            CampaignsInputHandler.SetActionPoints(_eventR.MoveRange);
        }

        /// <summary>
        ///     Core work on the Image
        /// </summary>
        /// <returns>FInished Cell</returns>
        private static Cells LoadImageCore()
        {
            //generate new Map
            _renderTile = new RenderTile(CampaignsRegister.MapList,
                CampaignsRegister.TransitionDictionary,
                CampaignsRegister.MasterTileDictionary,
                CampaignsRegister.Height,
                CampaignsRegister.Length,
                CampaignsRegister.BackroundImage,
                Log,
                //Player Layer defines the Max Layer, he will be the open Limit, Renderer adds two extra Layers for possible fancy Stuff
                CampaignsRegister.CurrentPoint.ZLayer,
                Crs);

            //Get the Triggers in Place
            _renderTile.RaiseAnimationFinished += RenderTileOnRaiseAnimationFinished;
            _renderTile.ImageClicked += RenderTileOnImageClicked;

            return _renderTile.GenerateMap();
        }

        /// <summary>
        ///     Callback from the other Module: InputHandler, Trigger Movement Animation
        ///     Displays the Movement Path
        /// </summary>
        /// <param name="myPath">Movement Path as List Coordinates</param>
        private static void Displaymovement(IReadOnlyCollection<int> myPath)
        {
            _renderTile.DisplaymovementAnimation(CampaignsRegister.ConvertIdToCoordinate(myPath),
                CampaignsRegister.ImageId, CampaignsResources.Timer);
            CampaignsRegister.SetPoints(myPath.Last());
        }

        /// <summary>
        ///     We had an Animation wait until it is finished so we can unlock the Main Window
        ///     After the animation we present the Player with the results of his action
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private static void RenderTileOnRaiseAnimationFinished(object sender, EventArgs e)
        {
            HandlePlayerInput();
        }

        /// <summary>
        ///     Listen to specific Key Stroke
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The pressed Key.</param>
        private static void Listener_OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.KeyPressed == Key.F3)
            {
                Prompts.Initiate(_renderTile);
            }
        }

        /// <summary>
        ///     Cleanup on Close
        /// </summary>
        internal static void CloseCampaign()
        {
            SaveGame.Cleanup(CampaignsRegister.CampaignName);
            MusicEngine.StopMusic();
            _listener.UnHookKeyboard();
            FinishMessage?.Invoke(null, string.Empty);
        }
    }
}