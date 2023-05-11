using Newtonsoft.Json;
using UEATSerializer.Serializer;

namespace UEATSerializer.UEAT
{
    public class UEnum : UObject
    {
        // Enum->NumEnums()
        public List<EnumName> Names { get; set; } = new List<EnumName>();
        // (uint8)Enum->GetCppForm()
        public byte CppForm { get; set; }

        public override void WriteJsonInlined(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WritePropertyName("Names");
            writer.WriteStartArray();
            foreach (var name in Names)
            {
                name.WriteJson(writer, serializer, objectHierarchy);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("CppForm");
            writer.WriteValue(CppForm);

            base.WriteJsonInlined(writer, serializer, objectHierarchy);
        }

        public class EnumName : ISerializableForUEAT
        {
            // Enum->GetValueByIndex(i)
            public long Value { get; set; }
            // Enum->GetNameStringByIndex(i)
            public string Name { get; set; }

            public void WriteJson(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
            {
                writer.WriteStartObject();
                WriteJsonInlined(writer, serializer, objectHierarchy);
                writer.WriteEndObject();
            }

            public void WriteJsonInlined(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
            {
                writer.WritePropertyName("Value");
                writer.WriteValue(Value);
                writer.WritePropertyName("Name");
                writer.WriteValue(Name);
            }
        }
    }


}
