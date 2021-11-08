using System;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace OMSSigner
{
    public sealed class XmlAnything<T> : IXmlSerializable
    {
        public XmlAnything() { }
        public XmlAnything(T t) { this.Value = t; }
        public T Value { get; set; }

        public void WriteXml(XmlWriter writer)
        {
            if (Value == null)
            {
                writer.WriteAttributeString("type", "null");
                return;
            }
            Type type = this.Value.GetType();
            XmlSerializer serializer = new XmlSerializer(type);
            writer.WriteAttributeString("type", type.AssemblyQualifiedName);
            serializer.Serialize(writer, this.Value);
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.HasAttributes)
                throw new FormatException("expected a type attribute!");
            string type = reader.GetAttribute("type");
            reader.Read(); // consume the value
            if (type == "null")
                return;// leave T at default value
            XmlSerializer serializer = new XmlSerializer(Type.GetType(type));
            this.Value = (T)serializer.Deserialize(reader);
            reader.ReadEndElement();
            
        }

        public XmlSchema GetSchema() { return (null); }

        public static XmlAnything<T>[] WrapFromArray(T[] source)
        {
            if (source == null) return default;

            XmlAnything<T>[] result = new XmlAnything<T>[source.Length];

            for (int i=0; i<source.Length; i++)
            {
                result[i]= new XmlAnything<T>(source[i]);
            }

            return result;
        }

        public static T[] WrapToArray(XmlAnything<T>[] source)
        {
            if (source == null) return default;

            T[] result = new T[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = source[i].Value;
            }

            return result;
        }
    }


}
