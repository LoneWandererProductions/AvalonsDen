/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/MapGenerator/EditorMapEngineDummy.cs
 * PURPOSE:     Map Dummy Generator
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;

namespace MapGenerator
{
    /// <summary>
    ///     We just generate the Borders and the Map
    /// </summary>
    internal static class EditorMapEngineDummy
    {
        /// <summary>
        ///     Generate the base Layout for the Border Map
        /// </summary>
        /// <param name="mapHeight">Height of the Map multiplied with three</param>
        /// <param name="mapLength">Length of the Map multiplied with three</param>
        /// <returns>Dummy Border as List  as <see cref="T:List{string}" />.</returns>
        internal static List<string> GenerateDummyBorder(int mapHeight, int mapLength)
        {
            var cache = GenerateDummyBorderAsArray(mapHeight, mapLength);
            var borders = EditorMapEngineProcessing.LoadBorderMapFromArray(cache, mapHeight, mapLength);
            return borders;
        }

        /// <summary>
        ///     Generate the base Layout for the Border Map
        /// </summary>
        /// <param name="mapHeight">Height of the Map multiplied with three</param>
        /// <param name="mapLength">Length of the Map multiplied with three</param>
        /// <returns>Dummy Border as Array.</returns>
        internal static int[,] GenerateDummyBorderAsArray(int mapHeight, int mapLength)
        {
            var cache = new int[mapLength * MapGeneratorResources.CellHeight,
                mapHeight * MapGeneratorResources.CellHeight];

            //Create Border Map base
            for (var y = 0; y < mapHeight * MapGeneratorResources.CellHeight; y++) // Map.IntMapLengthX
            for (var x = 0; x < mapLength * MapGeneratorResources.CellHeight; x++)
                cache[x, y] = MapGeneratorResources.Wall;

            cache = SetLengthLine(cache, mapHeight, mapLength);
            cache = SetHeightLine(cache, mapHeight, mapLength);

            return cache;
        }

        /// <summary>
        ///     Clean x row over the range
        /// </summary>
        /// <param name="cache">Border Map</param>
        /// <param name="mapHeight">Height of the Map multiplied with three</param>
        /// <param name="mapLength">Length of the Map multiplied with three</param>
        /// <returns>Cleaned Dummy Border as Array.</returns>
        private static int[,] SetLengthLine(int[,] cache, int mapHeight, int mapLength)
        {
            var yheight = 1;

            //x Line done
            do
            {
                var xlength = 1;

                do
                {
                    cache[xlength, yheight] = MapGeneratorResources.Path;
                    xlength++;
                } while (xlength < mapLength * MapGeneratorResources.CellHeight - 1);

                yheight += MapGeneratorResources.CellHeight;
            } while (yheight < mapHeight * MapGeneratorResources.CellHeight);

            return cache;
        }

        /// <summary>
        ///     Clean y Column over the range
        /// </summary>
        /// <param name="cache">Border Map</param>
        /// <param name="mapHeight">Height of the Map multiplied with three</param>
        /// <param name="mapLength">Length of the Map multiplied with three</param>
        /// <returns>Cleaned Dummy Border as Array.</returns>
        private static int[,] SetHeightLine(int[,] cache, int mapHeight, int mapLength)
        {
            var xlength = 1;

            //y Line done
            do
            {
                var yheight = 1;

                do
                {
                    cache[xlength, yheight] = MapGeneratorResources.Path;
                    yheight++;
                } while (yheight < mapHeight * MapGeneratorResources.CellHeight - 1);

                xlength += MapGeneratorResources.CellHeight;
            } while (xlength < mapLength * MapGeneratorResources.CellHeight);

            return cache;
        }
    }
}