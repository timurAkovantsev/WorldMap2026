using WorldMap2026.Model;
using WorldMap2026.Model.Items;
using WorldMap2026.Services;

namespace WorldMap2026.Presenter
{
    public enum ToolType
    {
        None,
        Eraser,
        Palette,
        Grass,
        Sand,
        Water,
        Rock,
        Windmill,
        Tree,
        Field,
        Flower
    }

    public class MapPresenter
    {
        private readonly IMapView _view;
        private MapModel? _map;
        private readonly PlacementRules _rules;
        private readonly IWorldStorage _storage;
        private string? _currentFilePath;
        private readonly Random _random = new Random();
        private readonly MapGenerator _generator;


        public MapPresenter(IMapView view, IWorldStorage storage)
        {
            _view = view;
            _rules = new PlacementRules();
            _storage = new JsonWorldStorage();
            _generator = new MapGenerator();

            _view?.CreateNewWorldRequested += CreateNewWorld;
            _view?.SaveWorldRequested += SaveWorld;
            _view?.LoadWorldRequested += LoadWorld;
            _view?.QuickSaveRequested += QuickSave;
            _view?.CursorMoved += MouseMove;
            _view?.CellOnClick += OnCellClicked;
        }

        private void MouseMove(Point point)
        {
            _view.UpdateCursorPosition(point.ToString());
        }

        private void QuickSave()
        {
            if (string.IsNullOrEmpty(_currentFilePath) || _map == null)
                _view.ShowMessage("Сначала создайте/загрузите мир и сохраните его хотя бы один раз!");
            else
                _storage.SaveMap(_map, _currentFilePath);
        }

        private void CreateNewWorld(int width, int height)
        {
            _map = _generator.GenerateMap(width, height);

            SyncViewWithModel();

            _view.UpdateStatisticsDisplay("[ Всего объектов: 0 ]");
        }
        private void LoadWorld(string filePath)
        {
            _map = _storage.LoadMap(filePath);
            _currentFilePath = filePath;
            SyncViewWithModel();
            UpdateStats();
        }

        private void SaveWorld(string filePath)
        {
            _currentFilePath = filePath;

            if (_map != null)
                _storage.SaveMap(_map, filePath);
        }

        private void SyncViewWithModel()
        {
            if (_map == null) return;

            CellRenderInfo[][] flatGrid = new CellRenderInfo[_map.Width][];
            for (int x = 0; x < _map.Width; x++)
            {
                flatGrid[x] = new CellRenderInfo[_map.Height];
                for (int y = 0; y < _map.Height; y++)
                {
                    flatGrid[x][y] = new CellRenderInfo
                    {
                        Type = _map.Cells[x][y].Type,
                        Color = _map.Cells[x][y].CellColor
                    };
                }
            }

            List<ObjectRenderInfo> renderObjects = _map.Objects.Select(obj => new ObjectRenderInfo
            {
                TypeName = obj.SpriteName,
                Location = obj.Location,
                Dimensions = obj.Dimensions,
                Variant = obj.SpriteVariant
            }).ToList();

            _view.UpdateMapData(flatGrid, renderObjects);
        }

        private void OnCellClicked(ToolType tool, Point location)
        {
            if (_map is null) return;

            if (tool == ToolType.Eraser)
            {
                List<GameObject> objectsToRemove = _map.Objects.FindAll(obj => obj.Hitbox.Contains(location));

                bool actionPerformed = false;

                foreach (GameObject obj in objectsToRemove)
                {
                    if (obj is Tree tree && tree.State == TreeState.Alive)
                    {
                        tree.Chop();
                        actionPerformed = true;
                    }
                    else
                    {
                        _map.Objects.Remove(obj);
                        actionPerformed = true;
                    }
                }

                if (actionPerformed)
                {
                    _view.OnSuccess();
                    SyncViewWithModel();
                    UpdateStats();
                }
            }
            else if (tool == ToolType.Palette)
            {
                _map.ChangeColorCells(_map.Cells[location.X][location.Y].Type, _view.GetColorForChange());

                _view.OnSuccess();
                SyncViewWithModel();
                UpdateStats();
            }
            else if (tool is ToolType.Grass or ToolType.Sand or ToolType.Water or ToolType.Rock)
            {
                TerrainType type = tool switch
                {
                    ToolType.Grass => TerrainType.Grass,
                    ToolType.Sand => TerrainType.Sand,
                    ToolType.Water => TerrainType.Water,
                    ToolType.Rock => TerrainType.Rock,
                    _ => TerrainType.Grass
                };

                if (_map.ReplaceCellTerrain(location, type))
                {
                    _view.OnSuccess();
                    SyncViewWithModel();
                }
                else
                {
                    _view.ShowMessage("Не удалось изменить покрытие");
                }

            }
            else
            {
                GameObject? newObj = tool switch
                {
                    ToolType.Windmill => new Windmill(location),
                    ToolType.Tree => new Tree(location),
                    ToolType.Field => new Field(location),
                    ToolType.Flower => new Flower(location, _random),
                    _ => null
                };

                if (newObj != null)
                {
                    if (_rules.CanPlaceObject(newObj, _map))
                    {
                        _map.Objects.Add(newObj);
                        _view.OnSuccess();
                        SyncViewWithModel();
                        UpdateStats();
                    }
                    else
                    {
                        _view.OnFail();
                    }
                }
            }
        }

        private void UpdateStats()
        {
            if(_map != null )
            _view.UpdateStatisticsDisplay($"[ Всего объектов: {_map.Objects.Count} ]");
        }

    }
}
