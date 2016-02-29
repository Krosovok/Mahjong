using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Exceptions
{
    class TileCreationException : MahjongException
    {
        public TileCreationException() : base("Попытка создать несуществующий тайл.") { }
    }
}
