using System.Text.Json.Serialization;

namespace WorldMap2026.Model.Items
{
    public enum TreeState
    {
        Alive,
        Chopped
    }

    public class Tree : GameObject
    {
        public Tree(Point location) : base(location, new Size(2, 2))
        {

        }

        [JsonInclude]
        public TreeState State { get; private set; } = TreeState.Alive;

        public override bool CanBePlacedOn(TerrainType terrain)
        {
            return terrain != TerrainType.Water && terrain != TerrainType.Rock;
        }

        public void Chop()
        {
            State = TreeState.Chopped;
        }

        public override string SpriteName => State == TreeState.Chopped ? "Stump" : base.SpriteName;
    }
}
