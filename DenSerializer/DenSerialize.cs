/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/DenSerializer/DenSerialize.cs
 * PURPOSE:     Class to Serialize all Objects, AvalonsDen specific
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Debugger;
using ExtendedSystemObjects;
using FileHandler;

namespace DenSerializer
{
    /// <summary>
    ///     The den serialize class.
    ///     TODO Remove Most of this Shit. must be done step by Step
    /// </summary>
    public static class DenSerialize
    {
        /// <summary>
        ///     Serializes Dictionary Type of: Dictionary
        ///     Uses SerializeDictionary
        /// </summary>
        /// <param name="transitionDct">Dictionary of Transitions</param>
        /// <param name="path">Target Path</param>
        public static void XmlSerializerTransition(Dictionary<int, List<int>> transitionDct, string path)
        {
            //check if file is empty, if empty return
            if (transitionDct.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorSerializerEmpty, path), ErCode.Error);
                FileHandleDelete.DeleteFile(path);
                return;
            }

            var myDictionary = new Dictionary<int, string>();

            foreach (var node in transitionDct)
            {
                var item = Serialize(node.Value);
                myDictionary.Add(node.Key, item);
            }

            XmlSerializeDictionary(myDictionary, path);
        }

        /// <summary>
        ///     helper class for Dictionary Object also used for external use
        /// </summary>
        /// <param name="dct">Type Id String</param>
        /// <param name="path">File Destination</param>
        private static void XmlSerializeDictionary(Dictionary<int, string> dct, string path)
        {
            //check if file is empty, if empty return
            if (dct.IsNullOrEmpty())
            {
                DebugLog.CreateLogFile(string.Concat(SerialResources.ErrorSerializerEmpty, path), ErCode.Error);
                FileHandleDelete.DeleteFile(path);
                return;
            }

            var sw = new StringWriter(CultureInfo.InvariantCulture);
            try
            {
                var tempDataItems = new List<DataItem>(dct.Count);
                tempDataItems.AddRange(dct.Keys.Select(key => new DataItem(key, dct[key])));

                var serializer = new XmlSerializer(typeof(List<DataItem>));

                var ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);

                serializer.Serialize(sw, tempDataItems, ns);

                using var tr = new StreamWriter(path);
                tr.Write(sw.ToString());
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
                if (true) sw.Dispose();
            }
        }

        /// <summary>
        ///     Converts generic Object into a XML string
        /// </summary>
        /// <typeparam name="T">Generic Type of Object</typeparam>
        /// <param name="obj">Object to Serialize</param>
        /// <returns>Object as XML string</returns>
        private static string Serialize<T>(this T obj)
        {
            var serializer = new DataContractSerializer(obj.GetType());

            using var writer = new StringWriter(CultureInfo.CurrentCulture);
            using var stm = new XmlTextWriter(writer);
            serializer.WriteObject(stm, obj);
            return writer.ToString();
        }
    }
}