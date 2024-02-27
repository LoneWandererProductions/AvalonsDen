/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        EditorView/EditorCampaigns.xaml.cs
 * PURPOSE:     Helper Class To Create a Campaign Framework:
 *              - Creates the Master File of the Campaign
 *              - Creates the Inventory of the Party
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;
using System.Windows;
using AvalonRuntime;
using CommonControls;
using DatabaseDriver;
using Debugger;
using Loader;
using Resources;

namespace EditorView
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Base Window for Generating Campaigns
    /// </summary>
    public sealed partial class EditorCampaigns
    {
        /// <summary>
        ///     CampaignManifest Object
        /// </summary>
        private CampaignManifest _myCampaign;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorView.EditorCampaigns" /> class.
        /// </summary>
        public EditorCampaigns()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Get everything in Place
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _myCampaign = new CampaignManifest();
            DataContext = _myCampaign;
        }

        /// <summary>
        ///     Close Windows
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void CampaignClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     save Campaign File
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void SaveCampaign_Click(object sender, RoutedEventArgs e)
        {
            SaveCampaignAs();
        }

        /// <summary>
        ///     Load existing Campaign
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void LoadCampaign_Click(object sender, RoutedEventArgs e)
        {
            LoadCampaign();
        }

        /// <summary>
        ///     Save Campaign Manifest
        /// </summary>
        private void SaveCampaignAs()
        {
            var pathObj = FileIoHandler.HandleFileSave(EditorCampaignResources.CampaignManifestDialog,
                Path.Combine(Directory.GetCurrentDirectory(), ArtConst.CampaignsFolder));

            if (pathObj == null) return;

            //Create new Inventory
            var partyInventory = new PartyInventory();

            //finally Save
            EditorSave.SaveCampaign(pathObj.FilePath, _myCampaign, partyInventory);

            //Create DataBase for items
            var rslt = HandlerInputSingleton.Create(pathObj.Folder, _myCampaign.CampaignName);
            var check = rslt.CreateMasterTable();

            if (!check) DebugLog.CreateLogFile(EditorCampaignResources.ErrorCouldNotCreateDb, ErCode.Error);
        }

        /// <summary>
        ///     Load Campaign Manifest
        /// </summary>
        private void LoadCampaign()
        {
            var pathObj = FileIoHandler.HandleFileOpen(EditorCampaignResources.CampaignManifestDialog,
                Path.Combine(Directory.GetCurrentDirectory(), ArtConst.CampaignsFolder));

            if (pathObj != null) DataContext = _myCampaign = WorkLoader.LoadCampaignManifest(pathObj.FilePath);
        }
    }
}