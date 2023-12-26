/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/EventTypeExtension.cs
 * PURPOSE:     Description of Event
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     Basic Event Element
    /// </summary>
    public sealed class EventTypeExtension : ObservableObject
    {
        /// <summary>
        ///     The id.
        /// </summary>
        private int _id;

        /// <summary>
        ///     The value.
        /// </summary>
        private string _value;

        /// <summary>
        ///     Help Variable int ID of EvenType
        /// </summary>
        /// <remarks>Set by user</remarks>
        /// <value>Links to EvenType</value>
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
        ///     Id or short String
        /// </summary>
        /// <remarks>Saved as String serialized on runtime</remarks>
        /// <value>Id or String</value>
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChangedEvent(nameof(Value));
            }
        }
    }
}