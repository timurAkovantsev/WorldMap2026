namespace WorldMap2026.Model
{
    public interface IWorldStorage
    {
        void SaveMap(MapModel map, string filePath);
        MapModel LoadMap(string filePath);
    }
}
