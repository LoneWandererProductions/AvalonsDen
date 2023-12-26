/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/CampaignManifest.cs
 * PURPOSE:     Description of the Campaign Files
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     Describes the basic Files needed for a campaign
    /// </summary>
    public sealed class CampaignManifest : ObservableObject
    {
        /// <summary>
        ///     The campaign description.
        /// </summary>
        private string _campaignDescription;

        /// <summary>
        ///     The campaign name.
        /// </summary>
        private string _campaignName;

        /// <summary>
        ///     The character.
        /// </summary>
        private int _character;

        /// <summary>
        ///     The character id.
        /// </summary>
        private int _characterId;

        /// <summary>
        ///     The start map.
        /// </summary>
        private string _startMap;

        /// <summary>
        ///     The start point.
        /// </summary>
        private int _startPoint;

        /// <summary>
        ///     The start time.
        /// </summary>
        private int _startTime;

        /// <summary>
        ///     The tile Dictionary.
        /// </summary>
        private string _tileDictionary;

        /// <summary>
        ///     Name of the Campaign as seen on the Selection screen
        /// </summary>
        public string CampaignName
        {
            get => _campaignName;
            set
            {
                _campaignName = value;
                RaisePropertyChangedEvent(nameof(CampaignName));
            }
        }

        /// <summary>
        ///     Description of the Campaign
        /// </summary>
        public string CampaignDescription
        {
            get => _campaignDescription;
            set
            {
                _campaignDescription = value;
                RaisePropertyChangedEvent(nameof(CampaignDescription));
            }
        }

        /// <summary>
        ///     Name of the StartMap
        /// </summary>
        public string StartMap
        {
            get => _startMap;
            set
            {
                _startMap = value;
                RaisePropertyChangedEvent(nameof(StartMap));
            }
        }

        /// <summary>
        ///     Name of the Tile Dictionary
        /// </summary>
        public string TileDictionary
        {
            get => _tileDictionary;
            set
            {
                _tileDictionary = value;
                RaisePropertyChangedEvent(nameof(TileDictionary));
            }
        }

        /// <summary>
        ///     Time In game
        /// </summary>
        public int StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                RaisePropertyChangedEvent(nameof(StartTime));
            }
        }

        /// <summary>
        ///     Id of the Character
        /// </summary>
        public int CharacterId
        {
            get => _characterId;
            set
            {
                _characterId = value;
                RaisePropertyChangedEvent(nameof(CharacterId));
            }
        }

        /// <summary>
        ///     Id of the Character
        /// </summary>
        public int Character
        {
            get => _character;
            set
            {
                _character = value;
                RaisePropertyChangedEvent(nameof(Character));
            }
        }

        /// <summary>
        ///     Start Id of the Character
        /// </summary>
        public int StartPoint
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
                RaisePropertyChangedEvent(nameof(StartPoint));
            }
        }
    }
}