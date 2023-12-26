/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/ItemExchange/Tile.xaml.cs
 * PURPOSE:     Basic Graphic Tile
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;
using Imaging;

namespace ItemExchange
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Basic Tile Item for Graphic display
    /// </summary>
    /// <seealso cref="T:System.Windows.Controls.UserControl" />
    /// <seealso cref="T:System.Windows.Markup.IComponentConnector" />
    public sealed partial class Tile
    {
        /// <summary>
        ///     The image path (readonly) for the Selection. Value: DependencyProperty.Register(nameof(SelectionImage),
        ///     typeof(string), typeof(Slot), null).
        /// </summary>
        public static readonly DependencyProperty SelectionImage = DependencyProperty.Register(nameof(SelectionImage),
            typeof(string),
            typeof(Tile), null);

        private ImageRender _render;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ItemExchange.Tile" /> class.
        /// </summary>
        public Tile()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets the image.
        /// </summary>
        /// <value>
        ///     The image.
        /// </value>
        public string ItmImage { get; set; }

        /// <summary>
        ///     Gets or sets the selection image.
        /// </summary>
        /// <value>
        ///     The SLC image.
        /// </value>
        public string SlcImage
        {
            get => (string)GetValue(SelectionImage);
            set => SetValue(SelectionImage, value);
        }

        /// <summary>
        ///     Gets or sets the item counter.
        /// </summary>
        /// <value>
        ///     The item counter.
        /// </value>
        public string ItemCounter { get; set; }

        /// <summary>
        ///     Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            LblNumber.Content = ItemCounter == "0" ? string.Empty : ItemCounter;
            if (ItmImage == null) return;

            ImageItem.Source = _render.GetBitmapImageFileStream(ItmImage);
            ImageSelection.Source = _render.GetBitmapImageFileStream(SlcImage);
        }

        /// <summary>
        ///     Handles the Loaded event of the Tile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Tile_Loaded(object sender, RoutedEventArgs e)
        {
            _render = new ImageRender();
            ImageBackground.Source = _render.GetBitmapImageFileStream(LootResources.BackroundImage);
        }
    }
}