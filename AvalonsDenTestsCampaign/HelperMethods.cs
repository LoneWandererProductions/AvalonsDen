/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/SharedAvalonsDenTests.cs
 * PURPOSE:     Basic Shared Tests for Avalon
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.IO;
using FileHandler;
using Loader;
using Resources;

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     Helper methods for AvalonsDenTestsCampaign
    /// </summary>
    internal static class HelperMethods
    {
        /// <summary>
        ///     Gets the Base data from another Folder
        /// </summary>
        /// <returns>Collection of the Data in need</returns>
        internal static LoaderContainer GetData()
        {
            var load = new LoaderContainer();

            var path = Path.Combine(DirectoryInformation.GetParentDirectory(3), ResourcesGeneral.Root);

            load.MasterBordersDictionary =
                WorkLoader.LoadTileBordersDct(Path.Combine(path, ResourcesGeneral.MasterBorderDct));
            load.MasterTileDictionary =
                WorkLoader.LoadTileDct(Path.Combine(path, ResourcesGeneral.MasterTileDct));
            return load;
        }
    }
}