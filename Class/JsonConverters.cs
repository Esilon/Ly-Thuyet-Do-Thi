using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Đồ_Thị.Class
{
    public class Array2DConverter : JsonConverter<int[,]>
    {
        public override int[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<List<List<int>>>(ref reader, options);
            if (list == null || list.Count == 0) return new int[0, 0];
            int rows = list.Count;
            int cols = list[0].Count;
            int[,] array = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                if (list[i].Count != cols)
                {
                    throw new JsonException($"Row {i} has {list[i].Count} columns, but {cols} columns were expected.");
                }
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = list[i][j];
                }
            }
            return array;
        }

        public override void Write(Utf8JsonWriter writer, int[,] value, JsonSerializerOptions options)
        {
            int rows = value.GetLength(0);
            int cols = value.GetLength(1);
            writer.WriteStartArray();
            for (int i = 0; i < rows; i++)
            {
                writer.WriteStartArray();
                for (int j = 0; j < cols; j++)
                {
                    writer.WriteNumberValue(value[i, j]);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }
    }

    public class PointFConverter : JsonConverter<PointF>
    {
        public override PointF Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;
            float x = root.GetProperty("X").GetSingle();
            float y = root.GetProperty("Y").GetSingle();
            return new PointF(x, y);
        }

        public override void Write(Utf8JsonWriter writer, PointF value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", value.X);
            writer.WriteNumber("Y", value.Y);
            writer.WriteEndObject();
        }
    }
}
