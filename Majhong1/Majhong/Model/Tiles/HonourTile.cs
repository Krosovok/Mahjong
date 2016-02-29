using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.Tiles
{
    abstract class HonourTile : Tile
    {
        public HonourTile(char type) : base(type) { }
    }
}
