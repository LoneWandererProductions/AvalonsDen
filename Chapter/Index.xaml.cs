/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Chapter/Index.xaml.cs
 * PURPOSE:     Start Menu
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Campaigns;
using Debugger;
using Resources;

namespace Chapter
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Start Class that controls it all
    /// </summary>
    internal sealed partial class Index
    {
        /// <summary>
        ///     My selection.
        /// </summary>
        private string _selection;

        /// <inheritdoc />
        /// <summary>
        ///     Central Control place for all Actions
        /// </summary>
        internal Index()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Sets the campaigns.
        /// </summary>
        public static Dictionary<string, CampaignManifest> Campaigns { private get; set; } =
            new();

        /// <summary>
        ///     Opens Editor Window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void EditorButton_Click(object sender, RoutedEventArgs e)
        {
            var path = Directory.GetCurrentDirectory();
            // Use ProcessStartInfo class.
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = path + ChapterResource.AppStartup,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = ChapterResource.ArgumentsNone
            };

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using var exeProcess = Process.Start(startInfo);
                Switcher.Chapters.Hide();
                exeProcess?.WaitForExit();
                Switcher.Chapters.Show();
            }
            catch (Exception exception)
            {
                //TODO Implement a Subclass of AvalonsDen Specific Exceptions
                DebugLog.CreateLogFile(exception.ToString(), 0);
            }
        }

        /// <summary>
        ///     Elaborate Name for credits
        ///     shoot me I have an ego
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void License_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new License());
        }

        /// <summary>
        ///     Thank Sayings and Authors, the usual
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Authors_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Authors());
        }

        /// <summary>
        ///     Save Game Loader
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Bookmark_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LoadGame());
        }

        /// <summary>
        ///     Opens Campaign Window
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void World_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in Campaigns.Keys) ListBoxCampaign.Items.Add(item);
        }

        /// <summary>
        ///     Selected a Campaign
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void LstBoxCampaign_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if we haven't selected anything or a campaign is running return
            if (ListBoxCampaign.SelectedItem == null) return;

            //else load Manifest and try to display campaign
            _selection = ListBoxCampaign.SelectedItem.ToString();
            var selectedCampaign = Campaigns[_selection];

            DisplayCampaign(selectedCampaign);
        }

        /// <summary>
        ///     Loads all Files needed for the Campaign
        /// </summary>
        /// <param name="myselectedCampaignManifest">Campaign description and Campaign specific data</param>
        private static void DisplayCampaign(CampaignManifest myselectedCampaignManifest)
        {
            var campaign = new CampaignsHandler();

            CampaignsHandler.FinishMessage += Campaign_OnLoaded;

            Switcher.Chapters.Hide();
            try
            {
                campaign.StartCampaign(myselectedCampaignManifest);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                Switcher.Chapters.Show();
                DebugLog.CreateDump();
            }
        }

        /// <summary>
        ///     Close the Whole mess, that's all folks
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Chapters.Close();
        }

        /// <summary>
        ///     The campaign on loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void Campaign_OnLoaded(object sender, string e)
        {
            Switcher.Chapters.Show();
        }
    }
}