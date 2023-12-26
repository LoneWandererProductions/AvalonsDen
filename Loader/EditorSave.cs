/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/EditorSave.cs
 * PURPOSE:     Save all Types of Objects, Here we Use the Serializer
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using AvalonRuntime;
using DenSerializer;
using FileHandler;
using Resources;
using Serializer;

namespace Loader
{
    /// <summary>
    ///     The shared save class.
    ///     Used for Editor Only
    /// </summary>
    public static class EditorSave
    {
        /// <summary>
        ///     Saves all generated Files into specific Path
        ///     Generates:
        ///     The Map
        ///     The Event Object Collection
        ///     The Transition File
        ///     Used by Editor
        /// </summary>
        /// <param name="path">Target Path and Name of Files</param>
        /// <param name="loadupMap">MapObject to save</param>
        public static void SaveMapAs(string path, LoaderContainer loadupMap)
        {
            //save basic Map Name
            loadupMap.MapObject.MapName = Path.GetFileNameWithoutExtension(path);

            path = PathInformation.GetPathWithoutExtension(path);

            // serialize basic Objects
            // The Map
            Serialize.SaveObjectToXml(loadupMap.MapObject, Path.ChangeExtension(path, ArtConst.MapExt));

            //TODO rework this shit

            //Dictionary specific conversions.
            Serialize.SaveDctObjectToXml(loadupMap.EventCollection.CoordinatesId,
                Path.ChangeExtension(path, ArtConst.CoordinatesIdExt));
            Serialize.SaveDctObjectToXml(loadupMap.EventCollection.EventTypeDictionary,
                Path.ChangeExtension(path, ArtConst.EventTypeExt));
            Serialize.SaveDctObjectToXml(loadupMap.EventCollection.EventTypeExtensionDictionary,
                Path.ChangeExtension(path, ArtConst.EventTypeExtensionExt));
            DenSerialize.XmlSerializerTransition(loadupMap.TransitionDictionary,
                Path.ChangeExtension(path, ArtConst.TransitionFileExt));
        }

        /// <summary>
        ///     Saves all generated Files into specific Path
        ///     contains:  EventCoordinatesList, EventTypeList, EventAssetList
        ///     Used by Editor
        /// </summary>
        /// <param name="path">Target Path and Name of Files</param>
        /// <param name="eventContainer">EventMasterCollection to save</param>
        public static void SaveEventMasterCollection(string path, EventContainer eventContainer)
        {
            path = PathInformation.GetPathWithoutExtension(path);

            Serialize.SaveDctObjectToXml(eventContainer.EventTypeDictionary,
                Path.ChangeExtension(path, ArtConst.EventTypeExt));
            Serialize.SaveDctObjectToXml(eventContainer.EventTypeExtensionDictionary,
                Path.ChangeExtension(path, ArtConst.EventTypeExtensionExt));
            Serialize.SaveDctObjectToXml(eventContainer.CoordinatesId,
                Path.ChangeExtension(path, ArtConst.CoordinatesIdExt));
        }

        /// <summary>
        ///     Saves a:
        ///     Campaign Manifest, .cpg
        ///     Party Inventory, .aiy
        ///     Used by Editor
        /// </summary>
        /// <param name="path">The path where we want to save it</param>
        /// <param name="campaign">The Campaign Object</param>
        /// <param name="inventory">The party inventory.</param>
        public static void SaveCampaign(string path, CampaignManifest campaign, PartyInventory inventory)
        {
            var folder = Path.GetDirectoryName(path);
            path = PathInformation.GetPathWithoutExtension(path);
            //Create the Campaign Manifest
            Serialize.SaveObjectToXml(campaign, Path.ChangeExtension(path, ArtConst.CampaignExt));

            // The Inventory, gets into the sub Directory

            //TODO add a Trace
            if (string.IsNullOrEmpty(folder)) return;

            Serialize.SaveObjectToXml(inventory.PartyOverview, Path.Combine(folder, LoaderRessource.PartyFile));
            Serialize.SaveDctObjectToXml(inventory.Carrying, Path.Combine(folder, LoaderRessource.InventoryFile));
            Serialize.SaveDctObjectToXml(inventory.Equipment, Path.Combine(folder, LoaderRessource.EquipmentFile));
        }

        /// <summary>
        ///     Saves the TileBorders Dictionary
        ///     Used by Editor
        /// </summary>
        /// <param name="borderDct">TileBorders Dictionary</param>
        /// <param name="path">The path where we want to save it</param>
        public static void SaveTileBordersDictionary(Dictionary<int, TileBorders> borderDct, string path)
        {
            Serialize.SaveDctObjectToXml(borderDct, path);
        }

        /// <summary>
        ///     Saves the TileDictionary
        ///     Used by Editor
        /// </summary>
        /// <param name="tileDct">TileDictionary </param>
        /// <param name="path">The path where we want to save it</param>
        public static void SaveTileDictionary(Dictionary<int, Tile> tileDct, string path)
        {
            Serialize.SaveDctObjectToXml(tileDct, path);
        }
    }
}