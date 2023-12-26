/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CampaignsRegister.cs
 * PURPOSE:     Simple Collections of Data from Campaign, will stay this way since it is a Single Instance App
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using AvalonRuntime;
using ExtendedSystemObjects;
using Resources;

namespace Campaigns
{
    /// <summary>
    ///     Reworked to factory Pattern, adapted for my own personal needs
    ///     reduced a lot of overhead
    /// </summary>
    internal static class CampaignsRegister
    {
        /// <summary>
        ///     Current Point where the Char is located
        ///     Start Point
        ///     Graphic Values
        ///     End Value
        /// </summary>
        internal static Coordinates CurrentPoint { get; private set; }

        /// <summary>
        ///     Id of Character that gets Displayed on the map
        /// </summary>
        internal static int ImageId { get; private set; }

        /// <summary>
        ///     Persistent Data over all Maps
        /// </summary>
        internal static Dictionary<int, Tile> MasterTileDictionary { get; private set; }

        /// <summary>
        ///     Current Time Count
        /// </summary>
        internal static int ActualTime { get; private set; }

        /// <summary>
        ///     Gets the inventory.
        /// </summary>
        /// <value>
        ///     The inventory.
        /// </value>
        internal static PartyInventory PartyInventory { get; private set; }

        /// <summary>
        ///     Used by Renderer and Cells
        ///     and Internal for Conversion of Coordinates to Id
        ///     Data is Map dependent
        ///     Part of MapObject
        /// </summary>
        internal static int Length => MapObject.Length;

        /// <summary>
        ///     Only used by Renderer and Cells
        ///     Data is Map dependent
        ///     Part of MapObject
        /// </summary>
        internal static int Height => MapObject.Height;

        /// <summary>
        ///     Used by Renderer and Cells
        ///     and Internal for Conversion of Coordinates to Id
        ///     Data is Map dependent
        ///     Part of MapObject
        /// </summary>
        internal static string MapName => MapObject.MapName;

        /// <summary>
        ///     Only used by Renderer and Cells
        ///     Data is Map dependent
        ///     Part of MapObject
        /// </summary>
        internal static string BackroundImage => MapObject.BackGroundImage;

        /// <summary>
        ///     Only used by Renderer and Cells
        ///     Data is Map dependent
        ///     Part of MapObject
        /// </summary>
        internal static List<SerializeableKeyValuePair.KeyValuePair<int, int>> MapList => MapObject.MapList;

        /// <summary>
        ///     Only used by Renderer and Cells
        ///     Transitions for one Map
        ///     Data is Map dependent
        /// </summary>
        internal static Dictionary<int, List<int>> TransitionDictionary { get; private set; }

        /// <summary>
        ///     Name of the Campaign
        /// </summary>
        internal static string CampaignName { get; private set; }

        /// <summary>
        ///     Events for the current Map
        /// </summary>
        private static Dictionary<int, EventType> EventTypeDictionary { get; set; }

        /// <summary>
        ///     Current Map we use
        /// </summary>
        private static MapObject MapObject { get; set; }

        /// <summary>
        ///     Get Layer from the given Data
        ///     If we don't have the Key return the -1
        /// </summary>
        internal static int Layer()
        {
            if (!MasterTileDictionary.IsNullOrEmpty() && MasterTileDictionary.ContainsKey(ImageId))
                return MasterTileDictionary[ImageId].Layer;

            return CampaignsResources.ErrorNumber;
        }

        /// <summary>
        ///     Set Start Point as Coordinate
        /// </summary>
        /// <param name="currentPoint"></param>
        internal static void SetPoints(Coordinates currentPoint)
        {
            CurrentPoint = currentPoint;
        }

        /// <summary>
        ///     Set the points.
        /// </summary>
        /// <param name="currentPoint">The currentPoint.</param>
        internal static void SetPoints(int currentPoint)
        {
            CurrentPoint = IdToCoordinate(currentPoint);
        }

        /// <summary>
        ///     Set Start Point as Coordinate from Id
        /// </summary>
        /// <param name="actualTime">Current In Game Time</param>
        internal static void SetActualTime(int actualTime)
        {
            ActualTime = actualTime;
        }

