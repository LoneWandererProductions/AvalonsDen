/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/Campaign.xaml.cs
 * PURPOSE:     Displays the Visuals
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.ComponentModel;
using System.Windows;

namespace Campaigns
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The campaign class.
    /// </summary>
    internal sealed partial class Campaign
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Campaigns.Campaign" /> class.
        /// </summary>
        /// <param name="myCell">References the Rendering Engine that displays the Board</param>
        internal Campaign(UIElement myCell)
        {
            InitializeComponent();
            Tst.Children.Add(myCell);
        }

        /// <summary>
        ///     Close all open Campaign Stuff
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Parameter</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            CampaignsHandler.CloseCampaign();
        }

        /// <summary>
        ///     Change the Board
        /// </summary>
        /// <param name="myCell">References the Rendering Engine that displays the Board</param>
        public void ChangeCell(UIElement myCell)
        {
            Tst.Children.Clear();
            Tst.Children.Add(myCell);
        }

        /// <summary>
        ///     TODO Improve
        /// </summary>
        public void SetLabel(int time, int day, int year)
        {
            LabelHeader.Content = "Time " + time + " Day " + day + " Year " + year;
        }
    }
}