using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using Mahjong.Model.HandSpace;
using Mahjong.Model.Tiles;

namespace Mahjong.View.Controls
{
    public partial class Win : UserControl
    {
        Dictionary<string, int> yakuPoints = new Dictionary<string, int>();

        public Win()
        {
            InitializeComponent();



            //yakuPoints.Add("Yakuhai", 1);
            //yakuPoints.Add("Chanta", 1);

            YakuPoints = this.yakuPoints;
        }

        public Dictionary<string, int> YakuPoints
        {
            set 
            { 
                yakuPoints = value;
                foreach (KeyValuePair<string, int> pair in yakuPoints)
                {
                    dataGridView1.Rows.Add(pair.Key, pair.Value);
                }
            }
            get { return yakuPoints; }
        }

        public Hand WinHand
        {
            set 
            {
                Hand hand = value;
                Tile lastTile = hand.LastTile;
                hand.Discard(lastTile);

                handView1.Hand = hand;
                handView1.UpdateHand();
                handView1.DrawTile(lastTile);

                //handView1.Hand = value;
            }
            get { return handView1.Hand; }
        }

        //private 
    }
}
