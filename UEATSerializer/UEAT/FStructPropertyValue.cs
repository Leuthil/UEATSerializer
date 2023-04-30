using Newtonsoft.Json;
using UEATSerializer.Serializer;

namespace UEATSerializer.UEAT
{
    public abstract class FStructPropertyValue : FPropertyValue
    {
    }

    public class FDateTimeStructPropertyValue : FStructPropertyValue
    {
        /// <summary>
        /// Number of ticks since midnight, January 1, 0001.
        /// </summary>
        public ulong Ticks { get; set; }

        public override void WriteJson(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WriteStartObject();
            // should be serialized as string
            writer.WriteValue(Ticks.ToString());
            writer.WriteEndObject();
        }
    }

    public class FTimespanStructPropertyValue : FStructPropertyValue
    {
        /// <summary>
        /// Number of ticks since midnight, January 1, 0001.
        /// </summary>
        public ulong Ticks { get; set; }

        public override void WriteJson(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WriteStartObject();
            // should be serialized as string
            writer.WriteValue(Ticks.ToString());
            writer.WriteEndObject();
        }
    }

    public class FFallbackStructPropertyValue : FStructPropertyValue
    {
        public List<KeyValuePair<string, FPropertyValue>> Properties { get; set; } = new List<KeyValuePair<string, FPropertyValue>>();

        public override int[] ResolveObjectReferences(PackageObjectHierarchy objectHierarchy)
        {
            HashSet<int> referencedObjects = new HashSet<int>();

            foreach (var property in Properties)
            {
                referencedObjects.UnionWith(property.Value.ResolveObjectReferences(objectHierarchy));
            }

            return referencedObjects.ToArray();
        }

        public override void WriteJson(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WriteStartObject();

            foreach (var property in Properties)
            {
                writer.WritePropertyName(property.Key);
                property.Value.WriteJson(writer, serializer, objectHierarchy);
            }

            writer.WriteEndObject();
        }
    }

    public class FGuidPropertyValue : FStructPropertyValue
    {
        public uint A { get; init; }
        public uint B { get; init; }
        public uint C { get; init; }
        public uint D { get; init; }

        public FGuidPropertyValue(uint v)
        {
            A = B = C = D = v;
        }

        public FGuidPropertyValue(uint a, uint b, uint c, uint d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public FGuidPropertyValue(string hexString)
        {
            A = Convert.ToUInt32(hexString.Substring(0, 8), 16);
            B = Convert.ToUInt32(hexString.Substring(8, 8), 16);
            C = Convert.ToUInt32(hexString.Substring(16, 8), 16);
            D = Convert.ToUInt32(hexString.Substring(24, 8), 16);
        }

        public override void WriteJson(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("A");
            writer.WriteValue(A);
            writer.WritePropertyName("B");
            writer.WriteValue(B);
            writer.WritePropertyName("C");
            writer.WriteValue(C);
            writer.WritePropertyName("D");
            writer.WriteValue(D);
            writer.WriteEndObject();
        }

        public override string ToString()
        {
            return $"{A:X8}-{B:X8}-{C:X8}-{D:X8}";
        }

        public static bool operator ==(FGuidPropertyValue one, FGuidPropertyValue two) => one.A == two.A && one.B == two.B && one.C == two.C && one.D == two.D;
        public static bool operator !=(FGuidPropertyValue one, FGuidPropertyValue two) => one.A != two.A || one.B != two.B || one.C != two.C || one.D != two.D;

        public override bool Equals(object? obj)
        {
            if (obj is not FGuidPropertyValue)
            {
                return false;
            }

            return this == (FGuidPropertyValue)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C, D);
        }
    }

}
