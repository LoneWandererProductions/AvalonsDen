/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/SaveInfos.cs
 * PURPOSE:     Object for Saving Basic infos
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace Resources
{
    /// <summary>
    ///     The save infos class.
    /// </summary>
    public sealed class SaveInfos
    {
        /// <summary>
        ///     Coordinates of Start point
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        ///     Coordinates of Start point
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        ///     Name of the Map
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        ///     Name of the Campaign
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        ///     Name of the Save
        /// </summary>
        public string SaveName { get; set; }

        /// <summary>
        ///     Ids of the Party Characters
        /// </summary>
        public List<int> CharacterId { get; set; }

        /// <summary>
        ///     Time in game
        /// </summary>
        public int ActualTime { get; set; }
    }
}