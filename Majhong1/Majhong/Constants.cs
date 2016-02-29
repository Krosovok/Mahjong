using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong
{
    /// <summary>
    /// Класс для хранения констант.
    /// </summary>
    abstract class Constants
    {
        public const string MAN_IMAGE = "man"; 
        public const string PIN_IMAGE = "pin";
        public const string SOU_IMAGE = "bamboo";
        public const string WIND_IMAGE = "wind";
        public const string DRAGON_IMAGE = "dragon";
        public const string FACE_IMAGE = "face_";

        public const string TILE = "tile";
        public const string BACK = "back";
        public const string LEFT = "left";
        public const string RIGHT = "right";
        public const int MAX_HAND_SIZE = 14; 

        //public const int TILE_HEIGHT = 
        public const int TILE_WIDTH = 42;
        public const int TILE_TOP_HEIGHT = 24;

        public const int ROUND = 360;

        public const int TILE_JUMPING = 10;

        public const int DISCARD_ROW = 6;

        //// Kanji
        public const string EAST    = "東";
        public const string SOUTH   = "南";
        public const string WEST    = "西";
        public const string NORTH   = "北";
    }
}
