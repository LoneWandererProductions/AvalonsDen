/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/EditorHandleGraphics..cs
 * PURPOSE:     Handles all Graphic Interactions in Editor
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using AvalonRuntime;
using CommonControls;
using Debugger;
using EditorMap;
using FileHandler;
using MapGenerator;
using Renderer;
using Resources;
using TransitionEngine;

namespace Editors
{
    /// <summary>
    ///     The editor handle graphics class.
    /// </summary>
    internal static class EditorHandleGraphics
    {
        /// <summary>
        ///     Initiate Cell, logging needed for the Editor
        /// </summary>
        private const bool EditorMode = true;

        /// <summary>
        ///     Initiate Cell, Player Layer not used in Editor, so it is null
        /// </summary>
        private const int Player = 0;

        /// <summary>
        ///     Orientation, clockwise
        /// </summary>
        private const bool Orientation = true;

        /// <summary>
        ///     Center Menu Activated?
        /// </summary>
        private const bool CenterButton = false;

        /// <summary>
        ///     Used for Simple Tile Display
        /// </summary>
        private static RenderTile _renderTile;

        /// <summary>
        ///     Used for Transitions
        /// </summary>
        private static TransitionGenerate _transGenerate;

        /// <summary>
        ///     Internal workings, can't be in lined
        /// </summary>
        private static int _selectedTile;

        /// <summary>
        ///     Load the Basics
        /// </summary>
        private static readonly MCursor Crs = new()
        {
            Background = EditorStringResource.IconBase,
            Idle = EditorStringResource.IconIdle
        };

        /// <summary>
        ///     match Return Values
        /// </summary>
        private static List<int> _menuItems;

        /// <summary>
        ///     Load the map.
        /// </summary>
        /// <returns>The Image Control<see cref="UIElement" />.</returns>
        internal static UIElement LoadMap()
        {
            var pathObj = FileIoHandler.HandleFileOpen(EditorResources.MapDialog,
                Path.Combine(Directory.GetCurrentDirectory(), EditorResources.CampaignsFolder));

            //null value check
            if (pathObj == null) return null;
            //else load Map
            EditorRegister.LoadAllData(pathObj.FilePath);

            _transGenerate = new TransitionGenerate(EditorRegister.MapObjct.Height, EditorRegister.MapObjct.Length,
                EditorRegister.TileDct,
                EditorRegister.TransitionDct);

            //build the View
            _renderTile = new RenderTile(EditorRegister.MapObjct.MapList, EditorRegister.TransitionDct,
                EditorRegister.TileDct,
                EditorRegister.MapObjct.Height, EditorRegister.MapObjct.Length,
                EditorRegister.MapObjct.BackGroundImage, EditorMode, Player, Crs);

            var myCell = _renderTile.GenerateMap();

            //Get the Triggers in Place
            _renderTile.ImageClicked += RenderTileOnImageClicked;
            EditorTileToolBox.ItemSelectedItem += EditorTileToolBoxOnItemSelectedItem;

            // Get Markers for Event in Place
            var eventList = EditorEventHandler.RebuildMap(_renderTile.GetMaxLayers());

            _renderTile.ChangeMultipleGraphics(eventList);

            return myCell;
        }

        /// <summary>
        ///     The new map.
        /// </summary>
        /// <param name="map">The map data.</param>
        /// <returns>The map as<see cref="UIElement" />.</returns>
        internal static UIElement NewMap(EventArgsMap map)
        {
            _transGenerate = new TransitionGenerate(map.Height, map.Length, EditorRegister.TileDct);

            //build the View
            _renderTile = new RenderTile(EditorRegister.TileDct, map.Height,
                map.Length, EditorMode, Player, Crs);

            var myCell = _renderTile.GenerateMap();

            //Get the Triggers in Place
            _renderTile.ImageClicked += RenderTileOnImageClicked;
            EditorTileToolBox.ItemSelectedItem += EditorTileToolBoxOnItemSelectedItem;

            // Initiate the EventHandler
            EditorEventHandler.BuildNewMap(map.Length);

            return myCell;
        }

