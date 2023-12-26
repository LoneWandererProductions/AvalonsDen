/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/TransitionEngine/TransitionException.cs
 * PURPOSE:     Exception Class
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 * SOURCES:     https://msdn.microsoft.com/en-us/library/system.exception.getobjectdata.aspx
 */

using System;
using System.Runtime.Serialization;

namespace TransitionEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     The transition exception class.
    /// </summary>
    /// <seealso cref="T:System.Exception" />
    [Serializable]
    public sealed class TransitionException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TransitionEngine.TransitionException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public TransitionException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TransitionEngine.TransitionException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        private TransitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TransitionEngine.TransitionException" /> class.
        /// </summary>
        public TransitionException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TransitionEngine.TransitionException" /> class.
        /// </summary>
        /// <param name="message">The message we declarte</param>
        /// <param name="innerException">
        ///     The Exception that caused the Exception or a null reference <see langword="Nothing" /> in
        ///     Visual Basic), if there is no inner Exception.
        /// </param>
        public TransitionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}