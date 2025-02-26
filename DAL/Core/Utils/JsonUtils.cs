using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MrDAL.Core.Utils;

public static class JsonUtils
{
    public static StringContent ToJsonStringContent(object data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return content;
    }

    public static (StreamContent Content, string Json) ToGZipJsonStreamContent(object data)
    {
        var json = data.ToJson();
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        var mStream = new MemoryStream();

        using (var gzip = new GZipStream(mStream, CompressionMode.Compress, true))
        {
            gzip.Write(jsonBytes, 0, jsonBytes.Length);
        }

        mStream.Position = 0;

        var content = new StreamContent(mStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Headers.ContentEncoding.Add("gzip");

        return (content, json);
    }

    public static string ToJson<T>(this T data) where T : class
    {
        try
        {
            return JsonConvert.SerializeObject(data);
        }
        catch (JsonException e)
        {
            e.ToNonQueryErrorResult("JsonUtils");
            return null;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("JsonUtils");
            return null;
        }
    }
}