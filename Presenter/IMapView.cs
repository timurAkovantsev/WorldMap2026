using WorldMap2026.Model;

namespace WorldMap2026.Presenter
{
    public readonly struct ObjectRenderInfo
    {
        public string TypeName { get; init; }
        public Point Location { get; init; }
        public Size Dimensions { get; init; }
        public int Variant { get; init; }
    }

    public readonly struct CellRenderInfo
    {
        public TerrainType Type { get; init; }
        public Color Color { get; init; }
    }

    public interface IMapView
    {
        event Action<int, int> CreateNewWorldRequested;
        event Action<string> SaveWorldRequested;
        event Action QuickSaveRequested;
        event Action<string> LoadWorldRequested;

        event Action<Point> CursorMoved;
        event Action<ToolType, Point> CellOnClick;

        void UpdateMapData(CellRenderInfo[][] grid, List<ObjectRenderInfo> objects);
        void UpdateStatisticsDisplay(string statsText);
        void ShowMessage(string message);
        void OnSuccess();
        void OnFail();
        Color GetColorForChange();
        void UpdateCursorPosition(string positionText);
    }
}