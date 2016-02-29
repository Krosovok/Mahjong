using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.Tiles
{
    /// <summary>
    /// Тайл масти пин.
    /// </summary>
    class PinTile :SuitedTile
    {
        public PinTile(char type) : base(type) { }

        public override TileCategory Category() { return TileCategory.Pin; }
    }
}
