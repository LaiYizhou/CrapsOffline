
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Text;

public class SerializerManager
{

    public static string Serialize(object o)
    {
        var xml = "";
        try
        {
            var serializer = new XmlSerializer(o.GetType());
            using (var ms = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(ms, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    var n = new XmlSerializerNamespaces();
                    n.Add("", "");
                    serializer.Serialize(writer, o, n);
                    ms.Seek(0, SeekOrigin.Begin);

                    using (var reader = new StreamReader(ms))
                    {
                        xml = reader.ReadToEnd();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(string.Format("Serialize {0} found error {1}", o, ex));
            xml = "";
        }
        return xml;
    }

    public static object Deserialize(Type t, string xml)
    {
        object o = null;
        try
        {
            var serializer = new XmlSerializer(t);
            using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                o = serializer.Deserialize(mem);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(string.Format("Deserialize {0} found error {1}", o, ex));
            o = null;
        }
        return o;
    }



}
