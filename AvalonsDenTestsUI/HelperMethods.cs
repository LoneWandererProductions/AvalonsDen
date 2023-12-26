/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTestsUI/SharedAvalonsDenTests.cs
 * PURPOSE:     Basic Shared Tests for Avalon
 * PROGRAMER:   Peter GeinitzWayfarer
 */

using System.IO;
using FileHandler;
using Loader;
using Resources;

namespace AvalonsDenTestsUI
{
    /// <summary>
    ///     The helper methods class.
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

            var path = Path.Combine(DirectoryInformation.GetParentDirectory(3), ResourcesGeneral.Path);

            load.MasterBordersDictionary =
                WorkLoader.LoadTileBordersDct(Path.Combine(path, ResourcesGeneral.MasterBorderDct));
            load.MasterTileDictionary =
                WorkLoader.LoadTileDct(Path.Combine(path, ResourcesGeneral.MasterTileDct));
            return load;
        }
    }
}