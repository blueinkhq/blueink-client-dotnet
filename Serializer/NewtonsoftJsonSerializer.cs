using System;
using System.Collections.Generic;
using System.IO;
using Blueink.Client.Net.v2.Helper;
using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.RequestModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blueink.Client.Net.v2.Serializer
{
    public interface IJsonSerializer : ISerializer
    {
    }

    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializer newtonsoftSerializer;

        private static NewtonsoftJsonSerializer instance;

        /// <summary>A singleton instance of the Newtonsoft JSON Serializer.</summary>
        public static NewtonsoftJsonSerializer Instance
        {
            get
            {
                return (instance = instance ?? new NewtonsoftJsonSerializer());
            }
        }

        static NewtonsoftJsonSerializer()
        {
            // Initialize the Newtonsoft serializer.
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            //   settings.Converters.Add(new RFC3339DateTimeConverter());
            newtonsoftSerializer = JsonSerializer.Create(settings);
        }

        public string Format
        {
            get { return "json"; }
        }

        public void Serialize(object obj, Stream target)
        {
            using (var writer = new StreamWriter(target))
            {
                if (obj == null)
                {
                    obj = string.Empty;
                }
                newtonsoftSerializer.Serialize(writer, obj);
            }
        }

        public string Serialize(object obj)
        {
            using (TextWriter tw = new StringWriter())
            {
                if (obj == null)
                {
                    obj = string.Empty;
                }
                newtonsoftSerializer.Serialize(tw, obj);
                return tw.ToString();
            }
        }

        public T Deserialize<T>(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(input);
        }

        public object Deserialize(string input, Type type)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return JsonConvert.DeserializeObject(input, type);
        }

        public T Deserialize<T>(Stream input)
        {
            // Convert the JSON document into an object.
            using (StreamReader streamReader = new StreamReader(input))
            {
                return (T)newtonsoftSerializer.Deserialize(streamReader, typeof(T));
            }
        }
    }

    public class StringOrBooleanOrNumberConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(object);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            if (reader.TokenType == JsonToken.String)
            {
                return reader.Value.ToString();
            }
            else if (reader.TokenType == JsonToken.Boolean)
            {
                return (bool)reader.Value;
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                return (int)reader.Value;
            }
            throw new JsonSerializationException("Unexpected token type.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is string)
            {
                writer.WriteValue((string)value);
            }
            else if (value is bool)
            {
                writer.WriteValue((bool)value);
            }
            else if (value is int)
            {
                writer.WriteValue((int)value);
            }
            else
            {
                throw new JsonSerializationException("Unexpected value type.");
            }
        }
    }

    public class DocumentOrTemplateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<IDocument>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var oList = value as IList<IDocument>;

            if (oList != null)
            {
                JArray array = new JArray();

                foreach (var item in oList)
                {
                    if (item is DocumentRef oDoc)
                    {
                        array.Add(JToken.FromObject(oDoc));
                    }
                    else if (item is TemplateRef oTemp)
                    {
                        array.Add(JToken.FromObject(oTemp));
                    }
                }

                array.WriteTo(writer);
            }
        }
    }
}