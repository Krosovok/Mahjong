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
    class FormHand
    {
        public GroupValue Value { get; set; }
        public Tile First { get; set; }

        public double Prioruty
        {
            get
            {
                switch (Value)
                {
                    case GroupValue.Tanki:
                        return 0.5;
                    case GroupValue.Renchan:
                        return 2;
                    case GroupValue.Kanchan:
                        return 2.5;
                    case GroupValue.Ryanmen:
                        return 3;
                    case GroupValue.Syanpon:
                        return 2.5;
                    case GroupValue.Pon:
                        return (First is SuitedTile)
                            ? 5 : 6;
                    case GroupValue.Kan:
                        return (First is SuitedTile)
                            ? 6 : 7;
                    case GroupValue.Chi:
                        return 4.5;
                    default:
                        return 0;
                }
            }
        }

        public FormHand(GroupValue value, Tile first)
        {
            this.Value = value;
            this.First = first;
        }

        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) { return false; }
            
            return ((this.First.CompareTo((obj as FormHand).First) == 0) &&
             this.Value == (obj as FormHand).Value);
        }
    }
}
