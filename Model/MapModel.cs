namespace WorldMap2026.Model
{
    public class MapModel
    {
        /// <summary>
        /// Сетка ландшафта. 
        /// </summary>
        /// <remarks>
        /// Используется зубчатый массив для поддержки JSON сериализации.
        /// </remarks>
        public TerrainCell[][] Cells { get; init; }

        public int Width { get; init; }
        public int Height { get; init; }

        public List<GameObject> Objects { get; init; }

        public MapModel(int width, int height)
        {
            Width = width;
            Height = height;
            Objects = new List<GameObject>();

            Cells = new TerrainCell[width][];
            for (int i = 0; i < width; i++)
            {
                Cells[i] = new TerrainCell[height];
            }
        }

        public void ChangeColorCells(TerrainType type, Color newColor)
        {
            foreach (TerrainCell[] line in Cells)
            {
                foreach (TerrainCell cell in line)
                {
                    if (cell.Type == type)
                        cell.CellColor = newColor;
                }
            }
        }

        public IEnumerable<GameObject> GetObjectsAt(Point location)
        {
            return Objects.Where(obj => obj.Hitbox.Contains(location));
        }

        public bool ReplaceCellTerrain(Point location, TerrainType newType)
        {
            if (location.X < 0 || location.Y < 0 || location.X >= Width || location.Y >= Height)
                return false;

            TerrainCell targetCell = Cells[location.X][location.Y];
            var objectsOnCell = GetObjectsAt(location);

            foreach (var obj in objectsOnCell)
            {
                if (!obj.CanBePlacedOn(newType))
                {
                    return false;
                }
            }

            targetCell.Type = newType;

            targetCell.CellColor = newType switch
            {
                TerrainType.Grass => Color.LightGreen,
                TerrainType.Water => Color.Blue,
                TerrainType.Sand => Color.Khaki,
                TerrainType.Rock => Color.Gray,
                _ => Color.Gray
            };

            return true;
        }
    }
}