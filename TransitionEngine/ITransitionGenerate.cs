/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/TransitionEngine/Transition.cs
 * PURPOSE:     Create and Handle Transitions for a Map, Interface Declaration
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

// ReSharper disable UnusedMemberInSuper.Global

using System.Collections.Generic;
using Resources;

namespace TransitionEngine
{
    /// <summary>
    ///     The ITransitionGenerate interface.
    /// </summary>
    internal interface ITransitionGenerate
    {
        /// <summary>
        ///     Get the transitions.
        /// </summary>
        /// <returns>The <see cref="T:Dictionary{int, List{int}}" />.</returns>
        Dictionary<int, List<int>> GetTransitions();

        /// <summary>
        ///     Add the transition.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The New Transition Set <see cref="T:Dictionary{int, int}" />.</returns>
        Dictionary<int, int> AddTransition(Coordinates coordinate);

        /// <summary>
        ///     Delete the transition.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The New Transition Set <see cref="T:Dictionary{int, int}" />.</returns>
        Dictionary<int, int> DeleteTransition(Coordinates coordinate);

        /// <summary>
        ///     Save the transition.
        /// </summary>
        /// <param name="path">The path.</param>
        void SaveTransition(string path);
    }
}