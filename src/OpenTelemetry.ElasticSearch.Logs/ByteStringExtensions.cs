using System;
using Google.Protobuf;

namespace OpenTelemetry.ElasticSearch.Logs.Exporter
{
    public static class ByteStringExtensions
    {
        public static string ToHexString(this ByteString byteString)
        {
            byte[] bytes = new byte[byteString.Length];
            byteString.CopyTo(bytes, 0);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}