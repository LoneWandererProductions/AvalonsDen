/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/Editor.xaml.cs
 * PURPOSE:     Editor Window, Generating Content for AvalonsDen
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeInternal

using System.ComponentModel;
using System.Windows;
using Debugger;
using EditorCampaign;
using EditorCharacter;
using EditorDialogTree;
using EditorEvent;
using EditorItems;
using EditorMap;
using SQLiteGui;

namespace Editors
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Base Window for Generating Content
    /// </summary>
    public sealed partial class Editor
    {
        /// <summary>
        ///     The log.
        /// </summary>
        private DebugLog _log;

        /// <summary>
        ///     Check how we want to handle this?
        /// </summary>
        private EditorTileToolBox _myBox;

        /// <inheritdoc />
        /// <summary>
        ///     Initiates Editor with LoadUpCollection and Worlds as Reference window when closing <see cref="Editor" />
        ///     class.
        /// </summary>
        public Editor()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Load our Toolbox
        /// </summary>
        private void LoadToolBox()
        {
            //TODO wrong and or missing Tiles!
            _myBox = new EditorTileToolBox(EditorRegister.TileDct)
            {
                Topmost = true
            };
            _myBox.Show();
        }

        /// <summary>
        ///     Open ToolBox
        ///     Cells
        ///     Initiate Tile Listener
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DebugRegister.IsRunning)
            {
                _log = new DebugLog();

                _log.StartWindow();
            }


            //TODO does not Update
            DebugLog.CreateLogFile(EditorResources.InformationLoad, ErCode.Information);
            MapEditor.MapData += NewMapCreated_Click;
            LoadToolBox();
        }

        /// <summary>
        ///     Loads all Files for a Map
        ///     Place it into the Form
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void OpenMaps_Click(object sender, RoutedEventArgs e)
        {
            var myCell = EditorHandleGraphics.LoadMap();
            //canceled the operation? Do nothing
            if (myCell == null) return;

            Display.Children.Clear();
            //Show Map
            Display.Children.Add(myCell);
        }

        /// <summary>
        ///     Saves all Files
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void SaveMapAs_Click(object sender, RoutedEventArgs e)
        {
            EditorHandleGraphics.SaveMap();
        }

        /// <summary>
        ///     Toggles Grid on Off
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            EditorHandleGraphics.ShowGrid();
        }

        /// <summary>
        ///     Show Numbers
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Numbers_Click(object sender, RoutedEventArgs e)
        {
            EditorHandleGraphics.ShowNumbers();
        }

        /// <summary>
        ///     Load ToolBox
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Toolbox_Click(object sender, RoutedEventArgs e)
        {
            LoadToolBox();
        }

        /// <summary>
        ///     Load the Console
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Console_Click(object sender, RoutedEventArgs e)
        {
            Prompts.Initiate();
        }

        /// <summary>
        ///     Open the Dialog Editor Window
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void DialogEditor_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var dialogEditor = new EditorDialog
            {
                ShowInTaskbar = true
            };
            if (_myBox.IsVisible) _myBox?.Close();

            dialogEditor.ShowDialog();

            LoadToolBox();
            Show();
        }

        /// <summary>
        ///     Open the Character Editor Window
        /// </summary>
        /// <param name="sender">Sender of Event</param>
        /// <param name="e">Event Args </param>
        private void CharacterEditor_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var characterEditor = new EditorChar
            {
                ShowInTaskbar = true
            };
            if (_myBox.IsVisible) _myBox?.Close();

            characterEditor.ShowDialog();

            LoadToolBox();
            Show();
        }

        /// <summary>
        ///     Creates new MapObject
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void NewCampaign_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var campaignsEditor = new EditorCampaigns
            {
                ShowInTaskbar = true
            };
            if (_myBox.IsVisible) _myBox?.Close();

            campaignsEditor.ShowDialog();

            LoadToolBox();
            Show();
        }

        /// <summary>
        ///     Opens Tile Editor
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void TileDct_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            if (_myBox.IsVisible) _myBox?.Close();

            var tileDctEditor = new EditorTileDctEditor
            {
                ShowInTaskbar = true
            };

            tileDctEditor.ShowDialog();

            LoadToolBox();
            Show();
        }

        /// <summary>
        ///     Creates new EventList for MapObject
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Events_Click(object sender, RoutedEventArgs e)
        {
            var editorEvent = new EventEditor();
            if (EditorRegister.EventTypeObjct != null)
                editorEvent.ShowEvents(EditorRegister.EventTypeObjct);
            else
                editorEvent.ShowEvents();

            EditorRegister.SetEventTypeDictionary(editorEvent.GetEventTypeContainer());
        }

        /// <summary>
        ///     Creates new Coordinates of Events for MapObject
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void NewMap_Click(object sender, RoutedEventArgs e)
        {
            var mapEditorMap = new MapEditor();
            mapEditorMap.CreateMap();
        }

        /// <summary>
        ///     New Menu for Background Image
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BackgroundImage_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var imageNameEditor = new EditorImageName
            {
                ShowInTaskbar = true
            };

            if (_myBox.IsVisible) _myBox?.Close();

            imageNameEditor.ShowDialog();

            //Check if someone just close the window and check if the path actual is set
            if (EditorRegister.MapObjct != null && !string.IsNullOrEmpty(EditorRegister.MapObjct.BackGroundImage))
                EditorHandleGraphics.LoadImageBackground(EditorRegister.MapObjct.BackGroundImage);

            LoadToolBox();
            Show();
        }

        /// <summary>
        ///     Edit Item Database of Campaign
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Items_Click(object sender, RoutedEventArgs e)
        {
            var addItem = new AddItemWindow();
            addItem.Show();
        }

        /// <summary>
        ///     Basic Database Editor
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Database_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var db = new SqLiteGuiWindow
            {
                ShowInTaskbar = true
            };

            if (_myBox.IsVisible) _myBox?.Close();

            db.ShowDialog();

            LoadToolBox();
            Show();
        }

        /// <summary>
        ///     The editor close click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void EditorClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     The new map created click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void NewMapCreated_Click(object sender, EventArgsMap e)
        {
            EditorRegister.SetMapObject(e.Height, e.Length);
            EditorRegister.SetTransitionDictionary(e.Transitions);

            var myCell = EditorHandleGraphics.NewMap(e);
            Display.Children.Clear();
            //Show Map
            Display.Children.Add(myCell);
        }

        /// <summary>
        ///     The window closing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The cancel event arguments.</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _myBox?.Close();
            _log.StopDebugging();
            _log = null;
        }
    }
}