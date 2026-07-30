using System.Drawing.Drawing2D;
using WorldMap2026.Model;
using WorldMap2026.Presenter;

namespace WorldMap2026.View
{

    public class MapRenderer
    {

        private readonly Image _imgWindmill = Properties.Resources.windmill;
        private readonly Image _imgTree = Properties.Resources.tree;
        private readonly Image _imgField = Properties.Resources.fieldMap;
        private readonly Image _imgStump = Properties.Resources.stumpMap;
        private readonly Image[] _flowerSkins;

        public MapRenderer()
        {
            _flowerSkins = new Image[]
            {
                Properties.Resources.flower1,
                Properties.Resources.flower2,
                Properties.Resources.flower3,
                Properties.Resources.flower4,
                Properties.Resources.flower5
            };
        }

        public void DrawWorld(Graphics g, CellRenderInfo[][] grid, List<ObjectRenderInfo> objects, int cellSize)
        {
            DrawTerrain(g, grid, cellSize);
            DrawObjects(g, objects, cellSize);
        }

        private void DrawTerrain(Graphics g, CellRenderInfo[][] grid, int cellSize)
        {

            int length = grid.Length;
            for (int x = 0; x < length; x++)
            {
                int width = grid[x].Length;
                for (int y = 0; y < width; y++)
                {
                    Rectangle rect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);

                    CellRenderInfo cell = grid[x][y];

                    Color baseColor = cell.Color;
                    Color darkTone = Darken(baseColor, 30);
                    Color lightTone = Lighten(baseColor, 30);

                    using (Brush cellBrush = cell.Type switch
                    {
                        TerrainType.Rock => new HatchBrush(HatchStyle.Sphere, darkTone, baseColor),
                        TerrainType.Sand => new HatchBrush(HatchStyle.LargeConfetti, lightTone, baseColor),
                        TerrainType.Grass => new HatchBrush(HatchStyle.Trellis, darkTone, baseColor),
                        TerrainType.Water => new HatchBrush(HatchStyle.Wave, darkTone, baseColor),

                        _ => new SolidBrush(baseColor)
                    })
                    {
                        g.FillRectangle(cellBrush, rect);
                    }

                    g.DrawRectangle(Pens.Black, rect);
                }
            }
        }

        private void DrawObjects(Graphics g, List<ObjectRenderInfo> objects, int cellSize)
        {
            void DrawSprite(ObjectRenderInfo obj)
            {
                Rectangle rect = new Rectangle(
                    obj.Location.X * cellSize,
                    obj.Location.Y * cellSize,
                    obj.Dimensions.Width * cellSize,
                    obj.Dimensions.Height * cellSize);

                Image? sprite = obj.TypeName switch
                {
                    "Windmill" => _imgWindmill,
                    "Tree" => _imgTree,
                    "Flower" => _flowerSkins[obj.Variant],
                    "Field" => _imgField,
                    "Stump" => _imgStump,
                    _ => null
                };

                if (sprite != null)
                {
                    g.DrawImage(sprite, rect);
                }
            }

            foreach (var obj in objects)
            {
                if (obj.TypeName == "Field")
                    DrawSprite(obj);
            }

            foreach (var obj in objects)
            {
                if (obj.TypeName == "Flower")
                    DrawSprite(obj);
            }

            foreach (var obj in objects)
            {
                if (obj.TypeName != "Field" && obj.TypeName != "Flower")
                    DrawSprite(obj);
            }
        }

        private Color Lighten(Color color, int amount)
        {
            return Color.FromArgb(
                color.A,
                Math.Min(255, color.R + amount),
                Math.Min(255, color.G + amount),
                Math.Min(255, color.B + amount)
            );
        }

        private Color Darken(Color color, int amount)
        {
            return Color.FromArgb(
                color.A,
                Math.Max(0, color.R - amount),
                Math.Max(0, color.G - amount),
                Math.Max(0, color.B - amount)
            );
        }
    }
}
