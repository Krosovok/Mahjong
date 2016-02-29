using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.Tiles
{
    static class TileFactory
    {
        /// <summary>
        /// Генерирует набор тайлов.
        /// </summary>
        /// <returns></returns>
        static public List<Tile> Tiles()
        {
            List<Tile> res = new List<Tile>();

            for (int i = 0; i < 4; i++)
            {

                foreach (char v in SuitedTile.values)
                {
                    res.Add(new ManTile(v));
                    res.Add(new PinTile(v));
                    res.Add(new SouTile(v));
                }
                foreach (char v in WindTile.values)
                {
                    res.Add(new WindTile(v));
                }
                foreach (char v in DragonTile.values)
                {
                    res.Add(new DragonTile(v));
                }

            }

            return res;
        }
    }
}
