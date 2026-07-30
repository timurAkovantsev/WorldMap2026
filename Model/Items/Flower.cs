using System.Text.Json.Serialization;

namespace WorldMap2026.Model.Items
{
    public class Flower : GameObject
    {
        [JsonInclude]
        public int Variant { get; private set; }

        [JsonConstructor]
        public Flower(Point location) : base(location, new Size(1, 1)) { }

        public override int SpriteVariant => Variant;

        public Flower(Point location, Random random)
            : base(location, new Size(1, 1))
        {
            Variant = random.Next(0, 5);
        }

        public override bool CanBePlacedOn(TerrainType terrain)
        {
            return terrain == TerrainType.Grass;
        }
    }
}
