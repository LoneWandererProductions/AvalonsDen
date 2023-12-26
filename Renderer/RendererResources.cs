/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Renderer/RenderingResources.cs
 * PURPOSE:     Simple Collection of all Strings for cleaner Code
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Renderer
{
    /// <summary>
    ///     The renderer Resources class, contains all strings and important Numbers.
    /// </summary>
    internal static class RendererResources
    {
        //Basic Resources

        /// <summary>
        ///     The Button idle (const). Value: "ButtonIdle".
        /// </summary>
        internal const string BtnIdle = "ButtonIdle";

        /// <summary>
        ///     The Button two (const). Value: "ButtonTwo".
        /// </summary>
        internal const string BtnTwo = "ButtonTwo";

        /// <summary>
        ///     The Button three (const). Value: "ButtonThree".
        /// </summary>
        internal const string BtnThree = "ButtonThree";

        /// <summary>
        ///     The Button four (const). Value: "ButtonFour".
        /// </summary>
        internal const string BtnFour = "ButtonFour";

        /// <summary>
        ///     The Button five (const). Value: "ButtonFive".
        /// </summary>
        internal const string BtnFive = "ButtonFive";

        /// <summary>
        ///     The Button six (const). Value: "ButtonSix".
        /// </summary>
        internal const string BtnSix = "ButtonSix";

        /// <summary>
        ///     The Button middle (const). Value: "ButtonMiddle".
        /// </summary>
        internal const string BtnMiddle = "ButtonMiddle";

        /// <summary>
        ///     Path Information where we expect the Tiles
        /// </summary>
        internal const string TilesFolder = "Tiles";

        /// <summary>
        ///     Path Information where we expect the Icons
        /// </summary>
        internal const string CoreIcons = @"Core\Icons";

        /// <summary>
        ///     Internal Resource Path
        /// </summary>
        internal const string Resources = "Resource";

        /// <summary>
        ///     Internal Resource, Dot Image for Movement Displays
        /// </summary>
        internal const string DotImage = "dot.png";

        /// <summary>
        ///     The name background image (const). Value: "BackgroundImage".
        /// </summary>
        internal const string NameBackgroundImage = "BackgroundImage";

        /// <summary>
        ///     The cell height (const). Value: 92.
        /// </summary>
        internal const int CellHeight = 92;

        /// <summary>
        ///     Formating string in message
        /// </summary>
        internal const string Message = " Message: ";

        /// <summary>
        ///     Formating string in message
        /// </summary>
        internal const string Path = "Path: ";

        /// <summary>
        ///     The label (const). Value: "lbl". Name Suffix
        /// </summary>
        public const string Lbl = "lbl";

        /// <summary>
        ///     Id for Deletion, must be 0, should be reconsidered TODO
        /// </summary>
        internal const int IdofDeleteTile = 0;

        //Logging, Error Informations

        /// <summary>
        ///     The separator (const). Value: " , ".
        /// </summary>
        internal const string Separator = " , ";

        /// <summary>
        ///     The warning transitions empty (const). Value: "Transitions were Empty".
        /// </summary>
        internal const string WarningTransitionsEmpty = "Transitions were Empty";

        /// <summary>
        ///     The warning no item (const). Value: "Selected Item was Empty".
        /// </summary>
        internal const string WarningNoItem = "Selected Item was Empty";

        /// <summary>
        ///     The warning map empty (const). Value: "Map Object was empty: ".
        /// </summary>
        internal const string WarningMapEmpty = "Map Object was empty: ";

        /// <summary>
        ///     The warning load image background (const). Value: "Load Image was called with wrong Parameters".
        /// </summary>
        internal const string WarningLoadImageBackground = "Load Image was called with wrong Parameters";

        /// <summary>
        ///     The warning changed tiles empty (const). Value: "Should not happen ChangedTiles were empty".
        /// </summary>
        internal const string WarningChangedTilesEmpty = "Should not happen ChangedTiles were empty";

        /// <summary>
        ///     The error missing file (const). Value: "File not found: ".
        /// </summary>
        internal const string ErrorMissingFile = "File not found: ";

        /// <summary>
        ///     The error exception (const). Value: "Exception in GetImageUriSource: ".
        /// </summary>
        internal const string ErrorException = "Exception in GetImageUriSource: ";

        /// <summary>
        ///     The error menu overflow (const). Value: "Exception to many Menu Items provided: ".
        /// </summary>
        internal const string ErrorMenuOverflow = "Exception to many Menu Items provided: ";

        /// <summary>
        ///     The error image key not found (const). Value: "Key for Image Cell was not found: ".
        /// </summary>
        internal const string ErrorImageKeyNotFound = "Key for Image Cell was not found: ";

        /// <summary>
        ///     The error transitions empty (readonly). Value: "Transition Dictionary was empty".
        /// </summary>
        internal const string ErrorTransitionsEmpty = "Transition Dictionary was empty";
    }
}