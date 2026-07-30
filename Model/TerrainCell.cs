using System.Text.Json.Serialization;

namespace WorldMap2026.Model
{

    public enum TerrainType
    {
        Grass,
        Rock,
        Water,
        Sand
    }

    public class TerrainCell
    {
        public TerrainType Type { get; set; }

        [JsonIgnore]
        public Color CellColor { get; set; }

        [JsonPropertyName("CellColor")]
        public string CellColorHex
        {
            get => ColorTranslator.ToHtml(CellColor);

            set => CellColor = ColorTranslator.FromHtml(value);
        }

        public TerrainCell() { }

        public TerrainCell(TerrainType type)
        {
            Type = type;
            CellColor = type switch
            {
                TerrainType.Grass => Color.LightGreen,
                TerrainType.Water => Color.Blue,
                TerrainType.Sand => Color.Khaki,
                TerrainType.Rock => Color.Gray,
                _ => Color.Gray
            };
        }
    }
}
