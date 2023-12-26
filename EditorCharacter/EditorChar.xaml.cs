/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorCharacter/EditorDalog.xaml.cs
 * PURPOSE:     Editor Window, Helps Generating Dialogs
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Windows;
using Debugger;

namespace EditorCharacter
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Edit and Create new Characters
    /// </summary>
    public sealed partial class EditorChar
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EditorCharacter.EditorChar" /> class.
        /// </summary>
        public EditorChar()
        {
            InitializeComponent();
            DebugLog.CreateLogFile(EditorCharacterResources.InformationLoad, ErCode.Information);
        }
    }
}