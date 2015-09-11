﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/*
 * StackOverflow
 * -------------
 * URL:
 * http://stackapps.com/a/1176
 */

namespace Telesharp.Common.JsonConverters
{
    /// <summary>
    ///     Useful when serializing/deserializing json for use with the Stack Overflow API, which produces and consumes Unix
    ///     Timestamp dates
    /// </summary>
    /// <remarks>
    ///     swiped from lfoust and fixed for latest json.net with some tweaks for handling out-of-range dates
    /// </remarks>
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        //public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        //{
        //    if (reader.TokenType != JsonToken.Integer)
        //        throw new Exception("Wrong Token Type");

        //    long ticks = (long)reader.Value;
        //    return ticks.FromUnixTime();
        //}

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long val;
            if (value is DateTime)
            {
                val = UnixDateTimeConverterHelper.ToUnixTime((DateTime) value);
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
            writer.WriteValue(val);
        }

        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
                throw new Exception("Wrong Token Type");

            var ticks = (long) reader.Value;
            return UnixDateTimeConverterHelper.FromUnixTime(ticks);
        }
    }

    public class UnixDateTimeConverterHelper
    {
        /// <summary>
        ///     Convert a long into a DateTime
        /// </summary>
        public static DateTime FromUnixTime(long self)
        {
            var ret = new DateTime(1970, 1, 1);
            return ret.AddSeconds(self);
        }

        /// <summary>
        ///     Convert a DateTime into a long
        /// </summary>
        public static long ToUnixTime(DateTime self)
        {
            if (self == DateTime.MinValue)
            {
                return 0;
            }

            var epoc = new DateTime(1970, 1, 1);
            var delta = self - epoc;

            if (delta.TotalSeconds < 0) throw new ArgumentOutOfRangeException("self");

            return (long) delta.TotalSeconds;
        }
    }
}