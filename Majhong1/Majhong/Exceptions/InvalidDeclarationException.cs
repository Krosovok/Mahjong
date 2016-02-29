using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong.Exceptions
{
    class InvalidDeclarationException : Exception
    {
        public InvalidDeclarationException() { }

        public InvalidDeclarationException(string message) : base(message) { }

        public InvalidDeclarationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
