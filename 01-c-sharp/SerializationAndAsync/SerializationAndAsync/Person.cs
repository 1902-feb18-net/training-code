using Newtonsoft.Json;
using System.Xml.Serialization;

namespace SerializationAndAsync
{
    // classes to be serialized/deserialized (DTOs, data transfer objects)
    // should be POCOs (plain old C# objects)
    // which means, it must have a zero-parameter constructor
    // and public get-set properties

    public class Person
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute("FullName")]
        public string Name { get; set; }

        [XmlElement(ElementName = "StreetAddress")]
        public Address Address { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public int Data { get; set; }
    }
}
