/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/EventType.cs
 * PURPOSE:     Description of Event
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     Event Dictionary(Key, EventType)
    ///     Id to Coordinates, Reference to CoordinatesId Dictionary(Event.Key, EventType.CoordinatesId)
    /// </summary>
    public class EventType : ObservableObject
    {
        /// <summary>
        ///     Here we define specific Event Types
        /// </summary>
        public enum TypeOfEvents
        {
            /// <summary>
            ///     The Interaction = 1 Event.
            /// </summary>
            Interaction = 1,

            /// <summary>
            ///     The Trade = 2 Event.
            /// </summary>
            Trade = 2,

            /// <summary>
            ///     The Talk = 3 Event.
            /// </summary>
            Talk = 3,

            /// <summary>
            ///     The Fight = 4 Event.
            /// </summary>
            Fight = 4,

            /// <summary>
            ///     The Look = 5 Event.
            /// </summary>
            Look = 5,

            /// <summary>
            ///     The LocationChange = 6 Event.
            /// </summary>
            LocationChange = 6,

            /// <summary>
            ///     The Map Change = 7 Event.
            /// </summary>
            MapChange = 7,

            /// <summary>
            ///     The AddItems = 8 Event.
            /// </summary>
            AddItems = 8,

            /// <summary>
            ///     The AddCharacter = 9 Event.
            /// </summary>
            AddCharacter = 9,

            /// <summary>
            ///     The RemoveCharacter = 10 Event.
            /// </summary>
            RemoveCharacter = 10,

            /// <summary>
            ///     The AddGold = 11 Event.
            /// </summary>
            AddGold = 11
        }

        /// <summary>
        ///     The coordinates id.
        /// </summary>
        private readonly int _coordinatesId;

        /// <summary>
        ///     The description.
        /// </summary>
        private readonly string _description;

        /// <summary>
        ///     The id for further event infos.
        /// </summary>
        private readonly int _idForFurtherEventInfo;

        /// <summary>
        ///     Is Dependent.
        /// </summary>
        private readonly bool _isDependent;

        /// <summary>
        ///     Is random.
        /// </summary>
        private readonly bool _isRandom;

        /// <summary>
        ///     Is repeatable.
        /// </summary>
        private readonly bool _isRepeatable;

        /// <summary>
        ///     Is step on.
        /// </summary>
        private readonly bool _isStepOn;

        /// <summary>
        ///     The label event.
        /// </summary>
        private readonly string _labelEvent;

        /// <summary>
        ///     The type of event.
        /// </summary>
        private readonly TypeOfEvents _typeOfEvent;

        /// <summary>
        ///     Is active.
        /// </summary>
        private bool _isActive;

        /// <summary>
        ///     Event Dictionary(Key, EventType)
        ///     Id to Coordinates, Reference to CoordinatesId Dictionary(Event.Key, EventType.CoordinatesId)
        /// </summary>
        public int CoordinatesId
        {
            get => _coordinatesId;
            init
            {
                _coordinatesId = value;
                RaisePropertyChangedEvent(nameof(CoordinatesId));
            }
        }

        /// <summary>
        ///     Title of Event
        /// </summary>
        public string LabelEvent
        {
            get => _labelEvent;
            init
            {
                _labelEvent = value;
                RaisePropertyChangedEvent(nameof(LabelEvent));
            }
        }

        /// <summary>
        ///     On click or can Player step on it
        /// </summary>
        public bool IsStepOn
        {
            get => _isStepOn;
            init
            {
                _isStepOn = value;
                RaisePropertyChangedEvent(nameof(IsStepOn));
            }
        }

        /// <summary>
        ///     repeatable Event
        /// </summary>
        public bool IsRepeatable
        {
            get => _isRepeatable;
            init
            {
                _isRepeatable = value;
                RaisePropertyChangedEvent(nameof(IsRepeatable));
            }
        }

        /// <summary>
        ///     if true no Coordinates instead whole Map
        /// </summary>
        public bool IsRandom
        {
            get => _isRandom;
            init
            {
                _isRandom = value;
                RaisePropertyChangedEvent(nameof(IsRandom));
            }
        }

        /// <summary>
        ///     check if it is part of a Event chain
        ///     0 is normal
        /// </summary>
        public bool IsDependent
        {
            get => _isDependent;
            init
            {
                _isDependent = value;
                RaisePropertyChangedEvent(nameof(IsDependent));
            }
        }

        /// <summary>
        ///     check if it is done
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                RaisePropertyChangedEvent(nameof(IsActive));
            }
        }

        /// <summary>
        ///     0. GetMap
        ///     1. Interaction
        ///     2. Trade
        ///     3. Talk
        ///     4. Fight
        ///     5. Look
        ///     6. Location Change
        ///     7. Map change
        ///     8. Add Item, Format change in AssetDictionary
        ///     9. Add Character
        ///     10. Start Point -> StartPoint
        ///     Check those! not really used
        ///     ....
        ///     11. Trap
        /// </summary>
        public TypeOfEvents TypeOfEvent
        {
            get => _typeOfEvent;
            init
            {
                _typeOfEvent = value;
                RaisePropertyChangedEvent(nameof(TypeOfEvent));
            }
        }

        /// <summary>
        ///     Defines the ID of another Dictionary that contains e.g. Loot. Enemies, Dialog Options
        /// </summary>
        public int IdForFurtherEventInfo
        {
            get => _idForFurtherEventInfo;
            init
            {
                _idForFurtherEventInfo = value;
                RaisePropertyChangedEvent(nameof(IdForFurtherEventInfo));
            }
        }

        /// <summary>
        ///     Description for internal use
        /// </summary>
        public string Description
        {
            get => _description;
            init
            {
                _description = value;
                RaisePropertyChangedEvent(nameof(Description));
            }
        }
    }
}