        /// <summary>
        ///     Save Changes to the actual Map
        /// </summary>
        internal static void SaveMap()
        {
            var pathObj = FileIoHandler.HandleFileSave(EditorResources.MapDialog,
                Path.Combine(Directory.GetCurrentDirectory(), EditorResources.CampaignsFolder));
            //Empty Return Value
            if (pathObj == null) return;

            //Creates Save Map and Border Map
            var mapE = new EditorMapEngine();

            EditorRegister.SetMapObject(mapE.ChangeMap(_renderTile.GetChangeList(), EditorRegister.MapObjct,
                EditorRegister.BorderDct, EditorRegister.TileDct));

            //Save the Event Map
            EditorRegister.SaveAllData(pathObj.FilePath);
            //Save Transition Map
            _transGenerate.SaveTransition(PathInformation.GetPathWithoutExtension(pathObj.FilePath));
        }

        /// <summary>
        ///     Show Grid on the Image Control
        /// </summary>
        internal static void ShowGrid()
        {
            _renderTile?.ShowGrid();
        }

        /// <summary>
        ///     Show Numbers on the Image Control
        /// </summary>
        public static void ShowNumbers()
        {
            _renderTile?.ShowNumbers();
        }

        /// <summary>
        ///     Load Background Image
        /// </summary>
        /// <param name="backgroundImage">Full Path to the Image</param>
        internal static void LoadImageBackground(string backgroundImage)
        {
            _renderTile?.LoadImageBackround(string.Empty, backgroundImage);
        }

        /// <summary>
        ///     Selected Tile
        /// </summary>
        /// <param name="sender">Tile Editor Cell</param>
        /// <param name="boxClickedEventArgs">Infos about clicked Tile</param>
        private static void EditorTileToolBoxOnItemSelectedItem(object sender, BoxClickedEventArgs boxClickedEventArgs)
        {
            _selectedTile = boxClickedEventArgs.TileId;
        }

        /// <summary>
        ///     Handle all the clicks pal
        /// </summary>
        /// <param name="sender">Cells Control</param>
        /// <param name="e">Information about clicked Cell</param>
        private static void RenderTileOnImageClicked(object sender, EditorClickedEventArgs e)
        {
            //Get Coordinate
            var item = ArtShared.IdToCoordinate(e.ImagePoint, EditorRegister.MapObjct.Length);
            item.TileId = _selectedTile;

            if (EditorRegister.TileDct.ContainsKey(_selectedTile))
                item.ZLayer = EditorRegister.TileDct[_selectedTile].Layer;

            HandleClickedImage(item, e.ClickType.ChangedButton, e.ImagePoint);
        }

        /// <summary>
        ///     New Handler for clicked Event of Toolbox
        ///     We Load the map into Cells
        ///     Later we start a new List to collect changes
        ///     ClickEvent Converts Tile into a new Type
        /// </summary>
        /// <param name="item">Clicked Target Tile</param>
        /// <param name="changedButton"></param>
        /// <param name="id">id of Coordinate</param>
        private static void HandleClickedImage(Coordinates item, MouseButton changedButton, int id)
        {
            var menu = new List<MenuItems>();

            if (changedButton == MouseButton.Right)
            {
                //Idle
                _menuItems = new List<int> { EditorResources.Idle };

                menu.Add(new MenuItems
                {
                    Position = 0,
                    Tooltip = EditorStringResource.ToolTipDeleteTile,
                    ImagePath = EditorStringResource.IconDeleteTile
                });
                //Single Tile delete
                _menuItems.Add(EditorResources.IdOfDeletePassableTile);

                menu.Add(new MenuItems
                {
                    Position = 1,
                    Tooltip = EditorStringResource.ToolTipDeleteMultiTile,
                    ImagePath = EditorStringResource.IconDeleteMultiTile
                });
                //Multi Tile delete
                _menuItems.Add(EditorResources.IdOfMultiTerrain);

                //Check if Event is here, we can remove them
                if (!EditorEventHandler.CheckEvenStatus(id))
                {
                    //Multi Tile delete
                    _menuItems.Add(EditorResources.IdOfEvent);
                    menu.Add(new MenuItems
                    {
                        Position = 2,
                        Tooltip = EditorStringResource.ToolTipAddEvent,
                        ImagePath = EditorStringResource.IconAddEvent
                    });
                }
                //Check if Event is here, if not we can add one
                else
                {
                    //Event Delete
                    _menuItems.Add(EditorResources.IdOfDeleteEvent);
                    menu.Add(new MenuItems
                    {
                        Position = 3,
                        Tooltip = EditorStringResource.ToolTipDeleteEvent,
                        ImagePath = EditorStringResource.IconDeleteEvent
                    });
                }

                var cache = _renderTile.DisplayMenu(menu, Orientation, CenterButton);

                item.TileId = _menuItems[cache]; //mapping Menu Item entry to Handler ID
            }

            DirectHandler(item, id);
        }

