using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Exceptions
{
    class HandStructureException : MahjongException
    {
        public HandStructureException() : base() { }

        public HandStructureException(string message) : base(message) { }

        public HandStructureException(string message, Exception innerException) : base(message, innerException) { }
    }
}
