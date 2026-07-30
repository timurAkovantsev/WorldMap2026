namespace WorldMap2026.Model.Items
{
    public class Windmill : GameObject
    {
        public Windmill(Point location) : base(location, new Size(2, 2))
        {
        }

        public override bool CanBePlacedOn(TerrainType terrain)
        {
            return terrain != TerrainType.Water;
        }
    }
}
