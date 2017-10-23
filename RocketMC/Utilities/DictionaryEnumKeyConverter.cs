using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RocketMC.Utilities
{
    /// <summary>
    /// ディクショナリのキーに対する文字列型と列挙型のJSON用コンバータ
    /// </summary>
    public class DictionaryEnumKeyConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType) => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var types = objectType.GetGenericArguments();
            var keyType = types[0];
            var valueType = types[1];
            var intermediateDictionaryType = typeof(Dictionary<,>).MakeGenericType(typeof(string), valueType);
            var intermediateDictionary = (IDictionary)Activator.CreateInstance(intermediateDictionaryType);
            serializer.Populate(reader, intermediateDictionary);

            var finalDictionary = (IDictionary)Activator.CreateInstance(objectType);
            foreach (DictionaryEntry pair in intermediateDictionary)
            {
                finalDictionary.Add(keyType.GetEnum(pair.Key.ToString()), pair.Value);
            }

            return finalDictionary;
        }
    }
}