namespace WorldMap2026.Model.Items
{
    public class Field : GameObject
    {
        public Field(Point location) : base(location, new Size(3, 3)) { }
    
        public override bool CanBePlacedOn(TerrainType terrain)
        {
            return terrain != TerrainType.Water && terrain != TerrainType.Rock;
        }
    }
}