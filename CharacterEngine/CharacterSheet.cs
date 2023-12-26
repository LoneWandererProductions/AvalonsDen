/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/CharacterSheet.cs
 * PURPOSE:     Helper Object that contains all needed Data
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace CharacterEngine
{
    /// <summary>
    ///     The character sheet class.
    /// </summary>
    internal sealed class CharacterSheet
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        internal int Id { get; set; }

        /// <summary>
        ///     Npc's don't have a Stat file
        /// </summary>
        internal bool Npc { get; set; }

        /// <summary>
        ///     We set the Name from the File Name
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        ///     The only Object that will be changed, copy will be saved in Autosave
        /// </summary>
        internal string CharacterBiographyPath { get; set; }

        /// <summary>
        ///     Only needed when we have a PC
        /// </summary>
        internal string CharacterStatsPath { get; set; }
    }
}