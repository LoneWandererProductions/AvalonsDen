/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Chapter/LoadGame.xaml.cs
 * PURPOSE:     Load a Game
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;
using Campaigns;
using Loader;
using Resources;

namespace Chapter
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Loads Game for Campaign
    /// </summary>
    internal sealed partial class LoadGame
    {
        /// <summary>
        ///     The handle.
        /// </summary>
        private SaveGameHandle _handle;

        /// <inheritdoc />
        /// <summary>
        ///     Simple Loader for the Save Files
        /// </summary>
        internal LoadGame()
        {
            InitializeComponent();
            _handle = new SaveGameHandle();
            ListBoxSaves.DataCollection = _handle.GetSaveFiles();
        }

        /// <summary>
        ///     Go back to Index
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void ButtonFlipPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Index());
        }

        /// <summary>
        ///     Delete Save Game
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            _handle = new SaveGameHandle();
            //_handle.DeleteSaveFile(ListBoxSaves.DeleteItems(ListBoxSaves.SelectedItem));
        }

        /// <summary>
        ///     Load Save Game
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            //Get all the needed Data
            _handle = new SaveGameHandle();
            SaveInfos save = null; //_handle.LoadSaveFile(ListBoxSaves.GetName(ListBoxSaves.SelectedItem));
            if (save == null) return;

            //run this shit show
            var campaign = new CampaignsHandler();
            Switcher.Chapters.Hide();
            campaign.LoadCampaign(save);
            //Get Back to the old Window
            Switcher.Switch(new Index());
            //Show Campaign Window
            Switcher.Chapters.Show();
        }
    }
}