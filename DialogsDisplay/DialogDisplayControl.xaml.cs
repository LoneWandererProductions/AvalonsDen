/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/DialogWindow.xaml.cs
 * PURPOSE:     User-control to Display a Dialog  within in the Campaign
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
    ///     Collects all Data and Displays it
    ///     Interaction logic for DialogDisplayControl.xaml
    /// </summary>
    public sealed partial class DialogDisplayControl
    {
        /// <summary>
        ///     The render (readonly). Value: new ImageRender().
        /// </summary>
        private readonly ImageRender _render = new();

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="DialogDisplayControl" /> class.
        /// </summary>
        public DialogDisplayControl()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Try to load an Background Image
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments. in this case the Path </param>
        private void DialogsDisplayProcessing_ReloadImages(object sender, ImagePath e)
        {
            try
            {
                BackroundImage.Source =
                    _render.GetBitmapImageFileStream(Path.Combine(DialogInteractionRegister.PortraitPath,
                        e.ImagePaths));
            }
            catch (ArgumentException ex)
            {
                DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
            }
            catch (NotSupportedException ex)
            {
                DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
            }
            catch (InvalidOperationException ex)
            {
                DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
            }
            catch (IOException ex)
            {
                DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
            }
        }

        /// <summary>
        ///     Set EventHandler for reloading an Image
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void DialogDisplayControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            DialogsDisplayProcessing.ReloadImages += DialogsDisplayProcessing_ReloadImages;
        }
    }
}