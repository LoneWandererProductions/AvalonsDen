/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Resources/StructPoints.cs
 * PURPOSE:     Helper Struct as simpler Replacement for Coordinates
 * PROGRAMER:   Wayfarer
 */

namespace EditorDialogTree
{
    /// <summary>
    ///     Simple Point Description
    ///     Todo Replace with Class Struct doesn't add anything of Value
    /// </summary>
    public struct StructPoints
    {
        /// <summary>
        ///     3 Dimensional Point
        /// </summary>
        /// <param name="xrow">x row</param>
        /// <param name="ycolumn">y column</param>
        /// <param name="zLayer">z layer</param>
        public StructPoints(int xrow, int ycolumn, int zLayer)

        {
            Xrow = xrow;
            Ycolumn = ycolumn;
            ZLayer = zLayer;
        }

        /// <summary>
        ///     Row as X
        /// </summary>
        public int Xrow { get; set; }

        /// <summary>
        ///     Column as Y
        /// </summary>
        public int Ycolumn { get; set; }

        /// <summary>
        ///     Layer of Map
        /// </summary>
        public int ZLayer { get; set; }
    }
}