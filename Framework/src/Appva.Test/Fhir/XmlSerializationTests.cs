// <copyright file="XmlSerializationTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Cryptography
{
    #region Import.

    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Xunit;
    using System.Text;
    using Appva.Fhir.Primitives;
    using Appva.Fhir.Resources.Security;
    using Appva.Fhir.Complex;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json.Converters;
    using System;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class XmlSerializationTests
    {
        #region Tests.

        /// <summary>
        /// 
        /// </summary>
        /*[Fact]
        public void Fhir_Xml_CanSerializeMeta_IsTrue()
        {
            var se = new SecurityEvent();
            se.Event = new SecurityEventEvent()
            {
                Action = new Code("C"),
                Outcome = new Code("12345"),
                Type = new CodeableConcept
                {
                    Coding = new Collection<Coding>{
                        new Coding { Code = new Code("sdf") }
                    },
                    Text = "test"
                }
                
            };
            var xml = serialize<SecurityEvent>(se);
            Assert.Equal("2", xml);
        }

        [Fact]
        public void Fhir_Json_CanSerializeMeta_IsTrue()
        {
            var se = new SecurityEvent();
            se.Event = new SecurityEventEvent()
            {
                Action = new Code("C"),
                Outcome = new Code("12345"),
                Type = new CodeableConcept
                {
                    Coding = new Collection<Coding>{
                        new Coding { Code = new Code("sdf") }
                    },
                    Text = "test"
                }

            };
            var json = serializeJson<SecurityEvent>(se);
            Assert.Equal("2", json);
        }*/

        public string serializeJson<T>(T obj) where T : class
        {
            /*using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                new JsonSerializer().Serialize(new JsonTextWriter(writer)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                }, obj);
                stream.Position = 0;
                return Encoding.UTF8.GetString(stream.ToArray());
            }*/
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new ConverterContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string serialize<T>(T obj) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    serializer.Serialize(writer, obj);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
        }

        #endregion
    }


    public class ConverterContractResolver : CamelCasePropertyNamesContractResolver
    {
        public new static readonly ConverterContractResolver Instance = new ConverterContractResolver();

        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);
            if (objectType == typeof(Code))
                contract.Converter = new StringEnumConverter();

            return contract;
        }
    }

    public class StringEnumConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            var e = value as Primitive<object>;
            writer.WriteValue(e.Value);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return "";
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            /*Type t = (ReflectionUtils.IsNullableType(objectType))
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;
            */
            return objectType.Equals(typeof(Primitive<>));
        }
    }
}
