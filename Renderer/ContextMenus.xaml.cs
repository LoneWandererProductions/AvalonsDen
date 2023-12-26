/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Renderer/ContextMenus.xaml.cs
 * PURPOSE:     Editor Menu for Tiles
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Debugger;

namespace Renderer
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Class that handles the clicks in the radial Menu
    /// </summary>
    internal sealed partial class ContextMenus
    {
        /// <summary>
        ///     Basic infos we need for the Context Menu
        /// </summary>
        private readonly MCursor _crs;

        /// <summary>
        ///     Id of clicked Button
        /// </summary>
        private Dictionary<string, int> _clickId;

        /// <summary>
        ///     The List of Buttons.
        /// </summary>
        private List<Button> _lstBtn;

        /// <summary>
        ///     The List of Images.
        /// </summary>
        private List<Image> _lstImg;

        /// <inheritdoc />
        /// <summary>
        ///     initiate The whole Stuff
        /// </summary>
        public ContextMenus()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Renderer.ContextMenus" /> class.
        /// </summary>
        /// <param name="crs">Context Menu Infos</param>
        public ContextMenus(MCursor crs)
        {
            _crs = crs;
            InitializeComponent();
        }

        /// <summary>
        ///     Get it from the Outside
        /// </summary>
        internal int ClickId { get; private set; }

        /// <summary>
        ///     Initiate Directories, we need them later.
        /// </summary>
        private void Initiate()
        {
            ////Initiate Buttons
            _clickId = new Dictionary<string, int>(6)
            {
                {RendererResources.BtnIdle, 0},
                {RendererResources.BtnTwo, 1},
                {RendererResources.BtnThree, 2},
                {RendererResources.BtnFour, 3},
                {RendererResources.BtnFive, 4},
                {RendererResources.BtnSix, 5},
                {RendererResources.BtnMiddle, 6}
            };

            //Initiate Buttons
            _lstBtn = new List<Button>(6) {ButtonTwo, ButtonThree, ButtonFour, ButtonFive, ButtonSix, ButtonMiddle};

            //Initiate Images
            _lstImg = new List<Image>(5) {ImageTwo, ImageThree, ImageFour, ImageFive, ImageSix};
        }

        /// <summary>
        ///     Load the background Symbol and the stop button
        /// </summary>
        private void LoadBackground()
        {
            var bitmapCell = CellsImageFileStream.GetImageFileStream(RendererResources.CoreIcons, _crs.Background);
            ImageBase.Source = bitmapCell;
            bitmapCell = CellsImageFileStream.GetImageFileStream(RendererResources.CoreIcons, _crs.Idle);
            ImageIdle.Source = bitmapCell;
        }

        /// <summary>
        ///     Jump Point for every click
        /// </summary>
        /// <param name="sender">Sender of Event</param>
        /// <param name="e">Event Details</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button) sender;
            if (btn == null) return;

            var id = _clickId[btn.Name];

            ClickId = id;

            Close();
        }

        /// <summary>
        ///     put answer to each Button
        ///     5 Buttons max
        ///     quite tricky since we handle orientations
        /// </summary>
        /// <param name="items">Menu Items</param>
        /// <param name="orientation">Clockwise true</param>
        /// <param name="centerbutton">Center Menu active</param>
        internal void GeneratePossibilities(List<MenuItems> items, bool orientation, bool centerbutton)
        {
            //Load Background and Idle
            LoadBackground();
            Initiate();
            //Set Button Inactive
            SetButtonInactive(items.Count, orientation, centerbutton);

            if (items.Count > 5)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorMenuOverflow, items.Count), ErCode.Error);
                return;
            }

            //Get it in order just in Case
            items = items.OrderBy(x => x.Position).ToList();

            // Reverse order against clockwise.
            if (!orientation)
            {
                _lstBtn.Reverse();
                _lstImg.Reverse();
            }

            for (var i = 0; i < items.Count; i++)
            {
                //Load Tool-tip
                var chartoolTip = new ToolTip {Content = items[i].Tooltip};
                ToolTipService.SetShowDuration(ButtonTwo, 2000);

                //load Button, handle idle and middle button
                var btn = !orientation ? _lstBtn[i + 1] : _lstBtn[i];

                //Fill Button
                btn.ToolTip = chartoolTip;

                //Load Image
                var bitmapCell =
                    CellsImageFileStream.GetImageFileStream(RendererResources.CoreIcons, items[i].ImagePath);

                //load Image
                var img = _lstImg[i];

                //Fill Image
                img.Source = bitmapCell;
            }

            // Reverse order against clockwise.
            if (orientation) return;

            //else
            _lstBtn.Reverse();
            _lstImg.Reverse();
        }

        /// <summary>
        ///     Activate and deactivate Specific Buttons that are active
        /// </summary>
        /// <param name="count">Number of Menu Entries</param>
        /// <param name="orientation">Clockwise true</param>
        /// <param name="centerbutton">Center Menu active</param>
        private void SetButtonInactive(int count, bool orientation, bool centerbutton)
        {
            //Wrong entry, deactivate most of it
            if (count > 5)
                for (var i = 1; i <= 5; i++)
                    _lstBtn[i].IsEnabled = false;

            //Specify active Buttons
            for (var i = 0; i <= 5; i++)
                //left
                if (orientation)
                    _lstBtn[i].IsEnabled = i < count;
                //right, max is five deduce the count
                else
                    _lstBtn[i].IsEnabled = i >= 5 - count;

            //Center Menu active or not?
            _lstBtn[^1].IsEnabled = centerbutton;
        }
    }
}