using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong.Exceptions
{
    class MachineRebellionException : MahjongException
    {
        public MachineRebellionException() : base() { }

        public MachineRebellionException(string message) : base(message) { }
    }
}
