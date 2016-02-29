using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.Tiles
{
    /// <summary>
    /// Тайл ветра.
    /// </summary>
    class WindTile : HonourTile
    {
        /// <summary>
        /// Допустимые значения для этого типа тайлов.
        /// </summary>
        public static readonly char[] values = { 'E', 'S', 'W', 'N' };

        public WindTile(char type) : base(type) 
        {
            if (!values.Contains(type))
                throw new Exceptions.TileCreationException();
        }

        public override TileCategory Category() { return TileCategory.Wind; }

        public override int CompareTo(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                throw new ArgumentException();
            }
            return Array.IndexOf(WindTile.values, this.Type) -
                Array.IndexOf(WindTile.values, (obj as Tile).Type);
        }
    }
}
