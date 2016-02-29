using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Exceptions
{
    /// <summary>
    /// Базовое исключение для сборки.
    /// </summary>
    class MahjongException : Exception
    {
        public MahjongException() : base() { }

        public MahjongException(string message) : base(message) { }

        public MahjongException(string message, Exception innerException) : base(message, innerException) { }
    }
}
