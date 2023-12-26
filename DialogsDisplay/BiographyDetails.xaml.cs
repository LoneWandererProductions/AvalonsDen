/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/BiographyDetails.xaml.cs
 * PURPOSE:     Display all the Character Details
 * PROGRAMER:   Peter Geinitz (Wayfarer) (Peter Geinitz)
 */

// ReSharper disable MemberCanBeInternal

using System;
using System.IO;
using System.Windows;
using CommonControls;
using Debugger;
using Imaging;

namespace DialogsDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The biography details class.
    /// </summary>
    public sealed partial class BiographyDetails
    {
        /// <summary>
        ///     The full image (readonly).
        /// </summary>
        private readonly string _fullImage;

        /// <summary>
        ///     The render (readonly). Value: new ImageRender().
        /// </summary>
        private readonly ImageRender _render = new();

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:DialogsDisplay.BiographyDetails" /> class.
        /// </summary>
        /// <param name="charInfo">The charInfo.</param>
        public BiographyDetails(CharInfo charInfo)
        {
            InitializeComponent();
            DataContext = charInfo;

            if (!string.IsNullOrEmpty(charInfo.FullImage)) _fullImage = charInfo.FullImage;
        }

        /// <summary>
        ///     Try to load Background Image
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_fullImage == null)
            {
                DebugLog.CreateLogFile(DialogsDisplayResources.ErrorImageNotSet, ErCode.Error);
                return;
            }

            try
            {
                FullImage.Source = _render.GetBitmapImageFileStream(Path.Combine(Directory.GetCurrentDirectory(),
                    DialogsDisplayResources.ImageCharacterFolder, _fullImage));
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