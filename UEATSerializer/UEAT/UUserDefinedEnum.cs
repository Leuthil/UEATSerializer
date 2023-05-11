using Newtonsoft.Json;
using UEATSerializer.Serializer;

namespace UEATSerializer.UEAT
{
    public class UUserDefinedEnum : UEnum
    {
        // Asset->DisplayNameMap
        public Dictionary<string, string> DisplayNameMap { get; set; } = new Dictionary<string, string>();

        public override void WriteJsonInlined(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WritePropertyName("DisplayNameMap");
            writer.WriteStartArray();
            foreach (var keyValuePair in DisplayNameMap)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Name");
                writer.WriteValue(keyValuePair.Key);
                writer.WritePropertyName("DisplayName");
                writer.WriteValue(keyValuePair.Value);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            base.WriteJsonInlined(writer, serializer, objectHierarchy);
        }
    }
}
