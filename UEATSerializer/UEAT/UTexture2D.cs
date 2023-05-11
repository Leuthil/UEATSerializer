using Newtonsoft.Json;
using UEATSerializer.Serializer;

namespace UEATSerializer.UEAT
{
    public class UTexture2D : UObject
    {
        /// <summary>
        /// Serialization disabled. No need to fill out.
        /// </summary>
        //public string LightingGuid { get; set; }

        /// <summary>
        /// Serialization disabled. No need to fill out.
        /// </summary>
        //public string ImportedSize { get; set; }

        public int TextureWidth { get; set; } = 1;
        public int TextureHeight { get; set; } = 1;
        public int TextureDepth { get; set; } = 1;
        public int NumSlices { get; set; } = 1;
        public string CookedPixelFormat { get; set; }
        public string SourceImageHash { get; set; } = 0.ToString("x2");

        public UTexture2D()
        {
            DisabledProperties.AddRange(new[] {
                "LightingGuid",
                "ImportedSize",
            });
        }

        protected override void WriteJsonForData(JsonWriter writer, JsonSerializer serializer, PackageObjectHierarchy objectHierarchy)
        {
            writer.WritePropertyName("TextureWidth");
            writer.WriteValue(TextureWidth);
            writer.WritePropertyName("TextureHeight");
            writer.WriteValue(TextureHeight);
            writer.WritePropertyName("TextureDepth");
            writer.WriteValue(TextureDepth);
            writer.WritePropertyName("NumSlices");
            writer.WriteValue(NumSlices);

            if (CookedPixelFormat != null)
            {
                writer.WritePropertyName("CookedPixelFormat");
                writer.WriteValue(CookedPixelFormat);
            }

            if (SourceImageHash != null)
            {
                writer.WritePropertyName("SourceImageHash");
                writer.WriteValue(SourceImageHash);
            }
        }
    }
}