        /// <summary>
        ///     Sets the register. Only needed for Map Change
        /// </summary>
        /// <param name="transitionDictionary">The transition dictionary.</param>
        /// <param name="mapObject">The map object.</param>
        /// <param name="eventTypeDictionary">The event type dictionary.</param>
        internal static void SetRegister(Dictionary<int, List<int>> transitionDictionary, MapObject mapObject,
            Dictionary<int, EventType> eventTypeDictionary)
        {
            TransitionDictionary = transitionDictionary;
            MapObject = mapObject;
            EventTypeDictionary = eventTypeDictionary;
        }

        /// <summary>
        ///     Sets the register. Initiation needed for Map Change and Start of a Map
        /// </summary>
        /// <param name="transitionDictionary">The transition dictionary.</param>
        /// <param name="mapObject">The map object.</param>
        /// <param name="eventTypeDictionary">The event type dictionary.</param>
        /// <param name="campaignName">Name of the campaign.</param>
        /// <param name="masterTileDictionary">The master tile dictionary.</param>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="inventory">The inventory.</param>
        internal static void SetRegister(Dictionary<int, List<int>> transitionDictionary, MapObject mapObject,
            Dictionary<int, EventType> eventTypeDictionary, string campaignName,
            Dictionary<int, Tile> masterTileDictionary, int imageId, PartyInventory inventory)
        {
            TransitionDictionary = transitionDictionary;
            MapObject = mapObject;
            EventTypeDictionary = eventTypeDictionary;
            CampaignName = campaignName;
            MasterTileDictionary = masterTileDictionary;
            ImageId = imageId;
            PartyInventory = inventory;
        }

        /// <summary>
        ///     Sets the ínventory.
        /// </summary>
        /// <param name="carrying">Items we carry.</param>
        internal static void SetInventoryCarrying(Dictionary<int, InventorySlot> carrying)
        {
            PartyInventory.Carrying = carrying;
        }

        /// <summary>
        ///     Sets the inventory.
        ///     It is quite fragile because it will be only saved to the disk on saves
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        internal static void SetInventory(PartyInventory inventory)
        {
            /* 
             * the way we Save we retun null as value so we have to chech for null values, 
             * in most cases it helps to find bugs in this case we get ugly constructs like this one,
             * still worth it in my Opinion
             */
            inventory ??= new PartyInventory();
            inventory.Carrying ??= new Dictionary<int, InventorySlot>();
            inventory.PartyOverview ??= new Party();
            inventory.Equipment ??= new Dictionary<int, Equipped>();
            PartyInventory = inventory;
        }

        /// <summary>
        ///     Sets the gold.
        /// </summary>
        /// <param name="gold">The gold.</param>
        internal static void SetGold(int gold)
        {
            PartyInventory.PartyOverview.Gold = gold;
        }

        /// <summary>
        ///     Converts Ids into for Rendering usable Coordinates
        /// </summary>
        /// <param name="path">Movement Path</param>
        /// <returns>Converted Path</returns>
        internal static List<Coordinates> ConvertIdToCoordinate(IEnumerable<int> path)
        {
            return path.Select(
                id =>
                    ArtShared.IdToCoordinate(
                        id,
                        Length,
                        Layer(),
                        ImageId)
            ).ToList();
        }

        /// <summary>
        ///     Calculate Tile Id
        /// </summary>
        /// <returns></returns>
        internal static int TileId()
        {
            return ArtShared.CalculateId(CurrentPoint, Length);
        }

        /// <summary>
        ///     We only need it this for a Map change We use it to Rollback the Data if something went wrong
        ///     Failsafe Method,
        ///     TODO we should Test this
        /// </summary>
        /// <returns>Current Map Data in use</returns>
        internal static LoaderContainer GetOld()
        {
            return new()
            {
                TransitionDictionary = TransitionDictionary,
                MapObject = MapObject,
                EventCollection = new EventContainer {EventTypeDictionary = EventTypeDictionary}
            };
        }

        /// <summary>
        ///     Calculate Coordinate from Id, here Player Coordinate only
        /// </summary>
        /// <param name="id">Tile Id</param>
        /// <returns>Coordinates</returns>
        private static Coordinates IdToCoordinate(int id)
        {
            return ArtShared.IdToCoordinate(id, Length, Layer(),
                ImageId);
        }
    }
}