/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/MapObject.cs
 * PURPOSE:     Saves a description of a Map
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Resources
{
    /// <summary>
    ///     The map object class.
    /// </summary>
    [XmlRoot("Map")]
    public sealed class MapObject
    {
        /// <summary>
        ///     Constructor for Dummy Map
        /// </summary>
        /// <param name="height">Height of Map</param>
        /// <param name="length">Length of Map</param>
        public MapObject(int height, int length)
        {
            Height = height;
            Length = length;
        }

        /// <summary>
        ///     Parameterless Constructor
        /// </summary>
        public MapObject()
        {
        }

        /// <summary>
        ///     Border Map
        /// </summary>
        [XmlElement("Borders")]
        public List<string> Borders { get; set; }

        /// <summary>
        ///     Height of Map
        /// </summary>
        [XmlElement("height")]
        public int Height { get; set; }

        /// <summary>
        ///     Length of Map
        /// </summary>
        [XmlElement("length")]
        public int Length { get; set; }

        //BackroundImage of Map
        /// <summary>
        ///     BackroundImage of Map
        /// </summary>
        [XmlElement("BackGroundImage")]
        public string BackGroundImage { get; set; }

        /// <summary>
        ///     Name of the Map
        /// </summary>
        [XmlElement("MapName")]
        public string MapName { get; set; }

        /// <summary>
        ///     Map Id and Tile Id
        /// </summary>
        public List<SerializeableKeyValuePair.KeyValuePair<int, int>> MapList { get; set; }
    }
}