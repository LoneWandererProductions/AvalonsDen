/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Engine/MusicEngine.cs
 * PURPOSE:     Basic Player Engine
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.IO;
using System.Windows.Media;

namespace GameEngine
{
    /// <summary>
    ///     Simple Player Class must be expended for now just testing stuff
    /// </summary>
    public static class MusicEngine
    {
        /// <summary>
        ///     The media player (readonly). Value: new MediaPlayer().
        /// </summary>
        private static readonly MediaPlayer MediaPlayer = new();

        /// <summary>
        ///     The play music.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void PlayMusic(string path)
        {
            var fileName = Directory.GetCurrentDirectory();
            MediaPlayer.Open(new Uri(fileName + path));
            MediaPlayer.Play();
        }

        /// <summary>
        ///     Stop playing music
        /// </summary>
        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}