/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/LootScreen.xaml.cs
 * PURPOSE:     Just Display our Loot Window
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Imaging;
using Resources;

namespace ItemExchange
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Basic Loot Screen
    /// </summary>
    public sealed partial class LootScreen
    {
        /// <summary>
        ///     The drag object.
        /// </summary>
        private UIElement _dragObject;

        /// <summary>
        ///     The Image render Interface
        /// </summary>
        private ImageRender _render;

        public LootScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Sets the data.
        /// </summary>
        /// <param name="loot">The loot.</param>
        internal void SetData(Dictionary<int, LootingItemView> loot)
        {
            LootRegister.ImagePaths = new Dictionary<int, string>();
            foreach (var image in loot)
                if (!string.IsNullOrEmpty(image.Value.Image))
                    LootRegister.ImagePaths.Add(image.Key, image.Value.Image);

            LootRegister.Loot = loot;

            Initiate();
        }

        /// <summary>
        ///     Initiates this instance.
        /// </summary>
        private void Initiate()
        {
            //describe the Cube
            StackExchange.GenerateCubes(5, 4, LootResources.Cell, LootResources.Splitter, LootResources.Row);

            if (LootRegister.Loot == null || LootRegister.ImagePaths == null) return;

            StackExchange.Initiate(LootRegister.Loot);

            _render = new ImageRender();

            foreach (var item in LootRegister.Loot.Where(item => item.Value.Id != -1))
                //          Item Position, Item Id, Amount
                ChangeImage(item.Key, item.Value.Id, item.Value.Amount);
        }

        /// <summary>
        ///     The user CTL preview mouse down.
        ///     TODO get something more nice looking
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse button event arguments.</param>
        private void CanvasMain_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(CanvasMain);

            //needed since we need something to hold the Click, in the future something like a hand?
            _dragObject = new UserControl
            {
                Background = Brushes.Blue, // <-- added this so I can see it
                Width = 20,
                Height = 20
            };

            Canvas.SetTop(_dragObject, point.Y);
            Canvas.SetLeft(_dragObject, point.X);

            var str = StackExchange.GetCell((int)point.X, (int)point.Y);
            var cubeId = StackExchange.GetId(point.X, point.Y);
            var tile = (Tile)FindName(str);

            if (tile == null) return;

            //should never happen!
            if (cubeId == -1)
            {
                //Todo add some Error Logging, this should in Theorie never happen.
                LootRegister.ItemSelected = -1;
                return;
            }

            LootRegister.TileSelected = str;
            LootRegister.ItemSelected = cubeId;

            CanvasMain.Children.Add(_dragObject);

            tile.SlcImage = LootResources.SelectionImage;
        }

        /// <summary>
        ///     The canvas main preview mouse move.
        ///     Custom Cursor
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void CanvasMain_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragObject == null) return;

            var point = Mouse.GetPosition(CanvasMain);

            Canvas.SetTop(_dragObject, point.Y);
            Canvas.SetLeft(_dragObject, point.X);
        }

        /// <summary>
        ///     The canvas main preview mouse up.
        ///     TODO add a snappy scroll back Item
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse button event arguments.</param>
        private void CanvasMain_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //clean Cursor
            _dragObject = null;
            CanvasMain.ReleaseMouseCapture();

            var point = e.GetPosition(sender as IInputElement);

            //get Name and Id of clicked Cell
            var str = StackExchange.GetCell((int)point.X, (int)point.Y);
            var id = StackExchange.GetId(point.X, point.Y);

            var tileNew = (Tile)FindName(str);

            //not found? Return.
            if (tileNew == null) return;

            //clean Cursor completly
            CanvasMain.Children.Clear();

            //Load clicked Cells old and new
            tileNew.SlcImage = LootResources.SelectionImage;
            //Get infos for the old Tile
            var tileOld = (Tile)FindName(LootRegister.TileSelected);

            //clean up of the error case should never happen, if we did not get the old Cell
            if (LootRegister.ItemSelected == -1 || id == -1)
            {
                LootRegister.Clear();

                if (tileOld != null) tileOld.SlcImage = LootResources.BlankImage;

                return;
            }

            tileOld?.Refresh();

            //Move the Items internal in the Dictionary
            StackExchange.Move(LootRegister.ItemSelected, id);

            //get the two tiles I need the items in the Tile refresh the Images
            var cubeOld = StackExchange.Cubes[LootRegister.TileSelected].Id;
            var item = StackExchange.Items[cubeOld];
            ChangeImage(cubeOld, item.Id, item.Amount);

            var cubeNew = StackExchange.Cubes[str].Id;
            item = StackExchange.Items[cubeNew];
            ChangeImage(cubeNew, item.Id, item.Amount);

            //Cleanup
            LootRegister.Clear();
        }

        /// <summary>
        ///     Changes the image.
        /// </summary>
        /// <param name="itemPosition">The item position.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="amount">The Amount of Items</param>
        private void ChangeImage(int itemPosition, int itemId, int amount)
        {
            var cellName = string.Empty;

            foreach (var cube in StackExchange.Cubes.Where(cube => cube.Value.Id == itemPosition)) cellName = cube.Key;

            var tile = (Tile)FindName(cellName);

            if (tile == null) return;

            tile.ItemCounter = amount.ToString();

            string path;

            //Empty Image
            if (itemId == -1 || amount == 0)
                path = LootResources.BlankImage;
            else if (LootRegister.ImagePaths.ContainsKey(itemId))
                path = Path.Combine(Directory.GetCurrentDirectory(), LootResources.ImagePath,
                    LootRegister.ImagePaths[itemId]);
            else
                // a real Image
                path = LootResources.ErrorImage;
            //Todo add some Error Logging
            tile.ImageItem.Source = _render.GetBitmapImageFileStream(path);

            tile.Refresh();
        }
    }
}