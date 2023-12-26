/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DenSerializer/DenDeSerialize.cs
 * PURPOSE:     Class to De-serialize all Objects
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Debugger;
using FileHandler;

namespace DenSerializer
{
    /// <summary>
    ///     The den De serialize class.
    ///     TODO Remove this shit, but before we must write tests
    /// </summary>
    public static class DenDeSerialize
    {
        /// <summary>
        ///     Returns a Dictionary of a TileHandlerNode
        ///     TODO not Nullable
        /// </summary>
        /// <param name="path">Target Path</param>
        /// <returns>Dictionary of TileHandlerNode</returns>
        [return: MaybeNull]
        public static Dictionary<int, List<int>> XmlDeSerializerTransitionDictionaryDictionary(string path)
        {
            //if File exists
            if (!FileHandleSearch.FileExists(path))
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorPath, path), ErCode.Error);
                return new Dictionary<int, List<int>>();
            }

            //check if file is empty, if empty return a new empty one
            if (!FileContent(path))
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorFileEmpty, path), ErCode.Error);
                return new Dictionary<int, List<int>>();
            }

            var sr = new StreamReader(path);

            try
            {
                var xs = new XmlSerializer(typeof(List<DataItem>));
                var lst = (List<DataItem>)xs.Deserialize(sr);

                var tileNodeVector = new Dictionary<int, List<int>>();

                if (lst == null) return null;

                foreach (var node in lst)
                {
                    var cache = Deserialize<List<int>>(node.Value);
                    tileNodeVector.Add(node.Key, cache);
                }

                return tileNodeVector;
            }
            catch (InvalidOperationException ex)
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorSerializerXml, ex), ErCode.Error);
            }
            catch (XmlException ex)
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorSerializerXml, ex), ErCode.Error);
            }
            catch (NullReferenceException ex)
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorSerializerXml, ex), ErCode.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorStream, ex), ErCode.Error);
            }
            catch (ArgumentException ex)
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorStream, ex), ErCode.Error);
            }
            catch (IOException ex)
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorStream, ex), ErCode.Error);
            }
            finally
            {
                sr.Dispose();
            }

            return new Dictionary<int, List<int>>();
        }

        /// <summary>
        ///     Generic deserializer.
        /// </summary>
        /// <typeparam name="T">>Generic Type of Object</typeparam>
        /// <param name="serialized">Object Serialized</param>
        /// <returns>The deserialized Object<see cref="T" />.</returns>
        private static T Deserialize<T>(this string serialized)
        {
            var serializer = new DataContractSerializer(typeof(T));

            using var reader = new StringReader(serialized);
            using var stm = new XmlTextReader(reader);
            return (T)serializer.ReadObject(stm);
        }

        /// <summary>
        ///     Basic check if File is empty
        /// </summary>
        /// <param name="path">Name of the File</param>
        /// <returns>Status if File has actual content and some Debug Messages for easier Debugging</returns>
        private static bool FileContent(string path)
        {
            return new FileInfo(path).Length != 0;
        }
    }
}