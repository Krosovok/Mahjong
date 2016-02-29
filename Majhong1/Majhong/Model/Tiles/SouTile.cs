using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.Tiles
{
    /// <summary>
    /// Тайл масти соу.
    /// </summary>
    class SouTile :SuitedTile
    {
        public SouTile(char type) : base(type) { }

        public override TileCategory Category() { return TileCategory.Sou; }
    }
}
