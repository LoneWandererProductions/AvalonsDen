/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterEngine/CharacterBiography.cs
 * PURPOSE:     Simple Helper Class to describe Participants for an dialog
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     The character biography class.
    /// </summary>
    public sealed class CharacterBiography : ObservableObject
    {
        /// <summary>
        ///     Standard alignments
        /// </summary>
        public enum Alignments
        {
            /// <summary>
            ///     The Lawful good = 0.
            /// </summary>
            LawfulGood = 0,

            /// <summary>
            ///     The Neutral good = 1.
            /// </summary>
            NeutralGood = 1,

            /// <summary>
            ///     The ChaoticGood = 2.
            /// </summary>
            ChaoticGood = 2,

            /// <summary>
            ///     The Lawful neutral = 3.
            /// </summary>
            LawfulNeutral = 3,

            /// <summary>
            ///     The TrueNeutral = 4.
            /// </summary>
            TrueNeutral = 4,

            /// <summary>
            ///     The Chaotic neutral = 5.
            /// </summary>
            ChaoticNeutral = 5,

            /// <summary>
            ///     The Lawful evil = 6.
            /// </summary>
            LawfulEvil = 6,

            /// <summary>
            ///     The Neutral evil = 7.
            /// </summary>
            NeutralEvil = 7,

            /// <summary>
            ///     The Chaotic evil = 8.
            /// </summary>
            ChaoticEvil = 8
        }

        /// <summary>
        ///     Describes Type of NPC
        /// </summary>
        public enum TypeIds
        {
            /// <summary>
            ///     The Player = 0.
            /// </summary>
            Player = 0,

            /// <summary>
            ///     The PartyMember = 1.
            /// </summary>
            PartyMember = 1,

            /// <summary>
            ///     The Npc = 2.
            /// </summary>
            Npc = 2
        }

        /// <summary>
        ///     The alignment.
        /// </summary>
        private Alignments _alignment;

        /// <summary>
        ///     The biography.
        /// </summary>
        private string _biography;

        /// <summary>
        ///     The faction.
        /// </summary>
        private string _faction;

        /// <summary>
        ///     The full image.
        /// </summary>
        private string _fullImage;

        /// <summary>
        ///     The id.
        /// </summary>
        private int _id;

        /// <summary>
        ///     The image.
        /// </summary>
        private string _image;

        /// <summary>
        ///     The is essential.
        /// </summary>
        private bool _isEssential;

        /// <summary>
        ///     The name.
        /// </summary>
        private string _name;

        /// <summary>
        ///     The npc.
        /// </summary>
        private bool _npc;

        /// <summary>
        ///     The type id.
        /// </summary>
        private TypeIds _typeId;

        /// <summary>
        ///     Id of Character
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChangedEvent(nameof(Id));
            }
        }

        /// <summary>
        ///     Is NPC or PC
        /// </summary>
        public bool Npc
        {
            get => _npc;
            set
            {
                _npc = value;
                RaisePropertyChangedEvent(nameof(Npc));
            }
        }

        /// <summary>
        ///     string path to Image
        ///     Perhaps in the future I might add special emotion Pic
        /// </summary>
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChangedEvent(nameof(Image));
            }
        }

        /// <summary>
        ///     Full body Image for Character Details
        /// </summary>
        public string FullImage
        {
            get => _fullImage;
            set
            {
                _fullImage = value;
                RaisePropertyChangedEvent(nameof(FullImage));
            }
        }

        /// <summary>
        ///     Campaign specific Trigger
        ///     Shared Parameter
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChangedEvent(nameof(Name));
            }
        }

        /// <summary>
        ///     Short Character Description
        ///     Shared Parameter
        /// </summary>
        public string Biography
        {
            get => _biography;
            set
            {
                _biography = value;
                RaisePropertyChangedEvent(nameof(Biography));
            }
        }

        /// <summary>
        ///     Faction Specific not yet Implemented
        /// </summary>
        public string Faction
        {
            get => _faction;
            set
            {
                _faction = value;
                RaisePropertyChangedEvent(nameof(Faction));
            }
        }

        /// <summary>
        ///     123
        ///     456
        ///     789
        ///     lawful, chaotic
        ///     good, evil
        /// </summary>
        public Alignments Alignment
        {
            get => _alignment;
            set
            {
                _alignment = value;
                RaisePropertyChangedEvent(nameof(Alignment));
            }
        }

        /// <summary>
        ///     Player 0,
        ///     Party member 1,
        ///     NPC 2
        /// </summary>
        public TypeIds TypeId
        {
            get => _typeId;
            set
            {
                _typeId = value;
                RaisePropertyChangedEvent(nameof(TypeId));
            }
        }

        /// <summary>
        ///     allowed to kill?
        /// </summary>
        public bool IsEssential
        {
            get => _isEssential;
            set
            {
                _isEssential = value;
                RaisePropertyChangedEvent(nameof(IsEssential));
            }
        }
    }
}