/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Engine/CellsImageFileStream.cs
 * PURPOSE:     Helper class for Cells handles all graphical interactions and loads/combines all images
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Debugger;
using ExtendedSystemObjects;
using Imaging;
using Resources;

namespace Renderer
{
    /// <summary>
    ///     Loads a BitMapImage out of a specific path
    ///     Can Combine two Images and returns a new one
    ///     Used by:
    ///     CellsToolBox,  Cells,  ContextMenus
    /// </summary>
    internal static class CellsImageFileStream
    {
        /// <summary>
        ///     The Image renderer Interface
        /// </summary>
        private static readonly ImageRender Renderer = new();

        /// <summary>
        ///     Loads File one Time
        ///     Can only used to load an Image Once
        /// </summary>
        /// <param name="path">path to the File</param>
        /// <param name="filename">Name of the File</param>
        /// <returns>a BitmapImage, or null</returns>
        internal static BitmapImage GetImageUriSource(string path, string filename)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), path, filename);

            if (!File.Exists(path))
            {
                DebugLog.CreateLogFile(RendererResources.ErrorMissingFile + path, ErCode.Warning);
                return null;
            }

            try
            {
                return Renderer.GetBitmapImage(path);
            }
            catch (NotSupportedException ex)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorException, ex), ErCode.Warning);
            }
            catch (IOException ex)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorException, ex), ErCode.Warning);
            }
            catch (InvalidOperationException ex)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorException, ex), ErCode.Warning);
            }

            return null;
        }

        /// <summary>
        ///     Loads File in a Stream
        ///     takes longer but can be changed on Runtime
        /// </summary>
        /// <param name="path">path to the File</param>
        /// <param name="filename">Name of the File</param>
        /// <returns>a BitmapImage, or null</returns>
        internal static BitmapImage GetImageFileStream(string path, string filename)
        {
            if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(path))
            {
                DebugLog.CreateLogFile(RendererResources.ErrorMissingFile, ErCode.Error);
                return null;
            }

            path = Path.Combine(Directory.GetCurrentDirectory(), path, filename);

            if (!File.Exists(path))
            {
                DebugLog.CreateLogFile(RendererResources.ErrorMissingFile + path, ErCode.Warning);
                return null;
            }

            try
            {
                return Renderer.GetBitmapImage(path);
            }
            catch (NotSupportedException e)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.Path, path, RendererResources.Message, e),
                    ErCode.Warning);

                return null;
            }
            catch (IOException e)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.Path, path, RendererResources.Message, e),
                    ErCode.Warning);

                return null;
            }
            catch (InvalidOperationException e)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.Path, path, RendererResources.Message, e),
                    ErCode.Warning);

                return null;
            }
        }

        /// <summary>
        ///     Mesh Transition Tiles, use underlying base Tile if not provide a vase new one
        /// </summary>
        /// <param name="transitions">List of Transition Tiles</param>
        /// <param name="key">Target Coordinate</param>
        /// <param name="baseTile">Tile on the coordinate if there is one</param>
        /// <param name="tileDct">The tile Dictionary.</param>
        /// <returns>
        ///     Combined BitmapImage, or an empty BitmapImage
        /// </returns>
        internal static BitmapImage LoadMultiTerain(List<int> transitions, Coordinates key,
            Dictionary<Coordinates, string> baseTile, Dictionary<int, Tile> tileDct)
        {
            if (transitions.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(RendererResources.ErrorTransitionsEmpty, ErCode.Error);
                return new BitmapImage();
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), RendererResources.TilesFolder);
            var lst = new List<string>();

            //Handle existing Tiles on the Tile, but only if they are under the Layer
            if (baseTile.ContainsKey(key) && key.ZLayer <= tileDct[transitions[0]].Layer)
            {
                var bases = baseTile[key];
                lst.Add(Path.Combine(path, bases));
            }

            foreach (var tile in transitions)
            {
                if (!tileDct.ContainsKey(tile))
                {
                    DebugLog.CreateLogFile(RendererResources.ErrorImageKeyNotFound, ErCode.Error);
                    continue;
                }

                lst.Add(Path.Combine(path, tileDct[tile].FileName));
            }

            try
            {
                return Renderer.CombineBitmap(lst).ToBitmapImage();
            }
            catch (NotSupportedException ex)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorException, ex), ErCode.Warning);
            }
            catch (IOException ex)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorException, ex), ErCode.Warning);
            }
            catch (InvalidOperationException ex)
            {
                DebugLog.CreateLogFile(string.Concat(RendererResources.ErrorException, ex), ErCode.Warning);
            }

            return new BitmapImage();
        }
    }
}