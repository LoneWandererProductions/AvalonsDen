/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTestsUI/Renderer.cs
 * PURPOSE:     Basic Tests for Avalon, here we try to test the Renderer, well good luck with that one
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using AvalonRuntime;
using MapGenerator;
using NUnit.Framework;
using Renderer;

namespace AvalonsDenTestsUI
{
    /// <summary>
    ///     The Avalon's den renderer unit test class.
    /// </summary>
    public sealed class Renderer
    {
        /// <summary>
        ///     Check if our comparer works
        ///     Map:
        ///     0,1,2,3
        ///     4,5,6,7
        ///     8,9,10,11
        ///     Basic Initiate
        /// </summary>
        [Test]
        [Apartment(ApartmentState.STA)]
        public void CheckRenderer()
        {
            //build map done
            var renderTile = new RenderTile(ResourcesLoader.MasterTile,
                ResourcesAvalonsDenRenderer.Height, ResourcesAvalonsDenRenderer.Length, true, 0, null);

            Assert.IsNotNull(renderTile, "Passed the basic Initiation");

            //build map done
            renderTile = new RenderTile(ResourcesLoader.MasterTile,
                ResourcesAvalonsDenRenderer.Height, ResourcesAvalonsDenRenderer.Length, false, 0, null);

            var layer = renderTile.GetMaxLayers();

            Assert.IsTrue(layer == 8, "Not the right Amount of layers");

            renderTile.GenerateMap();

            var lst = renderTile.GetChangeList();

            var coordinate = ArtShared.IdToCoordinate(2, ResourcesAvalonsDenRenderer.Length,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.GrassId].Layer,
                ResourcesAvalonsDenRenderer.GrassId);

            renderTile.ChangeSingleGraphics(coordinate);

            Assert.IsNotNull(lst, "Passed the basic Initiation: " + lst.Count);

            //build map done
            renderTile = new RenderTile(ResourcesLoader.MasterTile,
                ResourcesAvalonsDenRenderer.Height, ResourcesAvalonsDenRenderer.Length, true, 0, null);

            renderTile.GenerateMap();

            lst = renderTile.GetChangeList();

            coordinate = ArtShared.IdToCoordinate(2, 4,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.GrassId].Layer,
                ResourcesAvalonsDenRenderer.GrassId);

            renderTile.ChangeSingleGraphics(coordinate);

