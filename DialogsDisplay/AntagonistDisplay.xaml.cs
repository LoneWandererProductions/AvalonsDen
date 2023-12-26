/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/DialogControl.xaml.cs
 * PURPOSE:     User-control to Display Character Biography
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable MemberCanBeInternal

using System;
using System.IO;
using System.Windows;
using Debugger;
using Imaging;

namespace DialogsDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Display Portrait and offer the Biography
    /// </summary>
    public sealed partial class AntagonistDisplay
    {
        /// <summary>
        ///     The render (readonly). Value: new ImageRender().
        /// </summary>
        private readonly ImageRender _render = new();

        /// <summary>
        ///     The char info.
        /// </summary>
        private CharInfo _charInfo;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DialogsDisplay.AntagonistDisplay" /> class.
        /// </summary>
        public AntagonistDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Show the Details
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_charInfo.Name)) return;

            var details = new BiographyDetails(_charInfo) { Topmost = true };
            details.ShowDialog();
        }

        /// <summary>
        ///     Initiate the basic Stuff
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new CharInfo();
            DialogsDisplayProcessing.AntagonistDisplay += DialogsDisplayProcessing_AntagonistDisplay;
        }

        /// <summary>
        ///     Load Portrait
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="charInfo">The routed event arguments.</param>
        private void DialogsDisplayProcessing_AntagonistDisplay(object sender, CharInfo charInfo)
        {
            _charInfo = charInfo;
            DataContext = charInfo;

            try
            {
                AntagonistImage.Source =
                    _render.GetBitmapImageFileStream(Path.Combine(DialogInteractionRegister.PortraitPath,
                        _charInfo.Image));
            }
            catch (ArgumentException ex)
            {
                DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
            }
            catch (NotSupportedException ex)
            {
                DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
            }
            catch (IOException ex)
            {
                DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
            }
            catch (InvalidOperationException ex)
            {
                DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
            }
        }
    }
}