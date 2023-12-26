/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/Item.cs
 * PURPOSE:     Class and Data Object for all Image Handling, id and Path
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ViewModel;

namespace Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     The images class.
    /// </summary>
    public sealed class Images : ObservableObject
    {
        /// <summary>
        ///     The id image.
        /// </summary>
        private int _idImage;

        /// <summary>
        ///     The image path.
        /// </summary>
        private string _imagePath;

        /// <summary>
        ///     Gets or sets the id image.
        /// </summary>
        public int IdImage
        {
            get => _idImage;
            set
            {
                _idImage = value;
                RaisePropertyChangedEvent(nameof(IdImage));
            }
        }

        /// <summary>
        ///     Gets or sets the image path.
        /// </summary>
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                RaisePropertyChangedEvent(nameof(ImagePath));
            }
        }
    }
}