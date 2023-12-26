/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/TransitionEngine/TransitionLoader.cs
 * PURPOSE:     Handle Serialization of Transition File
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using AvalonRuntime;
using DenSerializer;

namespace TransitionEngine
{
    /// <summary>
    ///     The transition loader class.
    /// </summary>
    internal static class TransitionLoader
    {
        /// <summary>
        ///     Removes Extension and adds new one
        /// </summary>
        /// <param name="tileDictionary">The Transitions</param>
        /// <param name="path">Target Path</param>
        internal static void SaveTransition(Dictionary<int, List<int>> tileDictionary, string path)
        {
            DenSerialize.XmlSerializerTransition(tileDictionary,
                Path.ChangeExtension(path, ArtConst.TransitionFileExt));
        }
    }
}