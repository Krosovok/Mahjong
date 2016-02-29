using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using Mahjong.Properties;

namespace Mahjong.Model.Tiles
{
    /// <summary>
    /// Категория тайла. Возращается методом Category.
    /// Указывает на настоящий тип экземпляра.
    /// </summary>
    public enum TileCategory
    {
        Man = 0,
        Pin = 1,
        Sou = 2,
        Wind = 3,
        Dragon = 4
    }

    /// <summary>
    /// Базовый класс для тайлов.
    /// </summary>
    public abstract class Tile : IComparable
    {

        public Tile(char type)
        {
            Type = type;
            HandImage = this.HandImageFromResources();
            FaceImage = this.FaceImageFromResources();
        }

        /// <summary>
        /// Значение тайла. Конкретная цифра/дракон/ветер.
        /// </summary>
        public virtual char Type { protected set; get; }

        /// <summary>
        /// Получает изображение тайла в руке.
        /// Задаётся в конструкторе из типа тайла.
        /// </summary>
        public Image HandImage
        {
            private set;
            get;
        }
        /// <summary>
        /// Получает изображение лицевой стороны тайла.
        /// Задаётся в конструкторе из типа тайла.
        /// </summary>
        public Image FaceImage
        {
            private set;
            get;
        }

        #region Tile is ...
        /*
         * Методы, определяющие вид тайла.
         * В наследнике переопределён соответствующий метод
         * чтобы возвразать true.
         */

        /// <summary>
        /// Определяет к какой категории относятся тайлы.
        /// (Ман, Пин, Соу, Ветра или Драконы)
        /// </summary>
        public abstract TileCategory Category();
        
        /// <summary>
        /// Определяет, яляется ли тайл терминалом.
        /// </summary>
        public virtual bool IsTerminal() { return false; }

        #endregion

        /// <summary>
        /// Сравнивает этот тайл с другим тайлом этого же вида.
        /// Для мастей возвращает разницу в числовом значении тайла.
        /// </summary>
        public abstract int CompareTo(object obj);

        /// <summary>
        /// Получает изображение этого тайла в руке из ресурсов.
        /// </summary>
        private Image HandImageFromResources()
        {
            string imageName = ImageName(); 

            // Использование названия изображения для загрузки файла из ресурсов.
            return (Image)Resources.ResourceManager.GetObject(imageName);
        }

        /// <summary>
        /// Получает изображение лицевой стороны этого тайла из ресурсов.
        /// </summary>
        private Image FaceImageFromResources()
        {
            string imageName = ImageName();
            imageName = Constants.FACE_IMAGE + imageName;

            // Использование названия изображения для загрузки файла из ресурсов.
            return (Image)Resources.ResourceManager.GetObject(imageName);
        }

        /// <summary>
        /// Конструирует название изображения в ресурсах.
        /// </summary>
        private string ImageName()
        {
            // Создание названия изображения.
            string imageName = String.Empty;
            switch (this.Category())
            {
                case TileCategory.Man:
                    imageName += Constants.MAN_IMAGE;
                    break;
                case TileCategory.Pin:
                    imageName += Constants.PIN_IMAGE;
                    break;
                case TileCategory.Sou:
                    imageName += Constants.SOU_IMAGE;
                    break;
                case TileCategory.Wind:
                    imageName += Constants.WIND_IMAGE;
                    break;
                case TileCategory.Dragon:
                    imageName += Constants.DRAGON_IMAGE;
                    break;
            }
            imageName += this.Type;
            return imageName;
        }

        public static Tile CustomTile(TileCategory category, char value)
        {
            switch (category)
            {
                case TileCategory.Man:
                    return new ManTile(value);

                case TileCategory.Pin:
                    return new PinTile(value);

                case TileCategory.Sou:
                    return new SouTile(value);

                case TileCategory.Wind:
                    return new WindTile(value);

                case TileCategory.Dragon:
                    return new DragonTile(value);

                default:
                    return null;
            }
        }
    }
}
