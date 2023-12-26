/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DialogsDisplay/CampaignDialog.xaml.cs
 * PURPOSE:     Window to Display a Dialog  within in the Campaign
 * PROGRAMER:   Peter Geinitz (Wayfarer) (Peter Geinitz)
 * SOURCES:     https://docs.microsoft.com/de-de/dotnet/api/system.windows.threading.dispatcher?view=netframework-4.8
 */

// ReSharper disable MemberCanBeInternal

using System;
using System.Windows;
using System.Windows.Threading;

namespace DialogsDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Interaction logic for CampaignDialog.xaml
    ///     Called by Campaigns
    ///     Here we collect all Controls to Display the Dialog
    /// </summary>
    public sealed partial class DialogWindow
    {
        /// <inheritdoc />
        /// <summary>
        ///     Only ins use for Designer
        /// </summary>
        public DialogWindow()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initiate the Window and Load it via multiple Threads
        ///     Dispatcher Class is used for UI Elements
        /// </summary>
        /// <param name="dialogName">Name of the Dialog</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        public DialogWindow(string campaignName, string mapName, string dialogName)
        {
            InitializeComponent();

            //Makes sense to use UI Thread.
            Dispatcher?.BeginInvoke(DispatcherPriority.Loaded,
                new Action(() => FillMyDialog(campaignName, mapName, dialogName)));
        }

        /// <summary>
        ///     Initiate the Dialog Engine and the Feedback for Close
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="dialogName">Name of the Dialog</param>
        private void FillMyDialog(string campaignName, string mapName, string dialogName)
        {
            DialogsDisplayProcessing.DialogClose += CampaignDialog_DialogClose;
            DialogsDisplayProcessing.StartDialog(campaignName, mapName, dialogName);
        }

        /// <summary>
        ///     Just close the Dialog Window
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void CampaignDialog_DialogClose(object sender, EventArgs e)
        {
            Close();
        }
    }
}