            Assert.IsTrue(lst.Count == 1, "Passed the basic Initiation: " + lst.Count);
        }

        /// <summary>
        ///     Check if our comparer works
        ///     Map:
        ///     0,1,2,3
        ///     4,5,6,7
        ///     8,9,10,11
        ///     Build a Donut with hill Tiles
        ///     Basic Initiate
        ///     NW,N,NE
        ///     W ,X,E
        ///     SW,S,SE
        /// </summary>
        [Test]
        [Apartment(ApartmentState.STA)]
        public void CheckRendererCreateMap()
        {
            var masterTile = HelperMethods.GetData();

            //build map done
            var renderTile = new RenderTile(ResourcesLoader.MasterTile,
                ResourcesAvalonsDenRenderer.Height, ResourcesAvalonsDenRenderer.Length, true, 0, null);

            renderTile.GenerateMap();

            Assert.IsNotNull(renderTile, "Passed the basic Initiation");

            var coordinate = ArtShared.IdToCoordinate(5, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.GrassId].Layer,
                ResourcesAvalonsDenRenderer.GrassId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(6, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.GrassId].Layer,
                ResourcesAvalonsDenRenderer.GrassId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(7, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.GrassId].Layer,
                ResourcesAvalonsDenRenderer.GrassId);
            renderTile.ChangeSingleGraphics(coordinate);
            //overwrite
            coordinate = ArtShared.IdToCoordinate(5, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.GrassId].Layer,
                ResourcesAvalonsDenRenderer.GrassId);
            renderTile.ChangeSingleGraphics(coordinate);

            var lst = renderTile.GetChangeList();
            Assert.IsTrue(lst.Count == 3, "Passed the basic overwrite check one: " + lst.Count);

            //0,11
            //4,7
            //8,13
            //10,12
            //2,10
            //6,6
            //9,8
            //1,9

            //First
            coordinate = ArtShared.IdToCoordinate(9, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.HillSId].Layer,
                ResourcesAvalonsDenRenderer.HillSId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(8, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.HillSwId].Layer,
                ResourcesAvalonsDenRenderer.HillSwId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(4, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.HillWId].Layer,
                ResourcesAvalonsDenRenderer.HillWId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(0, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.HillNwId].Layer,
                ResourcesAvalonsDenRenderer.HillNwId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(1, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.HillNId].Layer,
                ResourcesAvalonsDenRenderer.HillNId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(2, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.HillNeId].Layer,
                ResourcesAvalonsDenRenderer.HillNeId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(6, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.HillEId].Layer,
                ResourcesAvalonsDenRenderer.HillEId);
            renderTile.ChangeSingleGraphics(coordinate);
            coordinate = ArtShared.IdToCoordinate(10, ResourcesAvalonsDenRenderer.Height,
                ResourcesLoader.MasterTile[ResourcesAvalonsDenRenderer.HillSeId].Layer,
                ResourcesAvalonsDenRenderer.HillSeId);
            renderTile.ChangeSingleGraphics(coordinate);

            lst = renderTile.GetChangeList();
            Assert.IsTrue(lst.Count == 11, "Passed the basic overwrite check two: " + lst.Count);

            //Nice now test our MapEngine
            var mapE = new EditorMapEngine();
            var map = mapE.Generate(ResourcesAvalonsDenRenderer.Height, ResourcesAvalonsDenRenderer.Length);

            //     0,1,2,3
            //     4,5,6,7
            //     8,9,10,11
            //     Build a Donut with hill Tiles
            //     Basic Initiate
            //     NW,N,NE
            //     W ,X,E
            //     SW,S,SE

            //1|1|1|1|1|1|1|1|1|1|1|1
            //1|0|0|0|0|0|0|0|1|0|0|1
            //1|0|1|1|0|1|1|0|1|1|0|1
            //1|0|1|1|0|1|1|0|1|1|0|1
            //1|0|0|0|0|0|0|0|1|0|0|1
            //1|0|1|1|0|1|1|0|1|1|0|1
            //1|0|1|1|0|1|1|0|1|1|0|1
            //1|0|0|0|0|0|0|0|1|0|0|1
            //1|1|1|1|1|1|1|1|1|1|0|1
            //1|0|1|1|0|1|1|0|1|1|0|1
            //1|0|0|0|0|0|0|0|0|0|0|1
            //1|1|1|1|1|1|1|1|1|1|1|1

            var compare = new List<string>
            {
                "1|1|1|1|1|1|1|1|1|1|1|1",
                "1|0|0|0|0|0|0|0|1|0|0|1",
                "1|0|1|1|0|1|1|0|1|1|0|1",
                "1|0|1|1|0|1|1|0|1|1|0|1",
                "1|0|0|0|0|0|0|0|1|0|0|1",
                "1|0|1|1|0|1|1|0|1|1|0|1",
                "1|0|1|1|0|1|1|0|1|1|0|1",
                "1|0|0|0|0|0|0|0|1|0|0|1",
                "1|1|1|1|1|1|1|1|1|1|0|1",
                "1|0|1|1|0|1|1|0|1|1|0|1",
                "1|0|0|0|0|0|0|0|0|0|0|1",
                "1|1|1|1|1|1|1|1|1|1|1|1"
            };

            map = mapE.ChangeMap(lst, map, masterTile.MasterBordersDictionary, ResourcesLoader.MasterTile);

            var one = compare.Aggregate(string.Empty, (current, line) => current + line + Environment.NewLine);
            var two = map.Borders.Aggregate(string.Empty, (current, line) => current + line + Environment.NewLine);

            Debug.WriteLine("one");
            Debug.WriteLine(one);
            Debug.WriteLine("two");
            Debug.WriteLine(two);

            Assert.IsTrue(one.Equals(two), "Map is not correct");

            renderTile.DeleteLayerGraphics(ArtShared.IdToCoordinate(9, ResourcesAvalonsDenRenderer.Height));
            renderTile.DeleteLayerGraphics(ArtShared.IdToCoordinate(8, ResourcesAvalonsDenRenderer.Height));
            renderTile.DeleteLayerGraphics(ArtShared.IdToCoordinate(4, ResourcesAvalonsDenRenderer.Height));
            renderTile.DeleteLayerGraphics(ArtShared.IdToCoordinate(0, ResourcesAvalonsDenRenderer.Height));
            renderTile.DeleteLayerGraphics(ArtShared.IdToCoordinate(1, ResourcesAvalonsDenRenderer.Height));
            renderTile.DeleteLayerGraphics(ArtShared.IdToCoordinate(2, ResourcesAvalonsDenRenderer.Height));
            renderTile.DeleteLayerGraphics(ArtShared.IdToCoordinate(6, ResourcesAvalonsDenRenderer.Height));
            renderTile.DeleteLayerGraphics(ArtShared.IdToCoordinate(10, ResourcesAvalonsDenRenderer.Height));

            lst = renderTile.GetChangeList();
            Assert.AreEqual(2, lst.Count, "Passed the basic overwrite check three: " + lst.Count);
        }
    }
}