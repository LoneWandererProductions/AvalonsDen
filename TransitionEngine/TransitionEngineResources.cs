/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/TransitionEngine/TransitionEngineResources.cs
 * PURPOSE:     Resource Files for strings and magic Number
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace TransitionEngine
{
    /// <summary>
    ///     The transition engine Resource class.
    /// </summary>
    internal static class TransitionEngineResources
    {
        /// <summary>
        ///     Simple First exception
        /// </summary>
        internal const string KeyName = "key: ";

        /// <summary>
        ///     Should not happen
        /// </summary>
        internal const string ErrorWrongKey = "Wrong Key was called: ";

        /// <summary>
        ///     Should not happen
        /// </summary>
        internal const string ErrorWrongCoordinate = "Coordinate with no Transition was provided";

        /// <summary>
        ///     Error in Generation of Tiles
        /// </summary>
        internal const string ErrorCouldnotfindStart = "Could not find start of Multi Terrain";

        /// <summary>
        ///     Simple First exception
        /// </summary>
        internal const string MultiTerrainNotFitting = "Could not Place Multi Terrain Tile";

        /// <summary>
        ///     if their is already a complex Multi Terrain Tile in Place don't overlay it
        /// </summary>
        internal const string MultiTerrainAlreadyInplace = "Multi Terrain Tile already in Place";

        /// <summary>
        ///     Type one is Multi Terrain Tile
        /// </summary>
        internal const int ErrorNumber = -1;

        /// <summary>
        ///     Dummy for Initiation
        /// </summary>
        internal const int CoordinateDirection = -1;
    }
}