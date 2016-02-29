using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Exceptions
{
    class HandViewException : MahjongException
    {
        public HandViewException() : base() { }

        public HandViewException(string message) : base(message) { }

        public HandViewException(string message, Exception innerException) : base(message, innerException) { }
    }
}