        /// <summary>
        ///     Take the first possible Option
        /// </summary>
        /// <param name="item">Clicked Cell and ID</param>
        /// <param name="id">id of Coordinate</param>
        private static void DirectHandler(Coordinates item, int id)
        {
            switch (item.TileId)
            {
                case EditorResources.Idle:
                    return;

                case EditorResources.IdOfDeletePassableTile:

                    //so long get all Tiles from the layer
                    _renderTile.DeleteLayerGraphics(item);

                    break;

                case EditorResources.IdOfDeleteEvent:
                    item.ZLayer = _renderTile.GetMaxLayers();
                    EditorEventHandler.DeleteEvent(id);
                    _renderTile.DeleteSingleGraphics(item);

                    break;

                case EditorResources.IdOfEvent:
                    item.ZLayer = _renderTile.GetMaxLayers();
                    EditorEventHandler.AddEvent(item, id);
                    item.TileId = EditorResources.SymbolOfEvent;

                    _renderTile.ChangeSingleGraphics(item);

                    break;

                case EditorResources.IdOfMultiTerrain:
                    try
                    {
                        DebugLog.CreateLogFile(EditorResources.InformationRenderingStart, ErCode.Information);
                        //delete Transitions, of Multi-terrain or Transitions

                        //get the deleted Transitions
                        var changeT = _transGenerate.DeleteTransition(item);
                        //order is important get the old Transitions
                        var newT = _transGenerate.GetTransitions();

                        _renderTile.DeleteTransitionGraphics(newT, changeT);

                        if (changeT == null)
                            DebugLog.CreateLogFile(EditorResources.InformationNothingToDo, ErCode.Information);

                        DebugLog.CreateLogFile(EditorResources.InformationRenderingEnd, ErCode.Information);
                    }
                    catch (TransitionException ex)
                    {
                        DebugLog.CreateLogFile(ex.ToString(), ErCode.Information);
                    }

                    break;

                default:
                    ChangeImages(item);
                    break;
            }
        }

        /// <summary>
        ///     Checks if Clicked Point has Coordinate
        ///     If it does Add Transitions
        ///     Else paint Tile
        /// </summary>
        /// <param name="item">item as Coordinate</param>
        private static void ChangeImages(Coordinates item)
        {
            switch (EditorRegister.TileDct[item.TileId].TileType)
            {
                case Tile.TileTypes.NoTransitions:
                    //Inform the TileHandler
                    _renderTile.ChangeSingleGraphics(item);
                    break;

                case Tile.TileTypes.MultiTerrain:
                    break;

                case Tile.TileTypes.TerrainWithTransitions:
                    //Handles all Transitions
                    try
                    {
                        DebugLog.CreateLogFile(EditorResources.InformationRenderingStart, ErCode.Information);

                        //get the added Transitions
                        var changeT = _transGenerate.AddTransition(item);
                        //order is important get the new Transitions
                        var newT = _transGenerate.GetTransitions();

                        _renderTile.ChangeTransitionGraphics(newT, changeT);

                        DebugLog.CreateLogFile(EditorResources.InformationRenderingEnd, ErCode.Information);
                    }
                    catch (TransitionException ex)
                    {
                        DebugLog.CreateLogFile(ex.ToString(), ErCode.Information);
                    }

                    break;

                case Tile.TileTypes.InternalTiles:
                    break;

                default:
                    return;
            }
        }
    }
}