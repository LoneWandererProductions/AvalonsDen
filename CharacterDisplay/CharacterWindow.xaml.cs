/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CharacterWindow.xaml.cs
 * PURPOSE:     Display Control of Character Value
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CharacterEngine;
using Debugger;
using ExtendedSystemObjects;

namespace CharacterDisplay
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     The character window class.
    /// </summary>
    internal sealed partial class CharacterWindow
    {
        /// <summary>
        ///     The campaign name (readonly).
        /// </summary>
        private readonly string _campaignName;

        /// <summary>
        ///     The characters (readonly).
        /// </summary>
        private readonly List<int> _characters;

        /// <summary>
        ///     The Character Dictionary.
        /// </summary>
        private Dictionary<int, CharacterBundle> _chrDct;

        /// <summary>
        ///     The tab Dictionary.
        /// </summary>
        private Dictionary<TabItem, int> _tabDct;

        /// <inheritdoc />
        /// <summary>
        ///     Generic Initiation
        /// </summary>
        public CharacterWindow()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:CharacterDisplay.CharacterWindow" /> class.
        /// </summary>
        /// <param name="campaignName">The campaignName.</param>
        /// <param name="characters">The characters.</param>
        public CharacterWindow(string campaignName, List<int> characters)
        {
            _campaignName = campaignName;
            _characters = characters;
            InitializeComponent();
        }

        /// <summary>
        ///     Initiate all Data
        ///     We need to create Controls equal to the amount of Characters.
        /// </summary>
        /// <param name="sender">Sender of Event</param>
        /// <param name="e">Event Args </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateCharacterControls(_characters);
            if (_chrDct.IsNullOrEmpty()) return;

            ChrCtrl.SetValues(_chrDct.First().Value);
        }

        /// <summary>
        ///     Generate fitting display for the active group
        /// </summary>
        /// <param name="characters">List of Character Ids</param>
        /// <returns>Fitting Control Group</returns>
        private void GenerateCharacterControls(IReadOnlyCollection<int> characters)
        {
            _tabDct = new Dictionary<TabItem, int>(characters.Count);
            _chrDct = new Dictionary<int, CharacterBundle>(characters.Count);

            foreach (var chr in characters)
            {
                //generate Tab and add our CharacterControl
                var tab = new TabItem();
                var rslt = CharacterDisplayProcessing.GetCharacters(_campaignName, chr);

                if (rslt == null)
                {
                    DebugLog.CreateLogFile(CharacterResources.ErrorCouldNotLoadCharacter, ErCode.Error);
                    continue;
                }

                tab.Header = rslt.Bio.Name;

                _tabDct.Add(tab, chr);
                _chrDct.Add(chr, rslt);

                //Add to tabControl
                TbCtrlCharacter.Items.Add(tab);
            }
        }

        /// <summary>
        ///     Not elegant but works TODO replace with custom Control
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The selection changed event arguments.</param>
        private void TbCtrlCharacter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabControl = sender as TabControl;

            if (!(tabControl?.SelectedItem is TabItem tb)) return;

            var id = _tabDct[tb];
            ChrCtrl.SetValues(_chrDct[id]);
        }
    }
}