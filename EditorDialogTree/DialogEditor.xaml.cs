/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorDialogTree/EditorDialogTree.xaml.cs
 * PURPOSE:     Drawing Board for the Visual Dialog Tree
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using AvalonRuntime;
using CommonControls;
using DialogEngine;
using ExtendedSystemObjects;
using Resources;
using Path = System.IO.Path;

namespace EditorDialogTree
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Interaction logic for UserControl1.xaml
    /// </summary>
    internal sealed partial class DialogEditor
    {
        /// <summary>
        ///     Dialog Engine Interface
        /// </summary>
        private static DialogEdit _diaghandler;

        //Graphical Stuff, Custom Controls loaded at runtime

        /// <summary>
        ///     The button add dictionary.
        /// </summary>
        private Dictionary<string, int> _buttonAddDct;

        /// <summary>
        ///     The button coordinates.
        /// </summary>
        private Dictionary<DialogPoints, DialogPoints> _buttonCoordinatesDct;

        /// <summary>
        ///     The button dialog dictionary.
        /// </summary>
        private Dictionary<string, int> _buttonDialogDct;

        /// <summary>
        ///     canvas Control, we paint on it
        /// </summary>
        private Canvas _paintCanvas;

        /// <summary>
        ///     The Dictionary Points.
        /// </summary>
        private Dictionary<int, DialogPoints> _pointsDct;

        /// <summary>
        ///     Grid
        /// </summary>
        private Grid _tree;

        /// <inheritdoc />
        /// <summary>
        ///     Initiate the mess
        /// </summary>
        internal DialogEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Add Event Handlers and initiate our Dialog Handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void UserCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            DialogDetails.DialogChange += DialogDetails_DialogChange;
            _diaghandler = new DialogEdit();
        }

        /// <summary>
        ///     Load saved Dialog and convert to the Tree
        /// </summary>
        /// <param name="dialog">Saved Dialog as List</param>
        private void DrawGraph(List<DialogObject> dialog)
        {
            _diaghandler.InitiateDialog(dialog);
            Register.DialogTree = _diaghandler.DialogTree;
            var dialogtree = TreeProcessing.ConvertToTreeDisplay(Register.DialogTree);
            Initiate(dialogtree);
        }

        /// <summary>
        ///     Create the Dialog Tree
        /// </summary>
        private void DrawGraph()
        {
            //initiate _dialogTree add StartElement
            Register.DialogTree = new Dictionary<int, DialogDisplay>();

            Register.DialogStructure = new Dictionary<int, Node>
            {
                {1, new Node {Id = 1, Level = 1}}
            };

            DialogEditorHandler.AddElement(1, 1);
            InitiateGui();
        }

        /// <summary>
        ///     Start everything up
        /// </summary>
        private void InitiateGui()
        {
            // Also Sets Register
            TreeProcessing.InitiateTreeDisplay(Register.DialogStructure);

            InitiateObjects();

            SetMetrics();

            DrawingBoard();

            PopulateControl();

            DrawLines();

            AddChildButtons();
        }

        /// <summary>
        ///     Redraw Dialog Tree
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void DialogDetails_DialogChange(object sender, EventArgs e)
        {
            //clean Canvas
            _paintCanvas.Children.Clear();
            //recalculate all Lines
            var dialogtree = TreeProcessing.ConvertToTreeDisplay(Register.DialogTree);
            // Also Sets Register
            TreeProcessing.InitiateTreeDisplay(dialogtree);
            //Draw all Lines again
            DrawLines();
        }

        /// <summary>
        ///     Load and initiate Dialog Tree
        /// </summary>
        /// <param name="dialogtree">Complete Dialog in Tree Form</param>
        private void Initiate(Dictionary<int, Node> dialogtree)
        {
            // Also Sets Register
            TreeProcessing.InitiateTreeDisplay(dialogtree);

            InitiateObjects();

            SetMetrics();

            DrawingBoard();

            PopulateControl();

            DrawLines();

            AddChildButtons();
        }

        /// <summary>
        ///     Clean Panel
        ///     Initiate the Control Collections
        /// </summary>
        private void InitiateObjects()
        {
            AddLevelPanel.Children.Clear();
            AddRowsPanel.Children.Clear();
            DialogPanel.Children.Clear();

            _pointsDct = new Dictionary<int, DialogPoints>(Register.DialogCount);
            _buttonCoordinatesDct = new Dictionary<DialogPoints, DialogPoints>(Register.DialogCount);
            _buttonAddDct = new Dictionary<string, int>(Register.Level);
            _buttonDialogDct = new Dictionary<string, int>(Register.DialogCount);
        }

        /// <summary>
        ///     Set Grid and Canvas
        /// </summary>
        private void SetMetrics()
        {
            //Generate Grid we work on
            _tree = new Grid();
            _tree = ExtendedGrid.ExtendGrid(Register.ColumnCellCount, Register.RowCellCount,
                TreeResources.NodeMargin, TreeResources.NodeMargin,
                true);

            var i = -1;

            //Double the height
            for (var y = 0; y < Register.RowCellCount; y++)
            {
                if (y == 0) i++;

                if (y % 2 == 0) i++;

                //Width
                for (var x = 0; x < Register.ColumnCellCount; x++)
                {
                    if (IsOdd(y)) continue;

                    var pointControls = new DialogPoints(x, i);
                    var pointCoordinate = new DialogPoints(x, y);

                    var myCanvas = new Canvas
                    {
                        Height = TreeResources.NodeMargin,
                        Width = TreeResources.NodeMargin,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };

                    _buttonCoordinatesDct.Add(pointControls, pointCoordinate);

                    Grid.SetRow(myCanvas, y);
                    Grid.SetColumn(myCanvas, x);

                    _tree.Children.Add(myCanvas);
                }
            }

            DialogPanel.Children.Add(_tree);
        }

        /// <summary>
        ///     Add Dialog Buttons, so we can edit Dialog
        /// </summary>
        private void PopulateControl()
        {
            foreach (var leaf in Register.DialogStructure.Values)
            {
                var points = new DialogPoints
                {
                    Xrow = leaf.XValue,
                    Ycolumn = leaf.Level
                };

                var coordinate = _buttonCoordinatesDct[points];

                var bButton = new Button
                {
                    Content = leaf.Id,
                    Name = TreeResources.BtnNameDel + leaf.Id,
                    Height = TreeResources.NodeMargin,
                    Width = TreeResources.NodeMargin,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                bButton.Click += Btn_Click;

                //add item to the catalog
                var point = new DialogPoints
                {
                    Xrow =
                        coordinate.Xrow * TreeResources.NodeMargin +
                        TreeResources.NodeMarginHalf,
                    Ycolumn =
                        coordinate.Ycolumn * TreeResources.NodeMargin +
                        TreeResources.NodeMarginHalf
                };

                _buttonDialogDct.Add(bButton.Name, leaf.Id);
                _pointsDct.Add(leaf.Id, point);

                Grid.SetRow(bButton, coordinate.Ycolumn);
                Grid.SetColumn(bButton, coordinate.Xrow);
                _tree.Children.Add(bButton);
            }
        }

        /// <summary>
        ///     Create Canvas and Add
        /// </summary>
        private void DrawingBoard()
        {
            _paintCanvas = new Canvas
            {
                Background = new SolidColorBrush(Colors.Aqua),
                Height = TreeResources.NodeMargin * Register.RowCellCount,
                Width = TreeResources.NodeMargin * Register.ColumnCellCount,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                SnapsToDevicePixels = true
            };

            Grid.SetRow(_paintCanvas, 0);
            Grid.SetColumn(_paintCanvas, 0);
            Grid.SetRowSpan(_paintCanvas, Register.RowCellCount);
            Grid.SetColumnSpan(_paintCanvas, Register.ColumnCellCount);

            _tree.Children.Add(_paintCanvas);
        }

        /// <summary>
        ///     Just Draw the dependence Lines
        /// </summary>
        private void DrawLines()
        {
            foreach (var node in Register.DialogStructure.Values)
            {
                if (node.ParentId.IsNullOrEmpty()) continue;

                var pointOne = _pointsDct[node.Id];

                foreach (
                    var pointTwo in
                    from id in node.ParentId where _pointsDct.ContainsKey(id) select _pointsDct[id])
                {
                    if (pointOne.Equals(pointTwo))
                    {
                        var circle = new Ellipse
                        {
                            Width = TreeResources.CircleRange,
                            Height = TreeResources.CircleRange,
                            StrokeThickness = TreeResources.StrokeRange,
                            Stroke = Brushes.LightGreen
                        };

                        Canvas.SetLeft(circle, pointOne.Xrow - TreeResources.CanvasPlacement);
                        Canvas.SetTop(circle, pointOne.Ycolumn - TreeResources.CanvasPlacement);

                        _paintCanvas.Children.Add(circle);

                        continue;
                    }

                    var line = new Line
                    {
                        Stroke = Brushes.LightSteelBlue,
                        X1 = pointOne.Xrow,
                        X2 = pointTwo.Xrow,
                        Y1 = pointOne.Ycolumn,
                        Y2 = pointTwo.Ycolumn,
                        StrokeThickness = TreeResources.StrokeRange
                    };

                    _paintCanvas.Children.Add(line);
                }
            }
        }

        /// <summary>
        ///     Add Controls
        ///     User Interactions
        /// </summary>
        private void AddChildButtons()
        {
            //Add Level
            var bLevel = new Button
            {
                Content = TreeResources.ButtonContentAdd,
                Height = TreeResources.ButtonControls,
                Width = TreeResources.ButtonControls,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            bLevel.Click += AddLvlv_Click;

            AddLevelPanel.Children.Add(bLevel);

            var treeAdd = ExtendedGrid.ExtendGrid(1, Register.RowCellCount, TreeResources.NodeMargin,
                TreeResources.NodeMargin, false);

            var j = 0;

            for (var i = 0; i < Register.RowCellCount; i += 2)
            {
                //Add Choice
                var bRows = new Button
                {
                    Name = TreeResources.BtnNameAdd + i,
                    Content = j + 1,
                    Height = TreeResources.ButtonControls,
                    Width = TreeResources.ButtonControls
                };

                j++;

                _buttonAddDct.Add(bRows.Name, j);

                bRows.Click += AddRow_Click;

                Grid.SetRow(bRows, i);
                Grid.SetColumn(bRows, 0);

                treeAdd.Children.Add(bRows);
            }

            AddRowsPanel.Children.Add(treeAdd);
        }

        /// <summary>
        ///     Check if the number is odd or not
        /// </summary>
        /// <param name="value">A number</param>
        /// <returns>If odd or not</returns>
        private static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        ///     Open DialogDetails
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var name = ((Button) sender).Name;

            var id = _buttonDialogDct[name];
            //combine Visual with Data Element
            Register.Cursor = Register.DialogTree[id];

            // Edit Visual and Data Element
            var details = new DialogDetails();
            details.Show();
            Register.DialogTree[id] = Register.Cursor;
        }

        /// <summary>
        ///     Add another Dialog level
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void AddLvlv_Click(object sender, RoutedEventArgs e)
        {
            var id = TreeProcessing.AddLevel();
            DialogEditorHandler.AddElement(id, Register.Level + 1);
            InitiateGui();
        }

        /// <summary>
        ///     Add leaf to the Dialog
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            var name = ((Button) sender).Name;
            var level = _buttonAddDct[name];
            var id = TreeProcessing.AddElement(level);
            DialogEditorHandler.InsertTree(id, level);

            InitiateGui();
        }

        /// <summary>
        ///     Refresh Dialog with a clean slate
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void Btnew_Click(object sender, RoutedEventArgs e)
        {
            DrawGraph();
        }

        /// <summary>
        ///     Open a Dialog
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void Btopen_Click(object sender, RoutedEventArgs e)
        {
            var pathObj = FileIoHandler.HandleFileOpen(TreeResources.DialogDialog,
                Path.Combine(Directory.GetCurrentDirectory(), ArtConst.CampaignsFolder));
            //Empty Return Value
            if (pathObj == null) return;

            var dialog = _diaghandler.LoadDialogObject(pathObj.FilePath);
            DrawGraph(dialog);
        }

        /// <summary>
        ///     Save the Dialog
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void Btsave_Click(object sender, RoutedEventArgs e)
        {
            var pathObj = FileIoHandler.HandleFileSave(TreeResources.DialogDialog,
                Path.Combine(Directory.GetCurrentDirectory(), ArtConst.CampaignsFolder));
            //Empty Return Value
            if (pathObj == null) return;

            _diaghandler.SaveDialogObjects(pathObj.FilePath, Register.DialogTree);
        }
    }
}