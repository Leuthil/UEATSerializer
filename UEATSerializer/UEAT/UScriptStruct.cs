using Newtonsoft.Json;
using UEATSerializer.Serializer;

namespace UEATSerializer.UEAT
{
    public class UScriptStruct : UStruct
    {
        // EStructFlags
        public uint StructFlags { get; set; }

        public override void WriteJsonInlined(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WritePropertyName("StructFlags");
            writer.WriteValue(StructFlags);

            base.WriteJsonInlined(writer, serializer, objectHierarchy);
        }
    }
}
