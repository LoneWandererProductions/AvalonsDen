/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/SaveGame.xaml.cs
 * PURPOSE:     Save Game Window
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;
using CampaignDriver;
using CommonControls;
using Loader;
using Resources;

namespace Campaigns
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Saves a game
    /// </summary>
    internal sealed partial class SaveGame
    {
        /// <summary>
        ///     Module that handles Saving
        ///     Also saves Changes to the Events and Dialogs etc
        /// </summary>
        private static readonly SaveGameHandle SaveHandle = new();

        /// <inheritdoc />
        /// <summary>
        ///     Load and Handle Save Games
        /// </summary>
        internal SaveGame()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Load the Save files
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void SaveGame_Loaded(object sender, RoutedEventArgs e)
        {
            ListBoxSave.DataCollection = SaveHandle.GetSaveFiles();
        }

        /// <summary>
        ///     Close the mess
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Type of event</param>
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Create a new save, ask if we want to Overwrite
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var name = TextBoxAddSave.Text;

            if (string.IsNullOrEmpty(name)) return;

            var saveInfos = CampaignsHelper.GenerateSave(name);

            //if (!ListBoxSave.IsItemInUse(name))
            //{
            //    GenerateSave(saveInfos, false);
            //    return;
            //}

            if (
                MessageBox.Show(CampaignsStringResource.SaveQuestion, CampaignsStringResource.SaveHeader,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) ==
                MessageBoxResult.Yes)
                GenerateSave(saveInfos, true);
        }

        /// <summary>
        ///     Delete Save Game
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            //SaveHandle.DeleteSaveFile(ListBoxSave.DeleteItems(ListBoxSave.SelectedItem));
        }

        /// <summary>
        ///     Generate a Save File
        /// </summary>
        /// <param name="saveInfos">Save Object</param>
        /// <param name="overwrite">Do we overwrite existing Element</param>
        private void GenerateSave(SaveInfos saveInfos, bool overwrite)
        {
            //if (!overwrite) ListBoxSave.AddItem(saveInfos.SaveName);

            //get Handler
            var cpn = HandlerOutputSingleton.Create();

            //save everything we have
            cpn.SetAutosave(saveInfos.CampaignName, saveInfos.MapName, CampaignsRegister.PartyInventory);

            SaveHandle.CreateSaveFile(saveInfos);
        }

        /// <summary>
        ///     Clean the Campaign Autosave Folder
        /// </summary>
        /// <param name="campaignName">Name of the Campaign</param>
        internal static void Cleanup(string campaignName)
        {
            SaveHandle.DeleteAutoSaveFile(campaignName);
        }

        /// <summary>
        ///     The list box load item selection changed.
        /// </summary>
        /// <param name="item">The DataItem.</param>
        private void ListBoxSave_ItemSelectionChanged(DataItem item)
        {
            TextBoxAddSave.Text = item.Name;
        }
    }
}