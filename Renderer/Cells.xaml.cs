/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Renderer/Rendering.cs
 * PURPOSE:     Help class that handles all graphical displays of the game
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * SOURCES:     https://docs.microsoft.com/de-de/dotnet/api/system.windows.threading.dispatcher?view=netframework-4.8
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AvalonRuntime;
using CommonControls;
using Debugger;
using ExtendedSystemObjects;
using Resources;

// ReSharper disable ArrangeBraces_foreach

namespace Renderer
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Basic Control for Display
    ///     Does all the heavy graphical lifting
    ///     Used in: Editor, Campaign
    /// </summary>
    public sealed partial class Cells
    {
        /// <summary>
        ///     Help Value for animation, Coordinates for actual Tile
        /// </summary>
        private static Coordinates _image;

        /// <summary>
        ///     Help Value for animation, List of Images
        /// </summary>
        private static List<Coordinates> _path;

        /// <summary>
        ///     Collection of Transitions for specific ID
        ///     Point to Image
        /// </summary>
        private static Dictionary<Coordinates, Image> _imageDct;

        /// <summary>
        ///     Master Tile Dictionary
        /// </summary>
        private static Dictionary<int, Tile> _tileDct;

        /// <summary>
        ///     Help Value for animation, Tile ID of the animated image
        /// </summary>
        /// <value>int ID</value>
        private static int _tileId;

        /// <summary>
        ///     Only Editor Mode
        /// </summary>
        private static bool _editorMode;

        /// <summary>
        ///     Help Value for BackgroundImage
        /// </summary>
        private Image _backgroundImage;

        /// <summary>
        ///     Label that we click
        ///     Name, Int Tile id
        /// </summary>
        private Dictionary<string, int> _clickPoint;

        /// <summary>
        ///     Help Value for animation, Count of the Path Tile List
        /// </summary>
        private int _count = -1;

        /// <summary>
        ///     Help Value for animation, Count of Time Ticks
        /// </summary>
        private int _countTime;

        /// <summary>
        ///     Help Value for animation, Dispatcher Timer Initialization
        /// </summary>
        private DispatcherTimer _dispatcherTimer;

        /// <summary>
        ///     The Grid we are working on
        /// </summary>
        private Grid _exGrid;

        /// <summary>
        ///     To make sure that event is raised just once
        ///     Returns true for active and false for already raised
        /// </summary>
        private bool _isTimerFinished;

        /// <summary>
        ///     Labels we use to click on and Display Id's
        /// </summary>
        private List<Label> _labelList;

        /// <summary>
        ///     Do we show Numbers?
        /// </summary>
        private bool _numbersShown;

        /// <summary>
        ///     Layer of the Player
        /// </summary>
        private int _playerLayer;

        /// <inheritdoc />
        /// <summary>
        ///     Initialize Cells
        /// </summary>
        internal Cells()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Base Tiles
        /// </summary>
        internal static Dictionary<Coordinates, string> BaseTile { get; private set; }

        /// <summary>
        ///     Public Event for publishing the click on Image
        /// </summary>
        public event EventHandler<EditorClickedEventArgs> LabelClicked;

        /// <summary>
        ///     Public Event for publishing when Animation is finished
        /// </summary>
        public event EventHandler RaiseAnimationFinished;

        /// <summary>
        ///     First builds a grid
        ///     Adds Images to the grid
        ///     Adds Click Handlers to the Images
        ///     Second Reads the contents of the Map and adds them to the images
        ///     Third is optional, adds a huge Background Image
        /// </summary>
        /// <param name="baseTile">Speed Ups, Coordinate with FileName</param>
        /// <param name="tileDct">Dictionary of Tiles</param>
        /// <param name="height">height of Map</param>
        /// <param name="width">width of Map</param>
        /// <param name="maxLayer">layers of Map</param>
        /// <param name="backgroundImage">Image we use in the background</param>
        /// <param name="editorMode">Is logging active</param>
        /// <param name="playerLayer">Layer of the Player</param>
        internal void InitGenerateMapOverview(Dictionary<Coordinates, string> baseTile,
            Dictionary<int, Tile> tileDct,
            int height, int width, int maxLayer, string backgroundImage, bool editorMode, int playerLayer)
        {
            //Initiate Values
            _clickPoint = new Dictionary<string, int>(height * width);
            _editorMode = editorMode;
            _playerLayer = playerLayer;
            BaseTile = baseTile;

            _imageDct = new Dictionary<Coordinates, Image>(height * width * maxLayer);

            ScrollGrid.Content = null;

            //Set Basic Values
            _tileDct = tileDct;

            //Build Control
            GenerateGrid(height, width, maxLayer);
            GenerateMap(baseTile);
            //Prepare Image Background

            LoadImageBackground(string.Empty, backgroundImage);

            // Initiate without grid
            InitializeComponent();
        }

        /// <summary>
        ///     Loads Background Image
        ///     Data Comes from MapObject
        ///     Two overloads:
        ///     Editor loads the whole stuff in imageName
        ///     Campaign uses path and imageName combined
        ///     Load Image as Background
        /// </summary>
        /// <param name="path">Optional Path if it is not in the usual location</param>
        /// <param name="imageName">Name of the Image</param>
        internal void LoadImageBackground(string path, string imageName)
        {
            //check for null
            if (string.IsNullOrEmpty(imageName) && string.IsNullOrEmpty(path))
            {
                DebugLog.CreateLogFile(
                    string.Concat(RendererResources.WarningLoadImageBackground, RendererResources.Separator, path,
                        RendererResources.Separator, imageName), ErCode.Warning);
                return;
            }

            _backgroundImage.Source = CellsImageFileStream.GetImageUriSource(path, imageName);
        }

        /// <summary>
        ///     Switch Grid on and off
        /// </summary>
        internal void ShowGrid()
        {
            _exGrid.ShowGridLines = !_exGrid.ShowGridLines;
        }

        /// <summary>
        ///     Switch Click able on and of
        /// </summary>
        internal void SetClick()
        {
            _exGrid.IsEnabled = !_exGrid.IsEnabled;
        }

        /// <summary>
        ///     Displays Number of the Tile
        /// </summary>
        internal void ShowNumbers()
        {
            if (!_numbersShown)
            {
                var i = -1;

                foreach (var label in _labelList)
                {
                    i++;
                    label.Content = i;
                }

                _numbersShown = true;
            }
            else
            {
                foreach (var label in _labelList)
                {
                    label.Content = string.Empty;
                }

                _numbersShown = false;
            }
        }

        /// <summary>
        ///     Displays a simple Animation while the Avatar moves over the map
        /// </summary>
        /// <param name="path">List of Coordinates</param>
        /// <param name="tileId">Id of the Avatar</param>
        /// <param name="timer">Time span in Milliseconds</param>
        internal void DisplayMovementAnimation(List<Coordinates> path, int tileId, int timer)
        {
            //Set Control Inactive
            _exGrid.IsEnabled = false;

            _isTimerFinished = true;
            _path = path;
            _tileId = tileId;
            _dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromMilliseconds(timer)
            };
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _count = -1;
            //switch for fine tuning
            _countTime = _path.Count - 1;
            _dispatcherTimer.Start();
        }

        /// <summary>
        ///     Changes multiple items with a new ones
        /// </summary>
        /// <param name="tiles">List of Ids and the Coordinates</param>
        internal static void ChangeMultipleGraphics(List<Coordinates> tiles)
        {
            //check if null
            if (tiles.IsNullOrEmpty())
            {
                return;
            }

            foreach (var items in tiles)
            {
                ChangeSingleGraphics(items);
            }
        }

        /// <summary>
        ///     Changes a specific item to a new one
        /// </summary>
        /// <param name="item">Id of Graphic and the Coordinates</param>
        internal static void ChangeSingleGraphics(Coordinates item)
        {
            //Error Check we log it just in case
            if (item == null || _tileDct == null)
            {
                DebugLog.CreateLogFile(RendererResources.WarningNoItem, ErCode.Warning, item);
                return;
            }

            //If null no need for further work
            if (item.TileId == RendererResources.IdofDeleteTile)
            {
                return;
            }

            ChangeGraphics(item);

            //Type is Internal don't save it
            if (_tileDct[item.TileId].TileType == Tile.TileTypes.InternalTiles)
            {
                return;
            }

            //Add to changeList, Check if Transitions and if it is true change Tiles
            if (_editorMode)
            {
                CellsEditorMode.AddTile(item);
            }
        }

        /// <summary>
        ///     Changes a specific item to a new one, extended Method without logging
        /// </summary>
        /// <param name="item">Coordinate of the Image</param>
        private static void ChangeGraphics(Coordinates item)
        {
            var imageUri = _tileDct[item.TileId].FileName;
            //new external Class
            var myBitmapCell = CellsImageFileStream.GetImageFileStream(RendererResources.TilesFolder, imageUri);

            //Error Check
            if (!_imageDct.ContainsKey(item))
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorImageKeyNotFound, nameof(ChangeGraphics)),
                    ErCode.Warning);
                return;
            }

            if (_editorMode)
            {
                myBitmapCell = CellsEditorMode.CheckAddTransitions(item, myBitmapCell);
            }

            var myCell = _imageDct[item];
            //set image source
            myCell.Source = myBitmapCell;
        }

        /// <summary>
        ///     Deletes a List of Images on a specified Layers
        /// </summary>
        /// <param name="itemLst">List of Coordinates</param>
        internal static void DeleteMultipleGraphics(List<Coordinates> itemLst)
        {
            //if empty do nothing no log needed yet
            if (itemLst.IsNullOrEmpty())
            {
                return;
            }

            if (_editorMode)
            {
                itemLst = CellsEditorMode.GetIntersection(itemLst);
            }

            foreach (var item in itemLst)
            {
                DeleteSingleGraphics(item);
            }
        }

        /// <summary>
        ///     Deletes all Images on a single Layers
        /// </summary>
        /// <param name="item">Coordinate of the Image</param>
        internal static void DeleteSingleGraphics(Coordinates item)
        {
            //if empty do nothing no log needed yet
            if (item == null || _tileDct == null)
            {
                return;
            }

            if (!_imageDct.ContainsKey(item))
            {
                DebugLog.CreateLogFile(
                    string.Concat(RendererResources.ErrorImageKeyNotFound, nameof(DeleteSingleGraphics)), ErCode.Error);
                return;
            }

            DeleteGraphics(item);

            //Add to changeList
            if (_editorMode)
            {
                CellsEditorMode.Remove(item);
            }
        }

        /// <summary>
        ///     Deletes all Images on a single Layers, extended Method without logging
        /// </summary>
        /// <param name="item">Coordinate of the Image</param>
        private static void DeleteGraphics(Coordinates item)
        {
            var myCell = _imageDct[item];

            //new external Class
            BitmapImage myBitmapCell = null;

            if (_editorMode)
            {
                myBitmapCell = CellsEditorMode.CheckDeleteTransitions(item);
            }

            //set image source
            myCell.Source = myBitmapCell;
        }

        /// <summary>
        ///     Dots the movement Path
        /// </summary>
        /// <param name="coordinate">List of Coordinates</param>
        internal static void DisplayMovementPath(List<Coordinates> coordinate)
        {
            if (coordinate.IsNullOrEmpty())
            {
                return;
            }

            //Extra Check, should not be possible, and it takes time but we should catch it, just in Case
            if (!_imageDct.ContainsKeys(coordinate))
            {
                DebugLog.CreateLogFile(
                    string.Concat(RendererResources.ErrorImageKeyNotFound, nameof(DisplayMovementPath)), ErCode.Error);
                return;
            }

            foreach (
                var myCell in
                coordinate.Select(item => _imageDct[item])
                    .Select(source => source))
            {
                SetImageFileStream(myCell, RendererResources.Resources, RendererResources.DotImage);
            }
        }

        /// <summary>
        ///     MouseDown Event for Image Controls Calls OnImageClicked()
        /// </summary>
        /// <param name="sender">Object that sends the Message</param>
        /// <param name="e">Type of Message</param>
        private void LabelClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // code to handle tick in SecondaryForm
            //get the button that was clicked
            if (!(sender is Label clickedButton))
            {
                return;
            }

            //create new click Object
            var args = new EditorClickedEventArgs
            {
                ClickType = e,
                //Get the Image Name
                ImagePoint = _clickPoint[clickedButton.Name]
            };

            LabelClicked?.Invoke(this, args);
        }

        /// <summary>
        ///     Well this Method pretty much violates all rules of being decent but mehh
        ///     This Method generates a ExtendedGrid adds a canvas to every Single Cell, onto the Grid he adds 7 Images
        ///     Not very Memory friendly but it works
        /// </summary>
        /// <param name="height">height of Map</param>
        /// <param name="length">length of Map</param>
        /// <param name="maxLayer">layers of Map</param>
        private void GenerateGrid(int height, int length, int maxLayer)
        {
            _exGrid = ExtendedGrid.ExtendGrid(length, height,
                RendererResources.CellHeight, RendererResources.CellHeight, false);

            //Initiate Labels
            _labelList = new List<Label>();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < length; x++)
                {
                    var myCanvas = new Canvas();

                    Grid.SetRow(myCanvas, y);
                    Grid.SetColumn(myCanvas, x);

                    _exGrid.Children.Add(myCanvas);

                    //maxLayer + one layer for the clicks
                    for (var z = 1; z <= maxLayer + 1; z++)
                    {
                        var images = new Image();
                        var points = new Coordinates(x, y, z);
                        images.Height = images.Width = RendererResources.CellHeight;

                        //Set Quality, last one doesn't need any touch ups
                        if (z <= maxLayer - 1)
                        {
                            RenderOptions.SetBitmapScalingMode(images, BitmapScalingMode.HighQuality);
                            RenderOptions.SetEdgeMode(images, EdgeMode.Aliased);
                        }

                        //Add the Image
                        _imageDct.Add(points, images);

                        myCanvas.Children.Add(images);

                        //No need to add them to all Images, layer 8 only, we add Labels to the last Layer so we can handle the clicks
                        if (z != maxLayer + 1)
                        {
                            continue;
                        }

                        var id = ArtShared.CalculateId(points, length);

                        var label = new Label
                        {
                            Height = RendererResources.CellHeight,
                            Width = RendererResources.CellHeight,
                            Name = RendererResources.Lbl + id
                        };

                        //Forget to add it I am with stoopid
                        myCanvas.Children.Add(label);

                        //Dispatcher Class is Used for UI Elements
                        label.Dispatcher?.BeginInvoke(DispatcherPriority.Normal,
                            (ThreadStart)(() => label.MouseDown += LabelClick_MouseDown));

                        //we only need the last list, for interactions from the Outside, the Dictionary is just for speed Ups
                        _clickPoint.Add(label.Name, id);
                        _labelList.Add(label);
                    }
                }
            }

            //Get Background Image in Place
            _backgroundImage = new Image
            {
                Height = RendererResources.CellHeight * height,
                Width = RendererResources.CellHeight * length,
                Name = RendererResources.NameBackgroundImage
            };

            //First Background Image
            //Second Our Grid
            ScrollGrid.Content = _backgroundImage;
            ScrollGrid.Content = _exGrid;
        }

        /// <summary>
        ///     Here we add all Tiles of the Map to our Grid
        /// </summary>
        /// <param name="baseTiles">Speed Ups, Coordinate with FileName</param>
        private static void GenerateMap(Dictionary<Coordinates, string> baseTiles)
        {
            if (baseTiles.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(RendererResources.WarningMapEmpty, ErCode.Warning);
                return;
            }

            foreach (var tile in baseTiles)
            {
                if (!_imageDct.ContainsKey(tile.Key))
                {
                    DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorImageKeyNotFound, nameof(GenerateMap)),
                        ErCode.Error);
                    continue;
                }

                var myCell = _imageDct[tile.Key];

                SetImageUri(myCell, RendererResources.TilesFolder, tile.Value);
            }
        }

        /// <summary>
        ///     Here we change TransitionGraphics
        /// </summary>
        /// <param name="transitions">Dictionary of Tile</param>
        /// <param name="baseTiles">Speed Ups, Coordinate with FileName</param>
        internal static void ChangeTransitionGraphics(Dictionary<Coordinates, List<int>> transitions,
            Dictionary<Coordinates, string> baseTiles)
        {
            if (transitions.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(RendererResources.WarningTransitionsEmpty, ErCode.Information);
                return;
            }

            foreach (var tile in transitions)
            {
                var images = CellsImageFileStream.LoadMultiTerain(tile.Value, tile.Key, baseTiles, _tileDct);

                //short version of Tile Adding
                var myCell = _imageDct[tile.Key];

                //Dispatcher Class is used for UI Elements
                myCell.Dispatcher?.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)(() => myCell.Source = images));

                //set image source
                myCell.Dispatcher?.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)(() => myCell.Source = images));
            }
        }

        /// <summary>
        ///     Get all Tiles we have to touch
        ///     Delete the old ones
        ///     Fill them again
        ///     A little bit overhead might be optimize able
        /// </summary>
        /// <param name="transition">Current  Dictionary Transition List (Updated List after Changes!)</param>
        /// <param name="changedTiles">Changed Tiles List</param>
        /// <param name="width">width of Map</param>
        internal static void ChangeTransitionGraphics(Dictionary<int, List<int>> transition,
            Dictionary<int, int> changedTiles, int width)
        {
            if (changedTiles.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(RendererResources.WarningChangedTilesEmpty, ErCode.Warning);
                return;
            }

            //Dictionary, Key is MasterId, Value is Tile Ids, Get all changes, here might be a point to improve
            var changeTransitions = changedTiles.ToDictionary(element => element.Key,
                element => transition[element.Key]);

            //pass the Information to our EditorMode
            var transitions = CellsProcessing.ConvertTransitions(changeTransitions, _tileDct, width);

            //load the whole mess
            ChangeTransitionGraphics(transitions, BaseTile);
        }

        /// <summary>
        ///     Get all Tiles we have to touch
        ///     Delete the old ones
        ///     Fill them again
        ///     A little bit overhead might be optimize able
        /// </summary>
        /// <param name="changedTiles">Changed Tiles List</param>
        /// <param name="width">width of Map</param>
        internal static void DeleteTransitionGraphics(Dictionary<int, int> changedTiles, int width)
        {
            if (changedTiles.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(RendererResources.WarningChangedTilesEmpty, ErCode.Warning);
                return;
            }

            //Dictionary, Key is MasterId, Value is Tile Id
            var lst = (from element in changedTiles
                let tile = _tileDct[element.Value]
                select ArtShared.IdToCoordinate(element.Key, width, tile.Layer)).ToList();

            //Delete Images without logging
            foreach (var elements in lst)
            {
                DeleteGraphics(elements);
            }
        }

        /// <summary>
        ///     Loads Bitmap and loads to appreciate Image
        ///     Uses FileStream slow but works all the time
        /// </summary>
        /// <param name="myCell">Target Image </param>
        /// <param name="path">Folder in Source</param>
        /// <param name="imageUri">Image Name</param>
        private static void SetImageFileStream(Image myCell, string path, string imageUri)
        {
            //new external Class
            var myBitmapCell = CellsImageFileStream.GetImageFileStream(path, imageUri);
            if (myBitmapCell == null)
            {
                return;
            }

            myBitmapCell.Freeze();
            //set image source, Dispatcher Class is used for UI Elements
            myCell.Dispatcher?.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)(() => myCell.Source = myBitmapCell));
        }

        /// <summary>
        ///     Loads Bitmap and loads to appreciate Image
        ///     Uses ImageURI fast but only Onetime change
        ///     Speed Up
        /// </summary>
        /// <param name="myCell">Target Image </param>
        /// <param name="path">Folder in Source</param>
        /// <param name="imageUri">Image Name</param>
        private static void SetImageUri(Image myCell, string path, string imageUri)
        {
            //new external Class
            var myBitmapCell = CellsImageFileStream.GetImageUriSource(path, imageUri);
            myBitmapCell.Freeze();
            //set image source, Dispatcher Class is used for UI Elements
            myCell.Dispatcher?.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)(() => myCell.Source = myBitmapCell));
        }

        /// <summary>
        ///     Helper Method for Animations
        ///     Ticks down the movement Counter
        /// </summary>
        /// <param name="count">Number of Tiles</param>
        /// <returns>Number of Frames</returns>
        private int MovementDisplay(int count)
        {
            count++;
            if (count <= _path.Count - 2)
            {
                _image = _path[count];
                _image.ZLayer = _playerLayer;
                DeleteSingleGraphics(_image);
                _image = _path[count + 1];
                _image.TileId = _tileId;
                _image.ZLayer = _playerLayer;
                ChangeSingleGraphics(_image);
                return count;
            }

            //switch for fine tuning
            if (count == _countTime)
            {
                OnRaiseAnimationFinished();
            }

            return count;
        }

        /// <summary>
        ///     Helper Event for the animation Timer
        /// </summary>
        /// <param name="sender">Object that sends the Message</param>
        /// <param name="e">Type of Message</param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            _count = MovementDisplay(_count);
        }

        /// <summary>
        ///     Raises Event if Animation is finished
        /// </summary>
        private void OnRaiseAnimationFinished()
        {
            if (!_isTimerFinished)
            {
                return;
            }

            //else Notify Subscribers
            _isTimerFinished = false;
            var handler = RaiseAnimationFinished;
            _dispatcherTimer.Stop();

            //Set Control Active
            _exGrid.IsEnabled = true;

            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}