/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/EditorDialogTree/DialogPoints.cs
 * PURPOSE:     Helper Object for placing DialogPoints
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

//Disabled Resharper Warnings, do not make the suggested Changes!
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace EditorDialogTree
{
    /// <summary>
    ///     Simple Point Description
    /// </summary>
    internal sealed class DialogPoints
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DialogPoints" /> class.
        /// </summary>
        internal DialogPoints()
        {
        }

        /// <summary>
        ///     2 Dimensional Point
        /// </summary>
        /// <param name="xrow">x row</param>
        /// <param name="ycolumn">y column</param>
        internal DialogPoints(int xrow, int ycolumn)
        {
            Xrow = xrow;
            Ycolumn = ycolumn;
        }

        /// <summary>
        ///     Row as X
        /// </summary>
        internal int Xrow { get; set; }

        /// <summary>
        ///     Column as Y in Order
        /// </summary>
        internal int Ycolumn { get; set; }

        /// <summary>
        ///     Compares If a Coordinate is equal to other Coordinate
        /// </summary>
        /// <param name="dialogOne"></param>
        /// <returns></returns>
        internal bool Equals(DialogPoints dialogOne)
        {
            return Xrow == dialogOne?.Xrow && Ycolumn == dialogOne.Ycolumn;
        }

        /// <summary>
        ///     Generate Hash Code just for the Three Attributes, we don't need More
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Xrow ^ Ycolumn;
        }

        /// <summary>
        ///     Provides the equal Command
        /// </summary>
        /// <param name="obj">Other Object</param>
        /// <returns>If Object is equal</returns>
        public override bool Equals(object obj)
        {
            return obj is DialogPoints dlg && Equals(dlg);
        }
    }
}