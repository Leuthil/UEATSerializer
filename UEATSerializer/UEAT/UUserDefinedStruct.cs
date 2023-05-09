using Newtonsoft.Json;
using UEATSerializer.Serializer;

namespace UEATSerializer.UEAT
{
    public class UUserDefinedStruct : UScriptStruct
    {
        public FGuidPropertyValue Guid { get; set; } = new FGuidPropertyValue(0, 0, 0, 0);
        public List<KeyValuePair<string, FPropertyValue>> StructDefaultInstanceProperties { get; set; } = new List<KeyValuePair<string, FPropertyValue>>();

        public override int[] ResolveObjectReferences(PackageObjectHierarchy objectHierarchy)
        {
            HashSet<int> referencedObjects = new HashSet<int>();

            foreach (var property in StructDefaultInstanceProperties)
            {
                referencedObjects.UnionWith(property.Value.ResolveObjectReferences(objectHierarchy));
            }

            referencedObjects.UnionWith(base.ResolveObjectReferences(objectHierarchy));

            return referencedObjects.ToArray();
        }

        public override void WriteJsonInlined(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WritePropertyName("Guid");
            // for some reason Guid should be serialized here as a single string instead of into separate components like it usually is
            //Guid.WriteJson(writer, serializer, objectHierarchy);
            writer.WriteValue($"{Guid.A:X8}{Guid.B:X8}{Guid.C:X8}{Guid.D:X8}");

            writer.WritePropertyName("StructDefaultInstance");
            writer.WriteStartObject();
            foreach (var property in StructDefaultInstanceProperties)
            {
                if (property.Value == null)
                {
                    continue;
                }

                writer.WritePropertyName(property.Key);
                property.Value.WriteJson(writer, serializer, objectHierarchy);
            }
            writer.WriteEndObject();

            base.WriteJsonInlined(writer, serializer, objectHierarchy);
        }
    }
}
