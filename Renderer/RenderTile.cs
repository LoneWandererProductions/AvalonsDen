/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Renderer/Rendering.cs
 * PURPOSE:     Interface that handles All external Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * SOURCES:     https://wpf.2000things.com/2012/11/30/702-dragging-an-image-within-a-wpf-application/
 *              https://docs.microsoft.com/de-de/dotnet/api/system.windows.threading.dispatcher?view=netframework-4.8
 */

using System;
using System.Collections.Generic;
using InterOp;
using Resources;
using Point = System.Drawing.Point;

namespace Renderer
{
    /// <inheritdoc />
    /// <summary>
    ///     The render tile class.
    /// </summary>
    public sealed class RenderTile : IRenderTile
    {
        /// <summary>
        ///     The Background image (readonly).
        /// </summary>
        private readonly string _backgroundImage;

        /// <summary>
        ///     The base tile (readonly).
        /// </summary>
        private readonly Dictionary<Coordinates, string> _baseTiles;

        /// <summary>
        ///     The context Menu (readonly).
        /// </summary>
        private readonly MCursor _crs;

        /// <summary>
        ///     The log (readonly). Used in Editor Mode
        /// </summary>
        private readonly bool _editorMode;

        /// <summary>
        ///     The height (readonly) of the Map.
        /// </summary>
        private readonly int _height;

        /// <summary>
        ///     The max layer (readonly).
        /// </summary>
        private readonly int _maxLayer;

        /// <summary>
        ///     The player layer (readonly).
        /// </summary>
        private readonly int _playerLayer;

        /// <summary>
        ///     The Tile Dictionary (readonly).
        /// </summary>
        private readonly Dictionary<int, Tile> _tileDct;

        /// <summary>
        ///     The Transitions Dictionary (readonly).
        /// </summary>
        private readonly Dictionary<Coordinates, List<int>> _transitions;

        /// <summary>
        ///     The width (readonly) of the Map.
        /// </summary>
        private readonly int _width;

        /// <summary>
        ///     Graphical Interface
        /// </summary>
        private Cells _cell;

        /// <summary>
        ///     Feedback from Cell
        /// </summary>
        private Point _position;

        /// <summary>
        ///     Initiate the Image Control for a new Map
        /// </summary>
        /// <param name="tileDct">Dictionary of Tiles</param>
        /// <param name="height">height of Map</param>
        /// <param name="width">width of Map</param>
        /// <param name="editorMode">Is logging active</param>
        /// <param name="playerLayer">Layer of the Player</param>
        /// <param name="crs">Context Menu Basics</param>
        public RenderTile(
            Dictionary<int, Tile> tileDct, int height, int width, bool editorMode, int playerLayer, MCursor crs)
        {
            _tileDct = tileDct;
            _height = height;
            _width = width;
            _editorMode = editorMode;
            _playerLayer = playerLayer;
            _crs = crs;
            _baseTiles = new Dictionary<Coordinates, string>();
            _transitions = new Dictionary<Coordinates, List<int>>();

            _maxLayer = CellsProcessing.GetMaxLayer(tileDct);

            CellsEditorMode.EditorModeInitiate(_tileDct, _transitions, null, width);
        }

        /// <summary>
        ///     Initiate the Image Control for an existing map
        /// </summary>
        /// <param name="mapDct">will be replaced with: Dictionary int, int</param>
        /// <param name="transitions">All multi Terrain Tiles</param>
        /// <param name="tileDct">Dictionary of Tiles</param>
        /// <param name="height">height of Map</param>
        /// <param name="width">width of Map</param>
        /// <param name="backroundImage">Image we use in the background</param>
        /// <param name="editorMode">Is logging active</param>
        /// <param name="playerLayer">Layer of the Player</param>
        /// <param name="crs">Context Menu basics</param>
        public RenderTile(List<SerializeableKeyValuePair.KeyValuePair<int, int>> mapDct,
            Dictionary<int, List<int>> transitions,
            Dictionary<int, Tile> tileDct, int height, int width, string backroundImage, bool editorMode,
            int playerLayer,
            MCursor crs)
        {
            _tileDct = tileDct;
            _height = height;
            _width = width;
            _backgroundImage = backroundImage;
            _editorMode = editorMode;
            _playerLayer = playerLayer;
            _crs = crs;
            _baseTiles = CellsProcessing.ConvertMap(mapDct, tileDct, _width);

            _transitions = CellsProcessing.ConvertTransitions(transitions, _tileDct, _width);
            _maxLayer = CellsProcessing.GetMaxLayer(tileDct);

            //Get Logging started
            if (!_editorMode) return;

            CellsEditorMode.EditorModeInitiate(_tileDct, _transitions, mapDct, width);
        }

