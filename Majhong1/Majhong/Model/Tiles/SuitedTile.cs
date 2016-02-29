using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.Tiles
{
    /// <summary>
    /// Базовый класс для тайлов мастей.
    /// </summary>
    public abstract class SuitedTile : Tile
    {
        /// <summary>
        /// Допустимые значения для этого типа тайлов.
        /// </summary>
        public static readonly char[] values = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public SuitedTile(char type) : base(type) 
        {
            if (!values.Contains(type))
                throw new Exceptions.TileCreationException();
        }

        public override bool IsTerminal()
        {
            return this.Type == values.First() || this.Type == values.Last();
        }

        public override int CompareTo(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                throw new ArgumentException();
            }
            return Array.IndexOf(SuitedTile.values, this.Type) -
                Array.IndexOf(SuitedTile.values, (obj as Tile).Type);
        }
    }
}
