/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Loader/LoaderContainer.cs
 * PURPOSE:     Collection of all needed Map objects
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace Resources
{
    /// <summary>
    ///     Basic Object to handle all Files needed to load a Map in a Campaign
    /// </summary>
    public sealed class LoaderContainer
    {
        /// <summary>
        ///     Gets or sets the name of the campaign.
        /// </summary>
        /// <value>
        ///     The name of the campaign.
        /// </value>
        public string CampaignName { get; set; }

        /// <summary>
        ///     Gets or sets the map object.
        /// </summary>
        /// <value>
        ///     The map object.
        /// </value>
        public MapObject MapObject { get; set; }

        /// <summary>
        ///     Gets or sets the master tile dictionary.
        /// </summary>
        /// <value>
        ///     The master tile dictionary.
        /// </value>
        public Dictionary<int, Tile> MasterTileDictionary { get; set; }

        /// <summary>
        ///     Gets or sets the master Borders dictionary.
        /// </summary>
        /// <value>
        ///     The master tile borders dictionary.
        /// </value>
        public Dictionary<int, TileBorders> MasterBordersDictionary { get; set; }

        /// <summary>
        ///     Gets or sets the Event Master Collection.
        /// </summary>
        /// <value>
        ///     The event collection.
        /// </value>
        public EventContainer EventCollection { get; set; }

        /// <summary>
        ///     Gets or sets the Tansitions.
        /// </summary>
        /// <value>
        ///     The transition dictionary.
        /// </value>
        public Dictionary<int, List<int>> TransitionDictionary { get; set; }
    }
}