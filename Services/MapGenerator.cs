using WorldMap2026.Model;
using WorldMap2026.Model.Items;

namespace WorldMap2026.Services
{
    /// <summary>
    /// Сервис процедурной генерации игрового мира.
    /// </summary>
    public class MapGenerator
    {
        private readonly Random _rand = new Random();

        /// <summary>
        /// Создает новую карту заданного размера со случайно сгенерированным ландшафтом и объектами.
        /// </summary>
        public MapModel GenerateMap(int width, int height)
        {
            MapModel map = new MapModel(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map.Cells[x][y] = new TerrainCell(TerrainType.Grass);
                }
            }

            int totalArea = width * height;
            int waterLakesCount = Math.Max(2, totalArea / 150);
            int rockPlateausCount = Math.Max(1, totalArea / 200);
            int sandPatchesCount = Math.Max(1, totalArea / 150);

            GenerateBlobs(map, TerrainType.Water, waterLakesCount, 2, 4);
            GenerateBlobs(map, TerrainType.Rock, rockPlateausCount, 2, 4);
            GenerateBlobs(map, TerrainType.Sand, sandPatchesCount, 1, 3);

            GenerateObjects(map);

            return map;
        }

        /// <summary>
        /// Генерирует участки ландшафта (озера, горы, песок) с использованием радиуса и случайного шума 
        /// для создания естественных неровных форм.
        /// </summary>
        private void GenerateBlobs(MapModel map, TerrainType type, int count, int minRadius, int maxRadius)
        {
            for (int i = 0; i < count; i++)
            {
                int centerX = _rand.Next(0, map.Width);
                int centerY = _rand.Next(0, map.Height);
                int radius = _rand.Next(minRadius, maxRadius + 1);

                for (int x = centerX - radius; x <= centerX + radius; x++)
                {
                    for (int y = centerY - radius; y <= centerY + radius; y++)
                    {
                        if (x >= 0 && x < map.Width && y >= 0 && y < map.Height)
                        {
                            double distance = Math.Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));
                            double noise = _rand.NextDouble() * 1.8;

                            if (distance + noise <= radius)
                            {
                                map.Cells[x][y] = new TerrainCell(type);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Рассчитывает количество объектов в зависимости от площади карты и запускает их размещение.
        /// </summary>
        private void GenerateObjects(MapModel map)
        {
            int area = map.Width * map.Height;

            int treeCount = area / 40;
            int flowerCount = area / 60;
            int fieldCount = area / 100;
            int windmillCount = Math.Max(1, area / 300);

            TryPlaceObjects(map, treeCount, p => new Tree(p));
            TryPlaceObjects(map, flowerCount, p => new Flower(p, _rand));
            TryPlaceObjects(map, fieldCount, p => new Field(p));
            TryPlaceObjects(map, windmillCount, p => new Windmill(p));
        }

        /// <summary>
        /// Пытается разместить объекты на карте в случайных координатах. 
        /// Делегирует проверку коллизий и соответствия ландшафта доменным правилам PlacementRules.
        /// </summary>
        private void TryPlaceObjects(MapModel map, int count, Func<Point, GameObject> objectFactory)
        {
            PlacementRules rules = new PlacementRules();

            for (int i = 0; i < count; i++)
            {
                for (int attempt = 0; attempt < 20; attempt++)
                {
                    int x = _rand.Next(0, map.Width);
                    int y = _rand.Next(0, map.Height);

                    GameObject newObj = objectFactory(new Point(x, y));

                    if (rules.CanPlaceObject(newObj, map))
                    {
                        map.Objects.Add(newObj);
                        break;
                    }
                }
            }
        }
    }
}