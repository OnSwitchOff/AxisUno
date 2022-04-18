// <copyright file="XmlExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Extensions
{
    using System.IO;
    using System.Xml.Serialization;
    using Microinvest.CommonLibrary.Helpers;

    /// <summary>
    /// Provides generic methods to serialize/deserialize xml data.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Serialize model to string.
        /// </summary>
        /// <typeparam name="T">Type of object to convert data.</typeparam>
        /// <param name="data">Object to convert data.</param>
        /// <returns>Xml file.</returns>
        /// <date>12.04.2022.</date>
        public static string SerializeData<T>(T data)
            where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriterUtf8 stringWriterUtf = new StringWriterUtf8())
            {
                xmlSerializer.Serialize(stringWriterUtf, data);
                return stringWriterUtf.ToString();
            }
        }

        /// <summary>
        /// Deserialize xml to model.
        /// </summary>
        /// <typeparam name="T">Type of object to convert data.</typeparam>
        /// <param name="xml">Xml file to convert to model.</param>
        /// <returns>Model of data.</returns>
        /// <date>12.04.2022.</date>
        public static T? DeserializeData<T>(string xml)
            where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (TextReader textReader = new StringReader(xml))
            {
                return xmlSerializer.Deserialize(textReader) as T;
            }
        }
    }
}
