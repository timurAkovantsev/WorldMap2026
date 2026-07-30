using System.Text.Json.Serialization;
using WorldMap2026.Model.Items; 

namespace WorldMap2026.Model
{
    /// <summary>
    /// Базовый абстрактный класс для всех размещаемых объектов на карте.
    /// </summary>
    [JsonPolymorphic]
    [JsonDerivedType(typeof(Windmill), "Windmill")]
    [JsonDerivedType(typeof(Tree), "Tree")]
    [JsonDerivedType(typeof(Field), "Field")]
    [JsonDerivedType(typeof(Flower), "Flower")]
    public abstract class GameObject
    {
        /// <summary>
        /// Логические координаты объекта на сетке карты.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Размеры объекта в клетках.
        /// </summary>
        public Size Dimensions { get; protected set; }

        /// <summary>
        /// Имя спрайта для отрисовки. По умолчанию совпадает с именем класса.
        /// </summary>
        [JsonIgnore]
        public virtual string SpriteName => GetType().Name;

        /// <summary>
        /// Вариант текстуры (используется для объектов с несколькими скинами).
        /// </summary>
        [JsonIgnore]
        public virtual int SpriteVariant => 0;

        /// <summary>
        /// Вычисляемая область, которую занимает объект.
        /// </summary>
        [JsonIgnore]
        public Rectangle Hitbox => new Rectangle(Location, Dimensions);

        protected GameObject(Point location, Size dimensions)
        {
            Location = location;
            Dimensions = dimensions;
        }

        /// <summary>
        /// Определяет, может ли объект быть установлен на указанный тип ландшафта.
        /// </summary>
        public virtual bool CanBePlacedOn(TerrainType terrain)
        {
            return true;
        }
    }
}