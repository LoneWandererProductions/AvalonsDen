/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Renderer/Renderer.cs
 * PURPOSE:     Interface that defines all Accesses
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Resources;

// ReSharper disable UnusedMemberInSuper.Global

namespace Renderer
{
    /// <summary>
    ///     The IRenderTile interface.
    /// </summary>
    internal interface IRenderTile
    {
        /// <summary>
        ///     The generated map.
        /// </summary>
        /// <returns>The <see cref="Cells" /> Game Display.</returns>
        Cells GenerateMap();

        /// <summary>
        ///     Get the max layers.
        /// </summary>
        /// <returns>The <see cref="int" /> amount of Layers.</returns>
        int GetMaxLayers();

        /// <summary>
        ///     Display the menu.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="centerbutton">The center button.</param>
        /// <returns>The id<see cref="int" /> of the clicked Menu Item.</returns>
        int DisplayMenu(List<MenuItems> items, bool orientation, bool centerbutton);

        /// <summary>
        ///     Load the Background image .
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="imageName">The imageName.</param>
        void LoadImageBackround(string path, string imageName);

        /// <summary>
        ///     Display movement animation.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="tileId">The tileId.</param>
        /// <param name="timer">The timer.</param>
        void DisplaymovementAnimation(List<Coordinates> path, int tileId, int timer);

        /// <summary>
        ///     Display movement path.
        /// </summary>
        /// <param name="tilePath">The tilePath.</param>
        void DisplaymovementPath(List<Coordinates> tilePath);

        /// <summary>
        ///     Change multiple graphics.
        /// </summary>
        /// <param name="tileLst">The tileId.</param>
        void ChangeMultipleGraphics(List<Coordinates> tileLst);

        /// <summary>
        ///     Change single graphics.
        /// </summary>
        /// <param name="tileId">The tileId.</param>
        void ChangeSingleGraphics(Coordinates tileId);

        /// <summary>
        ///     Change transition graphics.
        /// </summary>
        /// <param name="newTransition">The newTransition.</param>
        /// <param name="changedTiles">The changedTiles.</param>
        void ChangeTransitionGraphics(Dictionary<int, List<int>> newTransition, Dictionary<int, int> changedTiles);

        /// <summary>
        ///     Delete the transition graphics.
        /// </summary>
        /// <param name="newTransition">The newTransition.</param>
        /// <param name="changedTiles">The changedTiles.</param>
        void DeleteTransitionGraphics(Dictionary<int, List<int>> newTransition, Dictionary<int, int> changedTiles);

        /// <summary>
        ///     Delete single graphics.
        /// </summary>
        /// <param name="tileId">The tileId.</param>
        void DeleteSingleGraphics(Coordinates tileId);

        /// <summary>
        ///     Delete multiple graphics.
        /// </summary>
        /// <param name="tileLst">The tiles.</param>
        void DeleteMultipleGraphics(List<Coordinates> tileLst);

        /// <summary>
        ///     Delete the graphics over all layers.
        /// </summary>
        /// <param name="tileId">The tileId.</param>
        void DeleteLayerGraphics(Coordinates tileId);

        /// <summary>
        ///     Get the list of changed Graphics.
        /// </summary>
        /// <returns>The List of changed Tiles as <see cref="T:List{Coordinates}" />.</returns>
        List<Coordinates> GetChangeList();

        /// <summary>
        ///     Show the grid.
        /// </summary>
        void ShowGrid();

        /// <summary>
        ///     Show the numbers.
        /// </summary>
        void ShowNumbers();

        /// <summary>
        ///     Set the click.
        /// </summary>
        void SetClick();
    }
}