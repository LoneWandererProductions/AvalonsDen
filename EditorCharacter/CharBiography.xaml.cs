/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorCharacter/CharBiography.xaml.cs
 * PURPOSE:     Basic Npc Player Stats
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using AvalonRuntime;
using CharacterEngine;
using CommonControls;
using Debugger;
using ExtendedSystemObjects;
using FileHandler;
using Resources;

namespace EditorCharacter
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     TODO Basic logic checks like id and NPC, PC
    /// </summary>
    internal sealed partial class CharBiography
    {
        /// <summary>
        ///     The char handler (readonly). Value: new EditorCharacterHandler().
        /// </summary>
        private readonly EditorCharacterHandler _charHandler = new();

        /// <summary>
        ///     The characters Bio.
        /// </summary>
        private CharacterBiography _charBio;

        /// <summary>
        ///     The characters stats.
        /// </summary>
        private CharacterBaseStats _charStats;

        /// <summary>
        ///     The found characters.
        /// </summary>
        private Dictionary<string, string> _foundCharDct;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorCharacter.CharBiography" /> class.
        /// </summary>
        internal CharBiography()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     On Startup Initiate the most basic stuff
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitiateItems();
        }

        /// <summary>
        ///     Create new Character
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            InitiateItems();
        }

        /// <summary>
        ///     Load a Character
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            var pathObj = FileIoHandler.HandleFileOpen(EditorCharacterResources.CharacterBiographyDialog,
                Directory.GetCurrentDirectory());

            if (pathObj == null) return;

            LoadBio(pathObj.FilePath);
        }

        /// <summary>
        ///     Save all Created and or Changed Data
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var pathObj = FileIoHandler.HandleFileSave(EditorCharacterResources.CharacterBiographyDialog,
                Directory.GetCurrentDirectory());
            if (pathObj == null) return;

            var path = PathInformation.GetPathWithoutExtension(pathObj.FilePath);

            if (_charBio == null)
            {
                DebugLog.CreateLogFile(string.Concat(EditorCharacterResources.ErrorNoCharactersFound, path),
                    ErCode.Warning);
                ScrollTxtBoxInfo.AppendText(string.Concat(EditorCharacterResources.ErrorNoCharactersFound,
                    Environment.NewLine));
                return;
            }

            if (_charBio.Npc) _charStats = CtrlStats.GetStats();

            var check = _charHandler.SaveCharacter(_charStats, path + ArtConst.CharacterStatsExt);

            if (check)
            {
                DebugLog.CreateLogFile(EditorCharacterResources.CharacterStatsSaved, ErCode.Information);
                ScrollTxtBoxInfo.AppendText(string.Concat(EditorCharacterResources.CharacterStatsSaved,
                    Environment.NewLine));
            }
            else
            {
                DebugLog.CreateLogFile(EditorCharacterResources.ErrorCharacter, ErCode.Warning);
                ScrollTxtBoxInfo.AppendText(string.Concat(EditorCharacterResources.ErrorCharacter,
                    Environment.NewLine));
            }

            check = _charHandler.SaveCharacter(_charBio, Path.Combine(path, ArtConst.CharacterBiographyExt));

            if (check)
            {
                DebugLog.CreateLogFile(string.Concat(EditorCharacterResources.CharacterBioSaved, path),
                    ErCode.Information);
                ScrollTxtBoxInfo.AppendText(string.Concat(EditorCharacterResources.CharacterBioSaved,
                    Environment.NewLine));
            }
            else
            {
                DebugLog.CreateLogFile(EditorCharacterResources.ErrorCharacter, ErCode.Warning);
                ScrollTxtBoxInfo.AppendText(string.Concat(EditorCharacterResources.ErrorCharacter,
                    Environment.NewLine));
            }
        }

        /// <summary>
        ///     Try to load an overview with all Characters
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnOverView_Click(object sender, RoutedEventArgs e)
        {
            var path = FileIoHandler.ShowFolder(Directory.GetCurrentDirectory());

            if (string.IsNullOrEmpty(path)) return;

            _foundCharDct = _charHandler.GetCharacters(path);

            if (_foundCharDct.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(string.Concat(EditorCharacterResources.ErrorNoCharactersFound, path),
                    ErCode.Warning);
                ScrollTxtBoxInfo.AppendText(string.Concat(EditorCharacterResources.ErrorNoCharactersFound,
                    Environment.NewLine));
            }

            //ListBoxLoad.Collection = _foundCharDct.Keys.ToList();
        }

        /// <summary>
        ///     Load Character Data
        /// </summary>
        /// <param name="path">Target path</param>
        private void LoadBio(string path)
        {
            var sheet = _charHandler.LoadCharacter(path);

            if (sheet.Bio == null)
            {
                DebugLog.CreateLogFile(EditorCharacterResources.WarningCouldNotLoadCharacter, ErCode.Warning);
                ScrollTxtBoxInfo.AppendText(string.Concat(EditorCharacterResources.WarningCouldNotLoadCharacter,
                    Environment.NewLine));
                return;
            }

            _charBio = sheet.Bio;
            DataContext = _charBio;
            _charStats = sheet.Stats;
            CtrlStats.SetStats(_charStats);
        }

        /// <summary>
        ///     Initiate the Basic Observable Objects
        /// </summary>
        private void InitiateItems()
        {
            _charBio = new CharacterBiography();
            DataContext = _charBio;
            _charStats = new CharacterBaseStats();
            CtrlStats.SetStats(_charStats);
        }

        /// <summary>
        ///     Switch between Npc and Player Character
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void ChkbNpc_Checked(object sender, RoutedEventArgs e)
        {
            if (ChkbNpc.IsChecked != true) return;

            _charStats = new CharacterBaseStats();
            CtrlStats.SetStats(_charStats);
        }

        /// <summary>
        ///     The list box load item selection changed.
        /// </summary>
        /// <param name="item">The DataItem.</param>
        private void ListBoxLoad_ItemSelectionChanged(DataItem item)
        {
            if (!_foundCharDct.ContainsKey(item.Name)) return;

            var path = _foundCharDct[item.Name];
            LoadBio(path);
        }
    }
}