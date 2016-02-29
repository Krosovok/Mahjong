using Mahjong.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mahjong
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new Test());

            //
            //Random r = new Random();
            //List<Tile> tiles = TileFactory.Tiles();
            //Stack<Tile> wall = new Stack<Tile>();
            //for (int i = tiles.Count - 1; i >= 0; i--)
            //{
            //    int idx = r.Next(i);
            //    wall.Push(tiles[idx]);
            //    tiles.RemoveAt(idx);
            //}
            //


            //Application.Run(new Test());
            

            //String chun = Char.ConvertFromUtf32(126980) + Char.ConvertFromUtf32(126980) + Char.ConvertFromUtf32(126980);
            //// 🀄🀄🀄
            //string hatsu = Char.ConvertFromUtf32(126981);
            //// 🀅
            //string haku = Char.ConvertFromUtf32(126982);
            //// 🀆
            //System.Globalization.UnicodeCategory cat = Char.GetUnicodeCategory(chun[0]);

            
        }
    }

}
