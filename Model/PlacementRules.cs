using WorldMap2026.Model.Items;

namespace WorldMap2026.Model
{

    // разные уровни игры
    public class PlacementRules
    {
        public bool CanPlaceObject(GameObject newObject, MapModel map)
        {
            if (newObject.Hitbox.Left < 0 || newObject.Hitbox.Top < 0
                    || newObject.Hitbox.Right > map.Width || newObject.Hitbox.Bottom > map.Height)
                return false;

            for (int x = newObject.Hitbox.Left; x < newObject.Hitbox.Right; x++)
            {
                for (int y = newObject.Hitbox.Top; y < newObject.Hitbox.Bottom; y++)
                {
                    TerrainCell cell = map.Cells[x][y];

                    if (!newObject.CanBePlacedOn(cell.Type))
                        return false;
                }
            }

            foreach (var existingObj in map.Objects)
            {
                if (newObject.Hitbox.IntersectsWith(existingObj.Hitbox))
                {
                    if (newObject is Flower && existingObj is Flower)
                        return false;

                    if (newObject is Flower || existingObj is Flower)
                        continue;

                    return false;
                }
            }

            return true;
        }
    }
}
