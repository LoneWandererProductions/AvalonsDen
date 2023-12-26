/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/Tile.cs
 * PURPOSE:     Description of Tile
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace Resources
{
    /// <summary>
    ///     Describes Tile on Cell
    /// </summary>
    public sealed class Tile
    {
        /// <summary>
        ///     The tile types enum.
        /// </summary>
        public enum TileTypes
        {
            /// <summary>
            ///     The NoTransitions = 0.
            /// </summary>
            NoTransitions = 0,

            /// <summary>
            ///     The MultiTerrain = 1.
            /// </summary>
            MultiTerrain = 1,

            /// <summary>
            ///     The Terrain with Transitions = 2.
            /// </summary>
            TerrainWithTransitions = 2,

            /// <summary>
            ///     The InternalTiles = 3.
            /// </summary>
            InternalTiles = 3
        }

        /// <summary>
        ///     Full name of File with extension
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     Layer the Tile is on
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        ///     From 0 to 3 perhaps
        ///     0 standard Tile
        ///     1 Multi Terrain
        ///     2 Terrain with Transitions
        ///     3 Internal Tiles
        /// </summary>
        public TileTypes TileType { get; set; }

        /// <summary>
        ///     Id for connection with TileBorder Dictionary
        /// </summary>
        public int BorderId { get; set; }

        /// <summary>
        ///     If this Tile is a Transition piece put the Id of the Master here
        /// </summary>
        public int IdOfMaster { get; set; }

        /// <summary>
        ///     From 1 to 8 clock wise, direction of the Transition
        /// </summary>
        public int DirectionOfTransition { get; set; }
    }
}