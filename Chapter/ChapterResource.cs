/*
* COPYRIGHT:   See COPYING in the top level directory
* PROJECT:     AvalonsDen
* FILE:        AvalonsDen/AvalonsDen/ChapterResource.cs
* PURPOSE:     String Resources of Chapter
* PROGRAMER:   Wayfarer
*/

namespace Chapter
{
    /// <summary>
    ///     The chapter resource class.
    /// </summary>
    internal static class ChapterResource
    {
        //File Names
        /// <summary>
        ///     The license file (const). Value: @"\License.txt".
        /// </summary>
        internal const string LicenseFile = @"\License.txt";

        /// <summary>
        ///     The authors file (const). Value: @"\Authors.txt".
        /// </summary>
        internal const string AuthorsFile = @"\Authors.txt";

        /// <summary>
        ///     The app startup (const). Value: @"\Editors.exe".
        /// </summary>
        internal const string AppStartup = @"\Editors.exe";

        /// <summary>
        ///     The campaigns folder (const). Value: @"Content\Campaigns".
        /// </summary>
        internal const string CampaignsFolder = @"Content\Campaigns";

        /// <summary>
        ///     The arguments none (const). Value: "/".
        /// </summary>
        internal const string ArgumentsNone = "/";

        /// <summary>
        ///     The campaign ext search (const). Value: "*.cpg".
        /// </summary>
        internal const string CampaignExtSearch = "*.cpg";

        //Error
        /// <summary>
        ///     The error loadmanifest (const). Value: "Manifest was damaged: ".
        /// </summary>
        internal const string ErrorLoadmanifest = "Manifest was damaged: ";

        /// <summary>
        ///     The information loaded (const). Value: "Loaded up".
        /// </summary>
        internal const string InformationLoaded = "Loaded up";
    }
}