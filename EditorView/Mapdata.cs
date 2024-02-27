/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        EditorView/Mapdata.cs
 * PURPOSE:     Helper Object to create a Map
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;
using ViewModel;

// ReSharper disable UnusedAutoPropertyAccessor.Global, we serialize it so we can't change it to internal
// ReSharper disable MemberCanBeInternal, we serialize it so we can't change it to internal

namespace EditorView
{
    /// <inheritdoc />
    /// <summary>
    ///     The EventArgsMap class.
    /// </summary>
    public sealed class EventArgsMap : ObservableObject
    {
        /// <summary>
        ///     The height.
        /// </summary>
        private int _height;

        /// <summary>
        ///     The length.
        /// </summary>
        private int _length;

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChangedEvent(nameof(Height));
            }
        }

        /// <summary>
        ///     Gets or sets the length.
        /// </summary>
        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                RaisePropertyChangedEvent(nameof(Length));
            }
        }

        /// <summary>
        ///     Gets or sets the map.
        /// </summary>
        public List<SerializeableKeyValuePair.KeyValuePair<int, int>> Map { get; set; }

        /// <summary>
        ///     Gets or sets the transitions.
        /// </summary>
        public Dictionary<int, List<int>> Transitions { get; internal set; }
    }
}