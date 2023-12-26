/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/EditorRegister.cs
 * PURPOSE:     Helper Class To Track all Changes
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using Debugger;
using Loader;
using Resources;

namespace Editors
{
    /// <summary>
    ///     Does all the dirty work holding the data saving and loading it.
    /// </summary>
    internal static class EditorRegister
    {
        /// <summary>
        ///     Gets the tile Dictionary.
        /// </summary>
        internal static Dictionary<int, Tile> TileDct { get; private set; }

        /// <summary>
        ///     Gets the border Dictionary.
        /// </summary>
        internal static Dictionary<int, TileBorders> BorderDct { get; private set; }

        /// <summary>
        ///     Gets the map Object.
        /// </summary>
        internal static MapObject MapObjct { get; private set; }

        /// <summary>
        ///     Gets the event type Object.
        /// </summary>
        internal static EventContainer EventTypeObjct { get; private set; }

        /// <summary>
        ///     Gets the transition Dictionary.
        /// </summary>
        internal static Dictionary<int, List<int>> TransitionDct { get; private set; }

        /// <summary>
        ///     Set our TileDictionary
        /// </summary>
        /// <param name="tileDct">Tile Dictionary</param>
        internal static void SetTileDictionary(Dictionary<int, Tile> tileDct)
        {
            TileDct = tileDct;
        }

        /// <summary>
        ///     Set our BorderDictionary
        /// </summary>
        /// <param name="borderDct">Border Dictionary</param>
        internal static void SetBorderDictionary(Dictionary<int, TileBorders> borderDct)
        {
            BorderDct = borderDct;
        }

        /// <summary>
        ///     Set our Transition Dictionary
        /// </summary>
        /// <param name="transitions">Map Transitions</param>
        internal static void SetTransitionDictionary(Dictionary<int, List<int>> transitions)
        {
            TransitionDct = transitions;
        }

        /// <summary>
        ///     Set our Transition EventType Dictionary
        /// </summary>
        /// <param name="eventType">EventType Dictionary</param>
        internal static void SetEventTypeDictionary(EventContainer eventType)
        {
            EventTypeObjct = eventType;
        }

        /// <summary>
        ///     Initiate our Transition EventType Dictionary
        /// </summary>
        internal static void SetEventTypeDictionary()
        {
            EventTypeObjct = new EventContainer
            {
                CoordinatesId = new Dictionary<int, int>(),
                EventTypeDictionary = new Dictionary<int, EventType>(),
                EventTypeExtensionDictionary = new Dictionary<int, EventTypeExtension>()
            };
        }

        /// <summary>
        ///     Set our MapObject
        /// </summary>
        /// <param name="mapObject">Height of Map</param>
        internal static void SetMapObject(MapObject mapObject)
        {
            MapObjct = mapObject;
        }

        /// <summary>
        ///     Set our MapObject
        ///     and Initiate our Tile Map for the Map
        /// </summary>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        internal static void SetMapObject(int height, int length)
        {
            MapObjct = new MapObject
            {
                Height = height,
                Length = length,
                MapList = new List<SerializeableKeyValuePair.KeyValuePair<int, int>>()
            };
        }

        /// <summary>
        ///     Get our TileBorders Dictionary as List
        /// </summary>
        /// <returns>List of TileBorders</returns>
        internal static List<TileBorders> GetBorderDictionary()
        {
            return BorderDct?.Values.ToList();
        }

        /// <summary>
        ///     Get our Tiles Dictionary as List
        /// </summary>
        /// <returns>List of Tiles</returns>
        internal static List<Tile> GetTileDictionary()
        {
            return TileDct.Values.ToList();
        }

        /// <summary>
        ///     Done
        ///     Loaded from App
        /// </summary>
        /// <param name="load">Basic Master Files</param>
        internal static void LoadBase(LoaderContainer load)
        {
            if (load == null)
            {
                DebugLog.CreateLogFile(EditorResources.ErrorCouldNotLoadBasicFiles, ErCode.Error);
                return;
            }

            BorderDct = load.MasterBordersDictionary;
            TileDct = load.MasterTileDictionary;
        }

        /// <summary>
        ///     Saves all data into the EditorLoader
        /// </summary>
        /// <param name="path">path to Load the data</param>
        internal static void SaveAllData(string path)
        {
            var saveMap = new LoaderContainer
            {
                MapObject = MapObjct,
                EventCollection = EventTypeObjct,
                MasterBordersDictionary = BorderDct,
                TransitionDictionary = TransitionDct
            };
            EditorSave.SaveMapAs(path, saveMap);
        }

        /// <summary>
        ///     Load all data into the EditorLoader
        /// </summary>
        /// <param name="path">path to Load the data</param>
        internal static void LoadAllData(string path)
        {
            var load = WorkLoader.LoadCollectionMap(path);
            EventTypeObjct = load.EventCollection;
            TransitionDct = load.TransitionDictionary ?? new Dictionary<int, List<int>>();
            MapObjct = load.MapObject;
        }

        /// <summary>
        ///     Done
        ///     Saves Borders
        /// </summary>
        /// <param name="path">Target path</param>
        internal static void SaveBorders(string path)
        {
            EditorSave.SaveTileBordersDictionary(BorderDct, path);
        }

        /// <summary>
        ///     Done
        ///     Saves Tiles
        /// </summary>
        /// <param name="path">Target path</param>
        internal static void SaveTiles(string path)
        {
            EditorSave.SaveTileDictionary(TileDct, path);
        }
    }
}