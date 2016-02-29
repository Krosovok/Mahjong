using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahjong.Model.Tiles;

namespace Mahjong.Model.HandSpace
{
    public struct Tempai
    {
        public bool True { get; set; }

        public Dictionary<Tile, List<Tile>> TempaiVariants { get; set; }

        public List<Tile> Wait { get; set; }
    }
}
