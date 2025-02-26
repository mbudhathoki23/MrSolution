using System.IO;
using System.Xml.Serialization;

namespace MrDAL.Core.Utils;

public static class XmlUtils
{
    public static string SerializeToXml<T>(T model) where T : class
    {
        if (model == null) return null;

        var xmlSerializer = new XmlSerializer(model.GetType());
        try
        {
            using var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, model);
            return textWriter.ToString();
        }
        catch
        {
            return null;
        }
    }

    public static T XmlDeserialize<T>(string value) where T : class
    {
        if (string.IsNullOrWhiteSpace(value)) return null;

        var xmlSerializer = new XmlSerializer(typeof(T));
        using var textReader = new StringReader(value);
        try
        {
            return (T)xmlSerializer.Deserialize(textReader);
        }
        catch
        {
            return null;
        }
    }
}