        /// <summary>
        ///     External Event Handling
        /// </summary>
        public EventHandler RaiseAnimationFinished { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Returns Number of Layers
        /// </summary>
        /// <returns>The <see cref="T:System.Int32" /> amount of Layers.</returns>
        public int GetMaxLayers()
        {
            return _maxLayer;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Switch Grid on and off
        /// </summary>
        public void ShowGrid()
        {
            _cell?.ShowGrid();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Displays Number of the Tile
        /// </summary>
        public void ShowNumbers()
        {
            _cell?.ShowNumbers();
        }

        /// <inheritdoc />
        /// <summary>
        ///     The generated map overview.
        /// </summary>
        /// <returns>The <see cref="T:Renderer.Cells" /> Game Display.</returns>
        public Cells GenerateMap()
        {
            _cell = new Cells();
            //Initiate Cells
            _cell.InitGenerateMapOverview(_baseTiles, _tileDct, _height, _width, _maxLayer,
                _backgroundImage, _editorMode, _playerLayer);

            //Initiate Events
            InitiateEvents();
            //Display Transitions
            Cells.ChangeTransitionGraphics(_transitions, _baseTiles);
            return _cell;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load Image as Background
        /// </summary>
        /// <param name="path">Optional Path if it is not in the usual location</param>
        /// <param name="imageName">Name of the Image</param>
        public void LoadImageBackround(string path, string imageName)
        {
            _cell?.LoadImageBackground(path, imageName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Handle Right Click, ergo Menu from User side with Custom ContextMenue
        /// </summary>
        /// <param name="items">List of icons Ids and Tool-tips</param>
        /// <param name="orientation">Clockwise true</param>
        /// <param name="centerbutton">Center Menu active</param>
        /// <returns>The id<see cref="T:System.Int32" /> of the clicked Menu Item.</returns>
        public int DisplayMenu(List<MenuItems> items, bool orientation, bool centerbutton)
        {
            if (_crs == null) return -1;

            var ctm = new ContextMenus(_crs)
            {
                //TODO check with multiple Monitors
                //WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Left = _position.X,
                Top = _position.Y,
                Topmost = true
            };
            ctm.GeneratePossibilities(items, orientation, centerbutton);

            ctm.ShowDialog();

            return ctm.ClickId;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Displays a simple Animation while the Avatar moves over the map
        /// </summary>
        /// <param name="path">List of Coordinates</param>
        /// <param name="tileId">Id of the Avatar</param>
        /// <param name="timer">Time span in Milliseconds</param>
        public void DisplaymovementAnimation(List<Coordinates> path, int tileId, int timer)
        {
            _cell?.DisplayMovementAnimation(path, tileId, timer);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Display movement path.
        /// </summary>
        /// <param name="tilePath">The tilePath.</param>
        public void DisplaymovementPath(List<Coordinates> tilePath)
        {
            Cells.DisplayMovementPath(tilePath);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Change multiple graphics.
        /// </summary>
        /// <param name="tileLst">List of tileIds.</param>
        public void ChangeMultipleGraphics(List<Coordinates> tileLst)
        {
            Cells.ChangeMultipleGraphics(tileLst);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Change single graphics.
        /// </summary>
        /// <param name="tileId">The tileId.</param>
        public void ChangeSingleGraphics(Coordinates tileId)
        {
            Cells.ChangeSingleGraphics(tileId);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Add Transitions
        /// </summary>
        /// <param name="newTransition">All multi Terrain Tiles</param>
        /// <param name="changedTiles">Changed Transitions</param>
        public void ChangeTransitionGraphics(Dictionary<int, List<int>> newTransition,
            Dictionary<int, int> changedTiles)
        {
            //pass the Information to our EditorMode and Inform Cache
            CellsEditorMode.Transitions = CellsProcessing.ConvertTransitions(newTransition, _tileDct, _width);

            Cells.ChangeTransitionGraphics(newTransition, changedTiles, _width);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Remove Transitions
        /// </summary>
        /// <param name="newTransition">All multi Terrain Tiles</param>
        /// <param name="changedTiles">Changed Transitions</param>
        public void DeleteTransitionGraphics(Dictionary<int, List<int>> newTransition,
            Dictionary<int, int> changedTiles)
        {
            //Convert to readable Format, integrate changes
            var transitions = CellsProcessing.ConvertTransitions(newTransition, _tileDct, _width);

            //inform our cache about the new Transitions
            CellsEditorMode.Transitions = transitions;

            //Handle the dirty work
            Cells.DeleteTransitionGraphics(changedTiles, _width);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Delete the single graphics.
        /// </summary>
        /// <param name="tileId">The tileId.</param>
        public void DeleteSingleGraphics(Coordinates tileId)
        {
            Cells.DeleteSingleGraphics(tileId);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Delete the graphics over all layers.
        /// </summary>
        /// <param name="tileId">The tileId.</param>
        public void DeleteLayerGraphics(Coordinates tileId)
        {
            var tiles = GetTilesForAllLayers(tileId);
            Cells.DeleteMultipleGraphics(tiles);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Delete multiple graphics.
        /// </summary>
        /// <param name="tileLst">The tiles.</param>
        public void DeleteMultipleGraphics(List<Coordinates> tileLst)
        {
            Cells.DeleteMultipleGraphics(tileLst);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Returns a list of the changed Tiles
        /// </summary>
        /// <returns>The List of changed Tiles as <see cref="T:List{Coordinates}" />.</returns>
        public List<Coordinates> GetChangeList()
        {
            return CellsEditorMode.ChangeList;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Set Control to Active or Inactive
        /// </summary>
        public void SetClick()
        {
            _cell?.SetClick();
        }

        /// <summary>
        ///     Clicked Image
        /// </summary>
        public event EventHandler<EditorClickedEventArgs> ImageClicked;

        /// <summary>
        ///     Initiate Events
        /// </summary>
        private void InitiateEvents()
        {
            _cell.RaiseAnimationFinished += Cell_RaiseAnimationFinished;
            _cell.LabelClicked += Cell_LabelClicked;
        }

        /// <summary>
        ///     Tell Subscribers that a Cell was clicked. Event raised by Cell, from the Label
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The editor clicked event arguments.</param>
        private void Cell_LabelClicked(object sender, EditorClickedEventArgs e)
        {
            //Todo Add some padding! for edge Cases
            _position = WinScreenCoordinate.MousePosition;
            ImageClicked?.Invoke(this, e);
        }

        /// <summary>
        ///     Tell Subscribers that the Animation is finished. Event raised by Cell, from the Label
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Cell_RaiseAnimationFinished(object sender, EventArgs e)
        {
            RaiseAnimationFinished?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Get a List of all Coordinates at one Point with all Layers
        /// </summary>
        /// <param name="item">Coordinates with one Layer</param>
        /// <returns>List of Coordinates</returns>
        private List<Coordinates> GetTilesForAllLayers(Coordinates item)
        {
            if (_maxLayer == 0) return null;

            var items = new List<Coordinates>(_maxLayer);

            for (var i = 0; i <= _maxLayer; i++)
            {
                var point = new Coordinates(item.XRow, item.YColumn, i);
                items.Add(point);
            }

            return items;
        }
    }
}