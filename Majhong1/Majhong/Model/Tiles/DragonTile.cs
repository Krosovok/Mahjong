using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.Tiles
{
    class DragonTile : HonourTile
    {
        /// <summary>
        /// Допустимые значения для этого типа тайлов.
        /// </summary>
        public static readonly char[] values = 
        { 
          'C', // Chun - 🀄
          'B', // Haku - 🀆
          'F'  // Hatsu - 🀅
        };

        public DragonTile(char type) : base(type) 
        {
            if (!values.Contains(type))
                throw new Exceptions.TileCreationException();
        }

        public override TileCategory Category() { return TileCategory.Dragon; }

        public override int CompareTo(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                throw new ArgumentException();
            }
            return Array.IndexOf(DragonTile.values, this.Type) -
                Array.IndexOf(DragonTile.values, (obj as Tile).Type);
        }
    }
}
