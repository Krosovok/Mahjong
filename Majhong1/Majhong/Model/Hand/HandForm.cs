using Mahjong.Model.HandSpace;
using Mahjong.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.HandSpace
{
    [DebuggerDisplay("{this.Value}, {this.First.Type}")]
    public class HandForm
    {
        public GroupValue Value { get; set; }
        public Tile First { get; set; }

        public double Prioruty
        {
            get
            {
                switch (Value)
                {
                    case GroupValue.Single:
                        return 1;
                    case GroupValue.Penchan:
                        return 4;
                    case GroupValue.Kanchan:
                        return 4;
                    case GroupValue.Ryanmen:
                        return 6;
                    case GroupValue.Pair:
                        return 4;
                    case GroupValue.Pon:
                        return (First is SuitedTile)
                            ? 15 : 18;
                    case GroupValue.Kan:
                        return (First is SuitedTile)
                            ? 18 : 21;
                    case GroupValue.Chi:
                        return 13;
                    default:
                        return 0;
                }
            }
        }

        public HandForm(GroupValue value, Tile first)
        {
            this.Value = value;
            this.First = first;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) { return false; }

            return (this.First.Category() == (obj as HandForm).First.Category() && 
                (this.First.CompareTo((obj as HandForm).First) == 0) &&
             this.Value == (obj as HandForm).Value);
        }

        /// <summary>
        /// Определяет, содержит ли форма тайл с указанным значением.
        /// </summary>
        public bool Contains(char tileValue)
        {
            List<char> lst = new List<char>();
            lst.Add(First.Type);

            switch (Value)
            {
                case GroupValue.Single:
                case GroupValue.Pair:
                case GroupValue.Pon:
                case GroupValue.Kan:
                    return lst.Contains(tileValue);

                case GroupValue.Ryanmen:
                case GroupValue.Penchan:
                    lst.Add(SuitedTile.values[(int)First.Type]);
                    return lst.Contains(tileValue);

                case GroupValue.Kanchan:
                    lst.Add(SuitedTile.values[(int)First.Type + 1]);
                    return lst.Contains(tileValue);

                case GroupValue.Chi:
                    lst.Add(SuitedTile.values[(int)First.Type]);
                    lst.Add(SuitedTile.values[(int)First.Type + 1]);
                    return lst.Contains(tileValue);

                default:
                    return false;

            }

        }
    }
}
