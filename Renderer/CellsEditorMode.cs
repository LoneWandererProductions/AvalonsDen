/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Renderer/CellsEditorMode.cs
 * PURPOSE:     Basic Separation from Cell that handles all Editor Actions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using Debugger;
using ExtendedSystemObjects;
using Resources;

namespace Renderer
{
    /// <summary>
    ///     Class we only use in Editor Mode
    /// </summary>
    internal static class CellsEditorMode
    {
        /// <summary>
        ///     Dictionary of Tiles
        /// </summary>
        private static Dictionary<int, Tile> _tileDct;

        /// <summary>
        ///     Dictionary of Transitions
        /// </summary>
        internal static Dictionary<Coordinates, List<int>> Transitions { private get; set; }

        /// <summary>
        ///     List of changed Base Coordinates
        /// </summary>
        internal static List<Coordinates> ChangeList { get; private set; }

        /// <summary>
        ///     For cleanness we use this class to handle all Editor Mode Work
        /// </summary>
        /// <param name="tileDct">The tileDct.</param>
        /// <param name="transitions">The transitions.</param>
        /// <param name="mapDictionary">The mapDictionary.</param>
        /// <param name="width">The width.</param>
        internal static void EditorModeInitiate(Dictionary<int, Tile> tileDct,
            Dictionary<Coordinates, List<int>> transitions,
            List<SerializeableKeyValuePair.KeyValuePair<int, int>> mapDictionary, int width)
        {
            _tileDct = tileDct;
            Transitions = transitions;

            //Feed in Log File
            ChangeList = mapDictionary.IsNullOrEmpty()
                ? new List<Coordinates>()
                : CellsProcessing.ConvertMapToList(mapDictionary, width, tileDct);
        }

        /// <summary>
        ///     Add a Coordinate and Change the BaseTile as Usual
        /// </summary>
        /// <param name="item">Target Coordinate</param>
        internal static void AddTile(Coordinates item)
        {
            ChangeList.AddDistinct(item);
            Cells.BaseTile.AddDistinct(item, _tileDct[item.TileId].FileName);
        }

        /// <summary>
        ///     Add a Coordinate and Change the BaseTile as Usual
        /// </summary>
        /// <param name="item">Target Coordinate</param>
        internal static void Remove(Coordinates item)
        {
            ChangeList.Remove(item);
            if (Cells.BaseTile.ContainsKey(item)) Cells.BaseTile.Remove(item);
        }

        /// <summary>
        ///     Get specific Tile with all Transitions and base Tiles
        /// </summary>
        /// <param name="item">Target Coordinate</param>
        /// <param name="myBitmapCell">Image we will create</param>
        /// <returns>Merged Bitmap</returns>
        public static BitmapImage CheckAddTransitions(Coordinates item, BitmapImage myBitmapCell)
        {
            if (!Transitions.ContainsKey(item)) return myBitmapCell;

            var lst = Transitions[item];

            return CellsImageFileStream.LoadMultiTerain(lst, item, Cells.BaseTile, _tileDct);
        }

        /// <summary>
        ///     Check if we have to delete transitions.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The new Image <see cref="BitmapImage" />.</returns>
        public static BitmapImage CheckDeleteTransitions(Coordinates item)
        {
            if (Transitions?.ContainsKey(item) != true)
            {
                DebugLog.CreateLogFile(RendererResources.ErrorTransitionsEmpty, ErCode.Error);
                return null;
            }

            var lst = Transitions[item];

            return CellsImageFileStream.LoadMultiTerain(lst, item, Cells.BaseTile, _tileDct);
        }

        /// <summary>
        ///     Get the intersection of the Images.
        /// </summary>
        /// <param name="itemList">The itemList.</param>
        /// <returns>The List of Images<see cref="T:List{Coordinates}" />.</returns>
        public static List<Coordinates> GetIntersection(IEnumerable<Coordinates> itemList)
        {
            return itemList.Intersect(ChangeList).ToList();
        }
    }
}