/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/GearMisc.cs
 * PURPOSE:     Item Template for all Misc Items
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     The gear misc class.
    /// </summary>
    public static class GearMisc
    {
        /// <summary>
        ///     The Item type enum.
        /// </summary>
        public enum Type
        {
            /// <summary>
            ///     Consumable Item = 0.
            /// </summary>
            Consumable = 0,

            /// <summary>
            ///     Quest Item= 1.
            /// </summary>
            Quest = 1
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Miscellaneous Items
    /// </summary>
    public sealed class Miscellaneous : Item
    {
        /// <summary>
        ///     The type.
        /// </summary>
        private GearMisc.Type _type;

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        public GearMisc.Type Type
        {
            get => _type;
            set
            {
                _type = value;
                RaisePropertyChangedEvent(nameof(Type));
            }
        }
    }
}